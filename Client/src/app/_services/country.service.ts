import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CountryService {

  constructor(private http : HttpClient) { }

  getAllCountries(){
    return this.http.get<any>(environment.apiUrl+'Country/GetAllCountry');
  }
}
