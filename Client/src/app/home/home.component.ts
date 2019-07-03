import { ClaimsService } from 'src/app/_services/claims.service';
import { AuthenticationService } from './../_services/authentication.service';
import { Component, OnInit } from '@angular/core';
import { UserService } from '../_services/user.service';
import { ActivatedRoute } from '@angular/router';
import { Title } from '@angular/platform-browser';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})

export class HomeComponent implements OnInit {
  id: number;
  roles : any[];
  isAgent : boolean = false;

  constructor(
        private userService: UserService,
        private route : ActivatedRoute,
        private authService : AuthenticationService,
        private titleService : Title,
        private claimsService : ClaimsService) {}

  ngOnInit() {
    this.titleService.setTitle('Inicio');
    
    this.roles = this.authService.userId('rolesNames');
    var result = false;
    this.roles.forEach(ROL => {
      if (ROL.normalizedName == this.claimsService.rolSupervisor.toUpperCase()
          ||
          ROL.normalizedName == this.claimsService.rolMinistro.toUpperCase()
          ||
          ROL.normalizedName == this.claimsService.rolAdmin.toUpperCase()
          ){
          result = false;
      }else{
          result = true;
      }

      this.isAgent = result;
    });;
  }

}



