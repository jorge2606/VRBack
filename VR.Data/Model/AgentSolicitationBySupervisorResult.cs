
using System;
using System.Collections.Generic;
using System.Text;

namespace VR.Data.Model
{
    public class AgentSolicitationBySupervisorResult
    {
        public Guid Id { set; get; }
        public string Motive { set; get; }
        public decimal Total { set; get; }
        public Guid UserId { set; get; }
        public DateTime CreateDate { set; get; }
        public string State { set; get; }
        public Guid StateId { set; get; }
        public string MotiveReject { set; get; }
        public string FileNumber { set; get; }
        public string FullName { set; get; }
        public string Localities { set; get; }
        public Boolean IsRefund { set; get; }
        public Guid AprovedByFirstInstance { set; get; }
        public DateTime? FinalizeDate { set; get; }
        public DateTime? BeginDate { set; get; }
        public DateTime? EndDate { set; get; }
    }
}
