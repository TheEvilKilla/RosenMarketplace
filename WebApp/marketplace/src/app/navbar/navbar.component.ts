import { Component } from '@angular/core';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent {
user: any;
  constructor() {

  }

  showMenu = false;

  toggleMenu() {
    this.showMenu = !this.showMenu;
  }

  ngOnInit(): void {

  }
}
