import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ForgotPassword } from 'src/app/_models/passwords';
import { ResetPassword } from '../_models/reset-password';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class RecoveryPasswordService {

  constructor(private http : HttpClient) { }

  ForgotPassword(passwordObj : ForgotPassword){
    return this.http.post(environment.apiUrl+"User/ForgotPassword/",passwordObj);
  }

  ResetPassword(passwordObj : ResetPassword){
    return this.http.post<any>(environment.apiUrl+"User/ResetPassword/",passwordObj);
  }

}
