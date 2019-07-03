using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;
using System.Text;

namespace VR.Data.Model
{
    public class Destiny
    {
        public Guid Id { set; get; }
        public int Days { set; get; }
        public DateTime StartDate { set; get; }
        public DateTime EndDate { set; get; }
        public List<SupplementaryCity> SupplementaryCities { set; get; }
        public Decimal AdvanceCategory { set; get; }
        public Decimal PercentageCodeLiquidation { set; get; }
        public Decimal CostRepayment { set; get; }
        public int? AccountedForDays { set; get; }
        public int DaysWeekEnd { set; get; }
        public int DaysHolidays { set; get; }
        public int DaysPay { set; get; }

        [ForeignKey("Category")]
        public Guid CategoryId { set; get; }
        [ForeignKey("City")]
        public Guid? CityId { set; get; }
        [ForeignKey("CodeLiquidation")]
        public Guid CodeLiquidationId { set; get; }
        [ForeignKey("Country")]
        public Guid? CountryId { set; get; }
        [ForeignKey("Province")]
        public Guid? ProvinceId { set; get; }
        [ForeignKey("Transport")]
        public Guid TransportId { set; get; }
        [ForeignKey("SolicitationSubsidy")]
        public Guid SolicitationSubsidyId { set; get; }

        public Category Category { set; get; }
        public CodeLiquidation CodeLiquidation { set; get; }
        public Country Country { set; get; }
        public Transport Transport { set; get; }
        public Province Province { set; get; }
        public SolicitationSubsidy SolicitationSubsidy { set; get; }
        public City City { set; get; }
    }
}
