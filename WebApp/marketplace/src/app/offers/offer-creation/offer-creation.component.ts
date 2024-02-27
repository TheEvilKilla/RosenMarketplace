import { Component, Input, OnInit } from '@angular/core';
import { UntypedFormGroup } from '@angular/forms';
import { MarketplaceApiService } from 'src/app/core/marketplace-api/marketplace-api.service';

@Component({
  selector: 'app-offer-creation',
  templateUrl: './offer-creation.component.html',
  styleUrls: ['./offer-creation.component.scss']
})
export class OfferCreationComponent implements OnInit {

  offerForm: UntypedFormGroup;

  @Input()
  categories: string[];

  constructor(private marketplaceApiService: MarketplaceApiService) { }

  ngOnInit(): void {
  }

  offerSubmit() {
    this.marketplaceApiService.postOffer(this.offerForm.value).subscribe(() => {
      console.log('Offer created');
    });
  }
}
