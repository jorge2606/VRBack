import { FormGroup } from '@angular/forms';
import { Title } from '@angular/platform-browser';
import { Component, OnInit, ViewChild } from '@angular/core';
import { CreateExpenditureDto } from 'src/app/_models/expenditureType';
import { ExpenditureService } from 'src/app/_services/expenditure.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { ClaimsService } from 'src/app/_services/claims.service';

@Component({
  selector: 'app-create-expenditure',
  templateUrl: './create-expenditure.component.html',
  styleUrls: ['./create-expenditure.component.css']
})
export class CreateExpenditureComponent implements OnInit {

  model = new CreateExpenditureDto();
  error = '';
  @ViewChild('CategoryForm') categoryForm : any;
  percentageValid : boolean;

  constructor(
            private expenditureService : ExpenditureService,
            private titleService : Title,
            private toastrService : ToastrService,
            private routerService : Router,
            private claimService : ClaimsService
            ) {
              this.titleService.setTitle('Crear Tipo de Gasto');
              this.toastrService.toastrConfig.maxOpened = 1;
              this.toastrService.toastrConfig.positionClass = "toast-top-center";
              this.toastrService.toastrConfig.timeOut = 1000;
             }

  ngOnInit() {
    if(!this.claimService.haveClaim(this.claimService.canCreateExpenditure)){
      this.routerService.navigate(['/notAuthorized']);
    }
  }

  onSubmit(){
    this.expenditureService.createExpenditure(this.model).subscribe(
      x=>{
          this.toastrService.success("El concepto de gasto '"+this.model.name+"' se ha guardado correctamente.",'',
          {positionClass : 'toast-top-center', timeOut : 3000});
          this.routerService.navigate(['/expenditure']);
          this.error = '';
        },
      error => this.error = error.error.notifications
    );
  }

  
  change(value : any, model : CreateExpenditureDto){
    model.outsideProvince = value;
    this.model.percentage = undefined;
  }

  hasUnsavedData(){
    return this.categoryForm.dirty && !this.categoryForm.submitted;
  }

  changePercentage(event : string){
    if (event.length == 3){
      var fisrtValue = event[0];
      var twoLastValues = event.substring(1,3);
      switch(fisrtValue){
        case "0":
              { if (twoLastValues == "00"){this.toastrService.warning('El porcentaje debe ser mayor a 0.00'); this.percentageValid = false;} 
                else {
                    this.model.percentage = fisrtValue+"."+twoLastValues;
                    this.percentageValid = true;
                }
              break; }
        case "1":
              { if (twoLastValues != "00"){this.toastrService.warning('El porcentaje no puede ser mayor a 1.00');this.percentageValid = false;} 
                else {              
                  this.model.percentage = fisrtValue+"."+twoLastValues;      
                  this.percentageValid = true;
                }
              break;}
        default : {
              this.toastrService.warning('El porcentaje no puede ser mayor a '+fisrtValue+"."+twoLastValues);
              this.percentageValid = false;
        }
      }
    }else{
      this.percentageValid = false;
    }
  }
}
