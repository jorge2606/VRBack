import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Roles } from '../_models/roles';
import { rolesBelongUser } from '../users/users';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class RoleService {

  constructor(private http: HttpClient) { }

  getAll(){
    return this.http.get<rolesBelongUser[]>(environment.apiUrl+"Role/getAll");
  }

  getAllRoles(){
    return this.http.get<Roles>(environment.apiUrl+"Role/AllRoles");
  }

  create(rol : Roles){
    return this.http.post<any>(environment.apiUrl+"Role/create",rol);
  }

  getById(id : number){
    return this.http.get<any>(environment.apiUrl+"Role/getById/"+id);
  }
}