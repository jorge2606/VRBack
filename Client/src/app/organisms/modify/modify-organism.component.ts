import { ClaimsService } from 'src/app/_services/claims.service';
import { FormGroup } from '@angular/forms';
import { Title } from '@angular/platform-browser';
import { Component, OnInit, ViewChild } from '@angular/core';
import { UpdateOrganismDto } from 'src/app/_models/organism';
import { OrganismService } from 'src/app/_services/organism.service';
import { Router, ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-modify-organism',
  templateUrl: './modify-organism.component.html',
  styleUrls: ['./modify-organism.component.css']
})
export class ModifyOrganismComponent implements OnInit {

  model = new UpdateOrganismDto();
  id : number;
  error = '';
  @ViewChild('OrganismForm') organismForm : FormGroup;

  constructor(
              private organismService : OrganismService,
              private route : ActivatedRoute,
              private routerservice : Router,
              private titleService : Title,
              private toastrService : ToastrService,
              private claimService : ClaimsService
              ) { 
                this.titleService.setTitle('Modificar Organismo');
              }

  ngOnInit() {
    if(!this.claimService.haveClaim(this.claimService.canEditOrganism)){
      this.routerservice.navigate(['/notAuthorized']);
    }else{
      this.route.params.subscribe(
        p => this.id = p.id
      );

      this.organismService.findById(this.id).subscribe(
        x => {this.model.id = x.id, this.model.name = x.name, this.model.description = x.description}
      );
    }

  }

  onSubmit(){
    this.organismService.updateOrganism(this.model).subscribe(
      x=> {
        this.routerservice.navigate(['/organism']);
        this.toastrService.success("El organismo '"+this.model.name+"' se ha modificado correctamente.",'',
        {positionClass : 'toast-top-center', timeOut : 3000});
      },
      error => this.error = error.error.notifications
    );
  }


  msjValidEvent(msj : any){
  }
  
  hasUnsavedData(){
    return this.organismForm.dirty;
  }

}
