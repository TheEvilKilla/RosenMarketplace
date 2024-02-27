import { Component, OnInit } from '@angular/core';
import { OfferModel } from 'src/app/core/marketplace-api/models/offer.model';
import { MarketplaceApiService } from 'src/app/core/marketplace-api/marketplace-api.service';
@Component({
  selector: 'app-offer-list',
  templateUrl: './offer-list.component.html',
  styleUrls: ['./offer-list.component.scss']
})
export class OfferListComponent implements OnInit {

  currentPage: number = 1;
  initialElement: number = 0;
  currentElement: number = 4;
  pageSize: number = 4;
  offers: OfferModel[] = [];

  constructor(private marketplaceApiService: MarketplaceApiService) { 

  }

  increaseDisplayCount() {
    this.initialElement += 4;
    this.currentElement += 4;
    this.currentPage--;
  }

  ngOnInit(): void {
    this.marketplaceApiService.getOffers(0, 4).subscribe(offers => {
      this.offers = offers;
      console.log(offers);
    });
  }
}
