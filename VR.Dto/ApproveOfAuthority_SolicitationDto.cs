using System;
using System.Collections.Generic;
using System.Text;

namespace VR.Dto
{
    public class ApproveOfAuthority_SolicitationDto
    {
        public Guid Id { set; get; }
        public Guid SolicitationSubsidyId { set; get; }
        public Guid AprApproveOfAuthorityThatOrderCommissionId { set; get; }
    }
}
