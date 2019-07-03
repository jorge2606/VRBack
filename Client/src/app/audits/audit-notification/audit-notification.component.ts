import { Component, OnInit, Input } from '@angular/core';
import { NotificationAuditService } from 'src/app/_services/audits/notification-audit.service';

@Component({
  selector: 'app-audit-notification',
  templateUrl: './audit-notification.component.html',
  styleUrls: ['./audit-notification.component.css']
})
export class AuditNotificationComponent implements OnInit {

  notifications : Notification[]=[];
  @Input() userId : number;

  constructor(
    private notificationAuditService : NotificationAuditService
  ) { }

  ngOnInit() {
    this.notificationAuditService.getNotifications(this.userId)
    .subscribe(x => {
      this.notifications = x;
    })
  }

}
