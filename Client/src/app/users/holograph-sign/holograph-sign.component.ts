import { ToastrService } from 'ngx-toastr';
import { HttpClient } from '@angular/common/http';
import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { AuthenticationService } from 'src/app/_services/authentication.service';
import { MessBetweenCompService } from 'src/app/_services/mess-between-comp.service';
import { UserService } from 'src/app/_services/user.service';
import { FileUploader } from 'ng2-file-upload';
import { environment } from 'src/environments/environment';
import { Subject } from 'rxjs';
import { NgxSpinnerService } from 'ngx-spinner';
import { Title, DomSanitizer } from '@angular/platform-browser';

@Component({
  selector: 'app-holograph-sign',
  templateUrl: './holograph-sign.component.html',
  styleUrls: ['./holograph-sign.component.css']
})
export class HolographSignComponent implements OnInit {

  @Input('supervisorId') userIdInput : number;
  @Output() dirty = new EventEmitter<boolean>();
  //image
  uploader:FileUploader;
  hasBaseDropZoneOver = false;
  baseUrl = environment.apiUrl; 
  idUser : number;
  urlImage : any;
  subject = new Subject<any>();
  errorMsj : string;
  isDeleted : boolean;

  constructor(
    private authService : AuthenticationService, 
    private messaBetweenComp : MessBetweenCompService,
    private http : HttpClient,
    private spinner: NgxSpinnerService,
    private titleService : Title,
    private toastrService : ToastrService,
    private userService : UserService,
    private domanizeService : DomSanitizer
    ) { }

    fileOverBase(e:any):void {
    this.hasBaseDropZoneOver = e;
    }

    ngOnInit() {
      this.titleService.setTitle('Firma Hológrafa');
      if(!this.userIdInput){
        this.idUser = this.authService.userId('id');
      }else{
        this.idUser = this.userIdInput;
      }
      
      this.userService.getHolographSign(this.idUser,200,200)
      .subscribe(
        x =>{
          this.urlImage = this.domanizeService.bypassSecurityTrustResourceUrl("data:image/png;base64,"+x.response);
        }
      );
      //this.urlImage = this.urlFile(this.idUser,200,200)  + "r=" + (Math.random() * 100) + 1;
      //image
      this.initializeUploader();
    }

    urlFile(userId : number, width : number, height: number){
      this.userService.getHolographSign(userId,width,height)
      .subscribe(
        x =>{
          this.urlImage = this.domanizeService.bypassSecurityTrustResourceUrl("data:image/png;base64,"+x.response);
        }
      );
    }

    initializeUploader() {
      this.uploader = new FileUploader({
      url: this.baseUrl+'File/HolographSignUpdate/',
      authToken: 'Bearer ' + this.authService.userId('token'),
      additionalParameter :{
        'userId' : this.idUser
      },
      isHTML5: true,
      allowedFileType: ['image'],
      removeAfterUpload: true,
      autoUpload: false,
      maxFileSize: 10 * 1024 * 1024
    });

    this.uploader.onSuccessItem = (item, response, status, headers) => {
        
        if (response) {
          this.isDeleted = response["isDeleted"];
          this.urlFile(this.idUser,200,200);
          this.toastrService.success('Firma Actualizada','',{positionClass : 'toast-top-center', timeOut : 3000});
          //this.messaBetweenComp.sendMessage(this.urlImage); --> envia a la miniatura del navar 
        }
      }   
    }

    url = '';
    onSelectFile(event) {
    if (event.target.files && event.target.files[0]) {
        var mimeType = event.target.files[0].type;
        if (mimeType === "image/png" || mimeType === "image/jpg" || mimeType === "image/jpeg"){
          var reader = new FileReader();
          
          reader.readAsDataURL(event.target.files[0]); // read file as data url

          reader.onload = (event : any) => { // called once readAsDataURL is completed
          this.url = event.target.result;
          }
        }else{
          this.toastrService.warning('Solo se permiten archivos con extensiones .JPG y .PNG');
        }
      }
    }


    removePreview(){
      this.url='';
      this.uploader.cancelAll();
      this.uploader.clearQueue();
    }

    deleteProfilePhoto(id: number) {
      return this.http.delete(environment.apiUrl+'File/removeHolographSign/' + id);
    }

    eliminarPerfil(){
      //let url = this.urlFile(this.idUser,200,200);
      
      this.userService.getHolographSign(this.idUser,200,200)
      .subscribe(
        url =>{

          this.deleteProfilePhoto(this.idUser).subscribe(
            data => {
              this.isDeleted = data["response"]["isDeleted"];
              this.urlImage =  this.domanizeService.bypassSecurityTrustResourceUrl("data:image/png;base64,"+url.response),
              this.url = '',
              this.messaBetweenComp.sendMessage(this.urlImage),
              this.spinner.hide();
          },
          error => {
            console.log("Rrror", error);
            this.spinner.hide();
          }
          );
        }
      );
      this.spinner.show();
    }

}
