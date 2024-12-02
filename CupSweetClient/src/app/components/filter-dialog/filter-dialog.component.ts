import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { DialogRef } from '@ngneat/dialog';

@Component({
  selector: 'app-filter-dialog',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './filter-dialog.component.html',
  styleUrl: './filter-dialog.component.scss',
})
export class FilterDialogComponent {
  categories: string[] = ['Cupcake', 'Doces', 'Bolos'];
  selectedCategories: string[] = [];
  sortOrder: string = '';

  onCategoryChange(category: string, event: Event) {
    const checkbox = event.target as HTMLInputElement;
    if (checkbox.checked) {
      this.selectedCategories.push(category);
    } else {
      this.selectedCategories = this.selectedCategories.filter(
        (c) => c !== category
      );
    }
  }

  applyFilters() {
    console.log('Categorias selecionadas:', this.selectedCategories);
    console.log('Ordem de classificação:', this.sortOrder);
  }

  closeDialog() {
    console.log('Dialog fechado!');
  }
}
