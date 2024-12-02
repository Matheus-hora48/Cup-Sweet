import { Component } from '@angular/core';
import { CheckoutService } from '../../../services/checkout.service';
import { CommonModule } from '@angular/common';
import { StepProgressComponent } from '../../step-progress/step-progress.component';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { error } from 'console';
import { CartService } from '../../../services/cart.service';
import { AddressService } from '../../../services/address.service';
import { NavbarComponent } from '../../navbar/navbar.component';

@Component({
  selector: 'app-checkout-summary',
  standalone: true,
  imports: [
    CommonModule,
    StepProgressComponent,
    HttpClientModule,
    NavbarComponent,
  ],
  templateUrl: './checkout-summary.component.html',
  styleUrl: './checkout-summary.component.scss',
})
export class CheckoutSummaryComponent {
  currentStep = 1;

  addresses: any[] = [];
  selectedAddress = this.checkoutService.getSelectedAddress();
  paymentMethods = this.checkoutService.getPaymentMethods();
  selectedPaymentMethod = this.checkoutService.getSelectedPaymentMethod();

  cartItems: any[] = [];
  totalPrice = 0;

  constructor(
    private checkoutService: CheckoutService,
    private cartService: CartService,
    private toastr: ToastrService,
    private router: Router,
    private addressService: AddressService
  ) {}

  ngOnInit() {
    this.addressService.getAddresses().subscribe((response) => {
      this.addresses = response;
      if (this.addresses.length > 0) {
        this.selectedAddress = this.addresses[0];
        this.checkoutService.setSelectedAddress(this.selectedAddress);
      }
    });

    this.cartItems = this.checkoutService.getCartItems();
    this.calculateTotalPrice();
  }

  nextStep() {
    if (this.currentStep < 3) {
      this.currentStep++;
    }
  }

  previousStep() {
    if (this.currentStep > 1) {
      this.currentStep--;
    }
  }

  selectAddress(address: any) {
    this.checkoutService.setSelectedAddress(address);
    this.selectedAddress = address;
  }

  selectPaymentMethod(method: string) {
    this.checkoutService.setSelectedPaymentMethod(method);
    this.selectedPaymentMethod = method;
  }

  calculateTotalPrice() {
    this.totalPrice = this.cartItems.reduce((total, item) => {
      return total + item.preco * item.quantity;
    }, 0);
  }

  completeCheckout() {
    this.checkoutService.completeCheckout().subscribe(
      () => {
        this.toastr.success('Pedido finalizado!', 'Sucesso', {
          timeOut: 3000,
          positionClass: 'toast-top-right',
          closeButton: true,
          progressBar: true,
          progressAnimation: 'increasing',
          tapToDismiss: true,
        });
        this.cartService.cleanCart();
        this.router.navigate(['/home']);
      },
      (error) => {
        console.log(error);

        this.toastr.error('Erro ao finalizar pedido', 'Erro');
      }
    );
  }
}
