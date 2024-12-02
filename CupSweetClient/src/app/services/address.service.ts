import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { AuthService } from './auth.service';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AddressService {
  constructor(private http: HttpClient, private authService: AuthService) {}

  getAddresses(): Observable<any> {
    const userId = this.authService.getUser()?.id;

    return this.http.get(
      `https://cup-sweet.onrender.com/api/Endereco/usuario/${userId}`
    );
  }

  addAddress(newAddress: any): Observable<any> {
    const userId = this.authService.getUser()?.id;
    newAddress = { ...newAddress, userId: userId };
    return this.http.post('https://cup-sweet.onrender.com/api/Endereco', newAddress);
  }

  getAddressById(id: number): any | undefined {
    // return this.addresses.find((address) => address.id === id);
  }

  deleteAddress(id: any) {
    return this.http.delete(`https://cup-sweet.onrender.com/api/Endereco/${id}`);
  }
}
