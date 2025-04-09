import { Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { ProductosComponent } from '../app/productos/productos.component';

export const routes: Routes = [
    { path: '', redirectTo: 'login', pathMatch: 'full' },
    { path: 'login', component: LoginComponent },
    { path: 'productos', component: ProductosComponent }
  ];