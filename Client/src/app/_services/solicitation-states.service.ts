import { GuidClass } from './../_helpers/guid-class';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class SolicitationStatesService {

  Accepted = +"3535DEC7-3CA2-4930-BE5B-32FDBBA7D02C";
  Rejected = +"723CCAD5-5F9A-496C-8F99-D27817F8E3F2";
  Pending = +"D4E28CE0-D7F6-46A2-849E-471F7A4EAACE";
  Sent = +"A152C5B9-D0B5-4CDA-92E8-F479568CFEF6";
  Accounted = +"D385AE53-F2DB-48A6-AF68-5E1FBE80F0E0";
  Finished = +"2C332F3C-CF16-4BEB-85C4-C4677700CF2C";
  AccountForAcepted = +"00CE5670-BC4B-4435-8C67-D754824D0FF2";
  AccountForRejected = +"74DFD597-8669-4AF8-8564-350B1E1996E4";
  Aprobado_1ra_Instancia = +"A24D92F5-BF62-4026-9564-DA9518DE343B";
  Aprobado_2da_Instancia = +"92A437C6-B915-4789-B64C-DC07EADB526E";
  Postergado = "CD7FBE8C-E410-4A27-8362-381C43D42564";
  constructor(private http : HttpClient) { }

  addFielNumber(fields : any){
    return this.http.put<any>(environment.apiUrl+"SolicitationState/AddFielNumberDto", fields);
  }

  itHasNumberFile(id : number){
    return this.http.get<any>(environment.apiUrl+"SolicitationState/ItHasNumberFile/"+id);
  }
}
