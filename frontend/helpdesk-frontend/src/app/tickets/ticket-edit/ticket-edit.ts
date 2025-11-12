import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { TicketService } from '../../core/services/ticket.services';
import { Ticket } from '../../shared/models/ticket.model';

@Component({
  selector: 'app-ticket-edit',
  standalone: true,
  templateUrl: './ticket-edit.html',
  styleUrls: ['./ticket-edit.scss'],
  imports: [CommonModule, ReactiveFormsModule],
})
export class TicketEditComponent implements OnInit {
  ticketForm: FormGroup;
  loading = false;
  successMsg = '';
  errorMsg = '';
  ticketId!: number;

  priorities = ['Alta', 'Media', 'Baja'];
  statuses = ['Nuevo', 'En progreso', 'Resuelto', 'Cerrado'];

  constructor(
    private fb: FormBuilder,
    private ticketService: TicketService,
    private router: Router,
    private route: ActivatedRoute
  ) {
    this.ticketForm = this.fb.group({
      title: ['', [Validators.required, Validators.minLength(3)]],
      description: ['', Validators.required],
      priority: ['Media', Validators.required],
      status: ['Nuevo', Validators.required],
      assignedUser: ['', Validators.required],
    });
  }
  goBack() {
  this.router.navigate(['/tickets']);
}


  ngOnInit() {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.ticketId = +id;
      this.loadTicket(this.ticketId);
    }
  }

  /** Cargar datos del ticket existente */
  loadTicket(id: number) {
    this.loading = true;
    this.ticketService.getTicketById(id).subscribe({
      next: (ticket: Ticket) => {
        this.ticketForm.patchValue(ticket);
        this.loading = false;
      },
      error: (err) => {
        console.error(err);
        this.errorMsg = '❌ Error al cargar el ticket';
        this.loading = false;
      },
    });
  }

  /** Guardar cambios */
  onSubmit() {
    if (this.ticketForm.invalid) {
      this.ticketForm.markAllAsTouched();
      return;
    }

    this.loading = true;
    const dto = this.ticketForm.value;

    this.ticketService.updateTicket(this.ticketId, dto).subscribe({
      next: () => {
        this.successMsg = '✅ Ticket actualizado correctamente';
        this.loading = false;
        setTimeout(() => this.router.navigate(['/tickets']), 1500);
      },
      error: (err) => {
        console.error(err);
        this.errorMsg = '❌ Error al actualizar el ticket';
        this.loading = false;
      },
    });
  }
}