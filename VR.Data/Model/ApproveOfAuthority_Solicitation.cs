using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace VR.Data.Model
{
    public class ApproveOfAuthority_Solicitation
    {
        public Guid Id { set; get; }
        [ForeignKey("SolicitationSubsidy")]
        public Guid SolicitationSubsidyId { set; get; }
        [ForeignKey("AprApproveOfAuthorityThatOrderCommission")]
        public Guid AprApproveOfAuthorityThatOrderCommissionId { set; get; }

        public ApproveOfAuthorityThatOrderCommission AprApproveOfAuthorityThatOrderCommission { set; get; }
        public SolicitationSubsidy SolicitationSubsidy { set; get; }
    }
}
