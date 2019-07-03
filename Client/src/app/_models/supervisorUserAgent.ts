import { User } from 'src/app/_models/user';
export class SupervisorUserAgentBaseDto{
    id : number;
    supervisorId : number;
    agentId : number;
}

export class AllSupervisorUserAgent{
    id : number;
    supervisors : SupervisorUserAgentBaseDto[];
    supervisors2 : SupervisorUserAgentBaseDto[];
    agents : SupervisorUserAgentBaseDto[];
}

export class SupervisorsDto{
    supervisor : User;
    roleName : string;
}