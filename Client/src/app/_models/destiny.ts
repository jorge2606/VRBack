import { SupplementaryCityDto } from './supplementaryCity';

export class DestinyBaseDto{
    id : any;
    idExp : string;
    placeId : number;
    provinceId : number;
    provinceName : string;
    cityId : number; 
    cityName : string;
    supplementaryCities : SupplementaryCityDto[];
    countryId : number;
    countryName : string;
    codeLiquidationPercentage : number;
    startDate : any;
    days : number;
    daysWeekEnd : number = 0;
    daysHolidays : number = 0;
    categoryId : number;
    categoryName : string;
    codeLiquidationId : number;
    transport : any;
    transportId : any;
    transportBrand : string;
    transportModel : string;
    transportCarPlate : string;
    transportType : string;
    advanceCategory : number;
    percentageCodeLiquidation : number;
    solicitationTotalLetter : string;
    solicitationSubsidyId : number;
    textPercentage : string;
    daysPay : number;
    timeStartDate : any;
    timeEndDate : any;
}
export class destinies_from_store_procedure{
    days : number;
    daysLetters : string;
    advanceCategory : number;
}

export class DestinyDto extends DestinyBaseDto{
    accountedForDays : number;
}

export class destinyForModifyingSolicitationDto extends DestinyBaseDto{

}


