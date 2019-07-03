import { Component, Input } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'ngbd-modal-content',
  templateUrl: 'modals.component.html'
})
export class NgbdModalContent {
  @Input() Contenido;
  @Input() Encabezado;
  @Input() GuardaroEliminar;
  @Input() MsgClose;
  @Input() GuardaroEliminarClass;
  @Input() MsgCloseClass;
  @Input() MsgCloseHidden;
  @Input() GuardaroEliminarHidden;
  @Input() ClassContainer;

  constructor(public activeModal: NgbActiveModal) {}
}

