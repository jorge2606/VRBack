using System;
using System.Collections.Generic;
using System.Text;

namespace VR.Dto
{
    public class SolicitationStateDto
    {
        public Guid Id { set; get; }
        public DateTime ChangeDate { set; get; }
        public string FileNumber { set; get; }

        public Guid StateId { set; get; }
        public Guid SolicitationSubsidyId { set; get; }
    }

    public class AddFielNumberDto : SolicitationStateDto {}

    public class SolicitationSubsidyStateDto
    {
        public string Description { set; get; }
        public Boolean IsRefund { set; get; }
        public Guid UserId { set; get; }
        public string MotiveReject { set; get; }
    }

}
