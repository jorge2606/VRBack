import { FormGroup } from '@angular/forms';
import { AuthenticationService } from './../../_services/authentication.service';
import { DistributionBaseDto } from 'src/app/_models/distributions';
import { ToastrService } from 'ngx-toastr';
import { Title } from '@angular/platform-browser';
import { Component, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { UserService } from '../../_services/user.service';
import { modifyUser } from '../users';
import { OrganismService } from 'src/app/_services/organism.service';
import { DistributionService } from 'src/app/_services/distribution.service';
import { OrganismBaseDto } from 'src/app/_models/organism';

@Component({
  selector: 'app-settingofuser',
  templateUrl: './settingofuser.component.html',
  styleUrls: ['./settingofuser.component.css']
})
export class SettingofuserComponent implements OnInit {
  model = new modifyUser;
  submitted : boolean;
  passwordsAreEquals : Boolean = true;
  passwordEmpty : boolean = true;
  currentUrl : string;
  organismList : OrganismBaseDto[] = [];
  distributionList : DistributionBaseDto[]=[];
  selectedorganismId : number;
  selecteddistributionId : number;
  validCheckbox : boolean = true;
  permissions: any[] = [];
  editRol: any;
  editSignatureHolograpich: any;
  @ViewChild('userForm') userForm : FormGroup;
  

  constructor(
            private router : Router,
            private userService: UserService,
            private titleService : Title,
            private toastrService : ToastrService,
            private organimService : OrganismService,
            private distributionService : DistributionService,
            private authService : AuthenticationService
  ){
    this.currentUrl = this.router.url;
  }


  onChange(rol){
    console.log(rol.rolBelongUser);
  }

  changeStateCheckbox(rol : any){
    this.validCheckbox = false;

    if (rol.rolBelongUser){
      rol.rolBelongUser = !rol.rolBelongUser;
      this.model.rolesUser.forEach(
        x => {
            var result = x.rolBelongUser.toString() == "true" ? true : false;
            if(result){
              this.validCheckbox = true;
              return;
            }
        }
      );
  
      if (!this.validCheckbox){
        this.toastrService.info('Debe seleccionar al menos un rol','',{timeOut : 1000, positionClass : 'toast-top-center'});
        this.submitted = false;
        return;
      }
    }
    
    this.validCheckbox = true;
    return;

  }

   onSubmit() {
    this.submitted = true;

    this.validCheckbox = false;
    this.model.rolesUser.forEach(
      x => {
          if(x.rolBelongUser){
            console.log(x.rolBelongUser)
            this.validCheckbox = true;
          }
      }
    );

    if (!this.validCheckbox){
      this.toastrService.info('Debe seleccionar al menos un rol','',{timeOut : 1000, positionClass : 'toast-top-center'});
      this.submitted = false;
      return;
    }

    if (!this.validateCuil()){
      this.toastrService.info('El número de cuit/cuil no es válido');
      this.submitted = false;
      return;
    }

    if (this.model.password || this.model.repeatPassword){
        if (this.model.password !== this.model.repeatPassword){
          this.passwordsAreEquals = false;
            this.toastrService
            .error('Las contraseñas no coinciden','',{timeOut : 3000, positionClass : 'toast-top-center'})
          this.submitted = false;
        }else{
          this.passwordsAreEquals = true;
        }
    }

    if (this.model.dni.length < 11){
        this.toastrService.info('Faltan 1 o más dígitos en el campo Dni','',{timeOut : 1000, positionClass : 'toast-top-center'});
        this.submitted = false;
    }

    if (!this.submitted){
      return;
    }
    
    this.userService.updateProfileUsers(this.model).subscribe(
      () => {
        this.toastrService
          .success('El perfil se actualizó correctamente','',{timeOut : 3000, positionClass : 'toast-top-center'})
          this.router.navigate([this.currentUrl]);
      },
        error => {
         console.log(error);
         this.router.navigate([this.currentUrl]);
        }      
    );
  }

  getOrganismAll(){
    this.organimService.getAllOrganism()
    .subscribe(
      x => this.organismList = x
    );
  }

  getDistributionAll(){
    this.distributionService.allDistribution()
    .subscribe(
      x => this.distributionList = x
    );
  }
  
  ngOnInit() {
    this.titleService.setTitle('Mi Perfil');
    this.permissions = this.authService.userId('roles');
    this.editSignatureHolograpich = this.permissions.find(x => x.value == 'user.editSignatureHolograpichAsSupervisor');
    this.editRol = this.permissions.find(x => x.value == 'roles.edit');
    this.getOrganismAll();
    this.userService.getById().subscribe(i => {
        this.model.dni = i.dni,
        this.model.userName = i.userName,
        this.model.id = i.id,
        this.model.phoneNumber = i.phoneNumber,
        this.model.rolesUser = i.rolesUser,
        this.model.firstName = i.firstName,
        this.model.lastName = i.lastName,
        this.model.distributionId = i.distributionId,
        this.model.organismId = i.organismId,
        this.model.categoryId = i.categoryId,
        this.selectDistribution(this.model.organismId);
    })
  }

  setTitleTab(){
    this.titleService.setTitle('Mi Perfil');
  }

  comparePassword(){
    if (!this.model.password && !this.model.repeatPassword){
      this.passwordEmpty = true;
      this.passwordsAreEquals = true;
      return;
    }
    
    this.passwordsAreEquals = this.model.password === this.model.repeatPassword;
    this.passwordEmpty = false;
    return;
  }

  validateCuil(){
    var dniArray : number[] = [0,0,0,0,0,0,0,0,0,0];
    var cuit : string = this.model.dni;
    var base = [5, 4, 3, 2, 7, 6, 5, 4, 3, 2];
    var result = 0;
    var codVerificacion = parseInt(cuit.charAt(10));

    dniArray.forEach((item,index) => {
        result = result + (base[index] * parseInt(cuit.charAt(index)));
    });
    codVerificacion = 11 - (result % 11);
    if (codVerificacion == 11){
      codVerificacion = 0;
    }
    return parseInt(cuit.charAt(10)) == codVerificacion;
  }

  selectDistribution(organismId : number){
    var nameOrganim = "";
    if (this.organismList.find(x => x.id == organismId)){
        nameOrganim = this.organismList.find(x => x.id == organismId).name;
    }
    this.distributionService.findByIdOrganism(organismId)
    .subscribe(
      x => 
        {
          this.distributionList = x;
          if (this.distributionList.length < 1){
            this.toastrService.info('El organismo '+nameOrganim+' no tiene reparticiones.','',{timeOut : 2000, positionClass : 'toast-top-center'})
          }else{
            this.model.distributionId = this.distributionList[0].id;
          }
        }
    );
  }

  hasUnsavedData(){
      if(this.userForm){
        return this.userForm.dirty;
      }
      return false;
  }
}