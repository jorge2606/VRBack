import { CreateTransportDto, CarIsBeingUsedByOtherSolicitation } from 'src/app/_models/transport';
import { UpdateTransportDto } from './../_models/transport';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class TransportService {

  constructor(private http : HttpClient) { }

  getPaginator(filters: any) {
    return this.http.get<any>(environment.apiUrl+'Transport/page/',{params: filters});
  }

  findByIdTransport(id : number){
    return this.http.get<any>(environment.apiUrl+'Transport/FindByIdTransport/'+id);
  }

  updateTransport(updateTransport : UpdateTransportDto){
    return this.http.put<any>(environment.apiUrl+'Transport/Update/',updateTransport);
  }

  createTransport(CreateTransport : CreateTransportDto){
    return this.http.post<any>(environment.apiUrl+'Transport/Create/',CreateTransport);
  }

  deleteTransport(id : number){
    return this.http.delete<any>(environment.apiUrl+'Transport/Delete/'+id);
  }

  getAll(){
    return this.http.get<any>(environment.apiUrl+'Transport/GetAll/');
  }

  /**carIsBeingUsedByOtherSolicitation(transport : CarIsBeingUsedByOtherSolicitation){
    return this.http.post<boolean>(environment.apiUrl+'Transport/CarIsBeingUsedByOtherSolicitation', transport);
  }

  carIsBeingUsedByOtherSolicitationById(solicitationId : number){
    return this.http.get<any>(environment.apiUrl+'Transport/CarIsBeingUsedByOtherSolicitationById/'+solicitationId);
  }*/


}
