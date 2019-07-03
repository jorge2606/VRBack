import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ProvinceService {

  constructor(private http : HttpClient) { }

  getAll(){
    return this.http.get<any>(environment.apiUrl+"Province/GetAll/");
  }

  getByIdPlace(placeId : number){
    return this.http.get<any>(environment.apiUrl+"Province/PlaceId/"+placeId);
  }

  getProvincesByDistrictCity(districtCity : string){
    return this.http.get<any>(environment.apiUrl+"Province/DistrictCity/"+districtCity);
  }

  findByCountryId(countryId : number){
    return this.http.get<any>(environment.apiUrl+"Province/FindByCountryId/"+countryId);
  }
}
