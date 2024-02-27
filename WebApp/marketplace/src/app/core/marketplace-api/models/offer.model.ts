export class OfferModel {
  // category: CategoryModel;
  // description: string;
  // id: UUID;
  // location: string;
  // pictureUrl: string;
  // publishedOn: Date;
  // title: string;
  // userId: number; //Tiny int

  constructor(public id: number, public title: string, public description: string,
    public location: string, public pictureUrl: string, public publishedOn: Date,
    public user: UserModel, public category: CategoryModel) {

  }
}

export interface CategoryModel {
  id: number;
  name: string;
  offers: OfferModel[];
}

export interface UserModel {
  id: number;
  userName: string;
  offers: OfferModel[];
} 