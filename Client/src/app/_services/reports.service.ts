import { AuthenticationService } from 'src/app/_services/authentication.service';
import { DateDto } from './../_models/holiday';
import { HttpClient, HttpParams, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ReportsService {

  constructor(
    private http : HttpClient,
    private authService : AuthenticationService
  ) { }

  reportSolicitationByDestiniesAndDates(filters : any){
    var param = new HttpParams({
      fromObject :{
        'cityId' : filters.cityId,
        'countryId' : filters.countryId,
        'provinceId' : filters.provinceId,
        'endDay': !filters.endDate.day ? '0' : filters.endDate.day,
        'endMonth' : !filters.endDate.month ? '0' : filters.endDate.month,
        'endYear' : !filters.endDate.year ? '0' : filters.endDate.year,
        'startDateDay': !filters.startDate.day ? '0' : filters.startDate.day,
        'startDateMonth' : !filters.startDate.month ? '0' : filters.startDate.month,
        'startDateYear' : !filters.startDate.year ? '0' : filters.startDate.year
      }
    });
    
    return this.http.get<any>(environment.apiUrl+"Report/Report_SolicitationByDestiniesAndDates",{params: param});
  }


  solicitationWasApproved(id : number){
    return this.http.get<any>(environment.apiUrl+"SolicitationSubsidy/report/"+id);
  }

  
}
