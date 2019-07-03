import { LegalRulingsBaseDto } from './../_models/legalRuling';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class LegalRulingsService {

  constructor(private http : HttpClient) { }

  create(LegalRuling : LegalRulingsBaseDto){
    return this.http.post<any>(environment.apiUrl + "LegalRulings/",LegalRuling);
  }

  page(filters : any){
    var param = new HttpParams({
      fromObject : {
        'description' : filters.description,
        'number' : filters.number,
        'page' : filters.page,
        'day' : filters.date == null ? 0 : filters.date.day,
        'month' : filters.date == null ? 0 : filters.date.month, 
        'year' : filters.date == null ? 0 : filters.date.year
      }
    });
    return this.http.get<any>(environment.apiUrl+ "LegalRulings/Page", {params : param});
  }

  getById(id : number){
    return this.http.get<any>(environment.apiUrl + "LegalRulings/GetById/"+id);
  }

  delete(id : number){
    return this.http.delete<any>(environment.apiUrl+"LegalRulings/Delete/"+id);
  }
}
