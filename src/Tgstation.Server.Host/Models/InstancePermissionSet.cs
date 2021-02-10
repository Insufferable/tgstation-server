using System.ComponentModel.DataAnnotations;

namespace Tgstation.Server.Host.Models
{
	/// <inheritdoc />
	public sealed class InstancePermissionSet : Api.Models.Internal.InstancePermissionSet, IApiTransformable<Api.Models.InstancePermissionSetResponse>
	{
		/// <summary>
		/// The row Id
		/// </summary>
		public long Id { get; set; }

		/// <summary>
		/// The <see cref="Api.Models.EntityId.Id"/> of <see cref="Instance"/>
		/// </summary>
		public long InstanceId { get; set; }

		/// <summary>
		/// The <see cref="Models.Instance"/> the <see cref="InstancePermissionSet"/> belongs to.
		/// </summary>
		[Required]
		public Instance Instance { get; set; }

		/// <summary>
		/// The <see cref="Models.PermissionSet"/> the <see cref="InstancePermissionSet"/> belongs to.
		/// </summary>
		[Required]
		public PermissionSet PermissionSet { get; set; }

		/// <inheritdoc />
		public Api.Models.InstancePermissionSetResponse ToApi() => new Api.Models.InstancePermissionSetResponse
		{
			ByondRights = ByondRights,
			ChatBotRights = ChatBotRights,
			ConfigurationRights = ConfigurationRights,
			DreamDaemonRights = DreamDaemonRights,
			DreamMakerRights = DreamMakerRights,
			RepositoryRights = RepositoryRights,
			InstancePermissionSetRights = InstancePermissionSetRights,
			PermissionSetId = PermissionSetId
		};
	}
}
