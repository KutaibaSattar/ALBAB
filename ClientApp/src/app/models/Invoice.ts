export interface Invoice{
  id: number
  InvNo:string
  Date :Date
  appUserId:number
  comment : string,
  dbAccountId: number,
  addressId:number
  actionAcctId: number,
  purchDtl : IInvDetail[];
}
export interface saveInvoice{
  id: number
  InvNo:string
  Date :Date
  addressId:number
  comment : string,
  dbAccountId: number,
  actionAcctId: number,
  purchDtl : IInvDetail[];
}



export enum InvStatusType {
  Pending,
  Rejected,
  Accepted,
  Canceled,
 }

export interface IInvDetail {
  id:number;
  productId:number;
  purchId:number;
  quantity: number;
  price: number;
  description: string;
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
 export interface invoicesList {
    id: number;
     invNo: string
  }


export class Invoice implements Invoice{
  id = 0;
  invNo ='';
  appUserId = 0;
  dbAccountId = 0;
  actionAcctId = 0;
  date = new Date();
  invDetails : IInvDetail[];

}
