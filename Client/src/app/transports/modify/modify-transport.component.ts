import { ClaimsService } from './../../_services/claims.service';
import { FormGroup } from '@angular/forms';
import { TransportService } from './../../_services/transport.service';
import { Component, OnInit, ViewChild } from '@angular/core';
import { UpdateTransportDto } from 'src/app/_models/transport';
import { ActivatedRoute, Router } from '@angular/router';
import { Title } from '@angular/platform-browser';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-modify-transport',
  templateUrl: './modify-transport.component.html',
  styleUrls: ['./modify-transport.component.css']
})
export class ModifyTransportComponent implements OnInit {

  modelTransport = new UpdateTransportDto();
  id : number;
  error = '';
  @ViewChild('TransportForm') transportForm : FormGroup;

  constructor(private route : ActivatedRoute,
    private router : Router,
    private tranportService : TransportService,
    private titleService : Title,
    private toastrService : ToastrService,
    private claimService : ClaimsService
    ) { }

  ngOnInit() {
    this.titleService.setTitle('Modificar Transporte');
    if(!this.claimService.haveClaim(this.claimService.canEditTransport)){
      this.router.navigate(['/notAuthorized']);
    }else{
      this.route.params.subscribe(
        p => this.id = p.id
      );

      this.tranportService.findByIdTransport(this.id).subscribe(
        x => {
          this.modelTransport.id = x.id;
          this.modelTransport.brand = x.brand;
          this.modelTransport.carPlate = x.carPlate;
          this.modelTransport.model = x.model;
          this.modelTransport.type = x.type;
          }
      );
    }
  }

  onSubmit() {
    this.modelTransport.id = this.id;
    this.tranportService.updateTransport(this.modelTransport).subscribe(
      x => {
        this.router.navigate(['/transport']);
        this.toastrService.success("El transporte '"+this.modelTransport.model+"' se ha modificado correctamente.",'',
        {positionClass : 'toast-top-center', timeOut : 3000});
      },
        error => {
          this.error = error.error.notifications
      }      
    );
    //this.router.navigate(['/distribution']);
  }

  msjValidEvent(msj : any){

  }

  hasUnsavedData(){
    return this.transportForm.dirty;
  }
}
