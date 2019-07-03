class UserAuditBase{
    dni : number;
    userName : string;
    phoneNumber : String;
}
export class UserAudit{
    auditAction : string;
    auditDate : Date;
    auditUser : string;
    id : number;
    userName : string;
    current : UserAuditBase;
    previous : UserAuditBase;
}