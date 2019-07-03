using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using VR.Data.Interfaces;

namespace VR.Data.Model
{
    public class User : IdentityUser<Guid>, IsDeletedInterface
    {
        public string Dni { set; get; }
        public string FirstName { set; get; }
        public string LastName { set; get; }
        public Boolean IsDeleted { set; get; }
        [ForeignKey("Category")]
        public Guid CategoryId { set; get; }
        public Boolean SuperAdmin { set; get; }

        [ForeignKey("Distribution")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid? DistributionId { set; get; }
        public Distribution Distribution { set; get; }
        public Category Category { set; get; }
    }
}