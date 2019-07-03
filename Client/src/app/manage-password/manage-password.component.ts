import { Component, OnInit } from '@angular/core';
import { ForgotPassword } from '../_models/passwords';
import { RecoveryPasswordService } from '../_services/recovery-password.service';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-manage-password',
  templateUrl: './manage-password.component.html',
  styleUrls: ['./manage-password.component.css']
})
export class ManagePasswordComponent implements OnInit {

  model = new ForgotPassword();
  error = "";
  constructor(
            private managePassword : RecoveryPasswordService,
            private toastService : ToastrService,
            private spinnerService : NgxSpinnerService
            ) { }

  onSubmit(){
    if (!this.model.email){
      this.error = "ingrese un correo";
      return;
    }

    this.spinnerService.show();
    this.managePassword.ForgotPassword(this.model)
    .subscribe(x =>
      {
        this.toastService.success('Revise su bandeja de entrada, recibira un correo para restaurar su contraseña.');
        this.spinnerService.hide();
      }
      ,e=> {
            if (e.error.errors){
              var err = e.error.errors.Error || [];
              err.forEach(ERROR => {
                this.toastService.error(ERROR,'');
              });
            } 
            this.spinnerService.hide();
      }
      );
  }
  ngOnInit() {
  }

}
