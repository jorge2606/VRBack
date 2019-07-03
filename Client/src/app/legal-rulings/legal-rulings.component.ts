import { ToastrService } from 'ngx-toastr';
import { LegalRulingsService } from './../_services/legal-rulings.service';
import { LegalRulingsBaseDto } from './../_models/legalRuling';
import { Component, OnInit } from '@angular/core';
import { NgbdModalContent } from '../modals/modals.component';
import { NgbModalOptions, NgbModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-legal-rulings',
  templateUrl: './legal-rulings.component.html',
  styleUrls: ['./legal-rulings.component.css']
})
export class LegalRulingsComponent implements OnInit {

  page = 0;
  legal_list_length : number;
  textListEmpty : string = "No se encontró ningún usuario";
  classListEmpty : string = "alert-primary";
  legalRulings : LegalRulingsBaseDto[];
  col_size: number;
  itemsPerPage: number = 10;
  filters = {number : "", description : "", date : null, page : 0};
  ngbModalOptions: NgbModalOptions = {
    backdrop : 'static',
    keyboard : false
  };

  constructor(
      private legalRulingsService : LegalRulingsService,
      private modalService: NgbModal,
      private toastrService : ToastrService
      ) { }

  ngOnInit() {
    this.getPage(this.filters);
  }

  clear(){
    this.filters.date = {day : 0 , month : 0, year : 0};
    this.getPage(this.filters);
  }

  getPage(filters : any){
    this.legalRulingsService.page(filters)
    .subscribe(l => 
      {
        this.legalRulings = l.list;
        this.legal_list_length = this.legalRulings.length;
        this.col_size = l.totalRecords;
      });
  }

  findWhileWrite(){
    this.getPage(this.filters);
  }

  loadPage(page : number) {
    if (page > 0) {
      this.filters.page = page - 1;
    }
    this.getPage(this.filters);
  }

  openEliminar(legal : any) {
    const modalRef = this.modalService.open(NgbdModalContent,this.ngbModalOptions);
    modalRef.componentInstance.Encabezado = "Eliminar";
    modalRef.componentInstance.Contenido = "¿Desea eliminar a " + legal.number + " " + legal.description + "?";
    modalRef.componentInstance.GuardaroEliminar = "Eliminar";
    modalRef.componentInstance.MsgClose = "Cancelar";
    modalRef.componentInstance.GuardaroEliminarClass="btn-danger";
    modalRef.result.then(() => {
      this.legalRulingsService.delete(legal.id).subscribe(
        data => {
          this.toastrService.success("La resolución '"+legal.description+"' se ha eliminado correctamente.",'',
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
