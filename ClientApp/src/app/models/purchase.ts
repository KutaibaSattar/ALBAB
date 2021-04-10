
export interface Purchase{
  id: number
  purNo:string
  purDate? :Date
  gtotal?:number
  appUserId:number
  purchDtl : PurchDtl[];

}
export interface PurchHdr {
  id:number
  purNo:string
  purDate? :Date
  gtotal?:number
  appUserId:number


}
export class PurchDtl {
  id:number;
  productId:number;
  purchId:number;
  quantity: number;
  price: number;
  total: number;
  itemName: string;
}

function emptyArticle(): Purchase {
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


  }



