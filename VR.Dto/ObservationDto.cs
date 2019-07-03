using System;
using System.Collections.Generic;
using System.Text;

namespace VR.Dto
{
    public class ObservationDto
    {
        public Guid Id { set; get; }
        public string Description { set; get; }
        public Guid SolicitationId { set; get; }
    }
}
