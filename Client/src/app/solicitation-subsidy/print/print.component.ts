import { ToastrService } from 'ngx-toastr';
import { GuidClass } from './../../_helpers/guid-class';
import { ClaimsService } from './../../_services/claims.service';
import { Subject } from 'rxjs';
import { DestinyDto } from './../../_models/destiny';
import { DestinyService } from 'src/app/_services/destiny.service';
import { AuthenticationService } from 'src/app/_services/authentication.service';
import { Component, OnInit, SecurityContext } from '@angular/core';
import * as jspdf from 'jspdf';
import html2canvas from 'html2canvas';
import { ActivatedRoute, Router } from '@angular/router';
import { SolicitationSubsidyService } from 'src/app/_services/solicitation-subsidy.service';
import { SolicitationSubsidyDetail } from 'src/app/_models/solicitationSubsidy';
import { DomSanitizer, Title, SafeResourceUrl } from '@angular/platform-browser';
import { NgxSpinnerService } from 'ngx-spinner';
import { environment } from 'src/environments/environment';
import { SolicitationStatesService } from 'src/app/_services/solicitation-states.service';
import { ReportsService } from 'src/app/_services/reports.service';

@Component({
  selector: 'app-print',
  templateUrl: './print.component.html',
  styleUrls: ['./print.component.css']
})
export class PrintComponent implements OnInit {
  id : number;
  model = new SolicitationSubsidyDetail;
  firstName : string;
  lastName : string;
  prefixCuil : number;
  suffixCuil : number;
  dni : string;
  motive : string = "";
  today = new Date();
  totalExpenditures = 0.0;
  stringIframe : SafeResourceUrl = "";
  idUser : number;
  urlImage : string;
  destinieWithDaysInLetters : DestinyDto[] = [];
  previewImage : any;
  hideHtml : boolean = false;
  imgUrl : string;
  urlSupervisorId : string;
  urlSupervisorId2 : string;
  nameSupervisor1 : string;
  surnameSupervisor1 : string;
  nameSupervisor2 : string;
  surnameSupervisor2 : string;
  printObservable = new Subject<boolean>();
  categoryName : string;
  categoryDescription : string;
  distributionName : string;
  distributionDescription : string;
  totDest : number = 0;
  file: any= this.domSanitazer.bypassSecurityTrustResourceUrl("/assets/defaultPdf.pdf");


  constructor(
              private route : ActivatedRoute,
              private solicitationSubsidyService : SolicitationSubsidyService,
              private destinyService : DestinyService,
              private domSanitazer : DomSanitizer,
              private spinner: NgxSpinnerService,
              private titleService : Title,
              private router : Router,
              private solicitationStateService : SolicitationStatesService,
              private toastrService : ToastrService,
              private reportService : ReportsService
            ) { 
                this.titleService.setTitle('Imprimir Solcitud');
            }

  ngOnInit() {
    this.spinner.show();
    this.route.params.subscribe(
      url => {
        this.spinner.show();
        this.reportService.solicitationWasApproved(url.id)
        .subscribe(
          x =>{
            this.file = this.domSanitazer.bypassSecurityTrustResourceUrl("data:application/pdf;base64,"+x.response);
            this.spinner.hide();
          },
          err=>{
            this.toastrService.error('No se pudo cargar el reporte.');
            this.spinner.hide();
          }
        );
      }
    );
  }

}
