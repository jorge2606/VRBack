import { FindByIdCategoryDto } from 'src/app/_models/category';
import { CreateCategoryDto, UpdateCategoryDto } from './../_models/category';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  constructor(private http: HttpClient) { }

  createCategory(createCategoryDto : CreateCategoryDto){
      return this.http.post<any>(environment.apiUrl+'Category/Create/', createCategoryDto);
  }

  getPaginator(filters: any) {
    return this.http.get<any>(environment.apiUrl+'Category/page/', {params: filters});
  }

  getallCategories() {
    return this.http.get<any>(environment.apiUrl+'Category/GetAllCategories/');
  }

  deleteCategory(idCategory : number){
    return this.http.delete(environment.apiUrl+'Category/Delete/'+idCategory);
  }

  findByIdCategory(idCategory : number){
    return this.http.get<FindByIdCategoryDto>(environment.apiUrl+'Category/FindByIdCategory/'+idCategory)
  }
  updateCategory(updateCategory :UpdateCategoryDto){
    return this.http.put<any>(environment.apiUrl+'Category/UpdateCategory',updateCategory);
  }
  
}
