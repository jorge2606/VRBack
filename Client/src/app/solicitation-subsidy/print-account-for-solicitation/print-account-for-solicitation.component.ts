import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { SolicitationSubsidyDetail } from 'src/app/_models/solicitationSubsidy';
import { DestinyDto } from 'src/app/_models/destiny';
import { Subject } from 'rxjs';
import { ActivatedRoute, Router } from '@angular/router';
import { SolicitationSubsidyService } from 'src/app/_services/solicitation-subsidy.service';
import { DestinyService } from 'src/app/_services/destiny.service';
import { DomSanitizer, Title } from '@angular/platform-browser';
import { NgxSpinnerService } from 'ngx-spinner';
import { environment } from 'src/environments/environment';
import * as jspdf from 'jspdf';
import html2canvas from 'html2canvas';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-print-account-for-solicitation',
  templateUrl: './print-account-for-solicitation.component.html',
  styleUrls: ['./print-account-for-solicitation.component.css']
})
export class PrintAccountForSolicitationComponent implements OnInit {
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
  stringIframe : any = "";
  idUser : number;
  urlImage : string;
  destinieWithDaysInLetters : DestinyDto[] = [];
  previewImage : any;
  hideHtml : boolean = false;
  imgUrl : string;
  urlSupervisorId : string;
  urlSupervisorId2 : string;
  printObservable = new Subject<boolean>();
  categoryName : string;
  categoryAdvance : number;
  categoryDescription : string;
  distributionName : string;
  distributionDescription : string;
  totDest : number = 0;
  file: any = this.domSanitazer.bypassSecurityTrustResourceUrl("/assets/defaultPdf.pdf");


  constructor(
              private route : ActivatedRoute,
              private solicitationSubsidyService : SolicitationSubsidyService,
              private destinyService : DestinyService,
              private domSanitazer : DomSanitizer,
              private spinner: NgxSpinnerService,
              private titleService : Title,
              private router : Router,
              private toastrService : ToastrService,
              private httpClient : HttpClient
            ) { 
                this.titleService.setTitle('Imprimir Solcitud');
            }

  ngOnInit() {
    this.route.params.subscribe(
      url => {
        this.spinner.show();
        this.httpClient.get<any>(environment.apiUrl+"SolicitationSubsidy/reportAccountFor/"+url.id)
        .subscribe(
        x =>{
          this.file = this.domSanitazer.bypassSecurityTrustResourceUrl("data:application/pdf;base64,"+x.response);
          this.spinner.hide();
        },
        err=>{
          this.spinner.hide();
        }
      );
      }
    );
  }
}

