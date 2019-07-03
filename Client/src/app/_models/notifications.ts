import { AuditDto } from './audit';

export class Notifications extends AuditDto{
    id : number;
    textData : string;
    tittle : string;
    read : boolean;
    colapse : boolean;
    solicitationSubsidyId : number;
    userId : number;
    days : string;
}

