import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { environment } from 'src/environments/environment';
import { NgxSpinnerService } from 'ngx-spinner';
import { ActivatedRoute } from '@angular/router';
import { DomSanitizer } from '@angular/platform-browser';
import { ReportsService } from 'src/app/_services/reports.service';
import { GuidClass } from 'src/app/_helpers/guid-class';

@Component({
  selector: 'app-report-solicitation-subsidy',
  templateUrl: './report-solicitation-subsidy.component.html',
  styleUrls: ['./report-solicitation-subsidy.component.css']
})
export class ReportSolicitationSubsidyComponent implements OnInit {

  file: any  = this.domSanitazer.bypassSecurityTrustResourceUrl("/assets/defaultPdf.pdf");
  constructor(
        private spinner: NgxSpinnerService,
        private route : ActivatedRoute,
        private domSanitazer : DomSanitizer,
        private httpClient : HttpClient
    ) { }

  ngOnInit() {
    this.route.params.subscribe(
      url => {
        this.spinner.show();
        this.httpClient.get<any>(environment.apiUrl+"Report/Report_SolicitationByUser/"+url.userId)
        .subscribe(
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
