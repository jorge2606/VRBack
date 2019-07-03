import { Directive, ElementRef, Input, HostListener, Output, EventEmitter } from '@angular/core';

@Directive({
  selector: '[appCheckSpaceBlankOnInput]'
})
export class CheckSpaceBlankOnInputDirective {

  @Input() className : string="";
  @Input('nameCheckBlankDirective') name : string="";
  @Input('valueCheckBlankDirective') value : string="";
  @Output() msgEvent : EventEmitter<string> = new EventEmitter();

  namesInSpanish : any = {'name' : 'Nombre', 'description' : 'Descripción', 'advance' : 'Anticipo','default' : 's/n'};
  constructor(
      private el : ElementRef
  ) {
    }

  sendMessage(msj : string){
    
    this.msgEvent.emit(msj);
  }

  
  @HostListener('keyup') validSpaceBlank(){
      var msj = 'El campo '+ this.name ? 'default' : this.name +' no deberia estar vacio';

      if (this.value.length > 0){
        if (this.value.trim().length == 0){
          this.sendMessage(msj);
          this.el.nativeElement.className="form-control is-invalid";          
        }else{
          if (this.value[0] == ' '){
            var msj = 'El primer caráctcer del campo '+this.name ? 'default' : this.name +' no puede ser un espacio vacío'
            this.el.nativeElement.className="form-control is-invalid";
            this.sendMessage(msj);
          }else{
            this.el.nativeElement.className="form-control is-valid";
          }
        }
      }
  }


}
