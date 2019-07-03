import { SolicitationStatesBaseDto } from './solicitationState';
import { DestinyDto, destinyForModifyingSolicitationDto } from './destiny';
import { User } from '../users/users';
import { DateDto } from './holiday';
import { ApproveOfAuthorityThatOrderCommissionDto } from './ApproveOfAuthorityThatOrderCommission';
import { ObservationBaseDto } from './observation';

export class ImageDto{
    lastModified: string;
    lastModifiedDate : any;
    name : string;
    size : number;
    type : string;
    webkitRelativePath : string;
    urlImages : string;
}

export class Album{
    src: string;
    caption: string;
    thumb: string;
}

export class ExpenditureBaseDto {
    id : number;
    description : string;
    amount : number;
    expenditureTypeId : number; 
    expenditureTypeName : string;   
    urlImage : string;
    imageDto : ImageDto;
    imagesDto : ImageDto[];
    orderNumber : number;
    supportingDate : any;
    supportingDateTime : any;
    isRequested : boolean;
}
export class Expenditure extends ExpenditureBaseDto{
    accountedForAmount : number;
    disabled : boolean;
}

export class ExpenditureForModifyingDto extends ExpenditureBaseDto{

}


export class SolicitationSubsidyBaseDto{
    id : number;
    motive : string;
    userId : number;
    destinies : DestinyDto[];
    expenditures : Expenditure[];
    total : number;
    createDate : any;
    isRefund : boolean;
    state : string;
    stateId : number;
}

export class SolicitationSubsidyDetail extends SolicitationSubsidyBaseDto{
    user : User;
    finalizeDate : DateDto;
}


export class CreateSolicitationSubsidyDto{
    id : number;
    motive : string;
    userId : number;
    destinies : destinyForModifyingSolicitationDto[];
    expenditures : ExpenditureForModifyingDto[];
    total : number;
    createDate : any;
    isRefund : boolean;
    finalizeDate : DateDto;
    isCommission : boolean;
    randomKey : string;
}

export class CreateAccountForSolicitationSubsidyDto{
    id : number;
    motive : string;
    userId : number;
    destinies : DestinyDto[];
    expenditures : Expenditure[];
    total : number;
    createDate : any;
    isRefund : boolean;
    finalizeDate : DateDto;
    isCommission : boolean;
    randomKey : string;
    latestSolicitationStates : SolicitationStatesBaseDto[];
    fileNumber : string;
    allApproved : ApproveOfAuthorityThatOrderCommissionDto[];
    observations : ObservationBaseDto[];
}

export class AllSolicitationSubsidyDto extends SolicitationSubsidyBaseDto{
    user : any;
    state : string;
    motiveReject : string;
    fileNumber : string;
    fullName : string;
    localities : string;
    beginDate : Date;
    endDate : Date;
    daysWeekEnd : number = 0;
    daysHolidays : number = 0;
    aprovedByFirstInstance : number;
}

export class DetailSolicitationSubsidyDto{
    id : number;
    motive : string;
    user : any;
    destinies : DestinyDto[];
    expenditures : Expenditure[];
    total : number;
    createDate : any;
}

export class SolicitationIdDto extends DetailSolicitationSubsidyDto
{
    id : number;
    motiveReject : string;
    fileNumber : string;
    isRefund : boolean;
}