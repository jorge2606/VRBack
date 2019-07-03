import { ClaimsService } from 'src/app/_services/claims.service';
import { AuthenticationService } from 'src/app/_services/authentication.service';
import { ToastrService } from 'ngx-toastr';
import { CityBaseDto, AllCitiesDto } from './../../_models/city';
import { ProvinceBaseDto } from 'src/app/_models/province';
import { CountryBaseDto, AllCountryDto } from './../../_models/country';
import { CityService } from './../../_services/city.service';
import { ProvinceService } from './../../_services/province.service';
import { CountryService } from './../../_services/country.service';
import { Component, OnInit, ViewChild, ElementRef, Renderer2 } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { DateDto } from 'src/app/_models/holiday';
import { ReportsService } from 'src/app/_services/reports.service';
import { HttpParams, HttpRequest, HttpClient, HttpHeaders } from '@angular/common/http';
import { DomSanitizer } from '@angular/platform-browser';
import { environment } from 'src/environments/environment';
import { GuidClass } from 'src/app/_helpers/guid-class';
import { Router } from '@angular/router';

@Component({
  selector: 'app-reports',
  templateUrl: './reports.component.html',
  styleUrls: ['./reports.component.css']
})
export class ReportsComponent implements OnInit {

  model = {startDate : new DateDto(), endDate : new DateDto(), provinceId : null, countryId : null, cityId : null};
  countries : CountryBaseDto[];
  provinces : ProvinceBaseDto[];
  cities : CityBaseDto[];
  countriesMock: AllCountryDto[] = [];
  citiesMock: AllCitiesDto[] = [];
  selectedCountry: number;
  selectedProvince: number;
  selectedCity: number;
  file: any = this.domSanitazer.bypassSecurityTrustResourceUrl("/assets/defaultPdf.pdf");
  emptyGuid = GuidClass.empty;

  @ViewChild('provinceId',{read: ElementRef}) private provinceId : ElementRef;
  @ViewChild('cityId',{read: ElementRef}) private cityId : ElementRef;
  canViewPendingSolicitations: any;
  roles: any;
  canViewSolicitationsExpire: any;
  canViewExpendituresReport: any;
  canViewReportByOrganism: any;
  canViewReportByUsers: any;

  constructor(
    private countryService : CountryService,
    private provinceService : ProvinceService,
    private cityService : CityService,
    private spinner : NgxSpinnerService,
    private toastrService : ToastrService,
    private reportService : ReportsService,
    private domSanitazer : DomSanitizer,
    public renderer2Service : Renderer2,
    private authService : AuthenticationService,
    private httpClient : HttpClient,
    private claimService : ClaimsService,
    private router : Router
  ) { }

  ngOnInit() {
    if(!this.claimService.haveClaim(this.claimService.canViewPendingSolicitations) 
      || !this.claimService.haveClaim(this.claimService.canViewSolicitationsExpire)
      || !this.claimService.haveClaim(this.claimService.canViewExpendituresReport)
      || !this.claimService.haveClaim(this.claimService.canViewReportByOrganism)
      || !this.claimService.haveClaim(this.claimService.canViewReportByUsers)){
      this.router.navigate(['/notAuthorized']);
    }else{
      this.selectedCountry = this.model.countryId;
      this.selectedProvince = this.model.provinceId;
      this.selectedCity = this.model.cityId;
      this.roles = this.authService.userId('roles');
      this.canViewPendingSolicitations = this.roles.find(x => x.value == this.claimService.canViewPendingSolicitations);
      this.canViewSolicitationsExpire = this.roles.find(x => x.value == this.claimService.canViewSolicitationsExpire);
      this.canViewExpendituresReport = this.roles.find(x => x.value == this.claimService.canViewExpendituresReport);
      this.canViewReportByOrganism = this.roles.find(x => x.value == this.claimService.canViewReportByOrganism);
      this.canViewReportByUsers = this.roles.find(x => x.value == this.claimService.canViewReportByUsers);
  
      this.getAllCountries();
    }

  }

  onSubmit(){
    this.model.cityId = (this.model.cityId == null) ? GuidClass.empty : this.model.cityId;
    this.model.countryId = (this.model.countryId == null) ? GuidClass.empty : this.model.countryId;
    this.model.provinceId = (this.model.provinceId == null) ? GuidClass.empty : this.model.provinceId;

    this.selectedCountry = this.model.countryId;
    this.selectedProvince = this.model.provinceId;
    this.selectedCity = this.model.cityId;
    
    this.spinner.show();
    this.reportService.reportSolicitationByDestiniesAndDates(this.model)
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

  getAllCountries(){
    this.countryService.getAllCountries()
    .subscribe(x => {
      this.spinner.show();
      this.countries = x;
      this.countriesMock = x;
      this.spinner.hide();
    });
  }

  provinceThisCountry(countryId : number){
    if (!countryId){
      return;
    }
    this.spinner.show();
    this.provinceService.findByCountryId(countryId)
    .subscribe(
        x=> {
          this.provinces = x;
          this.spinner.hide();
          if (this.provinces.length == 0){
            this.model.provinceId = this.selectedProvince;
            this.renderer2Service.setAttribute(this.provinceId.nativeElement,"disabled","true");
            this.model.cityId = this.selectedCity;
            this.renderer2Service.setAttribute(this.cityId.nativeElement,"disabled","true");
          }else{
            this.renderer2Service.setAttribute(this.provinceId.nativeElement,"disabled","false");
            this.renderer2Service.setAttribute(this.cityId.nativeElement,"disabled","false");
          }
         },
         err => {
              this.spinner.hide();
         }
    );
  }

  citiesProvince(provinceId: number) {
    this.spinner.show();
    this.cityService.GetByIdCity(provinceId).subscribe(
      x => {
        this.cities = x;
        if (this.provinces.length != 1) {
          this.model.cityId = this.selectedCity;
        } else {
          this.model.cityId = this.cities[0].id;
        }
        this.spinner.hide();
      }
    );
  }

  clearStartDate(){
    this.model.startDate = {day : undefined , month : undefined, year : undefined};
  }

  clearEndDate(){
    this.model.endDate = {day : undefined , month : undefined, year : undefined};
  }
}
