import { ToastrService } from 'ngx-toastr';
import { ActivatedRoute, Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { Roles } from 'src/app/_models/roles';
import { RoleService } from 'src/app/_services/role.service';
import { FormGroup } from '@angular/forms';

@Component({
  selector: 'app-create-roles',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.css']
})
export class CreateRolesComponent implements OnInit {

  id : number;
  model = new Roles();

  constructor(
      private activateRoute : ActivatedRoute,
      private routerService : Router,
      private roleService : RoleService,
      private toastrservice : ToastrService
  ) { }

  ngOnInit() {
    this.activateRoute.params
    .subscribe(n => this.id = n.id);

    if (this.id){
      this.roleService.getById(this.id)
      .subscribe(l => {this.model = l.response}, err => { err });
    }

  }

  onSubmit(){
    this.roleService.create(this.model)
    .subscribe(m => {
      this.routerService.navigate(['/roles/']);
      this.toastrservice.success('El rol '+this.model.name,' se creo correctamente.');
    });
  }

}
