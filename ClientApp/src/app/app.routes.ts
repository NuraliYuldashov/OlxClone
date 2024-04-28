import { Routes } from '@angular/router';
import { HomeComponent } from './components/pages/home/home.component';
import { RegisterComponent } from './components/pages/auth/register/register.component';
import { LoginComponent } from './components/pages/auth/login/login.component';
import { MyAdsComponent } from './components/pages/my-ads/my-ads.component';
import { AddAdsComponent } from './components/pages/add-ads/add-ads.component';

export const routes: Routes = [
    { path: '', redirectTo: 'home', pathMatch: 'full' },
    { path: 'home', component: HomeComponent },
    { path: 'register', component: RegisterComponent },
    { path: 'login', component: LoginComponent },
    { path: 'my-ads', component: MyAdsComponent},
    { path: 'add-ads', component: AddAdsComponent}
];
