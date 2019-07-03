import { OrganismBaseDto } from "./organism";
import { User } from "../users/users";

export class DistributionBaseDto{
    id : number;
    name : string;
    description : string;
    organismId : number;
    organism : OrganismBaseDto;
    users : User[];
}

export class CreateDistributionDto extends DistributionBaseDto{}

export class UpdateDistributionDto extends DistributionBaseDto{}

export class DeleteDistributionDto extends DistributionBaseDto{}

export class FindByIdDistributionDto extends DistributionBaseDto{}