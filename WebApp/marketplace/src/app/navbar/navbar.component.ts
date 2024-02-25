import { Component } from '@angular/core';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent {
  user: any;
  showMenu: boolean = false;

  constructor() {

  }

  toggleMenu() {
    this.showMenu = !this.showMenu;
  }

  ngOnInit(): void {

  }
}
