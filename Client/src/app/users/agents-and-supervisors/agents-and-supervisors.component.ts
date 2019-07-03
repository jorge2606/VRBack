import { ToastrService } from 'ngx-toastr';
import { Component, OnInit } from '@angular/core';
import { SupervisorUserAgentService } from 'src/app/_services/supervisor-user-agent.service';
import { AllSupervisorUserAgent } from 'src/app/_models/supervisorUserAgent';
import { Title } from '@angular/platform-browser';
import { NgbdModalContent } from 'src/app/modals/modals.component';
import { NgbModal, NgbModalOptions } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-agents-and-supervisors',
  templateUrl: './agents-and-supervisors.component.html',
  styleUrls: ['./agents-and-supervisors.component.css']
})
export class AgentsAndSupervisorsComponent implements OnInit {

  agentsSupervisors : AllSupervisorUserAgent[];
  agentSupervisor_list_length : number;
  filters = { lastName : "",firstName : "", page : 0 };
  col_size: number;
  itemsPerPage: number = 10;
  page = 0;
  ngbModalOptions: NgbModalOptions = {
    backdrop : 'static',
    keyboard : false
  };
  
  constructor(
          private agentsAndSupervisors : SupervisorUserAgentService,
          private titleService : Title,
          private modalService: NgbModal,
          private toastrService : ToastrService) { }

  ngOnInit() {
    this.titleService.setTitle('Agentes y Supervisores');
    this.getUserAgents(this.filters);

  }

  getUserAgents(filters : any){
    this.agentsAndSupervisors.getPaginator(filters)
    .subscribe(
      x =>{
          this.agentsSupervisors = x.list;
          this.agentSupervisor_list_length = this.agentsSupervisors.length;
          this.col_size = x.totalRecords;
      },
      err => {
        console.log(err);
      }
    );
  }

  findWhileWrite(){
    this.loadPage(this.filters.page);
  }

  openEliminar(supervisor : any, agent : any) {
    const modalRef = this.modalService.open(NgbdModalContent,this.ngbModalOptions);
    modalRef.componentInstance.Encabezado = "Eliminar";
    modalRef.componentInstance.Contenido = "¿Desea desasignar a " + agent.lastName+", "+agent.firstName + " de " + supervisor.lastName+", "+supervisor.firstName + "?";
    modalRef.componentInstance.GuardaroEliminar = "Eliminar";
    modalRef.componentInstance.MsgClose = "Cancelar";
    modalRef.componentInstance.GuardaroEliminarClass="btn-danger";
    modalRef.result.then(() => {
      this.agentsAndSupervisors.deleteRelationshipBetweenAgentAndSupervisor(supervisor.id,agent.id)
        .subscribe(
        data => {
          this.toastrService.success("El Agente '"+agent.lastName+", "
          +agent.firstName+"' se desasigno correctamente del supervisor "+supervisor.lastName+", "
          +supervisor.firstName,'', {positionClass : 'toast-top-center', timeOut : 3000});
          this.loadPage(this.page);
        },
        error => {
          console.log("error", error);
        }
      );
    },
      () => {
        console.log('Backdrop click');
      })
  }

  loadPage(page : number) {
    if (page > 0) {
      this.filters.page = page - 1;
    }
    this.getUserAgents(this.filters);
  }

}
