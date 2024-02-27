import { Component, Input, OnInit } from '@angular/core';
import { OfferModel } from '../../core/marketplace-api/models/offer.model';

@Component({
  selector: 'app-offer-item',
  templateUrl: './offer-item.component.html',
  styleUrls: ['./offer-item.component.scss']
})
export class OfferItemComponent implements OnInit {

  @Input()
  offer: OfferModel;

  // title: string = 'Puff Pera Lona Impermeable Naranja';
  // details: string = 'CarlosRobles 10/01/2024  14:00';
  // description: string = "Esto es la descripcion de algo Esto es la descripcion de algoEsto es la descripcion de algoEsto es la descripcion de algo";
  // category: string = 'Hogar';

  title: string;
  details: string;
  description: string;
  category: string;

  constructor() {

  }

  ngOnInit(): void {
    this.title = this.offer.title;
    this.details = `${this.offer.user} ${this.offer.publishedOn.toDateString()} ${this.offer.publishedOn.toTimeString()}`;
    this.description = this.offer.description;
    this.category = this.offer.category.name;
  }
}
