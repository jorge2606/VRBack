import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { UserRoles } from '../_models/userRoles';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AspNetUsersRolesService {

  constructor(private http : HttpClient) { }

  getAllUsersRoles(){
    return this.http.get<any>(environment.apiUrl+"AspNetUserRoles/AllRoles");
  }

  onlyRolesUsersRoles(){
    return this.http.get<any>(environment.apiUrl+"AspNetUserRoles/OnlyRoles");
  }
}
