import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { SupervisorUserAgentBaseDto } from '../_models/supervisorUserAgent';
import { User } from '../_models/user';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class SupervisorUserAgentService {

  constructor(private http : HttpClient) { }

  create(supervisorAgent : SupervisorUserAgentBaseDto[]){
    return this.http.post<any>(environment.apiUrl+"SupervisorUserAgent/Create/",supervisorAgent);
  }

  getAll(){
    return this.http.get<any>(environment.apiUrl+"SupervisorUserAgent/AllSupervisorAgents");
  }

  getPaginator(filters: any) {
    return this.http.get<any>(environment.apiUrl+'SupervisorUserAgent/page',{params: filters});
  }

  isAgent(userId : number){
    return this.http.get<any>(environment.apiUrl+"SupervisorUserAgent/IsAgent/"+userId);
  }

  allSupervisors(){
    return this.http.get<any>(environment.apiUrl+"SupervisorUserAgent/AllSupervisors");
  }

  deleteRelationshipBetweenAgentAndSupervisor(supervisorId : number, agentId : number){
    return this.http.delete<any>(environment.apiUrl+"SupervisorUserAgent/deleteRelationshipBetweenAgentAndSupervisor/"+supervisorId+"/"+agentId);
  }
}
