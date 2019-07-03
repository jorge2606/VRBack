using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VR.Data.Model
{
    public class SupervisorUserAgent
    {
        [Key]
        public Guid Id { get; set; }
        public Guid? SupervisorId { set; get; }
        public Guid? SupervisorId2 { set; get; }
        public Guid AgentId { set; get; }
        public Boolean IsDeleted { set; get; }

        [ForeignKey("SupervisorId")]
        public User Supervisors { set; get; }
        [ForeignKey("SupervisorId2")]
        public User Supervisors2 { set; get; }
        [ForeignKey("AgentId")]
        public User Agents { set; get; }

    }
}
