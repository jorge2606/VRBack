import { AuthenticationService } from './../../_services/authentication.service';
import { CodeLiquidationService } from './../../_services/code-liquidation.service';
import { AllCountryDto } from './../../_models/country';
import { DestinyService } from 'src/app/_services/destiny.service';
import { Component, OnInit, Input } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { AllPlaceDto } from 'src/app/_models/place';
import { PlaceService } from 'src/app/_services/place.service';
import { AllProvinceDto } from 'src/app/_models/province';
import { AllCitiesDto } from 'src/app/_models/city';
import { CityService } from 'src/app/_services/city.service';
import { ProvinceService } from 'src/app/_services/province.service';
import { DestinyDto } from 'src/app/_models/destiny';
import { CategoryService } from 'src/app/_services/category.service';
import { AllCategoryDto } from 'src/app/_models/category';
import { TransportService } from 'src/app/_services/transport.service';
import { AllTransportDto, CarIsBeingUsedByOtherSolicitation } from 'src/app/_models/transport';
import { CountryService } from 'src/app/_services/country.service';
import { codeLiquidationBaseDto } from 'src/app/_models/codeLiquidation';
import { NgxSpinnerService } from 'ngx-spinner';
import { SupplementaryCityDto } from 'src/app/_models/supplementaryCity';
import { RandomAlphaNumberKeyService } from 'src/app/_services/random-alpha-number-key.service';
import { ToastrService } from 'ngx-toastr';


@Component({
  selector: 'app-add-destiny',
  templateUrl: './add-destiny.component.html',
  styleUrls: ['./add-destiny.component.css']
  //,providers: [I18n, {provide: NgbDatepickerI18n, useClass: CustomLanguageDatepickerI18n}] 
})
export class AddDestinyComponent implements OnInit {

  @Input() solicitationId : number;
  places: AllPlaceDto[] = [];
  transports: AllTransportDto[] = [];
  provinceMock: AllProvinceDto[] = [];
  provinces: AllProvinceDto[] = [];
  countries: AllCountryDto[] = [];
  countriesMock: AllCountryDto[] = [];
  cities: AllCitiesDto[] = [];
  citiesMock: AllCitiesDto[] = [];
  model  = new DestinyDto;
  codeLiquidations: codeLiquidationBaseDto[] = [];
  codeLiquidationsMock: codeLiquidationBaseDto[] = [];
  @Input() destiniesAdded: DestinyDto[] = [];
  isCollapsed = false;
  categories: AllCategoryDto[] = [];
  buttonDisbaled = true;
  selectedCountry: number;
  selectedProvince: number;
  categoryUser = new AllCategoryDto;
  selectedCity: number;
  selectedCodeLiquidation: number;
  selectedTransport: number;
  total: number = 0;
  carIsUsed: boolean;
  minDate  : any =  {year : new Date().getFullYear(),month : new Date().getMonth() + 1,day :new Date().getDate()};
  config = {
    displayKey: "name", //if objects array passed which key to be displayed defaults to description
    search: true,//true/false for the search functionlity defaults to false,
    height: 'auto', //height of the list so that if there are more no of items it can show a scroll defaults to auto. With auto height scroll will never appear
    placeholder: 'Seleccionar ...', // text to be displayed when no item is selected defaults to Select,
    customComparator: () => { }, // a custom function using which user wants to sort the items. default is undefined and Array.sort() will be used in that case,
    limitTo: 5, // a number thats limits the no of options displayed in the UI similar to angular's limitTo pipe
    moreText: 'Agregados', // text to be displayed whenmore than one items are selected like Option 1 + 5 more
    noResultsFound: '0 Resultados', // text to be displayed when no items are found while searching
    searchPlaceholder: 'Buscar', // label thats displayed in search input,
    searchOnKey: 'name' // key on which search should be performed this will be selective search. if undefined this will be extensive search on all keys
  }

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
  submitted : boolean = false;

  expenditureTaxi : number;

  constructor(
    public activeModal: NgbActiveModal,
    private placeService: PlaceService,
    private provinceService: ProvinceService,
    private cityService: CityService,
    private destinyService: DestinyService,
    private categoryService: CategoryService,
    private transportService: TransportService,
    private countryservice: CountryService,
    private codeLiquidationService: CodeLiquidationService,
    private spinnerDestination: NgxSpinnerService,
    private authService: AuthenticationService,
    private randomNumberService : RandomAlphaNumberKeyService,
    private toastrService : ToastrService
    ) { }

  ngOnInit() {
    this.selectedCountry = this.model.countryId;
    this.selectedProvince = this.model.provinceId;
    this.AllPlace();
    this.AllProvince();
    this.allCategories();
    this.allTransports();
    this.allCountries();
    this.allCodeLiquidation();
  }

  event(e : any){
    console.log(e);
  }

  allTransports() {
    this.transportService.getAll().subscribe(
      x => this.transports = x
    );
  }

  allCodeLiquidation() {
    this.codeLiquidationService.getAll().subscribe(
      x => {
        this.codeLiquidationsMock = x;
        this.codeLiquidations = x;
      }
    );
  }

  allCountries() {
    this.countryservice.getAllCountries().subscribe(
      x => {
        this.countriesMock = x;
        this.countries = x;
      }
    );
  }


  allCategories() {
    this.categoryService.getallCategories()
      .subscribe(
        x => {
          this.categoryUser = x.find(j => j.id == this.authService.userId('categoryId'));
          /*x.forEach(cat => {
            if (cat.order <= categoryUser.order) {
              this.categories.push(cat);
            }
          }); */
        }
      );
  }

  AllProvince() {
    this.provinceService.getAll().subscribe(
      x => {
        this.provinceMock = x;
        this.provinces = x;
      }
    );
  }

  changeCity(e: any) {
    console.log(e);
  }

  sendDataToComponent(model: DestinyDto[]) {
    this.destinyService.sendMessage(model);
    this.activeModal.close(null);
  }

  AllPlace() {
    this.placeService.getAll().subscribe(
      x => this.places = x.sort((a, b) => a.order - b.order)

    );
  }

  onChange(e: any) {
    console.log(e);
  }



  onSubmit() {
    this.submitted = true;
    let exist;
    if (!this.model.countryId) {
      if (this.destiniesAdded != null) {
        exist = this.destiniesAdded.find(
          x => x.cityId == this.model.cityId && x.provinceId == this.model.provinceId
        );
      }

      if (exist) {
        this.toastrService.error('Provincia y Localidad existentes','',{timeOut : 2000});
        return
      }
    }

    var newCarIsBeingUsed = new CarIsBeingUsedByOtherSolicitation(
      this.model.transportId.id,this.model.startDate,this.model.days
    );
    var startDateFromView = new Date(newCarIsBeingUsed.startDate.year,newCarIsBeingUsed.startDate.month,newCarIsBeingUsed.startDate.day);
    var endDateFromView = new Date(newCarIsBeingUsed.startDate.year,newCarIsBeingUsed.startDate.month,newCarIsBeingUsed.startDate.day);
    endDateFromView.setDate(endDateFromView.getDate() + newCarIsBeingUsed.days);

    this.destiniesAdded.forEach(
      dest => {
        var startDateStored = new Date(dest.startDate.year,dest.startDate.month,dest.startDate.day);
        var endDateStored = new Date(dest.startDate.year,dest.startDate.month,dest.startDate.day);
        endDateStored.setDate(endDateStored.getDate() + dest.days);

        if (!( endDateStored <  startDateFromView 
                || 
                startDateStored > endDateFromView )
           )
          {
              this.toastrService.error('El rango de fecha coincide con una solicitud anterior','',{timeOut : 2000});
              this.submitted = false;
          }
      }
    );

    if (!this.submitted){
      return;
    }
          
    let newDestiny = new DestinyDto;
    newDestiny.id = this.destiniesAdded.length + 1;
    newDestiny.placeId = this.model.placeId;
    newDestiny.cityId = this.model.cityId;
    if (this.model.cityId != null) {
      newDestiny.cityName = this.cities.find(x => x.id == this.model.cityId).name;
    }

    newDestiny.supplementaryCities = [];

    if (this.model.supplementaryCities) {
      this.model.supplementaryCities.forEach(x => {
        let newSup = new SupplementaryCityDto();
        newSup.cityId = x.id;
        let idCity = this.cities.find(j => j.id == x.id);
        if (idCity) {
          newSup.name = idCity.name;
        }
        newDestiny.supplementaryCities.push(newSup);
      }
      );
    }
    newDestiny.countryId = this.model.countryId;
    if (this.model.countryId != null) {
      newDestiny.countryName = this.countries.find(x => x.id == this.model.countryId).name;
    }
    newDestiny.provinceId = this.model.provinceId;
    if (this.model.provinceId != null) {
      newDestiny.provinceName = this.provinces.find(x => x.id == this.model.provinceId).name;
    }
    newDestiny.days = this.model.days;
    newDestiny.daysPay = this.model.days;
    newDestiny.categoryId =this.categoryUser.id;
    if (this.categoryUser.id != null) {
      newDestiny.categoryName = this.categoryUser.name;
      newDestiny.advanceCategory = this.categoryUser.advance;
    }
    
    if (this.model.codeLiquidationId){
      newDestiny.codeLiquidationId = this.model.codeLiquidationId;
      let codLiq = this.codeLiquidations.find(x=> x.id == this.model.codeLiquidationId);
      newDestiny.percentageCodeLiquidation = codLiq.percentage;
    }
    newDestiny.startDate = this.model.startDate;
    newDestiny.transport = this.model.transportId;
    newDestiny.transportId = this.model.transportId.id;
    newDestiny.transportBrand = this.model.transportId.brand;
    newDestiny.transportModel = this.model.transportId.model;
    newDestiny.transportCarPlate = this.model.transportId.carPlate;
       
    if(this.solicitationId){
      newDestiny.solicitationSubsidyId = this.solicitationId;
    }
    this.destiniesAdded = this.destiniesAdded || [];
    this.destiniesAdded.push(newDestiny);
    this.sendDataToComponent(this.destiniesAdded);
  }

  totalResultExpenditure() {
    if (this.categoryUser.id === undefined ||
      this.model.codeLiquidationId === undefined ||
      this.model.days === undefined) {
      return;
    }


    let resultDestiny = 0;
    let codLiquidation = this.codeLiquidations.find(codLiq => codLiq.id == this.model.codeLiquidationId);
    resultDestiny = resultDestiny + (this.categoryUser.advance * this.model.days * codLiquidation.percentage);

    this.total = resultDestiny;
  }

  changeCategory() {
    this.totalResultExpenditure();
  }

  onChangeCodLiq() {
    this.totalResultExpenditure();
  }

  keyUpDays() {
    this.totalResultExpenditure();
  }

  toogle(place: AllPlaceDto) {
    this.buttonDisbaled = false;
    this.model = new DestinyDto();
    this.model.placeId = place.id;

    place.checked = true;
    this.places.forEach(x => {
      if (x.id != place.id) {
        x.checked = !place.checked;
      }
    }
    );

    this.countries = this.countriesMock;
    if (this.countries[0].placeId != place.id) {
      this.provinces = this.provinceMock.filter(x => x.placeId == place.id);
      let firstProvince = this.provinces.sort()[0];
      if (this.provinces.length == 1) {
        this.model.provinceId = firstProvince.id;
      } else {
        this.model.provinceId = this.selectedProvince;
      }

      this.citiesProvince(firstProvince.id);
      this.countries = [];
    } else {
      //this.model.countryId = this.countries[0].id;
    }

    this.codeLiquidations = this.codeLiquidationsMock.filter(x => x.placeId == place.id).sort(x => x.percentage);
    this.cities = [];
  }

  changeCityId(cityId: number) {
    this.model.cityId = cityId;
  }

  citiesThisProvince(provinceId: any) {
    if (this.provinces.length == 0 && this.countries.length > 0) {
      //si hay paises y provincias => va a viajar fuera del pais
      this.model.cityId = null;
      this.model.provinceId = null;
    }
    else if (this.provinces.length == 1 && this.countries.length == 0) {
      //si hay paises y provincias => va a viajar dentro del pais
      //la persona va a viajar dentro de corrientes
      if (this.citiesMock.length == 0) {
        this.citiesProvince(provinceId);
      }
      this.cities = this.citiesMock;
      this.model.cityId = this.cities[0].id;
      this.model.provinceId = provinceId;
      //la propiedad citiesMock contiene todas las localidades de Corrientes Solamente.
    }
    else if (this.provinces.length > 1 && this.countries.length == 0) {
      //la persona va a viajar fuera de corrientes pero dentro del pais
      this.citiesProvince(provinceId);
      this.model.countryId = null;
    }
  }

  citiesProvince(provinceId: number) {
    this.spinnerDestination.show();
    this.cityService.GetByIdCity(provinceId).subscribe(
      x => {
        this.cities = x;
        if (this.provinces.length != 1) {
          this.model.cityId = this.selectedCity;
        } else {
          this.model.cityId = this.cities[0].id;
        }
        this.spinnerDestination.hide();
      }
    );
  }

}
