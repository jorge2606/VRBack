using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace VR.Data.Model
{
    public class Observation
    {
        public Guid Id { set; get; }
        public string Description { set; get; }
        [ForeignKey("SolicitationSubsidy")]
        public Guid SolicitationId { set; get; }

        public SolicitationSubsidy SolicitationSubsidy { set; get; }
    }
}
