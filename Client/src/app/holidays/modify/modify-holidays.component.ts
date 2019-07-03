import { ClaimsService } from 'src/app/_services/claims.service';
import { FormGroup } from '@angular/forms';
import { Title } from '@angular/platform-browser';
import { UpdateHolidayDto } from './../../_models/holiday';
import { HolidaysService } from './../../_services/holidays.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Component, OnInit, ViewChild } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-modify-holidays',
  templateUrl: './modify-holidays.component.html',
  styleUrls: ['./modify-holidays.component.css']
  //,providers: [I18n, {provide: NgbDatepickerI18n, useClass: CustomLanguageDatepickerI18n}] 
})
export class ModifyHolidaysComponent implements OnInit {

  id : number;
  model = new UpdateHolidayDto();
  date : Date;
  errorDatapicker = '';
  @ViewChild('holidaysForm') holidaysForm : FormGroup;

  constructor(private route : ActivatedRoute, 
              private holidayService : HolidaysService,
              private router : Router,
              private titleService : Title,
              private toastrService : ToastrService,
              private claimService : ClaimsService
              ) { 
                this.titleService.setTitle('Modificar Feriado');
              }

  ngOnInit() {
    if(!this.claimService.haveClaim(this.claimService.canEditHoliday)){
      this.router.navigate(['/notAuthorized']);
    }else{
      this.route.params.subscribe(
        x => this.id = x.id
      );

      this.holidayService.getByIdHoliday(this.id).subscribe(
        x => {
                this.model = x;
              }
      );
    }

  }

  onSubmit() {
    if (this.validateDate()){
      this.model.id = this.id;
      this.holidayService.updateHoliday(this.model).subscribe(
        () => {
          this.router.navigate(['/holidays']);
          this.toastrService.success("La fehca '"+this.model.description+"' se ha modificado correctamente.",'',
          {positionClass : 'toast-top-center', timeOut : 3000});
        },
          () => {
        }      
      );
    }

  }

  validateDate(){
    if (this.model.date &&
      (!this.model.date.day
        || !this.model.date.month
        || !this.model.date.year
        || this.model.date.day > 31 || this.model.date.month < 1
        || this.model.date.month > 12 || this.model.date.month < 1
        || this.model.date.year > 2099 || this.model.date.year < 1912)) {
      this.errorDatapicker = 'Formato de Fecha Incorrecto'
      return false;
    }
    this.errorDatapicker = '';
    return true;
  }

  msjValidEvent(){
  }

  hasUnsavedData(){
    return this.holidaysForm.dirty;
  }

}
