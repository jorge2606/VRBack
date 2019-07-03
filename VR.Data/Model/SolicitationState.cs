using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VR.Data.Model
{
    public class SolicitationState
    {
        public Guid Id { set; get; }
        public DateTime ChangeDate {set; get; }
        public string MotiveReject { set; get; }
        public string FileNumber { set; get; }
        public Guid SupervisorId { set; get; }
        public Guid SupervisorId2 { set; get; }

        [ForeignKey("State")]
        public Guid StateId { set; get; }
        [ForeignKey("SolicitationSubsidy")]
        public Guid SolicitationSubsidyId { set; get; }

        public State State { set; get; }
        public SolicitationSubsidy SolicitationSubsidy { set; get; }
    }
}
