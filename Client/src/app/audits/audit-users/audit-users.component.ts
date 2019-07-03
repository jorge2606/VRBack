import { Component, OnInit, Input } from '@angular/core';
import { UsersAuditService } from '../../_services/audits/users-audit.service';
import { UserAudit } from '../../_models/user-audit';
import { Title } from '@angular/platform-browser';

@Component({
  selector: 'app-audit-users',
  templateUrl: './audit-users.component.html',
  styleUrls: ['./audit-users.component.css']
})
export class AuditUsersComponent implements OnInit {

  @Input() userId : number;
  userAudit : UserAudit;

  constructor(
              private userAuditService : UsersAuditService,
              private titleService : Title) { }

  ngOnInit() {
    this.titleService.setTitle('Modificar Usuario - Auditoría');
    this.userAuditService.fetchModifiedOfUser(this.userId).subscribe(
      x => this.userAudit = x,
      error => console.log(error),
    );
  }

}
