import { CreateOrganismDto, UpdateOrganismDto } from './../_models/organism';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class OrganismService {

  constructor(private http : HttpClient) { }

  getAllOrganism(){
    return this.http.get<any>(environment.apiUrl+'Organism/GetAll');
  }

  getPaginator(filters: any) {
    return this.http.get<any>(environment.apiUrl+'Organism/page/', {params : filters});
  }

  createOrganism(createOrganism : CreateOrganismDto){
      return this.http.post<any>(environment.apiUrl+'Organism/Create',createOrganism);
  }

  updateOrganism(updateOrganism : UpdateOrganismDto){
    return this.http.put<any>(environment.apiUrl+'Organism/Update',updateOrganism);
  }

  findById(id : number){
    return this.http.get<any>(environment.apiUrl+'Organism/FindById/'+id);
  }

  deleteOrganism(id : number){
    return this.http.delete(environment.apiUrl+'Organism/Delete/'+id);
  }
}
