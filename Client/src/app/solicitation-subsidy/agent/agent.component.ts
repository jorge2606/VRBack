import { DatePipe } from '@angular/common';
import { Title } from '@angular/platform-browser';
import { FileNumberComponent } from './../../modals/file-number/file-number.component';
import { Component, OnInit } from '@angular/core';
import { SolicitationSubsidyService } from 'src/app/_services/solicitation-subsidy.service';
import { TransportService } from 'src/app/_services/transport.service';
import { NgbModal, NgbModalOptions } from '@ng-bootstrap/ng-bootstrap';
import { Router } from '@angular/router';
import { SolicitationSubsidyBaseDto, SolicitationIdDto, AllSolicitationSubsidyDto } from 'src/app/_models/solicitationSubsidy';
import { NgbdModalContent } from 'src/app/modals/modals.component';
import { SolicitationSubsidydetailComponent } from '../detail/solicitation-subsidydetail.component';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { AuthenticationService } from 'src/app/_services/authentication.service';
import { CarIsBeingUsedByOtherSolicitation } from 'src/app/_models/transport';
import { errorDto } from 'src/app/_models/error';
import { ClaimsService } from 'src/app/_services/claims.service';
import { SolicitationStatesService } from 'src/app/_services/solicitation-states.service';

@Component({
  selector: 'app-agent',
  templateUrl: './agent.component.html',
  styleUrls: ['./agent.component.css']
})
export class AgentComponent implements OnInit {

    filters = {
      page : 0,
      firstName : "",
      lastName : "",
      dni : 0
    }

    //paginator
    col_size : number;
    page = 0;
    itemsPerPage : number = 10;
    //
    solicitationSubsidies : AllSolicitationSubsidyDto[];
    error = '';
    transports : any;
    sizeIcon = "fa-lg";
    isRefund = {'isRefund' : true , 'isNotRefund' : false}
    today : string = this.pipeService.transform(new Date(),'yyyy/MM/dd');
    permissions : any[] = [];
    solicitationCreate: any;
    solicitationModerate: any;
    moderateRefund: any;
    createRefund: any;
    textListEmpty : string = "No se encontró ningúna solicitud";
    classListEmpty : string = "alert-primary";
    _acceptMySolicitation: any;
    ngbModalOptions: NgbModalOptions = {
      backdrop : 'static',
      keyboard : false
    };

  constructor(
            private solicitationSubsidyservice : SolicitationSubsidyService,
            private transportService : TransportService,
            private routerService : Router,
            private modalService: NgbModal,
            private spinner: NgxSpinnerService,
            private titleService : Title,
            private toastrService : ToastrService,
            private pipeService : DatePipe,
            private authService : AuthenticationService,
            private claimService : ClaimsService,
            private solicitationStateservice : SolicitationStatesService
            ) { 
              this.titleService.setTitle('Mis Solicitudes de Viático');
            }

  ngOnInit() {
    this.spinner.show();
    this.getAll(this.filters);
    this.getAllTransport();  
  }

  loadPage(page : any) {
    if (page > 0) {
      this.filters.page = page;
      this.getAll(this.filters);
    }
  }
  
  getAll(filters : any){
    this.solicitationSubsidyservice.getAllSolicitationSubsidiesAgent(filters).subscribe(
      x => {
          this.solicitationSubsidies = x.list;
          this.col_size = x.totalRecords;
          this.permissions = this.authService.userId('roles');
          this.solicitationCreate = this.permissions.find(claim => claim.value == 'solicitations.create');
          this.solicitationModerate = this.permissions.find(claim => claim.value == 'solicitations.moderateSolicitations');
          this.moderateRefund = this.permissions.find(claim => claim.value == 'solicitations.moderateRefunds');
          this.createRefund = this.permissions.find(claim => claim.value == 'solicitations.createRefund');
          this._acceptMySolicitation = this.permissions.find(claim => claim.value == 'solicitations.acceptMySolicitation');
        },err => {
          this.spinner.hide();
        }
    );
  }

  getAllTransport(){
    this.transportService.getAll().subscribe(
      x => {this.transports = x.response; this.spinner.hide();}
      ,err => {
        this.spinner.hide();
      }
    );
  }
  
  filter(){
    this.getAll(this.filters);
  }

  acceptMySolicitation(solicitation : SolicitationIdDto){
    this.solicitationSubsidyservice.aceptedMySolicitation(solicitation)
    .subscribe(() => this.getAll(this.filters)
    ,error => console.error(error));
  }

  aceptedMyAccountForSolicitation(solicitation : SolicitationIdDto){
    this.solicitationSubsidyservice.aceptedMyAccountForSolicitation(solicitation)
    .subscribe(() => this.getAll(this.filters)
    ,error => console.error(error));
  }
  //MODALS
  openEliminar(solicitud : SolicitationSubsidyBaseDto) {
    const modalRef = this.modalService.open(NgbdModalContent,{
      backdrop : 'static',
      keyboard : false
    });
    modalRef.componentInstance.Encabezado = "Eliminar";
    modalRef.componentInstance.Contenido = "¿Desea eliminar la solicitud : " + solicitud.motive + "?";
    modalRef.componentInstance.GuardaroEliminar = "Eliminar";
    modalRef.componentInstance.GuardaroEliminarClass = "btn-danger";
    modalRef.componentInstance.MsgClose = "Cancelar";
    modalRef.result.then(() => {
      this.solicitationSubsidyservice.delete(solicitud.id).subscribe(
        data => {
          this.toastrService.success("La solicitud de viático se ha eliminado correctamente.",'',
          {positionClass : 'toast-top-center', timeOut : 3000});
          this.getAll(this.filters);
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

    openDetail(id : number){
      const modalRef = this.modalService.open(SolicitationSubsidydetailComponent, {
        backdrop : 'static',
        keyboard : false,
        size : "lg"}
      );
      modalRef.componentInstance.idModal = id;
      modalRef.result.then(() => {
        this.getAll(this.filters);
      },
        () => {
          console.log('Backdrop click');
      })
    }

    openMotiveReject(motiveReject : string){
      const modalRef = this.modalService.open(NgbdModalContent, {
        backdrop : 'static',
        keyboard : false,
        size : "lg"
      });
      modalRef.componentInstance.Contenido = motiveReject;
      modalRef.componentInstance.Encabezado = "Motivo de Rechazo";
      modalRef.componentInstance.MsgClose = "Cerrar";
      modalRef.componentInstance.GuardaroEliminarHidden = true;
      modalRef.componentInstance.MsgCloseClass = "btn-primary";
      modalRef.result.then(() => {
        this.getAll(this.filters);
      },
        () => {
          console.log('Backdrop click');
      })
    }

    sendToSupervisor(sol : SolicitationSubsidyBaseDto){
      let newSolicitation = new SolicitationIdDto();
      newSolicitation.id = sol.id;
      this.spinner.show();
      this.solicitationSubsidyservice.sendSolicitationByEmail(newSolicitation)
      .subscribe(
        x => {
            this.spinner.hide();
            this.toastrService.success("La solicitud de viático se ha enviado correctamente.",'',
            {positionClass : 'toast-top-center', timeOut : 3000});
            this.getAll(this.filters);
          }
        ,
        e =>{
          this.spinner.hide();
          e = e.error.errors.error || [];
          e.forEach(err => {
            this.toastrService.error(err,'',
            {positionClass : 'toast-top-center', timeOut : 3000});
          });
          
          this.getAll(this.filters);
        }
      );
    }

    sendAccountForSolicitationToSupervisor(id : number){
      let newSolicitation = new SolicitationIdDto();
      newSolicitation.id = id;
      this.spinner.show();
        this.solicitationSubsidyservice.validateBeforeSendAccountFor(id)
        .subscribe(result =>{
          if (result.notifications.length == 0){
            this.solicitationSubsidyservice.sendAccountForToSupervisor(newSolicitation)
            .subscribe(
              x => {
                  this.spinner.hide();
                  this.toastrService.success("La rendición de una solicitud de viático se ha enviado correctamente.",'',
                  {positionClass : 'toast-top-center', timeOut : 3000});
                  this.getAll(this.filters);
                }
              ,
              e =>{
                this.spinner.hide();
                e = e.error.errors.error || [];
                e.forEach(err => {
                  this.toastrService.error(err,'',
                  {positionClass : 'toast-top-center', timeOut : 3000});
                });
                this.getAll(this.filters);
              }
            );
          }else{
            this.spinner.hide();
            result.notifications.forEach(notification => {
              this.toastrService.info(notification.value,'',{timeOut : 5000});
            });
          }
        });

    } 


    sendAccountForSolicitationFinallizeNormalyToSupervisor(id : number){
      let newSolicitation = new SolicitationIdDto();
      newSolicitation.id = id;
      this.spinner.show();
        this.solicitationSubsidyservice.validateBeforeSendAccountForFinalizeNormally(id)
        .subscribe(result =>{
          if (result.notifications.length == 0){
            this.solicitationSubsidyservice.sendAccountForToSupervisor(newSolicitation)
            .subscribe(
              x => {
                  this.spinner.hide();
                  this.toastrService.success("La rendición de una solicitud de viático se ha enviado correctamente.",'',
                  {positionClass : 'toast-top-center', timeOut : 3000});
                  this.getAll(this.filters);
                }
              ,
              e =>{
                this.spinner.hide();
                e = e.error.errors.error || [];
                e.forEach(err => {
                  this.toastrService.error(err,'',
                  {positionClass : 'toast-top-center', timeOut : 3000});
                });
                this.getAll(this.filters);
              }
            );
          }else{
            this.spinner.hide();
            result.notifications.forEach(notification => {
              this.toastrService.info(notification.value,'',{timeOut : 5000});
            });
          }
        });
    }

    finalizeSubsidy(solicitation : any){
      const modal = this.modalService.open(NgbdModalContent, {
        backdrop : 'static', keyboard : false, size : "lg", centered : true});
      modal.componentInstance.Contenido = '¿Desea finalizar esta solicitud de viático?';
      modal.componentInstance.Encabezado = 'finalizar viático';
      modal.componentInstance.GuardaroEliminar = 'Finalizar';
      modal.componentInstance.MsgClose = 'Cerrar';
      modal.componentInstance.GuardaroEliminarClass = 'btn-success';
      modal.componentInstance.MsgCloseClass = 'btn-default';
      modal.result.then(() => {
        this.solicitationSubsidyservice.finalizeSubsidy(solicitation.id)
        .subscribe(x =>{
          this.getAll(this.filters);
          this.toastrService.success('El viático ha finalizado, debe presentar su rendición de gastos.'
          ,'',{positionClass: 'toast-top-center', timeOut : 2000});
        });
        
      },
        () => {
          console.log('Backdrop click');
      })
    }
}
