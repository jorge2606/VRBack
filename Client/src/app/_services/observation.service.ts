import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ObservationBaseDto } from '../_models/observation';
import { environment } from 'src/environments/environment';
import { Subject, Observable } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class ObservationService {

  private subject = new Subject<any>();

  constructor(private http : HttpClient) { }

  create(observations : any){
    return this.http.post<any>(environment.apiUrl + "Observation/create" ,observations);
  }

  getById(id : number){
    return this.http.get<any>(environment.apiUrl + "Observation/getById/" + id);
  }

  delete(url : string, id : number){
      return this.http.delete<any>(url+id);
  }
  sendMessage(message: any) {
      this.subject.next(message);
  }

  clearMessage() {
      this.subject.next();
  }

  getMessage(): Observable<any> {
      return this.subject.asObservable();
  }

}
