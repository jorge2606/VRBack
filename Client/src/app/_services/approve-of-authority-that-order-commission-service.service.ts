import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ApproveOfAuthorityThatOrderCommissionService {

  constructor(private http : HttpClient) { }

  getApproved(Id : any){
    if (!Id){
      Id="00000000-0000-0000-0000-000000000000";
    }
    return this.http.get<any>(environment.apiUrl + "ApproveOfAuthorityThatOrderCommission/GetApproved/"+Id);
  }
}
