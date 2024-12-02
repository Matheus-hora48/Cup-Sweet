import { Component, OnInit } from '@angular/core';
import { ProductService } from '../../../services/product.service';
import { ProductCardComponent } from '../product-card/product-card.component';
import { CommonModule } from '@angular/common';
import { DialogService } from '@ngneat/dialog';
import { FilterDialogComponent } from '../../filter-dialog/filter-dialog.component';
import { Product } from '../../../dto/product_dto';

@Component({
  selector: 'app-product-list',
  standalone: true,
  imports: [ProductCardComponent, CommonModule],
  templateUrl: './product-list.component.html',
  styleUrl: './product-list.component.scss',
})
export class ProductListComponent implements OnInit {
  products: Product[] = [];
  searchQuery: string = '';

  constructor(
    private productService: ProductService,
    private dialog: DialogService
  ) {}

  ngOnInit() {
    this.productService.getProducts().subscribe((data) => {
      this.products = data;
    });
  }

  filteredProducts() {
    return this.products.filter(
      (product) =>
        product.nome.toLowerCase().includes(this.searchQuery.toLowerCase()) ||
        product.descricao.toLowerCase().includes(this.searchQuery.toLowerCase())
    );
  }

  toggleFilter() {
    this.dialog.open(FilterDialogComponent, {
      width: '400px',
    });
  }
}
