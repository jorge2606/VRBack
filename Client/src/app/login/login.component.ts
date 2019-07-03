import { NgxSpinnerService } from 'ngx-spinner';
import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Login } from './login';
import { AuthenticationService } from '../_services/authentication.service';
import { first } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { ToastrService } from 'ngx-toastr';
import { Title } from '@angular/platform-browser';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup;
  loading = false;
  submitted = false;
  returnUrl: string;
  notifications = '';  
  logout : boolean;
  

  constructor(
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private authenticationService: AuthenticationService,
    private spinnerService : NgxSpinnerService,
    private titleService : Title
    ) { }

    model = new Login();
    isLogged : Observable<boolean>;

   // convenience getter for easy access to form fields
   get f() { return this.loginForm.controls; }

   onSubmit() {
       this.spinnerService.show();
       this.submitted = true;
       this.loading = true;
       this.authenticationService.login(this.model)
           .pipe(first())
           .subscribe(
               () => {
                   this.authenticationService.userId('rolesNames').forEach(rol => {
                       if(rol.normalizedName == "AGENTE" || rol.normalizedName == "MINISTRO"){
                            this.returnUrl = "SolicitationSubsidy/agent";
                       }
                       if(rol.normalizedName == "SUPERVISOR"){
                            this.returnUrl = "SolicitationSubsidy/agent/solicitationSubsidies/true"
                       }
                       if(rol.normalizedName == "ADMINISTRADOR"){
                        this.returnUrl = "users"
                       }    
                   });
                   this.spinnerService.hide();
                   this.router.navigate([this.returnUrl]);
               },
               error => {
                   this.spinnerService.hide();
                   this.notifications = error.error.notifications;
                   this.loading = false;
               });
   }

    ngOnInit() {
        this.titleService.setTitle('Viáticos y Rendiciones');
        this.authenticationService.isLoggedIn
        .subscribe(x => {
            this.logout = x;

            if(!this.logout){
                this.loginForm = this.formBuilder.group({
                    username: ['', Validators.required],
                    Password: ['', Validators.required]
                  });
                  // get return url from route parameters or default to '/'
                  this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
            }else{
                this.router.navigate(['']);
            }
        });
    }

}
