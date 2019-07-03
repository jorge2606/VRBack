import { AuthenticationService } from './../../_services/authentication.service';
import { Component, OnInit } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { ActivatedRoute } from '@angular/router';
import { DomSanitizer } from '@angular/platform-browser';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-report-solicitation-subsidy-by-organism',
  templateUrl: './report-solicitation-subsidy-by-organism.component.html',
  styleUrls: ['./report-solicitation-subsidy-by-organism.component.css']
})
export class ReportSolicitationSubsidyByOrganismComponent implements OnInit {

  file : any  = this.domSanitazer.bypassSecurityTrustResourceUrl("/assets/defaultPdf.pdf");
  constructor(
    private spinner: NgxSpinnerService,
    private route : ActivatedRoute,
    private domSanitazer : DomSanitizer,
    private httpClient : HttpClient,
    private authService : AuthenticationService
  ) { }

  ngOnInit() {
    this.spinner.show();
    this.route.params.subscribe(
      url => {
    
        this.httpClient.get<any>(environment.apiUrl+"Report/Report_SolicitationByOrganism/"+url.id
          ).subscribe(
          x =>{
            this.file = this.domSanitazer.bypassSecurityTrustResourceUrl("data:application/pdf;base64,"+x.response);
            this.spinner.hide();
          },err=>{
            this.spinner.hide();
          }
        );
      }
    );
  }

}
