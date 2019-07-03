import { DistributionBaseDto } from 'src/app/_models/distributions';

export class User{
  id : number;
  dni: string;
  userName : string;
  phoneNumber : string;
  password : string;
  distributionId : number;
  //distribution : DistributionBaseDto;
  checked : boolean;
  rol : string;
  firstName : string;
  lastName : string;
  categoryId : number;
  categoryName : string;
  categoryDescription : string;
  categoryAdvance : number;
  distributionDescription: string;
  distributionName: string;

}

export class rolesBelongUser{
  id : number;
  name : string;
  rolBelongUser : any;
  normalizedName : string;
}
export class modifyUser extends User{
  rolesUser : rolesBelongUser[];
  repeatPassword : string;
  organismId : number;
  supervisorAgentId : any;
  supervisorAgentId2 : any;
}


export class createUser extends modifyUser{
}

