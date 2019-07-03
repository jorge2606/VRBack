import { Component, OnInit, Input } from '@angular/core';
import { NgbModal, NgbActiveModal, NgbModalOptions } from '@ng-bootstrap/ng-bootstrap';
import { GenericsCommunicationsComponentsService } from 'src/app/_services/generics-communications-components.service';

@Component({
  selector: 'app-notify-reject',
  templateUrl: './notify-reject.component.html',
  styleUrls: ['./notify-reject.component.css']
})
export class NotifyRejectComponent implements OnInit {
  @Input() title : string;

  model = {motive : ""}
  ngbModalOptions: NgbModalOptions = {
    backdrop : 'static',
    keyboard : false
  };

  constructor( 
              private modalService: NgbModal,
              public activeModal: NgbActiveModal,
              private genericsCommunicationsComponentsService : GenericsCommunicationsComponentsService) { }

  ngOnInit() {
  }

  save(){
    this.genericsCommunicationsComponentsService.sendMessage(this.model);
    this.activeModal.close();
  }

}
