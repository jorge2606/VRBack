export class TransportBaseDto{
    id : number;
    type : string;
    model : string;
    brand : string;
    carPlate : string;
    description : string;
}

export class CreateTransportDto extends TransportBaseDto{}

export class UpdateTransportDto extends TransportBaseDto{}

export class DeleteTransportDto extends TransportBaseDto{}

export class FindByIdTransportDto extends TransportBaseDto{}

export class AllTransportDto extends TransportBaseDto{}

export class CarIsBeingUsedByOtherSolicitation
{
    id : number;
    startDate : any;
    days : number;
    
    constructor(p_id : number, p_startDate : any, p_days : number){
        this.id = p_id;
        this.startDate = p_startDate;
        this.days = p_days;
    }
}