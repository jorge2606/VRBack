import { HttpClient } from '@angular/common/http';
import { DestinyDto } from 'src/app/_models/destiny';
import { Injectable } from '@angular/core';
import { Subject, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class DestinyService {

  private subject = new Subject<DestinyDto[]>();
 
  constructor(private http : HttpClient) { }
  
  delete(id : number){
      return this.http.delete<any>(environment.apiUrl+"Destiny/Delete/"+id);
  }

  create(destinations : any){
    return this.http.post<any>(environment.apiUrl+"Destiny/Create/",destinations);
  }
  sendMessage(message: DestinyDto[]) {
      this.subject.next(message);
  }

  clearMessage() {
      this.subject.next();
  }

  getMessage(): Observable<DestinyDto[]> {
      return this.subject.asObservable();
  }

  get_destinies(solicitationId : number){
    return this.http.get<any>(environment.apiUrl+"Destiny/Get_Destiny/"+solicitationId);
  }
  
}
