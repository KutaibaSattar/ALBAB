export interface IInvoice{
  id: number
  purNo:string
  purDate :Date
  appUserId:number
  comment : string,
  accountId: number
  purchDtl : IInvDetail[];
}

/* export interface IPurchHdr {
  id:number
  purNo:string
  purDate? :Date
  appUserId:number
} */

export interface IInvDetail {
  id:number;
  productId:number;
  purchId:number;
  quantity: number;
  price: number;
  total: number;
  itemName: string;
}

/* export function emptyArticle(): Purchase {
  return {
    id: 0,
    purNo:'',
    appUserId:null,
    purchDtl: [
      {
        id:0,
        productId:0,
        purchId:0,
        quantity:0,
        price: 0,
        total: 0,
        itemName:'',
      }
    ]
    }
  } */

export class IInvoice implements IInvoice{
  id = 0;
  purNo ='';
  appUserId = 0;
  purDate = new Date();
  purchDtl : IInvDetail[];

}
