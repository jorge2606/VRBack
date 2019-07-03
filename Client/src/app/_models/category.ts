export class Category{
    id : number;
    name : string;
    description : string;
    isDeleted : Date;
    advance : number;
    order : number;
}

export class CreateCategoryDto extends Category{}

export class UpdateCategoryDto extends Category{}

export class FindByIdCategoryDto extends Category{}

export class AllCategoryDto extends Category{}