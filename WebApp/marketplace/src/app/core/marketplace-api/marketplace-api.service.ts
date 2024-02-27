import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { OfferModel, UserModel } from './models/offer.model';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class MarketplaceApiService {

  private readonly marketplaceApUrl = 'https://localhost:5001/User';

  constructor(private http: HttpClient) { }

  getOffers(page: number, pageSize: number): Observable<OfferModel[]> {
    return this.http.get<OfferModel[]>(`${this.marketplaceApUrl}`);
  }

  getUserByUserName(userName: string): Observable<UserModel> {
    return this.http.get<UserModel>(`${this.marketplaceApUrl}/${userName}`);
  }

  postOffer(offer: OfferModel): Observable<string> {
    return this.http.post<any>(`${this.marketplaceApUrl}`, offer);
  }

  getCategories(): Observable<string[]> {
    // TODO: implement the logic to retrieve the categories from the service
    return of([]);
  }
}
