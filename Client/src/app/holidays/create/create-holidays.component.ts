import { FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { Title } from '@angular/platform-browser';
import { HolidaysService } from './../../_services/holidays.service';
import { CreateHolidayDto } from './../../_models/holiday';
import { Component, OnInit, ViewChild } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { ClaimsService } from 'src/app/_services/claims.service';

@Component({
  selector: 'app-create-holidays',
  templateUrl: './create-holidays.component.html',
  styleUrls: ['./create-holidays.component.css']
})
export class CreateHolidaysComponent implements OnInit {

  model = new CreateHolidayDto();
  errors = '';
  errorDatapicker = '';
  @ViewChild('holidaysForm') holidaysForm : FormGroup;

  constructor(
                private holidayService: HolidaysService,
                private titleService :Title,
                private routerService : Router,
                private toastrService : ToastrService,
                private claimService : ClaimsService
                ) { 
                    this.titleService.setTitle('Crear Feriado');
                }

  ngOnInit() {
    if(!this.claimService.haveClaim('holidays.create')){
      this.routerService.navigate(['/notAuthorized']);
    }
  }

  onSubmit() {
    if (!this.validateDate()) {
      return;
    }

    this.errorDatapicker = '';

    this.holidayService.createHoliday(this.model).subscribe(
      () => {
            this.routerService.navigate(['/holidays']);
            this.toastrService.success("La fecha '"+this.model.description+"' se ha guardado correctamente.",'',
              {positionClass : 'toast-top-center', timeOut : 3000});
          },
      errors => this.errors = errors.error.date
    );

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
