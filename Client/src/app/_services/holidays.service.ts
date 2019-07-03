import { CreateHolidayDto, FindByIdHolidayDto, UpdateHolidayDto, DateDto } from './../_models/holiday';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class HolidaysService {

  constructor(private http : HttpClient) { }

  getPageHoliday(filters : any){
    var param = new HttpParams({
      fromObject : {
        'description' : filters.description,
        'page' : filters.page,
        'day' : filters.date == null ? 0 : filters.date.day,
        'month' : filters.date == null ? 0 : filters.date.month, 
        'year' : filters.date == null ? 0 : filters.date.year
      }
    });
    return this.http.get<any>(environment.apiUrl+"Holiday/GetPageHoliday/", {params : param});
  }

  createHoliday(newHoliday : CreateHolidayDto){
    return this.http.post<any>(environment.apiUrl+"Holiday/create",newHoliday);
  }

  getByIdHoliday(id : number) {
    return this.http.get<FindByIdHolidayDto>(environment.apiUrl+"Holiday/getById/"+id);
  }

  haveHolidays(date : any, days : any) {
    var params = new HttpParams({
     fromObject : {
       'day' : date.day,
       'month' : date.month,
       'year' : date.year,
       'amountDays' : days
     }
    });
    return this.http.get<any>(environment.apiUrl+"Holiday/haveHolidays/",{params : params});
  }

  updateHoliday(updateHoliday : UpdateHolidayDto){
    return this.http.put<any>(environment.apiUrl+"Holiday/update",updateHoliday);
  }

  deleteHoliday(id : number){
    return this.http.delete<any>(environment.apiUrl+"Holiday/Delete/"+id);
  }

}
