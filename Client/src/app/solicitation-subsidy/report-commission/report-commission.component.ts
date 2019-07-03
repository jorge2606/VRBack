import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { DomSanitizer, Title } from '@angular/platform-browser';
import { NgxSpinnerService } from 'ngx-spinner';
import { ToastrService } from 'ngx-toastr';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-report-commission',
  templateUrl: './report-commission.component.html',
  styleUrls: ['./report-commission.component.css']
})
export class ReportCommissionComponent implements OnInit {

  file: any = this.domSanitazer.bypassSecurityTrustResourceUrl("/assets/defaultPdf.pdf");
  constructor(
    private route : ActivatedRoute,
    private router : Router,
    private domSanitazer : DomSanitizer,
    private spinner: NgxSpinnerService,
    private titleService : Title,
    private toastrService : ToastrService,
    private httpClient : HttpClient
  ) { }

  ngOnInit() {
    this.titleService.setTitle('Informe de Comisión');
    this.route.params.subscribe(
      url => {
        this.spinner.show();
        this.httpClient.get<any>(environment.apiUrl+"Report/reportCommission/"+url.id)
        .subscribe(
        x =>{
          this.file = this.domSanitazer.bypassSecurityTrustResourceUrl("data:application/pdf;base64,"+x);
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
