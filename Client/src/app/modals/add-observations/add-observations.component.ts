import { SolicitationSubsidyService } from 'src/app/_services/solicitation-subsidy.service';
import { GuidClass } from './../../_helpers/guid-class';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { ToastrService } from 'ngx-toastr';
import { ActivatedRoute } from '@angular/router';
import { ObservationBaseDto } from './../../_models/observation';
import { Component, OnInit, Input } from '@angular/core';
import { ObservationService } from 'src/app/_services/observation.service';

@Component({
  selector: 'app-add-observations',
  templateUrl: './add-observations.component.html',
  styleUrls: ['./add-observations.component.css']
})
export class AddObservationsComponent implements OnInit {
  
  @Input() observationList : ObservationBaseDto[] = []; 
  @Input() id : number;
  @Input() accountFor : boolean;
  cont : number = 0;
  newObservation = new ObservationBaseDto();
  observationDto = {observations : [], id : 0};
  
  
  constructor(
      private observationService : ObservationService,
      private activateRouteService : ActivatedRoute,
      private toastrService : ToastrService,
      private activateModal : NgbActiveModal,
      private solicitationSubsidyService : SolicitationSubsidyService
  ) { }


  ngOnInit() {
    this.observationService.getById(this.id)
    .subscribe(c => {
        this.observationList = c.response;
    },
      error=> {
        var e = error.error.errors.Error || [];
        e.forEach(i => {
          this.toastrService.error(i,'',{timeOut : 2000});
        });
      });
  }

  closeModal(){
    this.activateModal.close(null);
  }

  hidden(obs : ObservationBaseDto){
    obs.hidden = !obs.hidden;
  }

  add(){
    var newObs = new ObservationBaseDto();
    this.cont++;
    newObs.id =  this.cont;
    this.observationList.push(newObs);
  }

  remove(obs : ObservationBaseDto){
    const index = this.observationList.indexOf(obs, 0);

    this.observationList.splice(index,1);
    
  }

  send(){
    this.observationDto.id = this.id;
    this.observationList.forEach(c => {
      if (!GuidClass.isValid(c.id.toString())){
          c.id = GuidClass.empty
      }
    });
    this.observationDto.observations = this.observationList;
    
    this.solicitationSubsidyService.posponeSolicitation(this.observationDto)
    .subscribe(c => this.closeModal());
  }

  sendToAccountFor(){
    this.observationService.sendMessage(this.observationList);
    this.closeModal();
  }

}

