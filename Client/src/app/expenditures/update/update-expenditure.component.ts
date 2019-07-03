import { ClaimsService } from 'src/app/_services/claims.service';
import { FormGroup } from '@angular/forms';
import { Title } from '@angular/platform-browser';
import { Component, OnInit, ViewChild } from '@angular/core';
import { UpdateExpenditureDto, CreateExpenditureDto } from 'src/app/_models/expenditureType';
import { ExpenditureService } from 'src/app/_services/expenditure.service';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-update-expenditure',
  templateUrl: './update-expenditure.component.html',
  styleUrls: ['./update-expenditure.component.css']
})
export class UpdateExpenditureComponent implements OnInit {

  model = new UpdateExpenditureDto();
  error = '';
  id : number;
  @ViewChild('CategoryForm') categoryForm : any;
  percentageValid : boolean;
  onLoadPercentege : boolean;

  constructor(
              private expenditureService : ExpenditureService,
              private route : ActivatedRoute,
              private router : Router,
              private titleService : Title,
              private routerService : Router,
              private toastrService : ToastrService,
              private claimService : ClaimsService) { 
                this.titleService.setTitle('Modificar Tipo de Gasto');
                this.toastrService.toastrConfig.maxOpened = 1;
                this.toastrService.toastrConfig.positionClass = "toast-top-center";
                this.toastrService.toastrConfig.timeOut = 1000;
              }

  ngOnInit() {
    if(!this.claimService.haveClaim(this.claimService.canEditExpenditure)){
      this.router.navigate(['/notAuthorized']);
    }else{
      this.onLoadPercentege = false;
      this.route.params.subscribe(
        p => {this.id = p.id;}
      );

      this.expenditureService.findByIdExpenditure(this.id).subscribe(
        x => {this.model = x;}
      );
    }

  }

  onSubmit(){
    this.expenditureService.updateExpenditure(this.model).subscribe(
      x=> {
            this.toastrService.success("El concepto de gasto '"+this.model.name+"' se ha modificado correctamente.",'',
            {positionClass : 'toast-top-center', timeOut : 3000});
            this.routerService.navigate(['/expenditure']);
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
    this.onLoadPercentege = true;
    if (event.length == 3){
      var fisrtValue = event[0];
      var twoLastValues = event.substring(1,3);
      switch(fisrtValue){
        case "0":
              { if (twoLastValues == "00"){
                  this.toastrService.warning('El porcentaje debe ser mayor a 0.00'); this.percentageValid = false;
                } 
                else {
                  this.model.percentage = fisrtValue+"."+twoLastValues;
                  this.percentageValid = true;
                }
              break; }
        case "1":
              { if (twoLastValues != "00"){
                  this.toastrService.warning('El porcentaje no puede ser mayor a 1.00');this.percentageValid = false;
                } else {
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
