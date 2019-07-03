import { Injectable } from "@angular/core";
import { CanDeactivate } from "@angular/router/src/interfaces";

@Injectable()
export class CanDeactivateGuard implements CanDeactivate<any> {
  
  constructor(){}

  canDeactivate(component: any): boolean {
    if(component.hasUnsavedData()){
       if (confirm("Si continúa los datos no guardados se perderán. ¿Desea continuar?")) {
            return true;
        } else {
            return false;
        }
    }
    return true;
  }
}
