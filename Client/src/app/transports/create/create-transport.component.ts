import { FormGroup } from '@angular/forms';
import { Component, OnInit, ViewChild } from '@angular/core';
import { TransportService } from 'src/app/_services/transport.service';
import { CreateTransportDto } from 'src/app/_models/transport';
import { Title } from '@angular/platform-browser';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { ClaimsService } from 'src/app/_services/claims.service';

@Component({
  selector: 'app-create-transport',
  templateUrl: './create-transport.component.html',
  styleUrls: ['./create-transport.component.css']
})
export class CreateTransportComponent implements OnInit {

  modelTransport = new CreateTransportDto();
  error = '';
  responseSuccess : any;
  @ViewChild('TransportForm') transportForm : FormGroup;

  constructor(
        private transportService : TransportService,
        private titleService : Title,
        private toastrService :ToastrService,
        private routerService : Router,
        private claimService : ClaimsService
        ) { }

  ngOnInit() {
    this.titleService.setTitle('Crear Transporte');
    
    if(!this.claimService.haveClaim(this.claimService.canCreateTransport)){
      this.routerService.navigate(['/notAuthorized']);
    }
  }

  onSubmit(){
      this.transportService.createTransport(this.modelTransport).subscribe(
          x =>{
            this.modelTransport = this.responseSuccess = x,
            this.modelTransport.id = null,
            this.modelTransport.brand = "";
            this.modelTransport.carPlate = "";
            this.modelTransport.type = "";
            this.modelTransport.model = "";
            this.error = null;
            this.routerService.navigate(['/transport']);
            this.toastrService.success("El transporte '"+this.modelTransport.model +"' se ha guardado correctamente.",'',
              {positionClass : 'toast-top-center', timeOut : 3000});
          }, 
          error => this.error = error.error.notifications
        );
  }

  msjValidEvent(msj : any){
  }

  hasUnsavedData(){
    return this.transportForm.dirty;
  }
}
