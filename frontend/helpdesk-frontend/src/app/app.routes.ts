import { Routes } from '@angular/router';
import { LoginComponent } from './auth/login/login';
import { TicketListComponent } from './tickets/ticket-list/ticket-list';
import { TicketCreateComponent } from './tickets/ticket-create/ticket-create';
import { TicketEditComponent } from './tickets/ticket-edit/ticket-edit';
import { AuthGuard } from './core/guards/auth.guard';

export const routes: Routes = [
  

  { 
    path: '', 
    redirectTo: 'login', 
    pathMatch: 'full' 
  },


    { path: 'login', component: LoginComponent },
    { path: 'tickets', component: TicketListComponent, canActivate: [AuthGuard] },
   { path: 'tickets/new', component: TicketCreateComponent, canActivate: [AuthGuard] },
    // { path: 'tickets/edit/:id', component: TicketCreateComponent, canActivate: [AuthGuard] },
    { path: 'tickets/edit/:id', component: TicketEditComponent, canActivate: [AuthGuard] },
    { path: '**', redirectTo: 'login' }
];