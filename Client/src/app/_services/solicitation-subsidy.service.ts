import { SolicitationSubsidyBaseDto, SolicitationIdDto } from './../_models/solicitationSubsidy';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CreateSolicitationSubsidyDto } from '../_models/solicitationSubsidy';
import { overlapingDto } from '../_models/overlaping';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class SolicitationSubsidyService {

  constructor(private http : HttpClient) { }

  getAllSolicitationSubsidiesSupervisor(filter : any){
    return this.http.get<any>(environment.apiUrl+"SolicitationSubsidy/pageSupervisor/",{params : filter});
  }

  getAllSolicitationSubsidiesAgent(filter : any){
    return this.http.get<any>(environment.apiUrl+"SolicitationSubsidy/pageAgent/",{params : filter});
  }

  createSolicitation(createSolicitationSubsidy : CreateSolicitationSubsidyDto){
    return this.http.post<any>(environment.apiUrl+"SolicitationSubsidy/Create/",createSolicitationSubsidy);
  }

  createCommission(createCommission : CreateSolicitationSubsidyDto){
    return this.http.post<any>(environment.apiUrl+"SolicitationSubsidy/CreateCommission/",createCommission);
  }

  updateCommission(updateCommission : CreateSolicitationSubsidyDto){
    return this.http.post<any>(environment.apiUrl+"SolicitationSubsidy/UpdateCommission/",updateCommission);
  }

  createAccountFor(createSolicitationSubsidy : CreateSolicitationSubsidyDto){
    return this.http.post<any>(environment.apiUrl+"SolicitationSubsidy/CreateAccountFor/",createSolicitationSubsidy);
  }
  updateSolicitation(solicitation : any){
      return this.http.put<any>(environment.apiUrl+"SolicitationSubsidy/Update", solicitation);
  }

  getByIdSolicitation(solicitationSubsidyId : number){
    return this.http.get<any>(environment.apiUrl+"SolicitationSubsidy/getBySolicitationId/"+solicitationSubsidyId);
  }

  getByIdSolicitationNotAccountFor(solicitationSubsidyId : number){
    return this.http.get<any>(environment.apiUrl+"SolicitationSubsidy/GetByIdSolicitationSubsidy/"+solicitationSubsidyId);
  }

  getBySolicitationIdForEmail(solicitationSubsidyId : number){
    return this.http.get<any>(environment.apiUrl+"SolicitationSubsidy/getBySolicitationIdForEmail/"+solicitationSubsidyId);
  }
  getBySolicitationIdWhitState(solicitationSubsidyId : number){
    return this.http.get<any>(environment.apiUrl+"SolicitationSubsidy/GetBySolicitationIdWhitState/"+solicitationSubsidyId);
  }
  delete(id : number){
     return this.http.delete<any>(environment.apiUrl+"SolicitationSubsidy/Delete/"+id);
  }

  finalizeSubsidy(id : number){
    return this.http.delete<any>(environment.apiUrl+"SolicitationSubsidy/FinalizeSubsidy/"+id);
 }

  
  sendSolicitationByEmail(solicitation : SolicitationIdDto){
    return this.http.post(environment.apiUrl+"SolicitationSubsidy/sendSolicitation/",solicitation);
  }

  sendAccountForToSupervisor(solicitation : SolicitationIdDto){
    return this.http.post(environment.apiUrl+"SolicitationSubsidy/sendAccountForToSupervisor/",solicitation);
  }

  validateBeforeSendAccountFor(solicitationId : number){
    return this.http.get<any>(environment.apiUrl+"SolicitationSubsidy/validateBeforeSendAccountFor/"+solicitationId);
  }

  validateBeforeSendAccountForFinalizeNormally(solicitationId : number){
    return this.http.get<any>(environment.apiUrl+"SolicitationSubsidy/validateBeforeSendAccountForFinalizeNormally/"+solicitationId);
  }

  someSolicitationHasThisExpenditure(key : string, expenditureId : number){
    return this.http.get<any>(environment.apiUrl+"SolicitationSubsidy/SomeSolicitationHasThisExpenditure/"+key+"/"+expenditureId);
  }


  acepted(solicitation : SolicitationIdDto){
    return this.http.post<any>(environment.apiUrl+"SolicitationSubsidy/AceptedSolicitation",solicitation);
  }

  aceptedMySolicitation(solicitation : SolicitationIdDto){
    return this.http.post<any>(environment.apiUrl+"SolicitationSubsidy/AceptedMySolicitation",solicitation);
  }

  aceptedAccountForSolicitation(solicitation : SolicitationIdDto){
    return this.http.post<any>(environment.apiUrl+"SolicitationSubsidy/AceptedAccountForSolicitation",solicitation);
  }

  aceptedMyAccountForSolicitation(solicitation : SolicitationIdDto){
    return this.http.post<any>(environment.apiUrl+"SolicitationSubsidy/AceptedMyAccountForSolicitation",solicitation);
  }

  refused(solicitation : SolicitationIdDto){
    return this.http.post<any>(environment.apiUrl+"SolicitationSubsidy/RefusedSolicitation",solicitation);
  }

  refusedAccountForSolicitation(solicitation : SolicitationIdDto){
    return this.http.post<any>(environment.apiUrl+"SolicitationSubsidy/RefusedAccountForSolicitation",solicitation);
  }

  overlapingDates(overlapDate : overlapingDto){
    return this.http.post<any>(environment.apiUrl+"SolicitationSubsidy/overlaping",overlapDate);
  }

  SolicitationApprovedBySupervisorId(id : number){
    return this.http.get<any>(environment.apiUrl+"SolicitationSubsidy/SolicitationApprovedBySupervisorId/"+id);
  }

  wichStateSolicitation(solicitationId : number){
    return this.http.get<any>(environment.apiUrl+"SolicitationSubsidy/WichStateSolicitation/"+solicitationId);
  }

  getImageRefundExpenditure(expId : number, width : number, height : number){
    return this.http.get<any>(environment.apiUrl+"File/ExpenditureRefund/"+expId+"/"+width+"/"+height);
  }

  getUrlImageExpenditure(expId : number, width : number, height : number){
    return this.http.get<any>(environment.apiUrl+"File/ExpenditureRefundUrl/"+expId+"/"+width+"/"+height);
  }


  getImageHolographSignUrl(supervisorId : number, width : number, height : number){
    return this.http.get<any>(environment.apiUrl+"File/HolographSignUrl/"+supervisorId+"/"+width+"/"+height);
  }

  getAmountHolidaysAndWeekends(solicitationId : number){
    return this.http.get<any>(environment.apiUrl+"SolicitationSubsidy/getAmountHolidaysAndWeekends/"+solicitationId);
  }

  getByRandomKey(randomKey : string){
    return this.http.get<any>(environment.apiUrl+"SolicitationSubsidy/GetByRandomKey/"+randomKey);
  }

  posponeSolicitation(posponeList : any){
    return this.http.post<any>(environment.apiUrl+"SolicitationSubsidy/posponeSolicitation/",posponeList);
  }

}
