import { Component, OnInit } from '@angular/core';
import { Region } from '../../../models/Region';
import { HttpClient } from '@angular/common/http';
import { Inject } from '@angular/core';
import { NgFor } from '@angular/common';
import { CategoryComponent } from "./category/category.component";

@Component({
    selector: 'app-home',
    standalone: true,
    templateUrl: './home.component.html',
    styleUrl: './home.component.css',
    imports: [NgFor, CategoryComponent]
})
export class HomeComponent implements OnInit {

  public constructor (@Inject(HttpClient) private httpClient: HttpClient){}

  public regions: Region[] | undefined;

  ngOnInit(): void {
    this.httpClient.get<Region[]>('http://localhost:5284/api/Region/getall').subscribe(result => {
      this.regions = result;
    });
  }

  selectRegion(): void {
    var select = document.getElementById("region") as HTMLSelectElement;
    var region = this.regions?.find(r => r.Id == Number(select.value));
    var subRegion = document.getElementById("subregion") as HTMLSelectElement;
    subRegion.innerHTML = "";
    var option = document.createElement("option");
    option.text = "All";
    option.value = "0";
    subRegion.add(option);

    for (var i = 0; i < region?.SubRegions.length!; i++) {
      var option = document.createElement("option");
      option.text = region?.SubRegions[i].Name!;
      option.value = region?.SubRegions[i].Id.toString()!;
      subRegion.add(option);
    }
  }
}
