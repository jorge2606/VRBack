export class CountryBaseDto{
    id : number;
    name : string; 
    createdDate : any;
    isDeleted : any;
    placeId : number;
    iso3 : string;
    numCode : number;
    printableName : string;
}

export class AllCountryDto extends CountryBaseDto {}