import { Pipe, PipeTransform } from "@angular/core";

interface type {
  Id : number,
  key : string,
}


@Pipe({
  name: 'enumToArray'
})
export class EnumToArrayPipe implements PipeTransform {
  transform(value) : any {




    let array =  [];
    let obj : type = {Id:0,key:''};
    for(let key in value){

      if(value.hasOwnProperty(key)){

        obj.Id = value[key];
        obj.key = key;

        array.push(obj)

      }


    }

      console.log('array',array);
    // return Object.keys(value).filter(e => !isNaN(+e)).map(o => { return {index: +o, name: value[o]}});
  }
}
