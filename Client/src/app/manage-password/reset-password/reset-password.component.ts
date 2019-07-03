import { AuthenticationService } from './../../_services/authentication.service';
import { Component, OnInit } from '@angular/core';
import { ResetPassword } from '../../_models/reset-password';
import { ActivatedRoute, Router } from '@angular/router';
import { RecoveryPasswordService } from '../../_services/recovery-password.service';
import { LoginComponent } from '../../login/login.component';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';
import { parse } from '@fortawesome/fontawesome-svg-core';

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: ['./reset-password.component.css']
})
export class ResetPasswordComponent implements OnInit {

  model = new ResetPassword();
  idUserParam : string;
  codeParam : string;
  error: string;
  valid : boolean;
  passwordsAreEquals : Boolean = true;
  passwordEmpty : boolean = true;

  constructor(
      private route: ActivatedRoute, 
      private authService : AuthenticationService,
      private resetPass : RecoveryPasswordService,
      private router : Router,
      private toastrService : ToastrService,
      private spinnerService : NgxSpinnerService) { }

  ngOnInit() {
    this.authService.isLoggedIn.subscribe(
      isLogued =>{
        if(!isLogued){
          //le asigno el id que extraigo de la url
          var url = new String(this.router.url);
          this.codeParam = url.substr(url.indexOf("code") + 5,url.indexOf("&userId")-22);
          this.idUserParam = url.substr(url.indexOf("userId") + 7,url.length);
          /** this.route.params.subscribe(
            x => {
              this.idUserParam = x.userId,
              this.codeParam = x.code
            }
          );*/
        }
      }
    );

  }

  onSubmit(){
    if (!this.model.password){
      this.error = "Ingrese una contraseña";
      return;
    }
    if (!this.model.passwordConfirm){
      this.error = "Confirme su contraseña";
      return;
    }

    if (this.model.password != this.model.passwordConfirm){
      this.error = "Las contraseñas no coinciden.";
      return;
    }
    this.model.userId = this.idUserParam;
    this.model.passwordResetToken = this.codeParam;
    this.spinnerService.show();
    this.resetPass.ResetPassword(this.model)
    .subscribe(
      x =>{
        this.spinnerService.hide();
        this.router.navigate([LoginComponent]);
    },error=>{
      if (error.error.notifications){
        var e = error.error.notifications || [];
        e.forEach(element => {
            this.toastrService.error(element.value,'');
        });
      }

      if(error.error.errors){
        var err = error.error.errors.Error || [];
        err.forEach(element => {
          this.toastrService.error(element,'');
        });
      }
      this.spinnerService.hide();
    });
    
  }

  comparePassword(){
    if (!this.model.password && !this.model.passwordConfirm){
      this.passwordEmpty = true;
      this.passwordsAreEquals = true;
      return;
    }
    
    this.passwordsAreEquals = this.model.password === this.model.passwordConfirm;
    this.passwordEmpty = false;
    return;
  }

}
