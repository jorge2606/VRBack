import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CityBaseDto } from '../_models/city';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CityService {

  constructor(private http : HttpClient) { }

  getAll(){
    return this.http.get<any>(environment.apiUrl+"City/GetAll/");
  }

  GetByIdCity(cityId : number){
    return this.http.get<CityBaseDto[]>(environment.apiUrl+"City/GetByIdCity/"+cityId);
  }
}
