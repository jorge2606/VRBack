using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using VR.Data.Interfaces;

namespace VR.Data.Model
{
    public class Expenditure
    {
        public Guid Id { set; get; }
        public decimal Amount { set; get; }
        public decimal? AccountedForAmount { set; get; }
        public string Description { set; get; }
        public int OrderNumber { set; get; }
        public DateTime SupportingDate { set; get; }
        public bool IsRequested { set; get; }
        public decimal Percentage { set; get; }
        

        [ForeignKey("SolicitationSubsidy")]
        public Guid SolicitationSubsidyId { set; get; }
        [ForeignKey("ExpenditureType")]
        public Guid ExpenditureTypeId { set; get; }
        [ForeignKey("User")]
        public Guid? UserId { set; get; }
        public List<File> Images { set; get; }
        public SolicitationSubsidy SolicitationSubsidy { set; get; }
        public ExpenditureType ExpenditureType { set; get; }
        public User User { set; get; }
    }
}
