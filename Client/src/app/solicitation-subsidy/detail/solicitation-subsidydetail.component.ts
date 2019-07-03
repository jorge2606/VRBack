import { DestinyBaseDto } from './../../_models/destiny';
import { AuthenticationService } from './../../_services/authentication.service';
import { NotifyRejectComponent } from './../../modals/notify-reject/notify-reject.component';
import { SolicitationSubsidyService } from './../../_services/solicitation-subsidy.service';
import { SolicitationSubsidyDetail, SolicitationIdDto, ImageDto } from './../../_models/solicitationSubsidy';
import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgbActiveModal, NgbModal, NgbModalOptions } from '@ng-bootstrap/ng-bootstrap';
import { GenericsCommunicationsComponentsService } from 'src/app/_services/generics-communications-components.service';
import { CrystalLightbox } from 'ngx-crystal-gallery';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-solicitation-subsidydetail',
  templateUrl: './solicitation-subsidydetail.component.html',
  styleUrls: ['./solicitation-subsidydetail.component.css']
})
export class SolicitationSubsidydetailComponent implements OnInit {

  id : number;
  @Input() idModal : number;
  @Input() posponed : boolean;
  model = new SolicitationSubsidyDetail;
  firstName : string;
  lastName : string;
  prefixCuil : number;
  suffixCuil : number;
  dni : string;
  motive : string = "";
  supscription : any;
  currentUrl : string;
  sizeIcon="fa-lg";
  ngbModalOptions: NgbModalOptions = {
    backdrop : 'static',
    keyboard : false
  };

  constructor(
    private activatedRoute : ActivatedRoute,
    private router : Router,
    private solicitationSubsidyService : SolicitationSubsidyService,
    public activeModal: NgbActiveModal,
    private modalService: NgbModal,
    private genericsCommunicationsComponentsService : GenericsCommunicationsComponentsService,
    private authService : AuthenticationService,
    private lightbox : CrystalLightbox,
    private toastrService : ToastrService,
    private spinner: NgxSpinnerService
  ) { }

  ngOnInit() {
    this.model.expenditures = [];
    this.activatedRoute.params.subscribe(
      x => {
        this.id = x.id;
        if (!this.id){
          this.id = this.idModal;
        }
            if (this.id){
              this.solicitationSubsidyService.getByIdSolicitation(this.id)
              .subscribe(
                  solicitation => {
                    this.model = solicitation;
                    this.firstName = this.model.user.firstName;
                    this.lastName = this.model.user.lastName;
                    this.dni = this.model.user.dni;
                    this.currentUrl = this.router.url;
                  }
              );
            }
        }
    );
    
    this.getMotiveFromModal();
  }

  acepted(){
    this.spinner.show();
    let newSolicitationId = new SolicitationIdDto();
    newSolicitationId.id = this.id;
    newSolicitationId.fileNumber = "";
    newSolicitationId.destinies = this.model.destinies;
    this.solicitationSubsidyService.acepted(newSolicitationId)
    .subscribe(
      () => {this.spinner.hide(); this.activeModal.close()}
      ,err => {
        this.spinner.hide();
        if (err.error.result){
          var e = err.error.result.errors.Error || [];
          e.forEach((error,value) => {
            this.toastrService.error(error,'');
          });
        }
      }
    );
  }

  reject(){
    this.spinner.show();
    let newSolicitationId = new SolicitationIdDto();
    newSolicitationId.id = this.id;
    newSolicitationId.motiveReject = this.motive;
    this.solicitationSubsidyService.refused(newSolicitationId)
    .subscribe(
      () => {this.spinner.hide(); this.activeModal.close();},
      err=>{
        this.spinner.hide();
        if (err.error.errors){
          var e = err.error.errors.Error || [];
          e.forEach(error => {
            this.toastrService.error(error,'');
          });
        }
      }
    );
  }

  AddMotive(title : string){
    this.ngbModalOptions.size = 'lg';
    const modalRef = this.modalService.open(NotifyRejectComponent, this.ngbModalOptions);
    modalRef.componentInstance.title = title;
    modalRef.result.then(() => {
      this.reject();
    },
      () => {
        console.log('Backdrop click');
    })
  }

  getMotiveFromModal(){
    this.supscription = this.genericsCommunicationsComponentsService.getMessage().subscribe(
      message => this.motive = message.motive
      ,
      error => console.log(error)
    );
  }

  ngOnDestroy(){
    this.supscription.unsubscribe();
  }

  open(images : ImageDto[]){

    images.forEach(x =>{
      this.lightbox.open([{path : x}]);
    })
  }
  
  clear(des : DestinyBaseDto){
    des.startDate = {day : 0, month : 0, year : 0};
  }
}
