export class Roles{
    id : string;
    name : string;
    normalizedName : string;
    concurrencyStamp : number;
}

export class RoleUserDto{
    roleId : number;
    userId : number;
}