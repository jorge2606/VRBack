import { ClaimsService } from './../_services/claims.service';
import { AuthenticationService } from 'src/app/_services/authentication.service';
import { Title } from '@angular/platform-browser';
import { ActivatedRoute, Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { ExpenditureService } from '../_services/expenditure.service';
import { ExpenditureTypeBaseDto } from '../_models/expenditureType';
import { NgbdModalContent } from '../modals/modals.component';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-expenditures',
  templateUrl: './expenditures.component.html',
  styleUrls: ['./expenditures.component.css']
})
export class ExpendituresComponent implements OnInit {

  page = 0;
  col_size : number;
  itemsPerPage : number = 10;
  expenditures : ExpenditureTypeBaseDto[];
  error : any;
  id : number;
  permission : any[] = [];
  expenditureCreate: any;
  expenditureEdit: any;
  expenditureDelete: any;
  constructor(
            private expenditureService : ExpenditureService,
            private modalService : NgbModal,
            private titleService : Title,
            private toastrService : ToastrService,
            private authService : AuthenticationService,
            private claimService : ClaimsService,
            private routerService : Router
            ) { 
              this.titleService.setTitle('Tipos de Gastos');
            }

  ngOnInit() {
    if(!this.claimService.haveClaim(this.claimService.canViewExpenditure)){
      this.routerService.navigate(['/notAuthorized']);
    }else{
      this.getAllExpenditure(this.page); 
    }
    
  }

  loadPage(page : number){
    if (page > 0){
      page = page - 1;
    }
    this.getAllExpenditure(page);
  }

  getAllExpenditure(page : number){
    this.expenditureService.getPaginator(page).subscribe(
      result => {
        this.expenditures = result.list;
        this.col_size = result.totalRecords;
        this.permission = this.authService.userId('roles');
        this.expenditureCreate = this.permission.find(x => x.value == 'expenditures.create');
        this.expenditureEdit = this.permission.find(x => x.value == 'expenditures.edit');
        this.expenditureDelete = this.permission.find(x => x.value == 'expenditures.delete');
      },
      error => console.log(error)
    ); 
  }

  openEliminar(expenditureId: number, name: string, descp: string) {
    const modalRef = this.modalService.open(NgbdModalContent,{
      backdrop : 'static',
      keyboard : false
    });
    modalRef.componentInstance.Encabezado = "Eliminar";
    modalRef.componentInstance.Contenido = "¿Desea eliminar el Gasto : " + name + " " + descp + "?";
    modalRef.componentInstance.GuardaroEliminar = "Eliminar";
    modalRef.componentInstance.GuardaroEliminarClass = "btn-danger";
    modalRef.componentInstance.MsgClose = "Cancelar";
    modalRef.result.then(() => {
      this.expenditureService.deleteExpenditure(expenditureId).subscribe(
        data => {
            this.toastrService.success("El concepto de gasto '"+name+"' se ha eliminado correctamente.",'',
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

}
