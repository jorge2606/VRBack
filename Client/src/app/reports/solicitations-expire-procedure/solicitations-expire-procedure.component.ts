import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { ReportsService } from 'src/app/_services/reports.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { DomSanitizer } from '@angular/platform-browser';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-solicitations-expire-procedure',
  templateUrl: './solicitations-expire-procedure.component.html',
  styleUrls: ['./solicitations-expire-procedure.component.css']
})
export class SolicitationsExpireComponent implements OnInit {
  file: any  = this.domSanitazer.bypassSecurityTrustResourceUrl("/assets/defaultPdf.pdf");

  constructor(
    private reportService : ReportsService,
    private spinner: NgxSpinnerService,
    private domSanitazer : DomSanitizer,
    private httpClient : HttpClient
  ) { }

  ngOnInit() {
    this.spinner.show();
    this.httpClient.get<any>(environment.apiUrl+"Report/SolicitationsExpireProcedure")
    .subscribe(
    x =>{
      this.file = this.domSanitazer.bypassSecurityTrustResourceUrl("data:application/pdf;base64,"+x.response);
      this.spinner.hide();
    },err => {
      this.spinner.hide();
    }
  );
  }

}
