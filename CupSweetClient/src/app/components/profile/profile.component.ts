import { Component } from '@angular/core';
import { ProfileService } from '../../services/profile.service';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { AddressService } from '../../services/address.service';
import { NavbarComponent } from '../navbar/navbar.component';

@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [CommonModule, HttpClientModule, NavbarComponent],
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.scss',
})
export class ProfileComponent {
  user: any;
  addresses: any;

  constructor(
    private profileService: ProfileService,
    private router: Router,
    private addressService: AddressService
  ) {}

  ngOnInit() {
    this.profileService.getUserProfile().subscribe((data) => {
      this.user = data;
    });
    this.addressService.getAddresses().subscribe((data) => {
      this.addresses = data;
    });
  }

  addNewAddress() {
    this.router.navigate(['/add-address']);
  }

  deleteAddress(id: any) {
    this.addressService.deleteAddress(id).subscribe(() => {
      window.location.reload();
    });
  }
}
