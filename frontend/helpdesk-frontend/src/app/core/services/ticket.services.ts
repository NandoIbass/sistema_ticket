import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Ticket } from '../../shared/models/ticket.model';

@Injectable({ providedIn: 'root' })
export class TicketService {
  private base = '/api/tickets';
  constructor(private http: HttpClient) {}

getTickets(page = 1, pageSize = 10, status?: string, priority?: string) {
  let params: any = { page, pageSize };
  if (status) params.status = status;
  if (priority) params.priority = priority;

  return this.http.get(`${this.base}`, { params });
}

  getTicket(id: number): Observable<Ticket> {
    return this.http.get<Ticket>(`${this.base}/${id}`);
  }




createTicket(ticketData: any) {
  return this.http.post('/api/tickets', ticketData);
}


  updateTicket(id: number, ticket: Partial<Ticket>) {
    return this.http.put(`${this.base}/${id}`, ticket);
  }

  // deleteTicket(id: number) {
  //   return this.http.delete(`${this.base}/${id}`);
  // }
  getTicketById(id: number) {
  return this.getTicket(id); // redirige al método ya existente
}

  deleteTicket(id: number): Observable<void> {
    return this.http.delete<void>(`${this.base}/${id}`);
  }
}