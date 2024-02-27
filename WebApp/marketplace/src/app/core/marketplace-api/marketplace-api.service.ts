import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { OfferModel } from './models/offer.model';
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

  postOffer(offer: OfferModel): Observable<string> {
    // TODO: implement the logic to post a new offer, also validate whatever you consider before posting
    //Validar que el usuario exista
    return this.http.post<any>(`${this.marketplaceApUrl}`, offer);
  }

  getCategories(): Observable<string[]> {
    // TODO: implement the logic to retrieve the categories from the service
    return of([]);
  }
}
