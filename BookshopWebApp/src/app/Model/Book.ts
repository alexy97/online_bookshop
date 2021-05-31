export class Book {
    id?: number = 0;
    title: string = "";
    description: string = "";
    summary: string = "";
    iSBN: string = "";
    image?: Blob = new Blob() ;
    author: string = "";
    year: Date = new Date() ;
    price: number = 0;
    numPages: number = 0;
    numPurchases: number = 0;
    quantity: number = 0;
  }