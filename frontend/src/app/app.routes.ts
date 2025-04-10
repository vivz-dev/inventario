import { Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { ProductosComponent } from '../app/productos/productos.component';
import { RegistrarProductoComponent } from './registrar-producto/registrar-producto.component';
import { TablaProductosComponent } from './tabla-productos/tabla-productos.component';
// import { BuscarProductoComponent } from './buscar-producto/buscar-producto.component';


export const routes: Routes = [
    { path: '', redirectTo: 'login', pathMatch: 'full' },
    { path: 'login', component: LoginComponent },
    { path: 'productos', component: ProductosComponent }
  ];