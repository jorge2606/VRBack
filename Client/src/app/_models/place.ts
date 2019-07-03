export class PlaceBaseDto{
    id : number;
    description : string;
    checked : boolean;
    order : number;
}

export class AllPlaceDto extends PlaceBaseDto {}