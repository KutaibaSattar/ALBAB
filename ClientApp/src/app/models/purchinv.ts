import { PurchHdr } from "./purchhdr";
import { PurchDtl } from "./purchdtl";

export interface PurchInv{
  id:number
  purNo:string
  purDate? :Date
  gtotal?:number
  appUserId:number
purchDtlDtos : PurchDtl[];

}
