import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Ads } from '../../../models/Ads';
import { NgFor } from '@angular/common';

@Component({
  selector: 'app-my-ads',
  standalone: true,
  imports: [
    NgFor
  ],
  templateUrl: './my-ads.component.html',
  styleUrl: './my-ads.component.css'
})
export class MyAdsComponent implements OnInit {

  httpClient: HttpClient;

  public constructor(httpClient: HttpClient) {
    this.httpClient = httpClient;
  }

  adslar: Ads[] = [];

  ngOnInit(): void {

    var userData = localStorage.getItem('userData');
    var userId = JSON.parse(userData!).userId;

    this.httpClient.get<Ads[]>('http://localhost:5284/api/Ads/getall/' + userId).subscribe(result => {
      this.adslar = result;
      console.log(result);
    });
  }

}
