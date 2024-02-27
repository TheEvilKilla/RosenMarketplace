export class OfferModel {
  // category: CategoryModel;
  // description: string;
  // id: UUID;
  // location: string;
  // pictureUrl: string;
  // publishedOn: Date;
  // title: string;
  // userId: number; //Tiny int

  constructor(public user: string, public category: CategoryModel, public description: string, 
    public id: number, public location: string, public pictureUrl: string, 
    public publishedOn: Date, public title: string, public userId: number) {

  }
}

export interface CategoryModel {
  id: number;
  name: string;
  //The offers that belong to this category
  offers: OfferModel[];
}