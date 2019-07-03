export class ProvinceBaseDto {
    id : number;
    name : string;
    createdDate : Date;
    isDeleted : boolean;
    placeId : number;
    districtCity : string;
    countryId : number;
    poblation : number;
    printableName : string;
}

export class CreteProvinceDto extends ProvinceBaseDto {}

export class UpdateProvinceDto extends ProvinceBaseDto {}

export class DeleteProvinceDto extends ProvinceBaseDto {}

export class AllProvinceDto extends ProvinceBaseDto {}