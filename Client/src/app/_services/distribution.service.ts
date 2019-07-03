import { FindByIdDistributionDto, UpdateDistributionDto, CreateDistributionDto } from './../_models/distributions';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class DistributionService {

  constructor(private http: HttpClient) { }

  getPaginator(filters: any) {
    return this.http.get<any>(environment.apiUrl+'Distribution/page/',{params : filters});
  }

  allDistribution(){
    return this.http.get<any>(environment.apiUrl+'Distribution/AllDistributions');
  }
  findByIdDistribution(distributionId : number){
    return this.http.get<FindByIdDistributionDto>(environment.apiUrl+'Distribution/FindByIdDistribution/'+distributionId)
  }

  findByIdOrganism(organismId : number){
    return this.http.get<any>(environment.apiUrl+'Distribution/FindByIdOrganism/'+organismId);
  }

  updateDistribution(updateDistribution : UpdateDistributionDto){
    return this.http.put(environment.apiUrl+'Distribution/UpdateDistribution',updateDistribution);
  }
  
  deleteDistribution(distributionId : number){
    return this.http.delete(environment.apiUrl+'Distribution/DeleteDistribution/'+distributionId);
  }

  creteDistribution(createDistribution : CreateDistributionDto){
      return this.http.post(environment.apiUrl+'Distribution/CreateDistribution',createDistribution);
  }

}
