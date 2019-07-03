import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Subject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class GenericsCommunicationsComponentsService {

  private subject = new Subject<any>();
 
  constructor(private http : HttpClient) { }
  
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
