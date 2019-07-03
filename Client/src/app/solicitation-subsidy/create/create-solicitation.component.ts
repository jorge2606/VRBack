import { PlaceService } from 'src/app/_services/place.service';
import { destinyForModifyingSolicitationDto } from './../../_models/destiny';
import { AuthenticationService } from './../../_services/authentication.service';
import { HolidaysService } from './../../_services/holidays.service';
import { FormGroup } from '@angular/forms';
import { Title } from '@angular/platform-browser';
import { SolicitationSubsidyService } from './../../_services/solicitation-subsidy.service';
import { CodeLiquidationService } from './../../_services/code-liquidation.service';
import { CountryService } from './../../_services/country.service';
import { AllCountryDto } from './../../_models/country';
import { CityBaseDto } from './../../_models/city';
import { AddDestinyComponent } from './../../modals/add-destiny/add-destiny.component';
import { AddNewExpenditureComponent } from './../../modals/add-new-expenditure/add-new-expenditure.component';
import { AllExpenditureDto } from '../../_models/expenditureType';
import { ExpenditureService } from '../../_services/expenditure.service';
import { CityService } from './../../_services/city.service';
import { ProvinceBaseDto } from './../../_models/province';
import { AllCategoryDto } from './../../_models/category';
import { CategoryService } from 'src/app/_services/category.service';
import { CreateSolicitationSubsidyDto, Expenditure, ExpenditureForModifyingDto } from './../../_models/solicitationSubsidy';
import { Component, OnInit, ViewChild } from '@angular/core';
import { TransportService } from 'src/app/_services/transport.service';
import { AllTransportDto } from 'src/app/_models/transport';
import { ProvinceService } from 'src/app/_services/province.service';
import { NgbModal, NgbCalendar, NgbDate, NgbModalOptions } from '@ng-bootstrap/ng-bootstrap';
import { Subscription } from 'rxjs';
import { DestinyService } from 'src/app/_services/destiny.service';
import { codeLiquidationBaseDto } from 'src/app/_models/codeLiquidation';
import { ActivatedRoute, Router } from '@angular/router';
import { ExpendituresUserService } from 'src/app/_services/expenditures-user.service';
import { ToastrService } from 'ngx-toastr';
import { ClaimsService } from 'src/app/_services/claims.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { GuidClass } from 'src/app/_helpers/guid-class';

@Component({
  selector: 'app-create-solicitation',
  templateUrl: './create-solicitation.component.html',
  styleUrls: ['./create-solicitation.component.css']
})
export class CreateSolicitationComponent implements OnInit {

  isCollapsedDestiny = false;
  categories : AllCategoryDto[] = [];
  transports : AllTransportDto[] = [];
  isCollapsedExpenditure = false;
  ConceptExpenditureList : ExpenditureForModifyingDto[]=[];
  subscriptionExpenditure: Subscription;
  subscriptionDestiny : Subscription;
  _disabled = false;
  expenditures : AllExpenditureDto[];
  Allexpenditures : AllExpenditureDto[];
  destinies : destinyForModifyingSolicitationDto[] = [];
  model = new CreateSolicitationSubsidyDto;
  radioButtonRequired : boolean = true;
  provinces : ProvinceBaseDto[];
  cities : CityBaseDto[];
  verOcultarIconDestiny = "arrow-circle-up";
  verOcultarTextDestiny = "Ocultar";
  verOcultarIconExpenditure = "arrow-circle-up";
  verOcultarTextExpenditure = "Ocultar";
  countries : AllCountryDto[] = [];
  codeLiquidations : codeLiquidationBaseDto[] = [];
  id : number;
  citiesModify : CityBaseDto[] = [];
  msj = '';
  msjExito = '';
  msjWhenUserTryDeleteLastDestiny = '';
  supplementariesCities : string[];
  dirtyForm : boolean = false;
  submited : boolean = false;
  inputCode : boolean;
  whenIsCommission : boolean;
  addDestinyButtonWhenIsCommission : boolean;
  deleteAllConceptsWhenIsCommission : boolean;
  deleteConceptsWhenIsCommission : boolean;
  currentUserId : number;
  @ViewChild('solicitationSubsidy') solicitationForm : FormGroup;

  ngbModalOptions: NgbModalOptions = {
    backdrop : 'static',
    keyboard : false
  };

  constructor(
      private route : ActivatedRoute,
      private router : Router,
      private expenditureService : ExpenditureService,
      private modalService: NgbModal,
      private destinyService : DestinyService,
      private provinceService : ProvinceService,
      private cityService : CityService,
      private categoryService : CategoryService,
      private transportService : TransportService,
      private countryService : CountryService,
      private codeLiquidationService : CodeLiquidationService,
      private solicitationSubsidyService : SolicitationSubsidyService,
      private expenditureAgentService : ExpendituresUserService,
      private titleService : Title,
      private toastrService: ToastrService,
      private ngbCalendar : NgbCalendar,
      public authService : AuthenticationService,
      private claimService : ClaimsService,
      private spinner: NgxSpinnerService,
      private placeService : PlaceService
      ) { }

  ngOnInit() {
    this.spinner.show();
    this.titleService.setTitle('Crear Solicitud');

    if(!this.claimService.haveClaim(this.claimService.canCreateSolicitation)){
      this.router.navigate(['/notAuthorized']);
    }else{
      this.currentUserId = this.authService.userId('id');
      this.route.params.subscribe(
        x =>{
          this.id = x.id
      });
      
      this.model.destinies = [];
      this.model.expenditures = [];
      this.allexpenditures();
      this.allExpenditureFromModal();
      this.allDestinyFromModal();
      this.allCategories();
      this.allTransport();
      this.allCountries();
      this.allCodeLiquidation();
      this.totalResultExpenditure();
      
      if (this.id){
          this.titleService.setTitle('Modificar Solicitud');
          this.getSolicitation(this.id,"");
      }
    }
    this.spinner.hide();
  }


  getSolicitation(id : number, randomKey : string){
      if(id){
        this.solicitationSubsidyService.getByIdSolicitationNotAccountFor(this.id)
        .subscribe(
          x => {
                this.model = x;
                if (this.model.destinies != null){
                  for (let index = 0; index < this.model.destinies.length; index++) {
                    if (this.model.destinies[index].provinceId != null){
                      this.citiesThisProvinceModify(this.model.destinies[index].provinceId);
                    }
                  }                 
                }
                
                this.allProvice();
                this.totalResultExpenditure();
          }
        );
      }else{
        if(randomKey.length == 6){
          this.solicitationSubsidyService.getByRandomKey(randomKey)
          .subscribe(
            x => {
              if (x){
                this.model = x;

                if (this.model.destinies != null){
                  for (let index = 0; index < this.model.destinies.length; index++) {
                    if (this.model.destinies[index].provinceId != null){
                      this.citiesThisProvinceModify(this.model.destinies[index].provinceId);
                    }
                  }                 
                }
                
                this.allProvice();
                this.totalResultExpenditure();
              }else{
                this.toastrService.info('El Código no existe.','',{timeOut : 2000});
              }

            },
            err => {
              if (err.error.errors){
                var e = err.error.errors.Error || [];
                e.forEach(Errors => {
                    this.toastrService.error(Errors,'',{timeOut : 2000});
                });
              }
              
            }
          );
        }else{
          this.toastrService.warning('El Código debe contener 6 dígitos.');
        }
      }
  }

  allTransport(){
    this.transportService.getAll().subscribe(
      x => this.transports = x
    );
  }

  allCodeLiquidation(){
    this.codeLiquidationService.getAll()
    .subscribe(
      x => this.codeLiquidations = x
    );
  }

  allCountries(){
    this.countryService.getAllCountries()
    .subscribe(
      x => this.countries = x
    );
  }
  allCategories(){
    this.categoryService.getallCategories()
    .subscribe(
      x=> this.categories = x
    );
  }  
  allProvice(){
    this.provinceService.getAll()
    .subscribe(
      x=> this.provinces = x
    );
  }

  allDestinyFromModal(){
    this.subscriptionDestiny = this.destinyService.getMessage()
    .subscribe(
      x =>{
            this.model.destinies = x;
            x.forEach(
              x =>{
                  if (x.placeId.toString() == this.placeService.outsideOfProvince){
                    this.Allexpenditures.forEach(exp => {
                        if (exp.outsideProvince && this.model.expenditures.find(v => v.id == exp.id) === undefined){
                          var expenditureForModifiying = new ExpenditureForModifyingDto();
                          expenditureForModifiying.expenditureTypeId = exp.id;
                          expenditureForModifiying.expenditureTypeName = exp.name;
                          expenditureForModifiying.description = "Maximo para gastos de '"+ exp.name.toUpperCase()+"' debido a que el destino del viático es en ("+x.provinceName.toUpperCase()+") fuera de la provincia de corrientes";
                          expenditureForModifiying.amount = exp.percentage * (x.days * x.percentageCodeLiquidation * x.advanceCategory);
                          this.model.expenditures.push(expenditureForModifiying);
                        }
                    }); 
                  }
                  
                  if (
                    x.cityId !== undefined 
                    && x.provinceId !== undefined
                    && x.cityId != null
                    && x.provinceId != null){ 
                    //se ingreso una ciudad y una provincia
                    this.allProvice();
                    this.citiesThisProvince(x.provinceId);
                }
              }
            );
      }  
    );
  }

  citiesThisProvince(provinceId : number){
    this.cityService.GetByIdCity(provinceId).subscribe(
      x=>{
          this.cities = x;
         } 
    );
  }


  citiesThisProvinceModify(provinceId : number){
    this.cityService.GetByIdCity(provinceId).subscribe(
      x=>{
          this.cities = this.citiesModify.concat(x);
         } 
    );
  }

  allExpenditureFromModal(){
    this.subscriptionExpenditure = this.expenditureService.getMessage()
    .subscribe(
      x=>{
        this.model.expenditures = x;
      },
      error => console.log(error)
    );
  }

  allexpenditures(){
    this.expenditureService.getAll().subscribe(
      x => {this.Allexpenditures = x}
    );
  }

  reorder(){
    this.model.expenditures.forEach((c,index) =>{c.orderNumber = index; debugger});
  }
  removeExpenditure(expenditure : Expenditure){
      let minus : number = 0;
      const index = this.model.expenditures.indexOf(expenditure, 0);
      minus = minus + expenditure.amount;
      if (index > -1) {
        this.model.expenditures.splice(index, 1);
        this.reorder();
        this.dirtyForm = true;
        //this.deleteFromDatabaseExpenditure(expenditure.id);
      }
      
      this.totalResultExpenditure();
      
   }

   removeDestiny(destiny : destinyForModifyingSolicitationDto){
      
        let minus : number = 0;
        const index = this.model.destinies.indexOf(destiny, 0);
        let codLiq = this.codeLiquidations.find(x => x.id == destiny.codeLiquidationId);
        
        let category = this.categories.find(x => x.id == destiny.categoryId);
  
        minus = minus + (codLiq.percentage * category.advance);
        if (index > -1) {
          this.model.destinies.splice(index, 1);
        }
        this.totalResultExpenditure();
        this.dirtyForm = true;

   }

   deleteAllConcepts(){
     let array = this.model.expenditures;
     let minus : number = 0;
     if (array === undefined){
       return;
     }
      for (let i = array.length - 1; i > -1; i--) {
          minus = minus + array[i].amount;
          const indexDeleteAll = this.model.expenditures.indexOf(array[i], 0);
          if (indexDeleteAll > -1) {
            //this.deleteFromDatabaseExpenditure(this.model.expenditures[i].id);
            this.model.expenditures.splice(indexDeleteAll, 1);
          }
      }
      this.totalResultExpenditure();
      this.dirtyForm = true;
   }

   deleteAllDestinies(){
    let array = this.model.destinies;
    let minus : number = 0;
    if (array === undefined){
      return;
    }
     for (let i = array.length - 1; i > -1; i--) {
          let codLiq = this.codeLiquidations.find(x => x.id == array[i].codeLiquidationId);
          let category = this.categories.find(x => x.id == array[i].categoryId);

          minus = minus + (codLiq.percentage * category.advance);
          const indexDeleteAll = this.model.destinies.indexOf(array[i], 0);
          if (indexDeleteAll > -1) {
            //this.deleteFromDatabaseDestinies(array[i].id,indexDeleteAll);
            this.model.destinies.splice(indexDeleteAll, 1);
          }
     }
     this.totalResultExpenditure();
     this.dirtyForm = true;
  }

     //MODALS
  openAddNewConcept() {
    this.dirtyForm = true;
    const modalRef = this.modalService.open(AddNewExpenditureComponent,{
      backdrop : 'static',
      keyboard : false
    });
    if (this.model.expenditures === undefined)
    {
      this.model.expenditures = [];
    }

    modalRef.componentInstance.expendituresAdded = this.model.expenditures;
    modalRef.componentInstance.isCommission = this.model.isCommission;
    modalRef.componentInstance.keyRandom = this.model.randomKey;
    modalRef.result.then(()=> {
      this.totalResultExpenditure();
    },
    j => {
        console.log(j);      
      }
    );
  }

  AddDestiny(){
    this.dirtyForm = true;
    this.ngbModalOptions.size = 'lg';
    const modalRef = this.modalService.open(AddDestinyComponent,this.ngbModalOptions);

    if (this.model.destinies === undefined)
    {
      this.model.destinies = [];
    }

    let listDestinies : destinyForModifyingSolicitationDto[] = this.model.destinies;
    
    modalRef.componentInstance.destiniesAdded = listDestinies;
    modalRef.componentInstance.solicitationId = this.id;
    
    modalRef.result.then(
      () =>this.totalResultExpenditure()
    ,
    j => {
          console.log(j);
        }
    );   
  }

  ngOnDestroy() {
    this.subscriptionExpenditure.unsubscribe();
    this.subscriptionDestiny.unsubscribe();
  }

  changeValue(e : any){
    console.log(e);
  }

  deleteFromDatabaseExpenditure(id : number){
    this.expenditureAgentService.delete(id)
    .subscribe(
      () => []
    );
  }

  deleteFromDatabaseDestinies(id : number, index : number){
    this.destinyService.delete(id)
    .subscribe(
      () => {
        this.model.destinies.splice(index, 1);
      },
      e => {
                var errors : any = e.error.notifications;
                errors.forEach(element => {
                  this.msjWhenUserTryDeleteLastDestiny = element.value
                })

                this.msjToastInfo(this.msjWhenUserTryDeleteLastDestiny);
            }
    
    );
  }

  msjToastError(msg : string){
    this.toastrService.error(msg,'',
    {positionClass : 'toast-top-center', timeOut : 3000});
  }

  msjToastSuccess(msg : string){
    this.toastrService.success(msg,'',
    {positionClass : 'toast-top-center', timeOut : 3000});
  }

  msjToastInfo(msg : string){
    this.toastrService.info(msg,'',
    {positionClass : 'toast-top-center', timeOut : 3000});
  }
  
  onSubmit(){
      this.totalResultExpenditure();
      this.submited = true;
      if (this.model.destinies.length == 0){
        this.msjToastInfo('Debe ingresar al menos un destino');
        this.submited = false;
        return;
      }

      this.calculateHolidaysAndWeekEnds();

      this.model.destinies.forEach(c => {
        if (!GuidClass.isValid(c.id.toString())){
            c.id = GuidClass.empty
        }
      });
      
      if(this.model.isCommission){
        this.solicitationSubsidyService.getByRandomKey(this.model.randomKey)
        .subscribe(
          () =>{
            if(this.currentUserId == this.model.userId){
              this.spinner.show();
              this.solicitationSubsidyService.updateCommission(this.model).subscribe(
                () => {
                  this.spinner.hide();
                  this.router.navigate(['SolicitationSubsidy/agent']);
                  this.msjToastSuccess('La solicitud de viático se ha modificado correctamente');
                },
                err => {
                  this.spinner.hide();
                  if (err.error.errors){
                    var e = err.error.errors.Error || [];
                    e.forEach(Errors => {
                        this.toastrService.error(Errors,'',{timeOut : 2000});
                    });
                  }
                }
              );
            }else{
              this.spinner.show();
              this.solicitationSubsidyService.createCommission(this.model).subscribe(
                () => {
                    this.spinner.hide();
                    this.router.navigate(['SolicitationSubsidy/agent']);
                    this.msjToastSuccess('La solicitud de viático se ha guardado correctamente');
                },
                err =>{
                  this.spinner.hide();
                  if (err.error.errors){
                    var e = err.error.errors.Error || [];
                    e.forEach(Errors => {
                        this.toastrService.error(Errors,'',{timeOut : 2000});
                    });
                  }
                }
              );
            }  
          }
        );
      }else{
        this.model.randomKey = "";
        if(this.id){
          this.spinner.show();
          this.solicitationSubsidyService.updateSolicitation(this.model).subscribe(
            () => {
              this.spinner.hide();
              this.router.navigate(['SolicitationSubsidy/agent']);
              this.msjExito = 'Solicitud Enviada';
              this.msjToastSuccess('La solicitud de viático se ha modificado correctamente');
            },
            error => {
              this.spinner.hide()
              this.msj = error.error.errors.Error || [];
              // e.forEach(element => {
              //     this.msj = element;
              // });
            }
          );
        }else{
          this.spinner.show();
          this.solicitationSubsidyService.createSolicitation(this.model).subscribe(
            () => {
                this.spinner.hide();
                this.router.navigate(['SolicitationSubsidy/agent']);
                this.msjExito = 'Solicitud Actualizada';
                this.msjToastSuccess('La solicitud de viático se ha guardado correctamente');
            },error => {
                this.spinner.hide();
            }
          );
        }
      }
      
  }

  onChangeColapse(){
    this.dirtyForm = true;
    this.isCollapsedDestiny = !this.isCollapsedDestiny;
    if (this.isCollapsedDestiny){
      this.verOcultarIconDestiny = "arrow-circle-down";
      this.verOcultarTextDestiny = "Ver";
      return
    }
    this.verOcultarIconDestiny = "arrow-circle-up";
    this.verOcultarTextDestiny = "Ocultar";
  }

  changeCollapseExpenditure(){
    this.dirtyForm = true;
    this.isCollapsedExpenditure = !this.isCollapsedExpenditure;
    if (this.isCollapsedExpenditure){
      this.verOcultarIconExpenditure = "arrow-circle-down";
      this.verOcultarTextExpenditure = "Ver";
      return
    }
    this.verOcultarIconExpenditure = "arrow-circle-up";
    this.verOcultarTextExpenditure = "Ocultar";    
  }

    totalResultExpenditure(){
      let resultExpenditure = 0;
      let resultDestiny = 0;
      this.model.expenditures.forEach(
        expenditure => resultExpenditure = resultExpenditure +  expenditure.amount
      );

      this.model.destinies.forEach(
        destiny => {  
          resultDestiny = resultDestiny + (destiny.advanceCategory * destiny.daysPay * destiny.percentageCodeLiquidation);
        }
      );
      

      this.model.total = resultExpenditure + resultDestiny;
    }


    calculateHolidaysAndWeekEnds(){
      this.model.destinies.forEach(dest =>{
        if (!dest.id){
        
          for (let i = 0; i <= dest.days; i++) {
            var date = new Date(dest.startDate.year, dest.startDate.month - 1, dest.startDate.day);
            date.setDate(date.getDate() + i);
            let dateNow = new  NgbDate(date.getFullYear(), date.getMonth() + 1, date.getDate());
            //es Fin de semana
            let isWeekend = this.ngbCalendar.getWeekday(dateNow);
            if (isWeekend == 6 || isWeekend == 7){
              //contamos la cantidad de dias no laborables
              dest.daysWeekEnd = dest.daysWeekEnd + 1;
            }
          }

        }
      });
    }

    hasUnsavedData(){
      return (this.solicitationForm.dirty || this.dirtyForm) && !this.submited;
    }

    cleanInput(){
      this.model.randomKey = "";
    }

    randomAlphaNumberKey(lengthLetter : number, lengthNumber: number ) {
        var text = "";
        var num = "";
        var abcedario = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        var numeros = "0123456789";

        for (var i = 0; i < lengthLetter; i++){
          text += abcedario.charAt(Math.floor(Math.random() * abcedario.length));
        }
        
        for (var i = 0; i < lengthNumber; i++){
          num += numeros.charAt(Math.floor(Math.random() * numeros.length));
        }
        
      this.model.randomKey = text+"-"+num;
    }

    searchSolicitationByRandomKey(key : string){
      this.getSolicitation(null, key);
    }

}
