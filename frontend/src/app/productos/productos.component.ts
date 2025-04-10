import { Component, OnInit } from '@angular/core';
import { ProductoService } from '../services/producto.service';
import { CommonModule } from '@angular/common';
import { RegistrarProductoComponent } from '../registrar-producto/registrar-producto.component'; // Asegúrate de importar el componente
import { TablaProductosComponent } from '../tabla-productos/tabla-productos.component';


@Component({
  selector: 'app-productos',
  templateUrl: './productos.component.html',
  imports: [CommonModule, RegistrarProductoComponent, TablaProductosComponent], 
})
export class ProductosComponent implements OnInit {
  productos: any[] = [];

  ngOnInit(): void {

  }
}
