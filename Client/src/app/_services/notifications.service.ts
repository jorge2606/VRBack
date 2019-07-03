import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Notifications } from '../_models/notifications';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class NotificationsService {

  constructor(private http : HttpClient) { }

  getAllNotifications(){
    return this.http.get<Notifications[]>(environment.apiUrl+"Notification/GetSomeNotifications");
  }

  notificationRidden(id : Notifications){
    return this.http.put<Notifications>(environment.apiUrl+"Notification/NotificationRidden/",id);
  }

  getPaginator(page: number) {
    return this.http.get<any>(environment.apiUrl+'Notification/page/' + page);
  }

  delete(id : number){
    return this.http.delete<any>(environment.apiUrl+'Notification/Delete/'+id);
  }

}
