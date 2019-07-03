using System;
using System.Collections.Generic;
using System.Text;
using VR.Dto.User;

namespace VR.Dto
{
    public class SupervisorUserAgentBaseDto
    {
        public Guid Id { get; set; }
        public Guid? SupervisorId { set; get; }
        public Guid? SupervisorId2 { set; get; }
        public Guid AgentId { set; get; }
        public UserDto Supervisors { set; get; }
        public UserDto Supervisors2 { set; get; }
        public UserDto Agents { set; get; }
        public Boolean IsDeleted { set; get; }
    }

    public class CreateSupervisorAgentDto : SupervisorUserAgentBaseDto { }
    public class AllSupervisorAgentDto : SupervisorUserAgentBaseDto { }
    public class DeleteSupervisorAgentDto : SupervisorUserAgentBaseDto { }

    public class SupervisorsDto
    {
        public UserDto Supervisors { set; get; }
        public string RoleName { set; get; }
    }

    public class UrlSignHolograph
    {
        public string UrlSupervisorId { set; get; }
        public string UrlSupervisorId2 { set; get; }
        public string NameSupervisor1 { set; get; }
        public string SurnameSupervisor1 { set; get; }
        public string NameSupervisor2 { set; get; }
        public string SurnameSupervisor2 { set; get; }
    }
}
