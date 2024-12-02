import { Component, Input } from '@angular/core';
import { Router } from '@angular/router';
import { Product } from '../../../dto/product_dto';

@Component({
  selector: 'app-product-card',
  standalone: true,
  imports: [],
  templateUrl: './product-card.component.html',
  styleUrl: './product-card.component.scss',
})
export class ProductCardComponent {
  @Input() product!: Product;

  constructor(private router: Router) {}

  navigateToDetails() {
    this.router.navigate(['/product', this.product.produtoId]);
  }
}
