import { AllPlaceDto } from './../_models/place';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class PlaceService {

  insideOfTheProvince : string = 'f51d9b3e-787d-4a17-8b19-141f2ad78be6';
  outsideOfProvince : string = '2b786c07-4b7c-43b8-8760-6e3009465cec';
  outsideCountry : string = 'a19c0541-3bd5-4cc9-8836-661edb88d833';

  constructor(private http : HttpClient) { }

  getAll(){
    return this.http.get<AllPlaceDto[]>(environment.apiUrl+"Place/GetAll/");
  }
}
