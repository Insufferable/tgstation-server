﻿using System;
using System.Threading;
using System.Threading.Tasks;

using Tgstation.Server.Host.Swarm;

namespace Tgstation.Server.Host.Core
{
	/// <summary>
	/// Updates the server.
	/// </summary>
	interface IServerUpdater
	{
		/// <summary>
		/// Start the process of downloading and applying an update to a new server <paramref name="version"/>.
		/// </summary>
		/// <param name="swarmService">The <see cref="ISwarmService"/> to use to coordinate the update.</param>
		/// <param name="version">The TGS <see cref="Version"/> to update to.</param>
		/// <param name="cancellationToken">The <see cref="CancellationToken"/> for the operation.</param>
		/// <returns>A <see cref="Task{TResult}"/> resulting in the <see cref="ServerUpdateResult"/>.</returns>
		Task<ServerUpdateResult> BeginUpdate(ISwarmService swarmService, Version version, CancellationToken cancellationToken);
	}
}
