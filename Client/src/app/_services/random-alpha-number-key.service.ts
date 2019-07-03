import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class RandomAlphaNumberKeyService {

  constructor() { }

  randomAlphaNumberKey(lengthLetter : number, lengthNumber: number ) {
    var text = "";
    var num = "";
    var abcedario = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    var numeros = "0123456789";

    for (var i = 0; i < lengthLetter; i++){
      text += abcedario.charAt(Math.floor(Math.random() * abcedario.length));
    }
    
    for (var i = 0; i < lengthNumber; i++){
      num += numeros.charAt(Math.floor(Math.random() * numeros.length));
    }
    
    return text+"-"+num;
  }
}
