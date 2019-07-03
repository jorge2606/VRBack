import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ExpendituresUserService {

  constructor(private http : HttpClient) { }

  getByIdSolicitationSubsidy(id : number){
    return this.http.get<any>(environment.apiUrl+"Expenditure/GetByIdSolicitationSubsidy/{id}"+id);
  }

  delete(id : number){
    return this.http.delete<any>(environment.apiUrl+"Expenditure/Delete/"+id);
  }
}
