import { ToastrService } from 'ngx-toastr';
import { AspNetUsersRolesService } from './../../_services/asp-net-users-roles.service';
import { Component, OnInit, Input } from '@angular/core';
import { User } from 'src/app/_models/user';
import { NgbActiveModal, NgbModalOptions } from '@ng-bootstrap/ng-bootstrap';
import { UserRoles } from 'src/app/_models/userRoles';
import { SupervisorUserAgentService } from 'src/app/_services/supervisor-user-agent.service';
import { SupervisorUserAgentBaseDto } from 'src/app/_models/supervisorUserAgent';

@Component({
  selector: 'app-add-supervisor',
  templateUrl: './add-supervisor.component.html',
  styleUrls: ['./add-supervisor.component.css']
})
export class AddSupervisorComponent implements OnInit {

  @Input() usersSelected : User[];
  @Input() allUsers : User[];
  supervisors : User[] = [];
  supervisorSelected : User[] = [];
  supervisorAndAgents : SupervisorUserAgentBaseDto[] = [];
  msj = '';
  ngbModalOptions: NgbModalOptions = {
    backdrop : 'static',
    keyboard : false
  };

  constructor(
    public activeModal: NgbActiveModal,
    private aspNetRolesService : AspNetUsersRolesService,
    private supervisorUserAgentService : SupervisorUserAgentService,
    private toastService : ToastrService
    ) { }

  ngOnInit() {
    this.aspNetRolesService.getAllUsersRoles().subscribe(
      rolUser => {
        let userSupervisor : User;
        rolUser.forEach(
          user =>{
            userSupervisor = this.allUsers.find(x => x.id == user.userId);
            if (userSupervisor != undefined){
              this.supervisors.push(userSupervisor);
            } 
            
          }); 

      }
    );
    
  }

  checkSupervisor(supervisor : User){ 
    if (this.supervisorSelected.find(x => x.id == supervisor.id)){
      const indexDeleteAll = this.supervisorSelected.indexOf(supervisor, 0);
      if (indexDeleteAll > -1) {
        this.supervisorSelected.splice(indexDeleteAll, 1);
      }
    }else{
        this.supervisorSelected.push(supervisor);
    }
  }

  save(){
    if (this.supervisorSelected.length == 0){
      this.msj = 'No Selecciono ningún Supervisor';
      this.toastService.info(this.msj,'',{positionClass:'toast-top-center',timeOut: 2000})
      return;
    }

    this.usersSelected.forEach(
      Agent => {
          this.supervisorSelected.forEach(
            supervisor =>{
              let obj = new SupervisorUserAgentBaseDto()
              {
                obj.agentId = Agent.id,
                obj.supervisorId = supervisor.id
              };
              this.supervisorAndAgents.push(obj);
            }
          );
      });

    this.supervisorUserAgentService.create(this.supervisorAndAgents).subscribe(
      x => {
        this.msj = '';
        this.toastService.success('El supervisor fue asignado correctamente','',{positionClass:'toast-top-center',timeOut: 2000})
        this.activeModal.close();
      }
    );
  }

}
