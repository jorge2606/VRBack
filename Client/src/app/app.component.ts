import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from './_services/authentication.service';
import { Title } from '@angular/platform-browser';
import { Observable } from 'rxjs/internal/Observable';
import { UserService } from './_services/user.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  
  urlImage = "";
  idUser : number;
  islogged : Observable<boolean>;

  constructor(private authService : AuthenticationService,
              private userService : UserService,
              private titleService : Title
              ){}
  
  ngOnInit(){
    this.titleService.setTitle('Viáticos y Rendiciones');
    this.islogged = this.authService.isLoggedIn;

    this.islogged.subscribe(x => {
      //si el usuario esta logueado
      if(x){
        this.idUser = this.authService.userId('id');
        this.userService.getUserImage(this.idUser, 60,37)
        .subscribe(
          y => {
            this.urlImage = y.response;
          }
        );
      }else{
        this.urlImage = "";
      }
    });
  
  }
}
