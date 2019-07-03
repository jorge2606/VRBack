import { FormGroup } from '@angular/forms';
import { Title } from '@angular/platform-browser';
import { OrganismService } from 'src/app/_services/organism.service';
import { DistributionService } from './../../_services/distribution.service';
import { CreateDistributionDto } from './../../_models/distributions';
import { Component, OnInit, ViewChild } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { ClaimsService } from 'src/app/_services/claims.service';

@Component({
  selector: 'app-create-distributions',
  templateUrl: './create-distributions.component.html',
  styleUrls: ['./create-distributions.component.css']
})
export class CreateDistributionsComponent implements OnInit {

  model = new CreateDistributionDto();
  error : any;
  selectedOrganismId : number;
  organism : any[];
  @ViewChild('DistributionForm') distributionForm : FormGroup;

  constructor(
              private ditributionService : DistributionService, 
              private organismService : OrganismService,
              private titleService : Title,
              private toastrService : ToastrService,
              private routerService : Router,
              private claimService : ClaimsService
              ) { 
                this.titleService.setTitle('Crear Distribución');
              }

  ngOnInit() {
    if(!this.claimService.haveClaim(this.claimService.canCreateDistribution)){
      this.routerService.navigate(['/notAuthorized']);
    }else{
      this.organismService.getAllOrganism().subscribe(
        x => this.organism = x,
        error => this.error = error
      );
    }


  }

  onSubmit(){
    this.ditributionService.creteDistribution(this.model).subscribe(
      x=>{
          this.routerService.navigate(['/distribution']);
          this.toastrService.success("La distribución '"+this.model.name+"' se ha modificado correctamente.",'',
          {positionClass : 'toast-top-center', timeOut : 3000});
      },
      error => this.error = error.error.notifications
    );
  }

  msjValidEvent(){
    
  }

  hasUnsavedData(){
    return this.distributionForm.dirty;
  }
}
