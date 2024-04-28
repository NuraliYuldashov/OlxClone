import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NavbarComponent } from './components/root/navbar/navbar.component';
import { FooterComponent } from './components/root/footer/footer.component';
import { HomeComponent } from './components/pages/home/home.component';
import { HttpClientModule } from '@angular/common/http';
import { CategoryComponent } from './components/pages/home/category/category.component';
import { RegisterComponent } from './components/pages/auth/register/register.component';
import { LoginComponent } from './components/pages/auth/login/login.component';
import { ReactiveFormsModule } from '@angular/forms';
import { MyAdsComponent } from './components/pages/my-ads/my-ads.component';
import { AddAdsComponent } from './components/pages/add-ads/add-ads.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    RouterOutlet,
    NavbarComponent,
    FooterComponent,
    HomeComponent,
    HttpClientModule,
    CategoryComponent,
    RegisterComponent,
    LoginComponent,
    ReactiveFormsModule,
    MyAdsComponent,
    AddAdsComponent
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'ClientApp';
}
