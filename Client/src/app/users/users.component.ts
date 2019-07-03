import { ClaimsService } from './../_services/claims.service';
import { FormGroup } from '@angular/forms';
import { CreateuserComponent } from './create/create.component';
import { Title } from '@angular/platform-browser';
import { UserRoles } from 'src/app/_models/userRoles';
import { AddSupervisorComponent } from './../modals/add-supervisor/add-supervisor.component';
import { DistributionBaseDto } from 'src/app/_models/distributions';
import { Roles, RoleUserDto } from './../_models/roles';
import { NgbdModalContent } from './../modals/modals.component';
import { NgbModal, NgbModalOptions } from '@ng-bootstrap/ng-bootstrap';
import { Component, OnInit, ViewChild } from '@angular/core';
import { UserService } from '../_services/user.service';
import { User } from './users';
import { DistributionService } from '../_services/distribution.service';
import { ActivatedRoute, Router } from '@angular/router';
import { AspNetRolesService } from '../_services/asp-net-roles.service';
import { AspNetUsersRolesService } from '../_services/asp-net-users-roles.service';
import { AllAspNetRolesDto } from '../_models/aspNetRoles';
import { ToastrService } from 'ngx-toastr';
import { AuthenticationService } from '../_services/authentication.service';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
})

export class UsersComponent implements OnInit {
  filters = { page: 0, distributionId: 0, dni : "", firstName : "" }
  user_list: User[];
  allUsers : User[];
  roles_list: Roles;
  col_size: number;
  itemsPerPage: number = 10;
  value: number;
  distributions: DistributionBaseDto[];
  displayedColumns = ['dni', 'userName'];
  changeRolDto = new RoleUserDto();
  users_check : User[] = [];
  allCheckedProperty = false;
  userRoles : RoleUserDto[];
  roles : AllAspNetRolesDto[]
  user_list_length: number;
  textListEmpty : string = "No se encontró ningún usuario";
  classListEmpty : string = "alert-primary";
  create: any;
  addSupervisor: any;
  toSeeRelationshipBeetwenSupervisorAndAgent: any;
  edit: any;
  delete: any;
  page = 0;
  ngbModalOptions: NgbModalOptions = {
    backdrop : 'static',
    keyboard : false
  };


  constructor(
    private var_user_service: UserService, 
    private authService : AuthenticationService,
    private modalService: NgbModal,
    private distributionService: DistributionService,
    private route: ActivatedRoute,
    private aspNetRolesService : AspNetRolesService,
    private aspNetUsersRolesService : AspNetUsersRolesService,
    private titleService : Title,
    private router : Router,
    private toastrService : ToastrService,
    private claimService : ClaimsService) {}


  ngOnInit() {
    this.titleService.setTitle('Usuarios');
    if(!this.claimService.haveClaim(this.claimService.canViewUsers)){
      this.router.navigate(['/notAuthorized']);
    }else{
      this.create = this.claimService.haveClaim(this.claimService.canCreateUsers);
      this.addSupervisor = this.claimService.haveClaim(this.claimService.canAddSupervisorUsers);
      this.toSeeRelationshipBeetwenSupervisorAndAgent = this.claimService.haveClaim(this.claimService.canToSeeRelationshipBeetwenSupervisorAndAgent);
      this.edit = this.claimService.haveClaim(this.claimService.canEditUsers);
      this.delete = this.claimService.haveClaim(this.claimService.canDeleteUsers);
      this.allUsersWithInPage();
      this.allAspNetRolesService();
      this.allAspNetUserRolesService();
      //le asigno el id que extraigo de la url
      this.route.params.subscribe(
        p => {
            if (p.distributionId)
            {
              this.filters.distributionId = p.distributionId
            }
          }
      );
        
      this.distributionService.allDistribution().subscribe(
        x => {
          this.distributions = x;
          this.getAllUsers(this.filters);
        }
      );
    }

  }

  allAspNetRolesService(){
    this.aspNetRolesService.getAll().subscribe(
      roles => {
        this.roles = roles
      }
    );
  }

  allAspNetUserRolesService(){
    this.aspNetUsersRolesService.onlyRolesUsersRoles()
    .subscribe(
        userRoles =>this.userRoles = userRoles
      );
  }
  allUsersWithInPage(){
    this.var_user_service.getAll().subscribe(
      x => this.allUsers = x
    );
  }

  loadPage(page : number) {
    if (page > 0) {
      this.filters.page = page - 1;
    }
    this.getAllUsers(this.filters);
  }

  filterList() {
    this.loadPage(this.filters.page);
  }

  findWhileWrite(){
    this.loadPage(this.filters.page);
  }

  getAllUsers(filters: any): void {
    this.var_user_service.getPaginator(filters).subscribe(result => {
      this.user_list = result.list;
          this.user_list_length = this.user_list.length;
          this.users_check.forEach(
            users_checked=> {
                this.user_list.forEach(
                    user_list => {
                      if (user_list.id == users_checked.id){
                          user_list.checked = users_checked.checked;
                      }
                    }
                );
            }
          );
      this.retriveRoles();
      this.col_size = result.totalRecords;
    });
  }
  //MODALS
  openEliminar(id: number, dni: number, usuario: string) {
    const modalRef = this.modalService.open(NgbdModalContent,this.ngbModalOptions);
    modalRef.componentInstance.Encabezado = "Eliminar";
    modalRef.componentInstance.Contenido = "¿Desea eliminar a " + dni + " " + usuario + "?";
    modalRef.componentInstance.GuardaroEliminar = "Eliminar";
    modalRef.componentInstance.MsgClose = "Cancelar";
    modalRef.componentInstance.GuardaroEliminarClass="btn-danger";
    modalRef.result.then(() => {
      this.var_user_service.deleteUser(id).subscribe(
        data => {
          this.toastrService.success("El usuario '"+usuario+"' se ha eliminado correctamente.",'',
          {positionClass : 'toast-top-center', timeOut : 3000});
          this.loadPage(this.page);
        },
        error => {
          console.log("error", error);
        }
      );
    },
      () => {
        console.log('Backdrop click');
      })
  }

  check(user : any){
    this.allCheckedProperty = true;
    if (this.users_check.length < this.allUsers.length - 1){
      this.allCheckedProperty = false;
    }

    if (user.checked == true){
      this.users_check.push(user);
    }else{
      if (this.users_check.find(x => x.id == user.id)){
        var userDelete = this.users_check.find(x => x.id == user.id);
        userDelete.checked = user.checked;
        const indexDeleteAll = this.users_check.indexOf(userDelete, 0);
        if (indexDeleteAll > -1) {
          this.users_check.splice(indexDeleteAll, 1);
          this.allCheckedProperty = false;
        }
      }
    }
  }

  AddSupervisor(){
    this.ngbModalOptions.size='lg';
    const modalRef = this.modalService.open(AddSupervisorComponent,this.ngbModalOptions);
    modalRef.componentInstance.usersSelected = this.users_check;
    modalRef.componentInstance.allUsers = this.allUsers;
    modalRef.result.then(
        data => {
          this.users_check = [];
          this.user_list.forEach(x => {
            x.checked = false;
          });
          console.log("data", data);
        },
        error => {
          console.log("error", error);
        })
  }

  allChecked(){
    this.users_check = [];
    this.allCheckedProperty = !this.allCheckedProperty;

      if (this.allCheckedProperty){
            this.var_user_service.getAll().subscribe(
              result => {
                this.users_check = result;
                this.users_check.forEach(
                  x => {
                    x.checked = this.allCheckedProperty;
                  }
                );
                this.users_check.forEach(
                  users_checked=> {
                      this.user_list.forEach(
                          user_list => {
                            if (user_list.id == users_checked.id){
                                user_list.checked = users_checked.checked;
                            }
                          }
                      );
                  }
                );
            });
      }else{
        this.user_list.forEach(
          user_list => {
            user_list.checked = false;
          }
        );
      }
  }


  retriveRoles(){
    this.user_list.forEach(
      user =>{
         user.rol = "";
          this.userRoles.forEach(
              userRol =>{
                if(user.id == userRol.userId){
                  this.roles.forEach(
                    rol => {
                      if (rol.id == userRol.roleId){
                        user.rol = user.rol +" "+rol.normalizedName;
                      }
                    }//rol
                  );
                }
              }//userRol
          );
      }//users
    );
  }
}
