import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { UpdateRoleClaimPermission } from '../_models/update-role-claim-permission';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class RolesPermissionsService {

  constructor(private http: HttpClient) { }

  getAllRoleClaims(id: string) {
    return this.http.get<any[]>(environment.apiUrl+'Role/getallClaims?id=' + id);
  }

  saveRoleClaims(model: UpdateRoleClaimPermission) {
    return this.http.put<any>(environment.apiUrl+'Role/UpdateClaims/', model);
  }
}
