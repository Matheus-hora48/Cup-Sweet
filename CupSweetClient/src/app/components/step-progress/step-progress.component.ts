import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-step-progress',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './step-progress.component.html',
  styleUrl: './step-progress.component.scss'
})
export class StepProgressComponent {
  @Input() currentStep: number = 1;

  steps = ['Endereço', 'Pagamento', 'Resumo'];
}
