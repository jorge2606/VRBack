using System;

namespace VR.Dto.User
{
    public class UserFilterDto : FilterBaseDto
    {
        public string Dni { get; set; }

        public string FirstName { get; set; }

        public Guid? DistributionId { get; set; }
    }
}
