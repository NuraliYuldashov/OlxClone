import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Category } from '../../../../models/Category';
import { NgFor } from '@angular/common';
import { SubCategory } from '../../../../models/SubCategory';

@Component({
  selector: 'app-category',
  standalone: true,
  imports: [
    NgFor
  ],
  templateUrl: './category.component.html',
  styleUrl: './category.component.css'
})
export class CategoryComponent implements OnInit {
  httpClient: HttpClient;

  public constructor (httpClient: HttpClient) {
    this.httpClient = httpClient;
  }

  public categories: Category[] = [];
  public subCategories: SubCategory[] | undefined = [];
  public lastSelectedCategoryId: number = 0;

  ngOnInit(): void {
    this.httpClient.get<Category[]>('http://localhost:5284/api/category/getall').subscribe(result => {
      this.categories = result;
    }, error => console.error(error));
  }

  getSubCategories(categoryId: number): void {
    this.subCategories = this.categories.find(c => c.Id === categoryId)!.SubCategories;
    var collapse = document.getElementById('flush-collapseOne');
    if (this.subCategories!.length > 0 ) {
      if (categoryId == this.lastSelectedCategoryId) {
        collapse?.classList.remove('show');
        collapse?.setAttribute('aria-expanded', 'false');
        this.subCategories = [];
        this.lastSelectedCategoryId = 0;
      }
      else {
        collapse?.classList.add('show');
        collapse?.setAttribute('aria-expanded', 'true');
        this.lastSelectedCategoryId = categoryId;
      }
    }
    else {
      collapse?.classList.remove('show');
      collapse?.setAttribute('aria-expanded', 'false');
      this.subCategories = [];
    }
  }
}
