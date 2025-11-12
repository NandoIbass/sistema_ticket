
import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { TicketService } from '../../core/services/ticket.services';
import { Ticket } from '../../shared/models/ticket.model';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-ticket-list',
  standalone: true,
  templateUrl: './ticket-list.html',
  styleUrls: ['./ticket-list.scss'],
  imports: [CommonModule, FormsModule, RouterModule],
})
export class TicketListComponent implements OnInit {
  tickets: Ticket[] = [];
  editingTicket: Ticket | null = null;
  newTicket: Partial<Ticket> = {};

  //  Paginación
  page = 1;
  pageSize = 10;
  total = 0;
  loading = false;

  //Filtros
   filterStatus: string = '';
   filterPriority: string = '';

  //  Para usar Math en el HTML
  Math = Math;

  constructor(private ticketServices: TicketService) {}

  ngOnInit() {
    this.loadTickets();
  }

  loadTickets() {
  this.loading = true;

  this.ticketServices
    .getTickets(this.page, this.pageSize, this.filterStatus, this.filterPriority)
    .subscribe({
      next: (res: any) => {
        this.tickets = res.items || [];
        this.total = res.total || this.tickets.length;
        this.loading = false;
      },
      error: (err) => {
        console.error('Error al cargar los tickets:', err);
        this.loading = false;
      },
    });
}


  nextPage() {
    if (this.page * this.pageSize < this.total) {
      this.page++;
      this.loadTickets();
    }
  }


  prevPage() {
    if (this.page > 1) {
      this.page--;
      this.loadTickets();
    }
  }


  goToPage(page: number) {
    const totalPages = Math.ceil(this.total / this.pageSize);
    if (page >= 1 && page <= totalPages) {
      this.page = page;
      this.loadTickets();
    }
  }

  applyFilters() {
  this.page = 1; // Reiniciar a la primera página al aplicar filtro
  this.loadTickets();
}

clearFilters() {
  this.filterStatus = '';
  this.filterPriority = '';
  this.applyFilters();
}

 
  deleteTicket(id: number) {
    if (!confirm('¿Seguro que quieres eliminar este ticket?')) return;

    this.ticketServices.deleteTicket(id).subscribe({
      next: () => {
        this.tickets = this.tickets.filter(t => t.id !== id);
        this.total--;
      },
      error: (err) => {
        console.error('Error al eliminar el ticket:', err);
        alert('Ocurrió un error al intentar eliminar el ticket.');
      },
    });
  }


  saveEdit() {
    if (this.editingTicket) {
      const index = this.tickets.findIndex(t => t.id === this.editingTicket!.id);
      if (index > -1) this.tickets[index] = { ...this.editingTicket };
      this.editingTicket = null;
    }
  }
}