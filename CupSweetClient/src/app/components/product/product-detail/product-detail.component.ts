import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ProductService } from '../../../services/product.service';
import { CommonModule } from '@angular/common';
import { Product } from '../../../dto/product_dto';
import { FormsModule } from '@angular/forms';
import { CartService } from '../../../services/cart.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-product-detail',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './product-detail.component.html',
  styleUrl: './product-detail.component.scss',
})
export class ProductDetailComponent {
  product: Product | null = null;
  quantity: number = 1;

  constructor(
    private route: ActivatedRoute,
    private productService: ProductService,
    private cartService: CartService,
    private toastr: ToastrService
  ) {}

  ngOnInit() {
    const productId = +this.route.snapshot.paramMap.get('id')!;
    this.productService.getProductById(productId).subscribe((data) => {
      this.product = data;
    });
  }

  getFilledStars(): number[] {
    const rating =
      this.product?.avaliacoes?.reduce((a, b) => a + b.nota, 0) ||
      0 / (this.product?.avaliacoes?.length || 0);
    return Array(Math.min(5, rating)).fill(0);
  }

  getEmptyStars(): number[] {
    const rating =
      this.product?.avaliacoes?.reduce((a, b) => a + b.nota, 0) ||
      0 / (this.product?.avaliacoes?.length || 0);
    return Array(5 - Math.min(5, rating)).fill(0);
  }

  increaseQuantity() {
    if (this.product?.estoque && this.quantity < this.product.estoque) {
      this.quantity++;
    }
  }

  decreaseQuantity() {
    if (this.quantity > 1) {
      this.quantity--;
    }
  }

  addProductCart() {
    if (this.product?.produtoId) {
      const productWithQuantity: Product = {
        ...this.product,
        quantity: this.quantity,
      };

      this.cartService.addProduto(productWithQuantity);
      this.toastr.success('Produto adicionado ao carrinho!', 'Sucesso', {
        timeOut: 3000,
        positionClass: 'toast-top-right',
        closeButton: true,
        progressBar: true,
        progressAnimation: 'increasing',
        tapToDismiss: true,
      });

      window.history.back();
    } else {
      console.error(
        'Produto não possui ID, não pode ser adicionado ao carrinho.'
      );
    }
  }
}
