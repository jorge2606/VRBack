import { ToastrService } from 'ngx-toastr';
import { UserService } from './../../_services/user.service';
import { Component, OnInit, Input } from '@angular/core';
import { AuthenticationService } from '../../_services/authentication.service';
import { FileUploader, FileItem } from 'ng2-file-upload';
import { environment } from '../../../environments/environment';
import { Subject } from 'rxjs';
import { MessBetweenCompService } from '../../_services/mess-between-comp.service';
import { $ } from 'protractor';
import { Title, DomSanitizer } from '@angular/platform-browser';
import { ImageDto } from 'src/app/_models/solicitationSubsidy';

@Component({
  selector: 'app-photo-profile',
  templateUrl: './photo-profile.component.html',
  styleUrls: ['./photo-profile.component.css']
})
export class PhotoProfileComponent implements OnInit {

  constructor(private authService : AuthenticationService, 
              private messaBetweenComp : MessBetweenCompService,
              private userService : UserService,
              private titleService : Title,
              private domanizeService : DomSanitizer,
              private toastService : ToastrService) { }

    //image
    uploader:FileUploader;
    hasBaseDropZoneOver = false;
    baseUrl = environment.apiUrl; 
    idUser : number;
    urlImage : any;
    subject = new Subject<any>();
    url = '';
    image : ImageDto;
    sizeIcon = "fa-lg";
   
    fileOverBase(e:any):void {
      this.hasBaseDropZoneOver = e;
    }
  
  initializeUploader() {
    this.uploader = new FileUploader({
      url: this.baseUrl+'File/UpdateMyImage/',
      authToken: 'Bearer ' + this.authService.userId('token'),
      isHTML5: true,
      allowedFileType: ['image'],
      removeAfterUpload: true,
      autoUpload: false,
      maxFileSize: 10 * 1024 * 1024
    });

    this.uploader.onSuccessItem = (item, response, status, headers) => {
      if (response) {
        this.urlImage = this.authService.urlFile(this.idUser, 200,200) + "r=" + (Math.random() * 100) + 1;
        this.messaBetweenComp.sendMessage(this.urlImage);
      }
    }    
  }

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
        this.toastService.warning('Solo se permiten archivos con extensiones .JPG y .PNG');
      }
    }
  }
  ngOnInit() {
    this.titleService.setTitle('Mi Perfil - Imagen');
    //image
    this.initializeUploader();
    this.idUser = this.authService.userId('id');
    this.userService.getUserImage(this.idUser, 200,200)
    .subscribe(
      y => {
        this.urlImage = y.response;
      },err =>{
          this.toastService.error('No se pudo cargar la imagen.');
      }
    );
  }

  removePreview(){
    this.url='';
    this.uploader.cancelAll();
    this.uploader.clearQueue();
  }

  eliminarPerfil(){
    this.userService.deleteProfilePhoto(this.idUser,200,200).subscribe(
      data => {
        this.urlImage =  data.response.paths,
        this.url = '',
        this.messaBetweenComp.sendMessage(this.urlImage),
        console.log("POST Request is successful ", data)
      },
      error => {
          console.log("Error", error);
      }
    );

  }
}
