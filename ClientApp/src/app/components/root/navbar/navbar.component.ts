import { NgIf } from '@angular/common';
import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [
    RouterModule,
    NgIf
  ],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})
export class NavbarComponent {
  

  public loggedIn() {
    return localStorage.getItem('userData') != null;
  }

  public getUserFullName() {
    if (localStorage.getItem('userData') != null) {
      var userData = JSON.parse(localStorage.getItem('userData') || '{}');
      return userData.fullName;
    }
    return '';
  }

  public logout() {
    localStorage.removeItem('userData');
  }
}
