import { ExpenditureForModifyingDto, ImageDto, CreateAccountForSolicitationSubsidyDto } from './../../_models/solicitationSubsidy';
import { destinyForModifyingSolicitationDto, DestinyDto } from './../../_models/destiny';
import { AddDestinyRepaymentComponent } from './../../modals/add-destiny-repayment/add-destiny-repayment.component';
import { AuthenticationService } from './../../_services/authentication.service';
import { AddExpenditureRepaymentComponent } from './../../modals/add-expenditure-repayment/add-expenditure-repayment.component';
import { Component, OnInit, ViewChild } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { SolicitationSubsidyService } from 'src/app/_services/solicitation-subsidy.service';
import { NgbModal, NgbDate, NgbCalendar, NgbModalOptions, NgbTimepickerConfig } from '@ng-bootstrap/ng-bootstrap';
import { AllCategoryDto } from 'src/app/_models/category';
import { AllTransportDto } from 'src/app/_models/transport';
import { Expenditure, CreateSolicitationSubsidyDto } from 'src/app/_models/solicitationSubsidy';
import { Subscription } from 'rxjs';
import { AllExpenditureDto } from 'src/app/_models/expenditureType';
import { ProvinceBaseDto } from 'src/app/_models/province';
import { CityBaseDto } from 'src/app/_models/city';
import { AllCountryDto } from 'src/app/_models/country';
import { codeLiquidationBaseDto } from 'src/app/_models/codeLiquidation';
import { ExpenditureService } from 'src/app/_services/expenditure.service';
import { DestinyService } from 'src/app/_services/destiny.service';
import { ProvinceService } from 'src/app/_services/province.service';
import { CityService } from 'src/app/_services/city.service';
import { CategoryService } from 'src/app/_services/category.service';
import { TransportService } from 'src/app/_services/transport.service';
import { CountryService } from 'src/app/_services/country.service';
import { CodeLiquidationService } from 'src/app/_services/code-liquidation.service';
import { ExpendituresUserService } from 'src/app/_services/expenditures-user.service';
import { Title } from '@angular/platform-browser';
import { ToastrService } from 'ngx-toastr';
import { FileUploader } from 'ng2-file-upload';
import { environment } from 'src/environments/environment';
import { CrystalLightbox } from 'ngx-crystal-gallery';
import { FormGroup } from '@angular/forms';
import { ApproveOfAuthorityThatOrderCommissionDto } from 'src/app/_models/ApproveOfAuthorityThatOrderCommission';
import { ApproveOfAuthorityThatOrderCommissionService } from 'src/app/_services/approve-of-authority-that-order-commission-service.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { AddDestinyComponent } from 'src/app/modals/add-destiny/add-destiny.component';
import { DateDto } from 'src/app/_models/holiday';
import { GuidClass } from 'src/app/_helpers/guid-class';
import { AddObservationsComponent } from 'src/app/modals/add-observations/add-observations.component';
import { ObservationBaseDto } from 'src/app/_models/observation';

@Component({
  selector: 'app-repayment',
  templateUrl: './repayment.component.html',
  styleUrls: ['./repayment.component.css']
})
export class RepaymentComponent implements OnInit {
  
  uploader:FileUploader;
  isCollapsedDestiny = false;
  categories : AllCategoryDto[] = [];
  transports : AllTransportDto[] = [];
  isCollapsedExpenditure = false;
  ConceptExpenditureList : Expenditure[]=[];
  subscriptionExpenditure: Subscription;
  subscriptionObservation : Subscription;
  subscriptionDestiny : Subscription;
  _disabled = false;
  expenditures : AllExpenditureDto[];
  Allexpenditures : AllExpenditureDto[];
  destinies : DestinyDto[] = [];
  model = new CreateAccountForSolicitationSubsidyDto;
  DestinyStatic : DestinyDto[] = [];
  expenditureStatics : Expenditure[] = [];
  radioButtonRequired : boolean = true;
  provinces : ProvinceBaseDto[];
  cities : CityBaseDto[];
  verOcultarIconDestiny = "arrow-circle-up";
  verOcultarTextDestiny = "Ocultar";
  verOcultarIconExpenditure = "arrow-circle-up";
  verOcultarTextExpenditure = "Ocultar";
  countries : AllCountryDto[] = [];
  codeLiquidations : codeLiquidationBaseDto[] = [];
  citiesModify : CityBaseDto[] = [];
  msj = '';
  msjExito = '';
  msjWhenUserTryDeleteLastDestiny = '';
  supplementariesCities : string[];
  urlImage : string;
  idUser : number;
  id: any;
  url = '';
  hasBaseDropZoneOver = false;
  baseUrl = environment.apiUrl; 
  image : ImageDto;
  submit : boolean = true;
  ngbModalOptions: NgbModalOptions = {
    backdrop : 'static',
    keyboard : false
  };
  allApprovedOrder1 : ApproveOfAuthorityThatOrderCommissionDto[];
  allApprovedOrder2 : ApproveOfAuthorityThatOrderCommissionDto[];
  allApprovedOrder3 : ApproveOfAuthorityThatOrderCommissionDto[];
  lastItem : ApproveOfAuthorityThatOrderCommissionDto[];
  firstItem : ApproveOfAuthorityThatOrderCommissionDto[];
  cont : number = 0;

  configTransport = {
    displayKey: "description", //if objects array passed which key to be displayed defaults to description
    search: true,//true/false for the search functionlity defaults to false,
    height: '150px', //height of the list so that if there are more no of items it can show a scroll defaults to auto. With auto height scroll will never appear
    placeholder: 'Busqueda por Marca, Modelo y patente ...', // text to be displayed when no item is selected defaults to Select,
    customComparator: () => { }, // a custom function using which user wants to sort the items. default is undefined and Array.sort() will be used in that case,
    limitTo: 0, // a number thats limits the no of options displayed in the UI similar to angular's limitTo pipe
    moreText: 'Agregados', // text to be displayed whenmore than one items are selected like Option 1 + 5 more
    noResultsFound: '0 Resultados', // text to be displayed when no items are found while searching
    searchPlaceholder: 'Buscar', // label thats displayed in search input,
    searchOnKey: 'description' // key on which search should be performed this will be selective search. if undefined this will be extensive search on all keys
  }

  @ViewChild('solicitationSubsidy') solicitationForm : FormGroup;

  constructor(
      private activateRouter : ActivatedRoute,
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
      public router : Router,
      private authService : AuthenticationService,
      private lightbox : CrystalLightbox,
      private configTimePicker : NgbTimepickerConfig,
      private approveOfAuthorityThatOrderCommissionService : ApproveOfAuthorityThatOrderCommissionService,
      private spinner: NgxSpinnerService) { 
        this.configTimePicker.size="small";
        this.configTimePicker.spinners=false;
        this.configTimePicker.meridian = false;
      }

  ngOnInit() {
    this.spinner.show();
    this.activateRouter.params.subscribe(
      x =>{
        this.id = x.id
    });
    
    this.model.destinies = [];
    this.model.expenditures = [];

    if (this.id){
      this.titleService.setTitle('Modificar Reintegro');
      this.solicitationSubsidyService
      .getByIdSolicitation(this.id)
      .subscribe(x => {
        this.model = x;
        this.model.destinies.forEach(dest => {
            var newDestination = new DestinyDto();
            dest.transport = {id : dest.transportId, 
                              carPlate : dest.transportCarPlate,
                              transportType: dest.transportType,
                              description : dest.transportBrand+dest.transportModel+dest.transportCarPlate+" - "+dest.transportType
                            };
            newDestination.days = dest.days;
            newDestination.daysPay = dest.daysPay;
            if (!dest.accountedForDays){
              dest.accountedForDays = 0;
              dest.accountedForDays = dest.daysPay;
              dest.timeStartDate = {hour : undefined, minute : undefined};
              dest.timeEndDate = {hour : undefined, minute : undefined};
            }
            newDestination.provinceName = dest.provinceName;
            newDestination.countryName = dest.countryName;
            newDestination.cityName = dest.cityName;
            newDestination.supplementaryCities = dest.supplementaryCities;
            newDestination.advanceCategory = dest.advanceCategory;
            newDestination.countryId = dest.countryId;
            newDestination.percentageCodeLiquidation = dest.percentageCodeLiquidation;
            this.DestinyStatic.push(newDestination);
        });
        this.model.expenditures.forEach(
          (exp,index) => {
            var newExpenditure = new Expenditure();
            newExpenditure.id = exp.id;
            newExpenditure.expenditureTypeName = exp.expenditureTypeName;
            if (!exp.accountedForAmount){
              exp.accountedForAmount = 0;
              exp.accountedForAmount = exp.amount;
              exp.supportingDate = {day : 0, month : 0, year : 0};
            }
            newExpenditure.amount = exp.amount;
            newExpenditure.description = exp.description;
            if (!exp.orderNumber){
              exp.orderNumber = index + 1;
            }
            this.solicitationSubsidyService.getUrlImageExpenditure(exp.id,186,60)
            .subscribe(urlImg => {
                exp.imagesDto = urlImg.response;
            });
            if (exp.isRequested){
              //solamente se agregan los gastos que fueron solicitados inicialmente.
              this.expenditureStatics.push(newExpenditure);
            }
          }
        );
        this.spinner.hide();
      },err=> {
        this.spinner.hide();
      });
    }else{
      this.titleService.setTitle('Crear Reintegro');
      this.spinner.hide();
    }

    this.allexpenditures();
    this.allExpenditureFromModal();
    this.allDestinyFromModal();
    this.allCategories();
    this.allTransport();
    this.allCountries();
    this.allCodeLiquidation();
    this.allProvice();
    this.totalResult();
    this.allApproveOfAuthorityThatOrderCommission(this.id);
    this.initializeUploader();
  }


  allTransport(){
    this.transportService.getAll().subscribe(
      x => this.transports = x
    );
  }

  allApproveOfAuthorityThatOrderCommission(id : number){
    this.approveOfAuthorityThatOrderCommissionService
    .getApproved(id)
    .subscribe(x => {
      this.allApprovedOrder1 = x.response.filter(y => y.order == 1);
      this.allApprovedOrder2 = x.response.filter(j => j.order == 2);
      this.allApprovedOrder3 = x.response.filter(w => w.order == 0 && w.orderReport > 1 && w.orderReport < 5);
      this.firstItem = x.response.filter(x => x.order == 0 && x.orderReport == 1);
      this.lastItem = x.response.filter(x => x.order == 0 && x.orderReport == 5);
    });
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
    this.model.expenditures.forEach((c,index) => c.orderNumber = index + 1);
  }

  removeExpenditure(expenditure : Expenditure){
      let minus : number = 0;
      const index = this.model.expenditures.indexOf(expenditure, 0);
      minus = minus + expenditure.amount;
      if (index > -1) {
        this.model.expenditures.splice(index, 1);
        this.reorder();
        //this.deleteFromDatabaseExpenditure(expenditure.id);
      }
      
      this.totalResult();
      
   }

   removeDestiny(destiny : DestinyDto){
      
        let minus : number = 0;
        const index = this.model.destinies.indexOf(destiny, 0);
        let codLiq = this.codeLiquidations.find(x => x.id == destiny.codeLiquidationId);
        
        let category = this.categories.find(x => x.id == destiny.categoryId);
  
        minus = minus + (codLiq.percentage * category.advance);
        if (index > -1) {
          
          if (destiny.id){
            if(this.model.destinies.length > 1){
              this.deleteFromDatabaseDestinies(destiny.id,index);
            }else{
              this.msjToastInfo("Un reintegro de viático debe contener al menos 1 destino");
            }
          }else{
            this.model.destinies.splice(index, 1);
          }

        }
  
        this.totalResult();

   }

   initializeUploader() {
    this.uploader = new FileUploader({
      isHTML5: true,
      allowedFileType: ['image'],
      removeAfterUpload: true,
      autoUpload: false,
      maxFileSize: 10 * 1024 * 1024
    });

    this.uploader.onSuccessItem = (item, response) => {
      if (response) {
      }
    }    
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

      this.totalResult();
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
     this.totalResult();
  }

     //MODALS
  openAddNewConcept() {
    const modalRef = this.modalService.open(AddExpenditureRepaymentComponent,this.ngbModalOptions);
    if (this.model.expenditures === undefined)
    {
      this.model.expenditures = [];
    }

    let listExpenditures : Expenditure[] = this.model.expenditures;

    modalRef.componentInstance.expendituresAdded = listExpenditures;
    modalRef.result.then(()=> {
      this.totalResult();
    },
    j => {
        console.log(j);      
      }
    );
  }


  AddDestiny(){
    this.ngbModalOptions.size = 'lg';
    const modalRef = this.modalService.open(AddDestinyComponent,this.ngbModalOptions);

    if (this.model.destinies === undefined)
    {
      this.model.destinies = [];
    }

    let listDestinies : DestinyDto[] = this.model.destinies;
    
    modalRef.componentInstance.destiniesAdded = listDestinies;
    
    modalRef.result.then(
      () =>this.totalResult()
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
    this.submit = true;

    if (this.model.destinies.length == 0){
      this.msjToastInfo('Debe ingresar al menos un destino');
      return;
    }

    var today = new Date();
    var todayDto = new DateDto();
    todayDto.day = today.getDate();
    todayDto.month = (today.getMonth() + 1);
    todayDto.year = today.getFullYear();
    this.model.finalizeDate = todayDto;

    var dest = this.model.destinies.find(x => 
      !x.timeStartDate || !x.timeEndDate 
      || x.timeStartDate.hour == undefined || x.timeStartDate.minute == undefined 
      || x.timeEndDate.hour == undefined || x.timeEndDate.minute == undefined);

    if (dest){
      this.toastrService.info('El Destino '+( (dest.countryId == null) ? dest.provinceName+" "+dest.cityName : dest.countryName )+" no tiene campos sin completar",'Destinos');
      this.submit = false;
    }

    this.model.expenditures.forEach(
      j => {
        if(j.imagesDto == undefined || j.imagesDto.length == 0){
          this.toastrService.info('No se ha seleccionado ninguna imagen del concepto "'+ j.expenditureTypeName+'".','Concepto de Gastos');
          this.submit = false;
        }
      }
    );

    if(!this.submit){
      return;
    }
    this.model.observations.forEach(c => {
      if (!GuidClass.isValid(c.id.toString())){
          c.id = GuidClass.empty
      }
    });
    this.model.destinies.forEach(dest => {
      if(!dest.accountedForDays){
        dest.accountedForDays = dest.daysPay;
      }
      
      if (!GuidClass.isValid(dest.id.toString())){
          dest.id = GuidClass.empty
      }
    });
    this.model.allApproved = this.allApprovedOrder1.concat(this.allApprovedOrder2)
    .concat(this.allApprovedOrder3).concat(this.firstItem).concat(this.lastItem);
    this.spinner.show();
    this.model.isRefund = true;
    this.solicitationSubsidyService.createAccountFor(this.model)
    .subscribe(
        () => {
            this.spinner.hide();
            this.router.navigate(['SolicitationSubsidy/agent']);
            this.msjToastSuccess('El rendición de viático se ha guardado correctamente');
        },err => {
          this.spinner.hide();
          var e = err.error.errors.Error || [];
            if (err.error.errors){
              e.forEach(msj => {
                this.toastrService.error(e);
              });
            }
        }
    );

  }

  onSelectFile(newExp : any) {
    if (this.uploader.queue.length > 0) {
      this.uploader.queue.forEach(
        (imgs,index) => {
          var reader = new FileReader();
          let img = new ImageDto();
          var imgResult : any = imgs.file.rawFile;

          var mimeType = imgResult.type;
          if (mimeType === "image/png" || mimeType === "image/jpg" || mimeType === "image/jpeg"){
            img.name = imgResult.name;
            img.type = imgResult.type;
            img.size = imgResult.size;
            img.webkitRelativePath = imgResult.webkitRelativePath;
            img.lastModified = imgResult.lastModified;
            img.lastModifiedDate = imgResult.lastModifiedDate;
            
            reader.readAsDataURL(imgResult); // read file as data url
  
            reader.onload = (event : any) => { // called once readAsDataURL is completed
              this.url = event.target.result;
              img.urlImages = this.url;
            }
  
            newExp.imagesDto = (newExp.imagesDto == undefined) ? [] : newExp.imagesDto;
            newExp.imagesDto.push(img);
           
          }else{
            this.toastrService.warning(imgResult.name+' No es una imagen.');
          }
        }
      );

    }
    this.uploader.clearQueue();
  }


  deleteImage(expenditure : Expenditure, index : number){
    expenditure.imagesDto.splice(index,1);
    this.uploader.queue.splice(index,1);
    if (expenditure.imagesDto.length == 0){
      expenditure.imagesDto = undefined;
    }
  }

  clear(modelExp : any){
    modelExp.supportingDate = {day : 0 , month : 0, year : 0};
  }

  validateAmount(expenditure : Expenditure, e : any){
    let value : any = e;
    
    if (e === ""){
      value="0";
      expenditure.accountedForAmount = (value * 1);
    }else{
      value = value.replace(/[$,]/g,"");
      var dot = value.indexOf(".");
      if (dot != -1){//si el usuario ingresar un número sobreescribiendo todos los dígitos
        value = value.substring(0,dot);
      }

      expenditure.accountedForAmount = (value * 1);
    }
    
    this.totalResult();
  }

  validateTotal(model : any, e : any){
    let value : any = e;
    if (e === ""){
      value="0";
      model.total = value;
    }
    value = value.replace(/[$,.]/g,"");
    model.total = value;
    this.totalResult();
  }


  onChangeColapse(){
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
    this.isCollapsedExpenditure = !this.isCollapsedExpenditure;
    if (this.isCollapsedExpenditure){
      this.verOcultarIconExpenditure = "arrow-circle-down";
      this.verOcultarTextExpenditure = "Ver";
      return
    }
    this.verOcultarIconExpenditure = "arrow-circle-up";
    this.verOcultarTextExpenditure = "Ocultar";    
  }

    totalResult(){
      let resultExpenditure : number = 0;
      let resultDestiny : number = 0;
      this.model.expenditures.forEach(
        expenditure => 
        {
          expenditure.accountedForAmount = (expenditure.accountedForAmount == undefined) ? expenditure.amount : expenditure.accountedForAmount;
          resultExpenditure = resultExpenditure + (expenditure.accountedForAmount * 1);
        }
      );

      this.model.destinies.forEach(
        destiny => {
          resultDestiny = resultDestiny + (destiny.advanceCategory * destiny.daysPay * destiny.percentageCodeLiquidation);
        }
      );
      this.model.total = resultExpenditure + resultDestiny;
    }

    pospone(){
      const modalRef = this.modalService.open(AddObservationsComponent,{
        backdrop : 'static',
        keyboard : false,
        size : "lg"
      });

      if (this.model.observations === undefined)
      {
        this.model.observations = [];
      }
  
      let listObservations : ObservationBaseDto[] = this.model.observations;
      modalRef.componentInstance.id = this.id;
      modalRef.componentInstance.observationList = listObservations;
      modalRef.componentInstance.accountFor = true;
      modalRef.result.then(() => {
      },
        () => {
          console.log('Backdrop click');
      })
    }

    hidden(obs : ObservationBaseDto){
      obs.hidden = !obs.hidden;
    }
  
    add(){
      var newObs = new ObservationBaseDto();
      this.cont++;
      newObs.id =  this.cont;
      this.model.observations = this.model.observations || [];
      this.model.observations.push(newObs);
    }
  
    remove(obs : ObservationBaseDto){
      const index = this.model.observations.indexOf(obs, 0);
  
      this.model.observations.splice(index,1);
      
    }

    
    hasUnsavedData(){
      return (this.solicitationForm.dirty) && !this.submit;
    }
}
