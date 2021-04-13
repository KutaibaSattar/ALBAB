import { AbstractControl, ValidationErrors } from "@angular/forms";

export class DropDownValidators{

  // declare function as static so no need make an instacne of class for calling function , we can call directly

  static  shouldLimited(control: AbstractControl) : ValidationErrors| null{

    if ( typeof(<string>control.value) !==  'number')
        return { shouldLimited: true };

    return null;

  }

}
