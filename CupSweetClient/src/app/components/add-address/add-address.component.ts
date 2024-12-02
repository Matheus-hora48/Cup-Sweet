import { Component } from '@angular/core';
import { AddressService } from '../../services/address.service';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { error } from 'console';

@Component({
  selector: 'app-add-address',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './add-address.component.html',
  styleUrl: './add-address.component.scss',
})
export class AddAddressComponent {
  street: string = '';
  neighborhood: string = '';
  complement: string = '';
  city: string = '';
  number: string = '';
  postalCode: string = '';
  state: string = '';
  errorMessage: string = '';

  states: { code: string; name: string }[] = [
    { code: 'AC', name: 'Acre' },
    { code: 'AL', name: 'Alagoas' },
    { code: 'AM', name: 'Amazonas' },
    { code: 'BA', name: 'Bahia' },
    { code: 'CE', name: 'Ceará' },
    { code: 'ES', name: 'Espírito Santo' },
    { code: 'GO', name: 'Goiás' },
    { code: 'MA', name: 'Maranhão' },
    { code: 'MT', name: 'Mato Grosso' },
    { code: 'MS', name: 'Mato Grosso do Sul' },
    { code: 'MG', name: 'Minas Gerais' },
    { code: 'PA', name: 'Pará' },
    { code: 'PB', name: 'Paraíba' },
    { code: 'PR', name: 'Paraná' },
    { code: 'PE', name: 'Pernambuco' },
    { code: 'PI', name: 'Piauí' },
    { code: 'RJ', name: 'Rio de Janeiro' },
    { code: 'RN', name: 'Rio Grande do Norte' },
    { code: 'RS', name: 'Rio Grande do Sul' },
    { code: 'RO', name: 'Rondônia' },
    { code: 'RR', name: 'Roraima' },
    { code: 'SC', name: 'Santa Catarina' },
    { code: 'SP', name: 'São Paulo' },
    { code: 'SE', name: 'Sergipe' },
    { code: 'TO', name: 'Tocantins' },
    { code: 'DF', name: 'Distrito Federal' },
  ];

  constructor(private addressService: AddressService, private router: Router) {}

  addAddress() {
    if (
      !this.street ||
      !this.neighborhood ||
      !this.city ||
      !this.number ||
      !this.postalCode ||
      !this.state ||
      !this.state
    ) {
      this.errorMessage = 'Por favor, preencha todos os campos obrigatórios.';
      return;
    }

    const newAddress = {
      rua: this.street,
      bairro: this.neighborhood,
      complemento: this.complement,
      cidade: this.city,
      numero: this.number,
      cep: this.postalCode,
      estado: this.state,
    };

    this.addressService.addAddress(newAddress).subscribe(
      () => {
        this.errorMessage = '';
        this.router.navigate(['/profile']);
      },
      (error) => {
        this.errorMessage = `Ocorreu um erro ao salvar o endereço. `;
        return;
      }
    );
  }
}
