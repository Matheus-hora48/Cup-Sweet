import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { Product } from '../dto/product_dto';
import { Review } from '../dto/review_dto';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class ProductService {
  constructor(private http: HttpClient) {}

  getProducts(): Observable<Product[]> {
    return this.http.get<Product[]>('https://cup-sweet.onrender.com/api/produtos');
  }

  getProductById(id: number): Observable<Product> {
    return this.http.get<Product>(`https://cup-sweet.onrender.com/api/produtos/${id}`);
  }
}
