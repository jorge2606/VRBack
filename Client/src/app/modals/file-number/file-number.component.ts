import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { SolicitationStatesService } from './../../_services/solicitation-states.service';
import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-file-number',
  templateUrl: './file-number.component.html',
  styleUrls: ['./file-number.component.css']
})
export class FileNumberComponent implements OnInit {

  model = {'solicitationSubsidyId': 0 ,'fileNumber' : ''};
  @Input() solicitatioId : number;

  constructor(
    private solicitationState : SolicitationStatesService,
    private activeModal : NgbActiveModal
  ) { }

  ngOnInit() {
  }

  send(){
      this.model.solicitationSubsidyId = this.solicitatioId;
      this.solicitationState.addFielNumber(this.model)
      .subscribe(x => this.activeModal.close());
  }

  closeModal(){
    this.activeModal.dismiss('Close click')
  }
}
