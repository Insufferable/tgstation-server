﻿using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using Tgstation.Server.Host.Extensions;
using Tgstation.Server.Host.IO;

namespace Tgstation.Server.Host.System
{
	/// <inheritdoc />
	sealed class ProcessExecutor : IProcessExecutor
	{
		/// <summary>
		/// The <see cref="IProcessFeatures"/> for the <see cref="ProcessExecutor"/>.
		/// </summary>
		readonly IProcessFeatures processFeatures;

		/// <summary>
		/// The <see cref="IIOManager"/> for the <see cref="ProcessExecutor"/>.
		/// </summary>
		readonly IIOManager ioManager;

		/// <summary>
		/// The <see cref="ILogger"/> for the <see cref="ProcessExecutor"/>.
		/// </summary>
		readonly ILogger<ProcessExecutor> logger;

		/// <summary>
		/// The <see cref="ILoggerFactory"/> for the <see cref="ProcessExecutor"/>.
		/// </summary>
		readonly ILoggerFactory loggerFactory;

		/// <summary>
		/// Initializes a new instance of the <see cref="ProcessExecutor"/> class.
		/// </summary>
		/// <param name="processFeatures">The value of <see cref="processFeatures"/>.</param>
		/// <param name="ioManager">The value of <see cref="ioManager"/>.</param>
		/// <param name="logger">The value of <see cref="logger"/>.</param>
		/// <param name="loggerFactory">The value of <see cref="loggerFactory"/>.</param>
		public ProcessExecutor(
			IProcessFeatures processFeatures,
			IIOManager ioManager,
			ILogger<ProcessExecutor> logger,
			ILoggerFactory loggerFactory)
		{
			this.processFeatures = processFeatures ?? throw new ArgumentNullException(nameof(processFeatures));
			this.ioManager = ioManager ?? throw new ArgumentNullException(nameof(ioManager));
			this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
			this.loggerFactory = loggerFactory ?? throw new ArgumentNullException(nameof(loggerFactory));
		}

		/// <inheritdoc />
		public IProcess GetProcess(int id)
		{
			logger.LogDebug("Attaching to process {0}...", id);
			global::System.Diagnostics.Process handle;
			try
			{
				handle = global::System.Diagnostics.Process.GetProcessById(id);
			}
			catch (Exception e)
			{
				logger.LogDebug(e, "Unable to get process {0}!", id);
				return null;
			}

			return CreateFromExistingHandle(handle);
		}

		/// <inheritdoc />
		public IProcess GetCurrentProcess()
		{
			logger.LogTrace("Getting current process...");
			var handle = global::System.Diagnostics.Process.GetCurrentProcess();
			return CreateFromExistingHandle(handle);
		}

		/// <inheritdoc />
		public async Task<IProcess> LaunchProcess(
			string fileName,
			string workingDirectory,
			string arguments,
			string fileRedirect,
			bool readStandardHandles,
			bool noShellExecute)
		{
			if (fileName == null)
				throw new ArgumentNullException(nameof(fileName));
			if (workingDirectory == null)
				throw new ArgumentNullException(nameof(workingDirectory));
			if (arguments == null)
				throw new ArgumentNullException(nameof(arguments));

			if (!noShellExecute && readStandardHandles)
				throw new InvalidOperationException("Requesting output/error reading requires noShellExecute to be true!");

			logger.LogDebug(
				"{0}aunching process in {1}: {2} {3}",
				noShellExecute ? "L" : "Shell l",
				workingDirectory,
				fileName,
				arguments);
			var handle = new global::System.Diagnostics.Process();
			try
			{
				handle.StartInfo.FileName = fileName;
				handle.StartInfo.Arguments = arguments;
				handle.StartInfo.WorkingDirectory = workingDirectory;

				handle.StartInfo.UseShellExecute = !noShellExecute;

				var processStartTcs = new TaskCompletionSource();
				var lifetimeTaskTask = AttachExitHandlerBeforeLaunch(handle, processStartTcs.Task);

				Task<string> readTask = null;
				CancellationTokenSource disposeCts = null;
				if (readStandardHandles)
				{
					handle.StartInfo.RedirectStandardOutput = true;
					handle.StartInfo.RedirectStandardError = true;

					disposeCts = new CancellationTokenSource();
					readTask = ConsumeReaders(handle, processStartTcs.Task, fileRedirect, disposeCts.Token);
				}

				try
				{
					handle.Start();

					processStartTcs.SetResult();
				}
				catch (Exception ex)
				{
					processStartTcs.SetException(ex);
					throw;
				}

				var process = new Process(
					processFeatures,
					handle,
					disposeCts,
					await lifetimeTaskTask, // won't block
					readTask,
					loggerFactory.CreateLogger<Process>(),
					false);

				return process;
			}
			catch
			{
				handle.Dispose();
				throw;
			}
		}

		/// <inheritdoc />
		public IProcess GetProcessByName(string name)
		{
			logger.LogTrace("GetProcessByName: {processName}...", name ?? throw new ArgumentNullException(nameof(name)));
			var procs = global::System.Diagnostics.Process.GetProcessesByName(name);
			global::System.Diagnostics.Process handle = null;
			foreach (var proc in procs)
				if (handle == null)
					handle = proc;
				else
				{
					logger.LogTrace("Disposing extra found PID: {pid}...", proc.Id);
					proc.Dispose();
				}

			if (handle == null)
				return null;

			return CreateFromExistingHandle(handle);
		}

		/// <summary>
		/// Wrapper for <see cref="AttachExitHandler(global::System.Diagnostics.Process, Func{int})"/> to safely provide the process ID.
		/// </summary>
		/// <param name="handle">The <see cref="global::System.Diagnostics.Process"/> to attach an exit handler to.</param>
		/// <param name="startupTask">A <see cref="Task"/> that completes once the process represented by <paramref name="handle"/> launches.</param>
		/// <returns>The result of the call to <see cref="AttachExitHandler(global::System.Diagnostics.Process, Func{int})"/>.</returns>
		async Task<Task<int>> AttachExitHandlerBeforeLaunch(global::System.Diagnostics.Process handle, Task startupTask)
		{
			var id = -1;
			var result = AttachExitHandler(handle, () => id);
			await startupTask;
			id = handle.Id;
			return result;
		}

		/// <summary>
		/// Attach an asychronous exit handler to a given process <paramref name="handle"/>.
		/// </summary>
		/// <param name="handle">The <see cref="global::System.Diagnostics.Process"/> to attach an exit handler to.</param>
		/// <param name="idProvider">A <see cref="Func{TResult}"/> that can be called to get the <see cref="global::System.Diagnostics.Process.Id"/> safely.</param>
		/// <returns>A <see cref="Task{TResult}"/> that completes with the exit code of the process represented by <paramref name="handle"/>.</returns>
		Task<int> AttachExitHandler(global::System.Diagnostics.Process handle, Func<int> idProvider)
		{
			handle.EnableRaisingEvents = true;

			var tcs = new TaskCompletionSource<int>();
			void ExitHandler(object sender, EventArgs args)
			{
				var id = idProvider();
				try
				{
					try
					{
						var exitCode = handle.ExitCode;

						// Try because this can be invoked twice for weird reasons
						if (tcs.TrySetResult(exitCode))
							logger.LogTrace("PID {0} termination event completed", id);
						else
							logger.LogTrace("Ignoring duplicate PID {0} termination event", id);
					}
					catch (InvalidOperationException ex)
					{
						if (!tcs.Task.IsCompleted)
							throw;

						logger.LogTrace(ex, "Ignoring expected PID {0} exit handler exception!", id);
					}
				}
				catch (Exception ex)
				{
					logger.LogError(ex, "PID {0} exit handler exception!", id);
				}
			}

			handle.Exited += ExitHandler;

			return tcs.Task;
		}

		/// <summary>
		/// Consume the stdout/stderr streams into a <see cref="Task"/>.
		/// </summary>
		/// <param name="handle">The <see cref="global::System.Diagnostics.Process"/>.</param>
		/// <param name="startTask">The <see cref="Task"/> that completes when <paramref name="handle"/> starts.</param>
		/// <param name="fileRedirect">The optional path to redirect the streams to.</param>
		/// <param name="disposeToken">The <see cref="CancellationToken"/> that triggers when the <see cref="Process"/> is disposed.</param>
		/// <returns>A <see cref="Task{TResult}"/> resulting in the program's output/error text if <paramref name="fileRedirect"/> is <see langword="null"/>, <see langword="null"/> otherwise.</returns>
		async Task<string> ConsumeReaders(global::System.Diagnostics.Process handle, Task startTask, string fileRedirect, CancellationToken disposeToken)
		{
			await startTask;

			var pid = handle.Id;
			logger.LogTrace("Starting read for PID {pid}...", pid);

			var stdOutHandle = handle.StandardOutput;
			var stdErrHandle = handle.StandardError;
			Task<string> outputReadTask = null, errorReadTask = null;
			bool outputOpen = true, errorOpen = true;
			async Task<string> GetNextLine()
			{
				if (outputOpen && outputReadTask == null)
					outputReadTask = stdOutHandle.ReadLineAsync();

				if (errorOpen && errorReadTask == null)
					errorReadTask = stdErrHandle.ReadLineAsync();

				var completedTask = await Task.WhenAny(outputReadTask ?? errorReadTask, errorReadTask ?? outputReadTask).WithToken(disposeToken);
				var line = await completedTask;
				if (completedTask == outputReadTask)
				{
					outputReadTask = null;
					if (line == null)
						outputOpen = false;
				}
				else
				{
					errorReadTask = null;
					if (line == null)
						errorOpen = false;
				}

				if (line == null && (errorOpen || outputOpen))
					return await GetNextLine();

				return line;
			}

			using var fileStream = fileRedirect != null ? ioManager.CreateAsyncSequentialWriteStream(fileRedirect) : null;
			using var writer = fileStream != null ? new StreamWriter(fileStream) : null;

			string text;
			var stringBuilder = fileStream == null ? new StringBuilder() : null;
			try
			{
				while ((text = await GetNextLine()) != null)
				{
					if (fileStream != null)
					{
						await writer.WriteLineAsync(text);
						await writer.FlushAsync();
					}
					else
						stringBuilder.AppendLine(text);
				}

				logger.LogTrace("Finished read for PID {pid}", pid);
			}
			catch (OperationCanceledException ex)
			{
				logger.LogWarning(ex, "PID {pid} stream reading interrupted!", pid);
				if (fileStream != null)
					await writer.WriteLineAsync("-- Process detached, log truncated. This is likely due a to TGS restart --");
			}

			return stringBuilder?.ToString();
		}

		/// <summary>
		/// Create a <see cref="IProcess"/> given an existing <paramref name="handle"/>.
		/// </summary>
		/// <param name="handle">The <see cref="global::System.Diagnostics.Process"/> to create a <see cref="IProcess"/> from.</param>
		/// <returns>The <see cref="IProcess"/> based on <paramref name="handle"/>.</returns>
		IProcess CreateFromExistingHandle(global::System.Diagnostics.Process handle)
		{
			try
			{
				var pid = handle.Id;
				return new Process(
					processFeatures,
					handle,
					null,
					AttachExitHandler(handle, () => pid),
					null,
					loggerFactory.CreateLogger<Process>(),
					true);
			}
			catch
			{
				handle.Dispose();
				throw;
			}
		}
	}
}
