export class CityBaseDto{
    id : number;
    name : string;
    createdDate : Date;
    provinceId : number;
    IsDeleted : boolean;
}


export class AllCitiesDto extends CityBaseDto {}

export class ComplementariesCitiesDto{
    cityId : number;
}