import { AuthenticationService } from 'src/app/_services/authentication.service';
import { Component, OnInit } from '@angular/core';
import { SolicitationIdDto } from 'src/app/_models/solicitationSubsidy';
import { SolicitationSubsidyService } from 'src/app/_services/solicitation-subsidy.service';
import { Router, ActivatedRoute } from '@angular/router';
import { NotifyRejectComponent } from 'src/app/modals/notify-reject/notify-reject.component';
import { NgbModal, NgbModalOptions } from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';
import { CrystalLightbox } from 'ngx-crystal-gallery';
import { ClaimsService } from 'src/app/_services/claims.service';

@Component({
  selector: 'app-acept-or-refuse',
  templateUrl: './acept-or-refuse.component.html',
  styleUrls: ['./acept-or-refuse.component.css']
})
export class AceptOrRefuseComponent implements OnInit {

  model = new SolicitationIdDto();
  msjSuccess = '';
  msjError = '';
  firstName : string;
  lastName : string;
  prefixCuil : number;
  suffixCuil : number;
  dni : number;
  motive : string = "";
  currentUrl : string;
  aceptedSolicitation: any;
  permissions: any;
  moderateSolicitation: any;
  moderateRefund: any;
  ngbModalOptions: NgbModalOptions = {
    backdrop : 'static',
    keyboard : false
  };

    constructor(
        private solicitationSubsidyService : SolicitationSubsidyService,
        private Activatedrouter : ActivatedRoute,
        private modalService : NgbModal,
        public router : Router,
        public authService : AuthenticationService,
        public toastService : ToastrService,
        private lightbox : CrystalLightbox,
        private claimService : ClaimsService
    ) { }

  ngOnInit() {
      
    if(!this.claimService.haveClaim(this.claimService.canModerateRefund) && this.claimService.haveClaim(this.claimService.canModerateSolicitation)){
      this.router.navigate(['/notAuthorized']);
    }else{
      this.Activatedrouter.params.subscribe(
        x => {
              this.model.id = x.id;
                this.solicitationSubsidyService.getBySolicitationIdForEmail(x.id)
                .subscribe(
                    solicitation => {
                      this.model = solicitation;
                      this.firstName = this.model.user.firstName;
                      this.lastName = this.model.user.lastName;
                      this.prefixCuil = this.model.user.prefixCuil;
                      this.suffixCuil = this.model.user.suffixCuil;
                      this.dni = this.model.user.dni;
                      this.currentUrl = this.router.url;
                      if(this.model.isRefund){
                        this.model.expenditures.forEach(
                          exp=>{
                              exp.urlImage = this.authService.urlExpenditureRefundFile(exp.id,186,60);
                          }
                        );
                      }
                      }
                );
          }
      );
    }

  }

  acepted(){
    this.solicitationSubsidyService.acepted(this.model)
    .subscribe(
      x => {
          this.toastService.success('La solicitud fue aceptada.');
          this.router.navigate(['/']);
      }
      ,error => {
        if(error.error["result"].errors){
          var e = error.error["result"].errors.Error || [];
          e.forEach(element => {
              this.toastService.error(element,'');
          });
        }
      }
    );
  }

  reject(){
    this.solicitationSubsidyService.refused(this.model)
    .subscribe(
      x => {
        this.toastService.success('La solicitud fue rechazada.');
        this.router.navigate(['/']);
      }
      ,error => {
        if(error.error.errors){
          var e = error.error.errors.Error || [];
          e.forEach(element => {
              this.toastService.error(element,'');
          });
        }
      }
    );
  }

  AddMotive(opt : string, title : string){
    this.ngbModalOptions.size = 'lg';
    const modalRef = this.modalService.open(NotifyRejectComponent,this.ngbModalOptions);
    modalRef.componentInstance.title = title;
    modalRef.componentInstance.class = opt;
    modalRef.result.then(() => {
      this.reject();
    },
      () => {
        console.log('Backdrop click');
    })
  }

  dimiss(){
    this.modalService.dismissAll('Close click');
  }

}
