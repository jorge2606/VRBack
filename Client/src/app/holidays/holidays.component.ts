import { ClaimsService } from './../_services/claims.service';
import { AuthenticationService } from './../_services/authentication.service';
import { ToastrService } from 'ngx-toastr';
import { Title } from '@angular/platform-browser';
import { HolidayBaseDto, DateDto, FilterHolidayDto } from './../_models/holiday';
import { HolidaysService } from './../_services/holidays.service';
import { Component, OnInit } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NgbdModalContent } from '../modals/modals.component';
import { Router } from '@angular/router';

@Component({
  selector: 'app-holidays',
  templateUrl: './holidays.component.html',
  styleUrls: ['./holidays.component.css']
  //,providers: [I18n, {provide: NgbDatepickerI18n, useClass: CustomLanguageDatepickerI18n}] 
})

export class HolidaysComponent implements OnInit {

  filters : FilterHolidayDto = { page : 0, description : "", date : { day : 0, month : 0, year : 0}}
  holidays : HolidayBaseDto[];
  
  //paginator
  col_size : number;
  itemsPerPage : number = 10;
  errorDatapicker : string;
  //

  textListEmpty : string = "No se encontró ningún feriado";
  classListEmpty : string = "alert-primary";
  permissions : any[] = [];
  holidaysCreate: any;
  holidaysEdit: any;
  holidaysDelete: any;
  page = 0;

  constructor(
    private holidayService : HolidaysService,
    private modalService: NgbModal,
    private titleService : Title,
    private toastrService : ToastrService,
    private authService : AuthenticationService,
    private claimService : ClaimsService,
    private routerService : Router
  ) {
      this.titleService.setTitle('Feriados');
  }

  ngOnInit() {
    if(!this.claimService.haveClaim('user.create')){
      this.routerService.navigate(['/notAuthorized']);
    }else{
      this.getAllHolidays(this.filters);
    }
    
  }

  getAllHolidays(filters : any){
    this.holidayService.getPageHoliday(filters).subscribe(
      x => {
            this.holidays = x.list;
            this.col_size = x.totalRecords;
            this.permissions = this.authService.userId('roles');
            this.holidaysCreate = this.permissions.find(x => x.value == 'holidays.create');
            this.holidaysEdit = this.permissions.find(x => x.value == 'holidays.edit');
            this.holidaysDelete = this.permissions.find(x => x.value == 'holidays.delete');
            }
    );
  }

  filter(){  
    this.loadPage(this.filters.page);
  }

  clear(){
    this.filters.date = {day : 0 , month : 0, year : 0};
    this.loadPage(this.page);
  }

  validateDate(){
    if (this.filters.date &&
      (!this.filters.date.day
        || !this.filters.date.month
        || !this.filters.date.year
        || this.filters.date.day > 31 || this.filters.date.month < 1
        || this.filters.date.month > 12 || this.filters.date.month < 1
        || this.filters.date.year > 2099 || this.filters.date.year < 1912))
        {
          return false;
        }
          return true;
  }

  loadPage(page : any) {
    if (page > 0){
        this.filters.page = page - 1;
    }
    this.getAllHolidays(this.filters);
  }

    //MODALS
    openEliminar(holiday : HolidayBaseDto) {
      const modalRef = this.modalService.open(NgbdModalContent,{
        backdrop : 'static',
        keyboard : false
      });
      modalRef.componentInstance.Encabezado = "Eliminar";
      let dateToShow = holiday.date;

      modalRef.componentInstance.Contenido = "¿Desea eliminar feriado : " + holiday.description + " " + 
      dateToShow.day+"/"+ (dateToShow.month + 1)+"/"+ dateToShow.year+ "?";

      modalRef.componentInstance.GuardaroEliminar = "Eliminar";
      modalRef.componentInstance.GuardaroEliminarClass = "btn-danger";
      modalRef.componentInstance.MsgClose = "Cancelar";
      modalRef.result.then(() => {
        this.holidayService.deleteHoliday(holiday.id).subscribe(
          () => {
            this.toastrService.success("La fecha '"+holiday.description+"' se ha eliminado correctamente.",'',
            {positionClass : 'toast-top-center', timeOut : 3000});
            this.loadPage(this.page);
          },
          error => {
              console.log("error", error);
          }
      );
      },
        () => {
          console.log('Backdrop click');
      })
    }

}
