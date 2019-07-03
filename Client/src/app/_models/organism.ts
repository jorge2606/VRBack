export class OrganismBaseDto{
    id : number;
    name : string;
    description : string;
}

export class CreateOrganismDto extends OrganismBaseDto {}

export class UpdateOrganismDto extends OrganismBaseDto {}

export class FindByIdOrganismDto extends OrganismBaseDto {}