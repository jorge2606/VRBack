import { SolicitationSubsidyService } from 'src/app/_services/solicitation-subsidy.service';
import { AllExpenditureDto } from '../../_models/expenditureType';
import { Component, OnInit, Input } from '@angular/core';
import { NgbActiveModal, NgbModalOptions } from '@ng-bootstrap/ng-bootstrap';
import { Expenditure } from 'src/app/_models/solicitationSubsidy';
import { ExpenditureService } from 'src/app/_services/expenditure.service';

@Component({
  selector: 'app-add-new-expenditure',
  templateUrl: './add-new-expenditure.component.html',
  styleUrls: ['./add-new-expenditure.component.css']
})
export class AddNewExpenditureComponent implements OnInit {

  modelExp = new Expenditure();
  @Input() expendituresAdded : Expenditure[] = [];
  @Input() isCommission : boolean;
  expendituresCbox : AllExpenditureDto[] = [];
  msgExist : string;
  selectedExpenditure : number;
  @Input() keyRandom : string;
  ngbModalOptions: NgbModalOptions = {
    backdrop : 'static',
    keyboard : false
  };

  constructor(
          public activeModal: NgbActiveModal,
          private expenditureService : ExpenditureService,
          private solicitationSubsidyService : SolicitationSubsidyService) { }

  ngOnInit() {
    this.allExpenditure();
  }

  addNewConcept(){

    var amount = this.modelExp.amount.toString().replace(",",".");
    
    if( isNaN( parseFloat( amount ) ) ){
      this.msgExist = "Importe debe ser un número";
      return;
    }else{
      this.modelExp.amount = parseFloat( amount );
    }
    
    if (this.isCommission){
      let expType : AllExpenditureDto;
      this.solicitationSubsidyService.someSolicitationHasThisExpenditure(this.keyRandom,this.modelExp.id)
      .subscribe(exp => {
        if (exp){
          this.msgExist = this.expendituresCbox.find(e => e.id == this.modelExp.id).name+" ya fue solicitado por otro miembro de esta comisión.";
          return;
        }
        
        this.msgExist = "";
        let newExp = new Expenditure();
        newExp.description = this.modelExp.description;
        newExp.amount = this.modelExp.amount;
        newExp.expenditureTypeId = this.modelExp.id;
        if (this.modelExp.id != null){
          newExp.expenditureTypeName = this.expendituresCbox.find(x => x.id == this.modelExp.id).name;
        }
        this.expendituresAdded = this.expendituresAdded || [];
        newExp.orderNumber = this.expendituresAdded.length + 1;
        this.expendituresAdded.push(newExp);
        this.sendData(); 
      });
    }else{
      this.msgExist = "";
      let newExp = new Expenditure();
      newExp.description = this.modelExp.description;
      newExp.amount = this.modelExp.amount;
      newExp.expenditureTypeId = this.modelExp.id;
      if (this.modelExp.id != null){
        newExp.expenditureTypeName = this.expendituresCbox.find(x => x.id == this.modelExp.id).name;
      }
      this.expendituresAdded = this.expendituresAdded || [];
      newExp.orderNumber = this.expendituresAdded.length + 1;
      this.expendituresAdded.push(newExp);
      this.sendData();
    }

  }

  sendData(){
    this.expenditureService.sendMessage(this.expendituresAdded);
    this.activeModal.close(null);
  }

  allExpenditure(){
    this.expenditureService.getAll().subscribe(
      x => this.expendituresCbox = x
    );
  }


}
