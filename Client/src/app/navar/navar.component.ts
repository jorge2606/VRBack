import { ClaimsService } from './../_services/claims.service';
import { SolicitationSubsidyBaseDto } from 'src/app/_models/solicitationSubsidy';
import { SolicitationSubsidyService } from './../_services/solicitation-subsidy.service';
import { SupervisorUserAgentService } from 'src/app/_services/supervisor-user-agent.service';
import { AuthenticationService } from './../_services/authentication.service';
import { Observable } from 'rxjs';
import { Notifications } from './../_models/notifications';
import { NotificationsService } from './../_services/notifications.service';
import { Component, OnInit, Input, Renderer } from '@angular/core';
import { MessBetweenCompService } from '../_services/mess-between-comp.service';
import { NgbdModalContent } from '../modals/modals.component';
import { NgbModal, NgbModalOptions } from '@ng-bootstrap/ng-bootstrap';
import { ListNotificationsComponent } from '../modals/list-notifications/list-notifications.component';
import { SolicitationSubsidydetailComponent } from '../solicitation-subsidy/detail/solicitation-subsidydetail.component';
import { Router } from '@angular/router';
import { GenericsCommunicationsComponentsService } from '../_services/generics-communications-components.service';
import { DetailAccountForSolicitationComponent } from '../solicitation-subsidy/detail-account-for-solicitation/detail-account-for-solicitation.component';
import { ToastrService } from 'ngx-toastr';
import { UserService } from '../_services/user.service';
import { DomSanitizer } from '@angular/platform-browser';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-navar',
  templateUrl: './navar.component.html',
  styleUrls: ['./navar.component.css']
})
export class NavarComponent implements OnInit {
  
  notification : Notifications[] = [];
  isLogged : Observable<boolean>;
  idUser : number;
  cantNotif : number = 0;
  @Input() urlImage : any;
  userName : string;
  firstName : string;
  lastName : string;
  isloggedUser : boolean;
  solicitationSubsidy : SolicitationSubsidyBaseDto;
  isRefund = {'yes' : true, 'not' : false};
  roles : any [] = [];
  rolesNames : any[] = [];
  showTabSolicitation : any;
  showTabUsers : any;
  showTabRoles : any;
  showTabCategories : any;
  showTabDistribution : any;
  showTabTransports : any;
  showTabExpenditures : any;
  showTabOrganism : any;
  showTabHolidays : any;
  createSolicitation: any;
  createRefund: any;
  moderateSolicitation: any;
  moderateRefund: any;
  moderateViewRefund: any;
  moderateViewSolicitation: any;

  canViewReport : any;
  canViewPendingSolicitations : any;
  canViewSolicitationsExpire : any;
  canViewExpendituresReport : any;
  canViewReportByOrganism : any;
  canViewReportByUsers : any;

  show : boolean = true;
  showTabAudits: any;
  currentYear : number;
  ngbModalOptions: NgbModalOptions = {
    backdrop : 'static',
    keyboard : false
  };
  canCreateLegalRuling: any;
  canEditLegalRuling: any;
  canDeleteLegalRuling: any;
  canViewLegalRuling: any;

  constructor(private notificationServices : NotificationsService, 
              private authService : AuthenticationService,
              private messaBetweenComp : MessBetweenCompService,
              private modalService: NgbModal,
              private supervisorUserAgentService : SupervisorUserAgentService,
              private comunicationService : GenericsCommunicationsComponentsService,
              private solicitationSubsidyService : SolicitationSubsidyService,
              private router : Router,
              private toastrService : ToastrService,
              private claimService : ClaimsService,
              private userService : UserService,
              private domanizeService : DomSanitizer) { }


  retriveNotifications(){
    this.notificationServices.getAllNotifications().subscribe(
      x => {
            this.notification = x;
            this.cantNotif = this.notification.length;
            this.idUser = this.authService.userId('id');
            this.userName = this.authService.userId('userName');
            this.firstName = this.authService.userId('firstName');
            this.lastName = this.authService.userId('lastName');
      },err=>{
        console.log(err);
      }
      );
   }

  ngOnInit() {
    this.isLogged = this.authService.isLoggedIn;
    this.idUser = this.authService.userId('id');
    this.isLogged.subscribe(x => {
      this.isloggedUser = x;
      //si el usuario esta logueado
      if(x){
        this.currentYear = new Date().getFullYear();
        this.roles = this.authService.userId('roles');
        this.rolesNames = this.authService.userId('rolesNames');
        this.showTabSolicitation = this.roles.find(
          claims => claims.value == this.claimService.canViewSolicitation 
          || claims.value == this.claimService.canCreateSolicitation
          || claims.value == this.claimService.canCreateRefund
          || claims.value == this.claimService.canModerateSolicitation
          || claims.value == this.claimService.canModerateRefund
          || claims.value == this.claimService.canViewrefund
          || claims.value == this.claimService.canViewSolicitation);

        this.createSolicitation = this.roles.find(x => x.value == this.claimService.canCreateSolicitation);
        this.createRefund = this.roles.find(x => x.value == this.claimService.canCreateRefund);
        this.moderateSolicitation = this.roles.find(x => x.value == this.claimService.canModerateSolicitation);
        this.moderateRefund = this.roles.find(x => x.value == this.claimService.canModerateRefund);
        this.moderateViewRefund = this.roles.find(x => x.value == this.claimService.canModerateRefund);
        this.moderateViewSolicitation = this.roles.find(x => x.value == this.claimService.canViewSolicitation);

        this.showTabUsers = this.roles.find(x => x.value == this.claimService.canViewUsers);
        this.showTabCategories = this.roles.find(x => x.value == this.claimService.canViewCategory);
        this.showTabDistribution = this.roles.find(x => x.value == this.claimService.canViewDistribution);
        this.showTabExpenditures = this.roles.find(x => x.value == this.claimService.canViewExpenditure);
        this.showTabOrganism = this.roles.find(x => x.value == this.claimService.canViewOrganism);
        this.showTabRoles = this.roles.find(x => x.value == this.claimService.canViewRoles);
        this.showTabHolidays = this.roles.find(x => x.value == this.claimService.canViewHoliday);
        this.showTabTransports = this.roles.find(x => x.value == this.claimService.canViewTransport);
        this.showTabAudits = this.roles.find(x => x.value == this.claimService.canAudits);
        this.canViewLegalRuling = this.roles.find(x => x.value == this.claimService.canViewLegalRuling);;

        this.canViewReport = this.roles.find(x => x.value == this.claimService.canViewReport);

        this.comunicationService.getMessage()
        .subscribe(() => {
            this.retriveNotifications();
        });
        //cuando actualiza su imagen
        this.messaBetweenComp.getMessage().subscribe( 
          () => {
            this.userService.getUserImage(this.idUser, 60,37)
            .subscribe(
              y => {
                this.urlImage = this.domanizeService.bypassSecurityTrustResourceUrl(y.response);
                console.log('getMessage')
              }
            );
          }
        );
        
        if (!this.urlImage){
          this.userService.getUserImage(this.idUser, 60,37)
          .subscribe(
            y => {
              this.urlImage = this.domanizeService.bypassSecurityTrustResourceUrl(y.response);
            }
          );
        }
        this.retriveNotifications();
      }
    })    
  }

  
  logout(){
    const modalRef = this.modalService.open(NgbdModalContent,{
      backdrop : 'static',
      keyboard : false
    });
    modalRef.componentInstance.Encabezado = "Cerrar Sesión";
    modalRef.componentInstance.Contenido = "¿Desea salir de la aplicación?";
    modalRef.componentInstance.GuardaroEliminar = "Salir";
    modalRef.componentInstance.MsgClose = "Cancelar";
    modalRef.componentInstance.GuardaroEliminarClass = "btn-primary";
    modalRef.componentInstance.MsgCloseClass = "btn-default";
    modalRef.result.then(() => {    
      this.authService.logout();
    },
    () => {
      console.log('Backdrop click');
    })
  }

  seeThisNotification(notificationridden : any) {
      this.modalService.dismissAll();
      this.supervisorUserAgentService.isAgent(notificationridden.creatorUserId)
      .subscribe(user => {
          if (user){//si es verdadero entonces este mensaje es de uno de mis agentes
            this.solicitationSubsidyService.getBySolicitationIdWhitState(notificationridden.solicitationSubsidyId)
            .subscribe(
              solicitation => {
                
                  if (!solicitation.finalizeDate){//si no es una rendición
                      this.notificationServices.notificationRidden(notificationridden).subscribe(
                        () =>{
                            this.retriveNotifications();
                            const modalRef = this.modalService.open(SolicitationSubsidydetailComponent, {
                              backdrop : 'static',
                              keyboard : false,
                              size : "lg"});
                            modalRef.componentInstance.idModal = notificationridden.solicitationSubsidyId;
                            
                            modalRef.result.then(() => { 
                              console.log(this.router.url);
                              this.router.navigate([this.router.url])
                            },
                            () => {
                                console.log('Backdrop click');
                            })
                          } 
                      );
                  }else{
                    this.notificationServices.notificationRidden(notificationridden).subscribe(
                      () =>{
                            
                            this.retriveNotifications();
                            const modalRef = this.modalService.open(DetailAccountForSolicitationComponent, {
                              backdrop : 'static',
                              keyboard : false,
                              size : "lg"});
                            modalRef.componentInstance.idModal = notificationridden.solicitationSubsidyId;
                            modalRef.result.then(() => { 
                              this.router.navigate([this.router.url])
                            },
                            () => {
                                console.log('Backdrop click');
                            });      
                        } 
                    );
                  }
              }
            );

          }else{
            this.solicitationSubsidyService.wichStateSolicitation(notificationridden.solicitationSubsidyId)
            .subscribe(solicitationState => {
              if (solicitationState.description == 'Rechazado'){
                //significa que es mi supervisor y la nootificación es para mi
                this.notificationServices.notificationRidden(notificationridden)
                .subscribe(
                  () =>{
                      this.retriveNotifications();
                      const modalRef = this.modalService.open(NgbdModalContent, {
                        backdrop : 'static',
                        keyboard : false,
                        size : "lg"
                      });
                      modalRef.componentInstance.Contenido = solicitationState.motiveReject;
                      modalRef.componentInstance.Encabezado = "Motivo de Rechazo";
                      modalRef.componentInstance.MsgClose = "Cerrar";
                      modalRef.componentInstance.GuardaroEliminarHidden = true;
                      modalRef.componentInstance.MsgCloseClass = "btn-primary";
                      this.router.navigate(['SolicitationSubsidy/agent']);
                      modalRef.result.then(() => {    },
                      () => {
                          console.log('Backdrop click');
                      })
                    } 
                )
              }
              if (solicitationState.description === 'Rendido' || 'Finalizado'){
                this.notificationServices.notificationRidden(notificationridden)
                .subscribe(
                  () =>{
                      this.retriveNotifications();
                  });
              }

              if (solicitationState.description === 'Rendición Rechazada'){
                this.retriveNotifications();
                this.router.navigateByUrl('SolicitationSubsidy/agent/accountFor/'+notificationridden.solicitationSubsidyId);
              }
              //si soy propietario de la solicitud
              if(solicitationState.description == 'Aceptado' && solicitationState.userId == this.authService.userId('id') ){
                this.notificationServices.notificationRidden(notificationridden).subscribe(
                  () =>{
                      this.retriveNotifications();
                      if (!solicitationState.isRefund){
                        this.router.navigateByUrl('SolicitationSubsidy/agent/print/'+notificationridden.solicitationSubsidyId);
                      }else{
                        this.router.navigateByUrl('SolicitationSubsidy/agent/printAccountFor/'+notificationridden.solicitationSubsidyId);
                      }
                      
                    } 
                )
              }else if (solicitationState.description == 'Aceptado' && solicitationState.userId != this.authService.userId('id'))
              {
                this.notificationServices.notificationRidden(notificationridden).subscribe(
                  () =>{
                      this.retriveNotifications();
                      if (!solicitationState.isRefund){
                        this.toastrService.info('Este Reintegro ya fue aprobado');  
                      }else{
                        this.toastrService.info('Esta solicitud ya fue aprobada');
                      }
                      
                    } 
                )
              }

              if(solicitationState.description == 'Rendición Aceptada' && solicitationState.userId == this.authService.userId('id')){
                this.notificationServices.notificationRidden(notificationridden).subscribe(
                  () =>{
                      this.retriveNotifications();
                      this.router.navigateByUrl('SolicitationSubsidy/agent/printAccountFor/'+notificationridden.solicitationSubsidyId);
                    } 
                )
              }else if (solicitationState.description == 'Rendición Aceptada' && solicitationState.userId != this.authService.userId('id'))
              {
                this.notificationServices.notificationRidden(notificationridden).subscribe(
                  () =>{
                      this.retriveNotifications();
                      this.toastrService.info('Esta rendición de solicitud ya fue aprobada');
                    } 
                )
              }

              if(solicitationState.description == 'Aprobado 1ra. Instancia' 
                  && this.authService.userId('id') != solicitationState.userId){
                this.notificationServices.notificationRidden(notificationridden).subscribe(
                  () =>{
                      this.retriveNotifications();
                      this.router.navigateByUrl('/SolicitationSubsidy/agent/supervisor/solicitationSubsidies/'+solicitationState.isRefund);
                    } 
                )
              }else if(solicitationState.description == 'Aprobado 1ra. Instancia'
                && this.authService.userId('id') == solicitationState.userId){
                this.notificationServices.notificationRidden(notificationridden).subscribe(
                  () =>{
                      this.retriveNotifications();
                      } 
                )
              }

              if(solicitationState.description == 'Rendición Aprobada 1ra. Instancia' 
                && this.authService.userId('id') == solicitationState.userId){
                this.notificationServices.notificationRidden(notificationridden).subscribe(
                () =>{
                    this.retriveNotifications();
                  } 
                )
              }else if (solicitationState.description == 'Rendición Aprobada 1ra. Instancia' 
                && this.authService.userId('id') != solicitationState.userId){
                  this.notificationServices.notificationRidden(notificationridden).subscribe(
                    () =>{
                      this.retriveNotifications();
                      this.router.navigateByUrl('/SolicitationSubsidy/agent/supervisor/solicitationSubsidies/'+solicitationState.isRefund)
                  }) 
              }
              
            });

          }
      });

  }

  seeAllNotification() {
    const modalRef = this.modalService.open(ListNotificationsComponent,this.ngbModalOptions);
    modalRef.componentInstance.Encabezado = "Todas las Notificaciones";
    modalRef.componentInstance.button = "Entendido";
    modalRef.componentInstance.ButtonHidden = false;
    modalRef.result.then(() => {
    },
      () => {
        console.log('Backdrop click');
    })
  }
  

  delete(id : number){
    const modalRef = this.modalService.open(NgbdModalContent,{
      backdrop : 'static',
      keyboard : false
    });
    modalRef.componentInstance.Encabezado = "Eliminar";
    modalRef.componentInstance.Contenido = "¿Desea Eliminar?";
    modalRef.componentInstance.GuardaroEliminar = "Eliminar";
    modalRef.componentInstance.MsgClose = "Cancelar";
    modalRef.componentInstance.MsgCloseClass = "btn-default";
    modalRef.componentInstance.GuardaroEliminarClass = "btn-danger";
    modalRef.result.then(() => {
          this.notificationServices.delete(id).subscribe(
            ()=>{
              
            }
            ,
            error => {
                console.log("error", error);
            }
          );
    })
  }

}
