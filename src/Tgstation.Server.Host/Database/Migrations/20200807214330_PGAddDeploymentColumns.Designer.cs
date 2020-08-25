// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Tgstation.Server.Host.Database.Migrations
{
	[DbContext(typeof(PostgresSqlDatabaseContext))]
	[Migration("20200807214330_PGAddDeploymentColumns")]
	partial class PGAddDeploymentColumns
	{
		/// <inheritdoc />
		protected override void BuildTargetModel(ModelBuilder modelBuilder)
		{
#pragma warning disable 612, 618
			modelBuilder
				.HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
				.HasAnnotation("ProductVersion", "3.1.6")
				.HasAnnotation("Relational:MaxIdentifierLength", 63);

			modelBuilder.Entity("Tgstation.Server.Host.Models.ChatBot", b =>
				{
					b.Property<long>("Id")
						.ValueGeneratedOnAdd()
						.HasColumnType("bigint")
						.HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

					b.Property<int>("ChannelLimit")
						.HasColumnType("integer");

					b.Property<string>("ConnectionString")
						.IsRequired()
						.HasColumnType("character varying(10000)")
						.HasMaxLength(10000);

					b.Property<bool?>("Enabled")
						.HasColumnType("boolean");

					b.Property<long>("InstanceId")
						.HasColumnType("bigint");

					b.Property<string>("Name")
						.IsRequired()
						.HasColumnType("character varying(100)")
						.HasMaxLength(100);

					b.Property<int>("Provider")
						.HasColumnType("integer");

					b.Property<long>("ReconnectionInterval")
						.HasColumnType("bigint");

					b.HasKey("Id");

					b.HasIndex("InstanceId", "Name")
						.IsUnique();

					b.ToTable("ChatBots");
				});

			modelBuilder.Entity("Tgstation.Server.Host.Models.ChatChannel", b =>
				{
					b.Property<long>("Id")
						.ValueGeneratedOnAdd()
						.HasColumnType("bigint")
						.HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

					b.Property<long>("ChatSettingsId")
						.HasColumnType("bigint");

					b.Property<decimal?>("DiscordChannelId")
						.HasColumnType("numeric(20,0)");

					b.Property<string>("IrcChannel")
						.HasColumnType("character varying(100)")
						.HasMaxLength(100);

					b.Property<bool?>("IsAdminChannel")
						.IsRequired()
						.HasColumnType("boolean");

					b.Property<bool?>("IsUpdatesChannel")
						.IsRequired()
						.HasColumnType("boolean");

					b.Property<bool?>("IsWatchdogChannel")
						.IsRequired()
						.HasColumnType("boolean");

					b.Property<string>("Tag")
						.HasColumnType("character varying(10000)")
						.HasMaxLength(10000);

					b.HasKey("Id");

					b.HasIndex("ChatSettingsId", "DiscordChannelId")
						.IsUnique();

					b.HasIndex("ChatSettingsId", "IrcChannel")
						.IsUnique();

					b.ToTable("ChatChannels");
				});

			modelBuilder.Entity("Tgstation.Server.Host.Models.CompileJob", b =>
				{
					b.Property<long>("Id")
						.ValueGeneratedOnAdd()
						.HasColumnType("bigint")
						.HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

					b.Property<string>("ByondVersion")
						.IsRequired()
						.HasColumnType("text");

					b.Property<int?>("DMApiMajorVersion")
						.HasColumnType("integer");

					b.Property<int?>("DMApiMinorVersion")
						.HasColumnType("integer");

					b.Property<int?>("DMApiPatchVersion")
						.HasColumnType("integer");

					b.Property<Guid?>("DirectoryName")
						.IsRequired()
						.HasColumnType("uuid");

					b.Property<string>("DmeName")
						.IsRequired()
						.HasColumnType("text");

					b.Property<int?>("GitHubDeploymentId")
						.HasColumnType("integer");

					b.Property<long?>("GitHubRepoId")
						.HasColumnType("bigint");

					b.Property<long>("JobId")
						.HasColumnType("bigint");

					b.Property<int?>("MinimumSecurityLevel")
						.HasColumnType("integer");

					b.Property<string>("Output")
						.IsRequired()
						.HasColumnType("text");

					b.Property<long>("RevisionInformationId")
						.HasColumnType("bigint");

					b.HasKey("Id");

					b.HasIndex("DirectoryName");

					b.HasIndex("JobId")
						.IsUnique();

					b.HasIndex("RevisionInformationId");

					b.ToTable("CompileJobs");
				});

			modelBuilder.Entity("Tgstation.Server.Host.Models.DreamDaemonSettings", b =>
				{
					b.Property<long>("Id")
						.ValueGeneratedOnAdd()
						.HasColumnType("bigint")
						.HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

					b.Property<bool?>("AllowWebClient")
						.IsRequired()
						.HasColumnType("boolean");

					b.Property<bool?>("AutoStart")
						.IsRequired()
						.HasColumnType("boolean");

					b.Property<long>("HeartbeatSeconds")
						.HasColumnType("bigint");

					b.Property<long>("InstanceId")
						.HasColumnType("bigint");

					b.Property<int>("Port")
						.HasColumnType("integer");

					b.Property<int>("SecurityLevel")
						.HasColumnType("integer");

					b.Property<long>("StartupTimeout")
						.HasColumnType("bigint");

					b.Property<long>("TopicRequestTimeout")
						.HasColumnType("bigint");

					b.HasKey("Id");

					b.HasIndex("InstanceId")
						.IsUnique();

					b.ToTable("DreamDaemonSettings");
				});

			modelBuilder.Entity("Tgstation.Server.Host.Models.DreamMakerSettings", b =>
				{
					b.Property<long>("Id")
						.ValueGeneratedOnAdd()
						.HasColumnType("bigint")
						.HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

					b.Property<int>("ApiValidationPort")
						.HasColumnType("integer");

					b.Property<int>("ApiValidationSecurityLevel")
						.HasColumnType("integer");

					b.Property<long>("InstanceId")
						.HasColumnType("bigint");

					b.Property<string>("ProjectName")
						.HasColumnType("character varying(10000)")
						.HasMaxLength(10000);

					b.Property<bool?>("RequireDMApiValidation")
						.IsRequired()
						.HasColumnType("boolean");

					b.HasKey("Id");

					b.HasIndex("InstanceId")
						.IsUnique();

					b.ToTable("DreamMakerSettings");
				});

			modelBuilder.Entity("Tgstation.Server.Host.Models.Instance", b =>
				{
					b.Property<long>("Id")
						.ValueGeneratedOnAdd()
						.HasColumnType("bigint")
						.HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

					b.Property<long>("AutoUpdateInterval")
						.HasColumnType("bigint");

					b.Property<int>("ChatBotLimit")
						.HasColumnType("integer");

					b.Property<int>("ConfigurationType")
						.HasColumnType("integer");

					b.Property<string>("Name")
						.IsRequired()
						.HasColumnType("character varying(10000)")
						.HasMaxLength(10000);

					b.Property<bool?>("Online")
						.IsRequired()
						.HasColumnType("boolean");

					b.Property<string>("Path")
						.IsRequired()
						.HasColumnType("text");

					b.HasKey("Id");

					b.HasIndex("Path")
						.IsUnique();

					b.ToTable("Instances");
				});

			modelBuilder.Entity("Tgstation.Server.Host.Models.InstanceUser", b =>
				{
					b.Property<long>("Id")
						.ValueGeneratedOnAdd()
						.HasColumnType("bigint")
						.HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

					b.Property<decimal>("ByondRights")
						.HasColumnType("numeric(20,0)");

					b.Property<decimal>("ChatBotRights")
						.HasColumnType("numeric(20,0)");

					b.Property<decimal>("ConfigurationRights")
						.HasColumnType("numeric(20,0)");

					b.Property<decimal>("DreamDaemonRights")
						.HasColumnType("numeric(20,0)");

					b.Property<decimal>("DreamMakerRights")
						.HasColumnType("numeric(20,0)");

					b.Property<long>("InstanceId")
						.HasColumnType("bigint");

					b.Property<decimal>("InstanceUserRights")
						.HasColumnType("numeric(20,0)");

					b.Property<decimal>("RepositoryRights")
						.HasColumnType("numeric(20,0)");

					b.Property<long>("UserId")
						.HasColumnType("bigint");

					b.HasKey("Id");

					b.HasIndex("InstanceId");

					b.HasIndex("UserId", "InstanceId")
						.IsUnique();

					b.ToTable("InstanceUsers");
				});

			modelBuilder.Entity("Tgstation.Server.Host.Models.Job", b =>
				{
					b.Property<long>("Id")
						.ValueGeneratedOnAdd()
						.HasColumnType("bigint")
						.HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

					b.Property<decimal?>("CancelRight")
						.HasColumnType("numeric(20,0)");

					b.Property<decimal?>("CancelRightsType")
						.HasColumnType("numeric(20,0)");

					b.Property<bool?>("Cancelled")
						.IsRequired()
						.HasColumnType("boolean");

					b.Property<long?>("CancelledById")
						.HasColumnType("bigint");

					b.Property<string>("Description")
						.IsRequired()
						.HasColumnType("text");

					b.Property<long?>("ErrorCode")
						.HasColumnType("bigint");

					b.Property<string>("ExceptionDetails")
						.HasColumnType("text");

					b.Property<long>("InstanceId")
						.HasColumnType("bigint");

					b.Property<DateTimeOffset?>("StartedAt")
						.IsRequired()
						.HasColumnType("timestamp with time zone");

					b.Property<long>("StartedById")
						.HasColumnType("bigint");

					b.Property<DateTimeOffset?>("StoppedAt")
						.HasColumnType("timestamp with time zone");

					b.HasKey("Id");

					b.HasIndex("CancelledById");

					b.HasIndex("InstanceId");

					b.HasIndex("StartedById");

					b.ToTable("Jobs");
				});

			modelBuilder.Entity("Tgstation.Server.Host.Models.ReattachInformation", b =>
				{
					b.Property<long>("Id")
						.ValueGeneratedOnAdd()
						.HasColumnType("bigint")
						.HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

					b.Property<string>("AccessIdentifier")
						.IsRequired()
						.HasColumnType("text");

					b.Property<long>("CompileJobId")
						.HasColumnType("bigint");

					b.Property<int>("LaunchSecurityLevel")
						.HasColumnType("integer");

					b.Property<int>("Port")
						.HasColumnType("integer");

					b.Property<int>("ProcessId")
						.HasColumnType("integer");

					b.Property<int>("RebootState")
						.HasColumnType("integer");

					b.HasKey("Id");

					b.HasIndex("CompileJobId");

					b.ToTable("ReattachInformations");
				});

			modelBuilder.Entity("Tgstation.Server.Host.Models.RepositorySettings", b =>
				{
					b.Property<long>("Id")
						.ValueGeneratedOnAdd()
						.HasColumnType("bigint")
						.HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

					b.Property<string>("AccessToken")
						.HasColumnType("character varying(10000)")
						.HasMaxLength(10000);

					b.Property<string>("AccessUser")
						.HasColumnType("character varying(10000)")
						.HasMaxLength(10000);

					b.Property<bool?>("AutoUpdatesKeepTestMerges")
						.IsRequired()
						.HasColumnType("boolean");

					b.Property<bool?>("AutoUpdatesSynchronize")
						.IsRequired()
						.HasColumnType("boolean");

					b.Property<string>("CommitterEmail")
						.IsRequired()
						.HasColumnType("character varying(10000)")
						.HasMaxLength(10000);

					b.Property<string>("CommitterName")
						.IsRequired()
						.HasColumnType("character varying(10000)")
						.HasMaxLength(10000);

					b.Property<bool?>("CreateGitHubDeployments")
						.IsRequired()
						.HasColumnType("boolean");

					b.Property<long>("InstanceId")
						.HasColumnType("bigint");

					b.Property<bool?>("PostTestMergeComment")
						.IsRequired()
						.HasColumnType("boolean");

					b.Property<bool?>("PushTestMergeCommits")
						.IsRequired()
						.HasColumnType("boolean");

					b.Property<bool?>("ShowTestMergeCommitters")
						.IsRequired()
						.HasColumnType("boolean");

					b.HasKey("Id");

					b.HasIndex("InstanceId")
						.IsUnique();

					b.ToTable("RepositorySettings");
				});

			modelBuilder.Entity("Tgstation.Server.Host.Models.RevInfoTestMerge", b =>
				{
					b.Property<long>("Id")
						.ValueGeneratedOnAdd()
						.HasColumnType("bigint")
						.HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

					b.Property<long>("RevisionInformationId")
						.HasColumnType("bigint");

					b.Property<long>("TestMergeId")
						.HasColumnType("bigint");

					b.HasKey("Id");

					b.HasIndex("RevisionInformationId");

					b.HasIndex("TestMergeId");

					b.ToTable("RevInfoTestMerges");
				});

			modelBuilder.Entity("Tgstation.Server.Host.Models.RevisionInformation", b =>
				{
					b.Property<long>("Id")
						.ValueGeneratedOnAdd()
						.HasColumnType("bigint")
						.HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

					b.Property<string>("CommitSha")
						.IsRequired()
						.HasColumnType("character varying(40)")
						.HasMaxLength(40);

					b.Property<long>("InstanceId")
						.HasColumnType("bigint");

					b.Property<string>("OriginCommitSha")
						.IsRequired()
						.HasColumnType("character varying(40)")
						.HasMaxLength(40);

					b.HasKey("Id");

					b.HasIndex("InstanceId", "CommitSha")
						.IsUnique();

					b.ToTable("RevisionInformations");
				});

			modelBuilder.Entity("Tgstation.Server.Host.Models.TestMerge", b =>
				{
					b.Property<long>("Id")
						.ValueGeneratedOnAdd()
						.HasColumnType("bigint")
						.HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

					b.Property<string>("Author")
						.IsRequired()
						.HasColumnType("text");

					b.Property<string>("BodyAtMerge")
						.IsRequired()
						.HasColumnType("text");

					b.Property<string>("Comment")
						.HasColumnType("character varying(10000)")
						.HasMaxLength(10000);

					b.Property<DateTimeOffset>("MergedAt")
						.HasColumnType("timestamp with time zone");

					b.Property<long>("MergedById")
						.HasColumnType("bigint");

					b.Property<int>("Number")
						.HasColumnType("integer");

					b.Property<long?>("PrimaryRevisionInformationId")
						.IsRequired()
						.HasColumnType("bigint");

					b.Property<string>("PullRequestRevision")
						.IsRequired()
						.HasColumnType("character varying(40)")
						.HasMaxLength(40);

					b.Property<string>("TitleAtMerge")
						.IsRequired()
						.HasColumnType("text");

					b.Property<string>("Url")
						.IsRequired()
						.HasColumnType("text");

					b.HasKey("Id");

					b.HasIndex("MergedById");

					b.HasIndex("PrimaryRevisionInformationId")
						.IsUnique();

					b.ToTable("TestMerges");
				});

			modelBuilder.Entity("Tgstation.Server.Host.Models.User", b =>
				{
					b.Property<long?>("Id")
						.ValueGeneratedOnAdd()
						.HasColumnType("bigint")
						.HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

					b.Property<decimal>("AdministrationRights")
						.HasColumnType("numeric(20,0)");

					b.Property<string>("CanonicalName")
						.IsRequired()
						.HasColumnType("text");

					b.Property<DateTimeOffset?>("CreatedAt")
						.IsRequired()
						.HasColumnType("timestamp with time zone");

					b.Property<long?>("CreatedById")
						.HasColumnType("bigint");

					b.Property<bool?>("Enabled")
						.IsRequired()
						.HasColumnType("boolean");

					b.Property<decimal>("InstanceManagerRights")
						.HasColumnType("numeric(20,0)");

					b.Property<DateTimeOffset?>("LastPasswordUpdate")
						.HasColumnType("timestamp with time zone");

					b.Property<string>("Name")
						.IsRequired()
						.HasColumnType("character varying(10000)")
						.HasMaxLength(10000);

					b.Property<string>("PasswordHash")
						.HasColumnType("text");

					b.Property<string>("SystemIdentifier")
						.HasColumnType("text");

					b.HasKey("Id");

					b.HasIndex("CanonicalName")
						.IsUnique();

					b.HasIndex("CreatedById");

					b.HasIndex("SystemIdentifier")
						.IsUnique();

					b.ToTable("Users");
				});

			modelBuilder.Entity("Tgstation.Server.Host.Models.ChatBot", b =>
				{
					b.HasOne("Tgstation.Server.Host.Models.Instance", "Instance")
						.WithMany("ChatSettings")
						.HasForeignKey("InstanceId")
						.OnDelete(DeleteBehavior.Cascade)
						.IsRequired();
				});

			modelBuilder.Entity("Tgstation.Server.Host.Models.ChatChannel", b =>
				{
					b.HasOne("Tgstation.Server.Host.Models.ChatBot", "ChatSettings")
						.WithMany("Channels")
						.HasForeignKey("ChatSettingsId")
						.OnDelete(DeleteBehavior.Cascade)
						.IsRequired();
				});

			modelBuilder.Entity("Tgstation.Server.Host.Models.CompileJob", b =>
				{
					b.HasOne("Tgstation.Server.Host.Models.Job", "Job")
						.WithOne()
						.HasForeignKey("Tgstation.Server.Host.Models.CompileJob", "JobId")
						.OnDelete(DeleteBehavior.Cascade)
						.IsRequired();

					b.HasOne("Tgstation.Server.Host.Models.RevisionInformation", "RevisionInformation")
						.WithMany("CompileJobs")
						.HasForeignKey("RevisionInformationId")
						.OnDelete(DeleteBehavior.Cascade)
						.IsRequired();
				});

			modelBuilder.Entity("Tgstation.Server.Host.Models.DreamDaemonSettings", b =>
				{
					b.HasOne("Tgstation.Server.Host.Models.Instance", "Instance")
						.WithOne("DreamDaemonSettings")
						.HasForeignKey("Tgstation.Server.Host.Models.DreamDaemonSettings", "InstanceId")
						.OnDelete(DeleteBehavior.Cascade)
						.IsRequired();
				});

			modelBuilder.Entity("Tgstation.Server.Host.Models.DreamMakerSettings", b =>
				{
					b.HasOne("Tgstation.Server.Host.Models.Instance", "Instance")
						.WithOne("DreamMakerSettings")
						.HasForeignKey("Tgstation.Server.Host.Models.DreamMakerSettings", "InstanceId")
						.OnDelete(DeleteBehavior.Cascade)
						.IsRequired();
				});

			modelBuilder.Entity("Tgstation.Server.Host.Models.InstanceUser", b =>
				{
					b.HasOne("Tgstation.Server.Host.Models.Instance", "Instance")
						.WithMany("InstanceUsers")
						.HasForeignKey("InstanceId")
						.OnDelete(DeleteBehavior.Cascade)
						.IsRequired();

					b.HasOne("Tgstation.Server.Host.Models.User", null)
						.WithMany("InstanceUsers")
						.HasForeignKey("UserId")
						.OnDelete(DeleteBehavior.Cascade)
						.IsRequired();
				});

			modelBuilder.Entity("Tgstation.Server.Host.Models.Job", b =>
				{
					b.HasOne("Tgstation.Server.Host.Models.User", "CancelledBy")
						.WithMany()
						.HasForeignKey("CancelledById");

					b.HasOne("Tgstation.Server.Host.Models.Instance", "Instance")
						.WithMany("Jobs")
						.HasForeignKey("InstanceId")
						.OnDelete(DeleteBehavior.Cascade)
						.IsRequired();

					b.HasOne("Tgstation.Server.Host.Models.User", "StartedBy")
						.WithMany()
						.HasForeignKey("StartedById")
						.OnDelete(DeleteBehavior.Cascade)
						.IsRequired();
				});

			modelBuilder.Entity("Tgstation.Server.Host.Models.ReattachInformation", b =>
				{
					b.HasOne("Tgstation.Server.Host.Models.CompileJob", "CompileJob")
						.WithMany()
						.HasForeignKey("CompileJobId")
						.OnDelete(DeleteBehavior.Cascade)
						.IsRequired();
				});

			modelBuilder.Entity("Tgstation.Server.Host.Models.RepositorySettings", b =>
				{
					b.HasOne("Tgstation.Server.Host.Models.Instance", "Instance")
						.WithOne("RepositorySettings")
						.HasForeignKey("Tgstation.Server.Host.Models.RepositorySettings", "InstanceId")
						.OnDelete(DeleteBehavior.Cascade)
						.IsRequired();
				});

			modelBuilder.Entity("Tgstation.Server.Host.Models.RevInfoTestMerge", b =>
				{
					b.HasOne("Tgstation.Server.Host.Models.RevisionInformation", "RevisionInformation")
						.WithMany("ActiveTestMerges")
						.HasForeignKey("RevisionInformationId")
						.OnDelete(DeleteBehavior.Cascade)
						.IsRequired();

					b.HasOne("Tgstation.Server.Host.Models.TestMerge", "TestMerge")
						.WithMany("RevisonInformations")
						.HasForeignKey("TestMergeId")
						.OnDelete(DeleteBehavior.ClientNoAction)
						.IsRequired();
				});

			modelBuilder.Entity("Tgstation.Server.Host.Models.RevisionInformation", b =>
				{
					b.HasOne("Tgstation.Server.Host.Models.Instance", "Instance")
						.WithMany("RevisionInformations")
						.HasForeignKey("InstanceId")
						.OnDelete(DeleteBehavior.Cascade)
						.IsRequired();
				});

			modelBuilder.Entity("Tgstation.Server.Host.Models.TestMerge", b =>
				{
					b.HasOne("Tgstation.Server.Host.Models.User", "MergedBy")
						.WithMany("TestMerges")
						.HasForeignKey("MergedById")
						.OnDelete(DeleteBehavior.Restrict)
						.IsRequired();

					b.HasOne("Tgstation.Server.Host.Models.RevisionInformation", "PrimaryRevisionInformation")
						.WithOne("PrimaryTestMerge")
						.HasForeignKey("Tgstation.Server.Host.Models.TestMerge", "PrimaryRevisionInformationId")
						.OnDelete(DeleteBehavior.Cascade)
						.IsRequired();
				});

			modelBuilder.Entity("Tgstation.Server.Host.Models.User", b =>
				{
					b.HasOne("Tgstation.Server.Host.Models.User", "CreatedBy")
						.WithMany("CreatedUsers")
						.HasForeignKey("CreatedById");
				});
#pragma warning restore 612, 618
		}
	}
}
