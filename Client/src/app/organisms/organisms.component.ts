import { AuthenticationService } from './../_services/authentication.service';
import { Title } from '@angular/platform-browser';
import { CreateOrganismDto } from './../_models/organism';
import { Component, OnInit } from '@angular/core';
import { OrganismService } from '../_services/organism.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NgbdModalContent } from '../modals/modals.component';
import { ToastrService } from 'ngx-toastr';
import { ClaimsService } from '../_services/claims.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-organisms',
  templateUrl: './organisms.component.html',
  styleUrls: ['./organisms.component.css']
})
export class OrganismsComponent implements OnInit {

  filters = { page: 0, name : ""}
  page = 0; 
  organism : CreateOrganismDto[];
  col_size : number;
  itemsPerPage : number = 10;
  textListEmpty : string = "No se encontró ningún organismo";
  classListEmpty : string = "alert-primary";
  permissions : any[] = [];
  organimCreate: any;
  organimEdit: any;
  organimDelete: any;
  organism_list_length : number;

  constructor(
              private organismService : OrganismService,
              private modalService : NgbModal,
              private titleService : Title,
              private toastrService : ToastrService,
              private authService : AuthenticationService,
              private claimService : ClaimsService,
              private routerSerivice : Router
              ) { 
                this.titleService.setTitle('Organismos');
              }

  ngOnInit() {
    if(!this.claimService.haveClaim(this.claimService.canViewOrganism)){
      this.routerSerivice.navigate(['/notAuthorized']);
    }else{
      this.getAllOrganism(this.filters); 
    }
    
  }

  getAllOrganism(page : any){
    this.organismService.getPaginator(this.filters).subscribe(
      result => {
        this.organism = result.list;
        this.organism_list_length = this.organism.length;
        this.col_size = result.totalRecords;
        this.permissions = this.authService.userId('roles');
        this.organimCreate = this.permissions.find(x => x.value == 'organisms.create');
        this.organimEdit = this.permissions.find(x => x.value == 'organisms.edit');
        this.organimDelete = this.permissions.find(x => x.value == 'organisms.delete');
      },
      error => console.log(error)
    ); 
  }

  loadPage(page : number){
    if (page > 0){
      this.filters.page = page - 1;
    }
    this.getAllOrganism(this.filters);
    
  }

  openEliminar(id: number, name: string, descp: string) {
    const modalRef = this.modalService.open(NgbdModalContent,{
      backdrop : 'static',
      keyboard : false
    });
    modalRef.componentInstance.Encabezado = "Eliminar";
    modalRef.componentInstance.Contenido = "¿Desea eliminar el Organismo : " + name + " " + descp + "?";
    modalRef.componentInstance.GuardaroEliminar = "Eliminar";
    modalRef.componentInstance.GuardaroEliminarClass = "btn-danger";
    modalRef.componentInstance.MsgClose = "Cancelar";
    modalRef.result.then(() => {
      this.organismService.deleteOrganism(id).subscribe(
        data => {
            this.toastrService.success("El organismo '"+name+"' se ha eliminado correctamente.",'',
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

  findWhileWrite(){
    this.loadPage(this.filters.page);
  }

}
