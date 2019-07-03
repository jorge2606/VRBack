import { ToastrService } from 'ngx-toastr';
import { AuthenticationService } from './../../_services/authentication.service';
import { Component, OnInit, Input } from '@angular/core';
import { Expenditure, ImageDto } from 'src/app/_models/solicitationSubsidy';
import { AllExpenditureDto } from 'src/app/_models/expenditureType';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { ExpenditureService } from 'src/app/_services/expenditure.service';
import { FileUploader, FileItem } from 'ng2-file-upload';
import { environment } from 'src/environments/environment';
import { MessBetweenCompService } from 'src/app/_services/mess-between-comp.service';
import { isNumber } from 'util';

@Component({
  selector: 'app-add-expenditure-repayment',
  templateUrl: './add-expenditure-repayment.component.html',
  styleUrls: ['./add-expenditure-repayment.component.css']
})
export class AddExpenditureRepaymentComponent implements OnInit {


  modelExp = new Expenditure();
  @Input() expendituresAdded : Expenditure[] = [];
  expendituresCbox : AllExpenditureDto[] = [];
  msgExist : string;
  selectedExpenditure : number;
  url = '';
  uploader:FileUploader;
  baseUrl = environment.apiUrl; 
  image : ImageDto;
  urlImage : string;
  hasBaseDropZoneOver = false;
  idUser : number;

  constructor(
          public activeModal: NgbActiveModal,
          private expenditureService : ExpenditureService,
          private authService : AuthenticationService,
          private toastService : ToastrService
          ) { }

  ngOnInit() {
    this.idUser = this.authService.userId('id');
    this.allExpenditure();
    this.initializeUploader();
  }

  addNewConcept(){
    if (!this.url){
      this.msgExist = "Ingrese una imagen de su comprobante.";
      return;      
    }

    var amount = this.modelExp.amount.toString().replace(",",".");
    
    if( isNaN( parseFloat( amount ) ) ){
      this.msgExist = "Importe debe ser un número";
      return;
    }else{
      this.modelExp.amount = parseFloat( amount );
    }
    
    this.msgExist = "";
    let newExp = new Expenditure();
    newExp.description = this.modelExp.description;
    newExp.amount = this.modelExp.amount;
    newExp.expenditureTypeId = this.modelExp.id;
    newExp.supportingDate = this.modelExp.supportingDate;
    if (this.modelExp.id != null){
      newExp.expenditureTypeName = this.expendituresCbox.find(x => x.id == this.modelExp.id).name;
    }
    newExp.urlImage = this.url;
    let img = new ImageDto()
    img.name = this.image.name;
    img.type = this.image.type;
    img.size = this.image.size;
    img.webkitRelativePath = this.image.webkitRelativePath;
    img.lastModified = this.image.lastModified;
    img.lastModifiedDate = this.image.lastModifiedDate;
    img.urlImages = this.url;
    newExp.imagesDto = [];
    newExp.imagesDto.push(img);
    this.expendituresAdded = this.expendituresAdded || [];
    newExp.orderNumber = this.expendituresAdded.length + 1;
    this.expendituresAdded.push(newExp);
    this.sendData();
  }
  

  onSelectFile(event) {
    if (event.target.files && event.target.files[0]) {
      var mimeType = event.target.files[0].type;
      if (mimeType === "image/png" || mimeType === "image/jpg" || mimeType === "image/jpeg"){
        this.image = event.target.files[0];
        var reader = new FileReader();
  
        reader.readAsDataURL(event.target.files[0]); // read file as data url
  
        reader.onload = (event : any) => { // called once readAsDataURL is completed
          this.url = event.target.result;
        }
      }else{
        this.toastService.warning('Solo se permiten archivos con extensiones .JPG y .PNG');
      }
    }
  }

  removePreview(){
    this.url='';
    this.uploader.cancelAll();
    this.uploader.clearQueue();
  }


  clear(modelExp : any){
    modelExp.supportingDate = {day : 0 , month : 0, year : 0};
  }

  sendData(){
    this.activeModal.close(null);
  }

  allExpenditure(){
    this.expenditureService.getAll().subscribe(
      x => {this.expendituresCbox = x}
    );
  }

  fileOverBase(e:any):void {
    this.hasBaseDropZoneOver = e;
  }

  initializeUploader() {
    this.uploader = new FileUploader({
      url: '',
      authToken: 'Bearer ' + this.authService.userId('token'),
      isHTML5: true,
      allowedFileType: ["image/png","image/jpg"],
      removeAfterUpload: true,
      autoUpload: false,
      maxFileSize: 10 * 1024 * 1024
    });
  }

  eliminarPerfil(){
      this.urlImage =  '';
      this.url = '';

    }

}
