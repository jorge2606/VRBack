import { ActivatedRoute, Router } from '@angular/router';
import { LegalRulingsService } from './../../_services/legal-rulings.service';
import { Component, OnInit } from '@angular/core';
import { LegalRulingsBaseDto } from 'src/app/_models/legalRuling';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-create-legal-ruling',
  templateUrl: './create-legal-ruling.component.html',
  styleUrls: ['./create-legal-ruling.component.css']
})
export class CreateLegalRulingComponent implements OnInit {

  model : LegalRulingsBaseDto = new LegalRulingsBaseDto();
  id : number;
  constructor(
    private legalRulingService : LegalRulingsService,
    private toastrService : ToastrService,
    private activateRoute : ActivatedRoute,
    private routerService : Router
  ) { }

  ngOnInit() {
    this.activateRoute.params
    .subscribe(url => {
      this.id = url.id;
      if(this.id){
        this.legalRulingService.getById(this.id)
        .subscribe(leg => this.model = leg.response);
      }
    });
  }

  onSubmit(){
    this.legalRulingService.create(this.model)
    .subscribe(l => {
        this.toastrService.success('La resolución '+this.model.description+' '+this.model.number+' se guardó correctamente.');
        this.routerService.navigateByUrl("/administrativeResolution");
    });
  }

}
