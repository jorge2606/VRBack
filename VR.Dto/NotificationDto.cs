using Service.Common.ServiceResult;
using System;

namespace VR.Dto
{
    public class NotificationBaseDto
    {
        public Guid Id { get; set; }
        public long? CreatorUserId { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public DateTime? LastModifierUserId { get; set; }
        public string TextData { get; set; }
        public string Tittle { get; set; }
        public Guid UserId { get; set; }
        public int NotificationType { get; set; }
        public Guid EntityId { get; set; }
        public Guid SolicitationSubsidyId { get; set; }
    }


    public class NotificationDto
    {
        public Guid Id { get; set; }
        public string TextData { get; set; }
        public string Tittle { get; set; }
        public bool Read { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreationTime { get; set; }
        public Guid SolicitationSubsidyId { get; set; }
        public Guid CreatorUserId { get; set; }
        public string Days { set; get; }
    }

    public class CreateNotificationDto
    {
        public Guid Id { get; set; }
        public Guid CreatorUserId { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public Guid LastModifierUserId { get; set; }
        public string TextData { get; set; }
        public string Tittle { get; set; }
        public Guid UserId { get; set; }
        public int NotificationType { get; set; }
        public Guid EntityId { get; set; }
        public Guid SolicitationSubsidyId { get; set; }
    }
}
