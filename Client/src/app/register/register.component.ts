import { Router } from '@angular/router';
import { Register } from './../_models/register';
import { UserService } from './../_services/user.service';
import { Component, OnInit } from '@angular/core';
import { Login } from '../login/login';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  model = new Register();
  errors: any[] = [];
  login = new Login();
  loading: boolean;
  error = '';  
  constructor(private userService : UserService, private router : Router) { }

  onSubmit(){
    this.errors = [];
    this.registerUser();    
  }

  registerUser(){
    this.model.userName = this.model.email;
    this.userService.register(this.model).subscribe(
      result => {
        this.router.navigate(['Home']);
      },
        error => {
          this.errors = error.error.notifications;
        }      
    );
    console.log(this.model);
  }
  
  ngOnInit() {

  }

}
