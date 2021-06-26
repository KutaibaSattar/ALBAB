import { Directive, AfterViewInit,OnDestroy,Optional, HostListener } from '@angular/core';
import { MatAutocompleteTrigger} from  '@angular/material/autocomplete';


@Directive({
  selector: '[TabEnter]'
})
export class TabEnterDirective  {

  observable: any;
  constructor(@Optional() private autoTrigger: MatAutocompleteTrigger) {}

  @HostListener('keydown.tab', ['$event.target']) onBlur() {
      if (this.autoTrigger.activeOption) {
         this.autoTrigger.writeValue(this.autoTrigger.activeOption.value)
         this.autoTrigger._onChange(this.autoTrigger.activeOption.value)
      }
  }

}
