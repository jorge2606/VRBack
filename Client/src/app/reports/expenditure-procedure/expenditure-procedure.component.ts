import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { ActivatedRoute } from '@angular/router';
import { DomSanitizer } from '@angular/platform-browser';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-expenditure-procedure',
  templateUrl: './expenditure-procedure.component.html',
  styleUrls: ['./expenditure-procedure.component.css']
})
export class ExpenditureProcedureComponent implements OnInit {
  file: any = this.domSanitazer.bypassSecurityTrustResourceUrl("/assets/defaultPdf.pdf");

  constructor(
    private spinner: NgxSpinnerService,
    private route : ActivatedRoute,
    private domSanitazer : DomSanitizer,
    private httpClient : HttpClient
  ) { }

  ngOnInit() {
    this.spinner.show();
    this.httpClient.get<any>(environment.apiUrl+"Report/ExpendituresReport")
    .subscribe(
    x =>{
      this.file = this.domSanitazer.bypassSecurityTrustResourceUrl("data:application/pdf;base64,"+x.data.stream);
      this.spinner.hide();
    },
    err => {
      this.spinner.hide();
    }
    );
  }

}
