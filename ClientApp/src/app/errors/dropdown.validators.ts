import { AbstractControl, ValidationErrors } from "@angular/forms";

export class DropDownValidators{

  // declare function as static so no need make an instacne of class for calling function , we can call directly

  static  shouldLimited(control: AbstractControl) : ValidationErrors| null{

    if (control.value ==  null)
      return null;

    if ( control.value != '' && typeof(<string>control.value) !==  'number')
        return { shouldLimited: true };

    return null;

  }

 /*  static shouldBeUnique(control: AbstractControl): ValidationErrors|null{


  } */

}
