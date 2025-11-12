import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { TicketService } from '../../core/services/ticket.services';

@Component({
  selector: 'app-ticket-create',
  standalone: true,
  templateUrl: './ticket-create.html',
  styleUrls: ['./ticket-create.scss'],
  imports: [CommonModule, ReactiveFormsModule],
})
export class TicketCreateComponent {
  ticketForm: FormGroup;
  loading = false;
  successMsg = '';
  errorMsg = '';

  priorities = ['Alta', 'Media', 'Baja'];

  constructor(
    private fb: FormBuilder,
    private ticketService: TicketService,
    private router: Router
  ) {
    this.ticketForm = this.fb.group({
      title: ['', [Validators.required, Validators.minLength(3)]],
      description: ['', Validators.required],
      priority: ['', Validators.required],
      assignedUser: ['', Validators.required],
    });
  }

  onSubmit() {
    if (this.ticketForm.invalid) {
      this.ticketForm.markAllAsTouched();
      return;
    }



    this.loading = true;
    this.errorMsg = '';
    this.successMsg = '';

    const dto = this.ticketForm.value;

    this.ticketService.createTicket(dto).subscribe({
      next: (res) => {
        this.successMsg = '✅ Ticket creado correctamente';
        this.loading = false;
        setTimeout(() => this.router.navigate(['/tickets']), 1500);
      },
      error: (err) => {
        this.errorMsg = '❌ Error al crear el ticket';
        this.loading = false;
        console.error(err);
      },
    });
  }

  cancel() {
  this.router.navigate(['/tickets']);
}
}

