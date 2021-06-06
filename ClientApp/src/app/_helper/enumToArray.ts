import { Pipe, PipeTransform } from "@angular/core";

interface type {
  id : number,
  key : string,
}


@Pipe({
  name: 'enumToArray'
})
export class EnumToArrayPipe implements PipeTransform {
  transform(value) : any {
    let array =  [];




    for(let key in value){
      if(value.hasOwnProperty(key)){
        var obj = {} as type;
        obj.id = +value[key];
        obj.key = key;
       array.push(obj)
      }
    }

   return array;
    // return Object.keys(value).filter(e => !isNaN(+e)).map(o => { return {index: +o, name: value[o]}});
  }
}
