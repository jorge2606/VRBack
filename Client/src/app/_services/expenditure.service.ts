import { CreateExpenditureDto, UpdateExpenditureDto } from '../_models/expenditureType';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Subject, Observable } from 'rxjs';
import { Expenditure } from '../_models/solicitationSubsidy';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ExpenditureService {

  private subject = new Subject<any[]>();
  
  constructor(private http : HttpClient) { }

  getPaginator(page: number) {
    return this.http.get<any>(environment.apiUrl+'ExpenditureType/page/' + page);
  }

  createExpenditure(createExpenditure : CreateExpenditureDto){
      return this.http.post<any>(environment.apiUrl+'ExpenditureType/Create',createExpenditure);
  }

  findByIdExpenditure(id : number){
    return this.http.get<any>(environment.apiUrl+'ExpenditureType/FindById/'+id);
  }

  findByIdSolicitationSubsidy(solicitationId : number){
    return this.http.get<any>(environment.apiUrl+'ExpenditureType/FindBysolicitationId/'+solicitationId);
  }

  updateExpenditure(updateExpenditure : UpdateExpenditureDto){
    return this.http.put<any>(environment.apiUrl+'ExpenditureType/Update/',updateExpenditure);
  }

  deleteExpenditure(id : number){
    return this.http.delete<any>(environment.apiUrl+'ExpenditureType/Delete/'+id);
  }

  getAll(){
    return this.http.get<any>(environment.apiUrl+'ExpenditureType/GetAll/');
  }

  sendMessage(message: any[]) {
    this.subject.next(message);
  }

  clearMessage() {
    this.subject.next();
  }

  getMessage(): Observable<any[]> {
    return this.subject.asObservable();
  }

}
