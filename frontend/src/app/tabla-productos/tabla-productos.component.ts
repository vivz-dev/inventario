import { Component, OnInit } from '@angular/core';
import { ProductoService } from '../services/producto.service'; // El servicio para interactuar con la API
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-tabla-productos',
  templateUrl: './tabla-productos.component.html',
  styleUrls: ['./tabla-productos.component.scss'],
  imports: [CommonModule]
})
export class TablaProductosComponent implements OnInit {

  productos: any[] = [];

  constructor(private productosService: ProductoService) {}

  ngOnInit() {
    this.productosService.obtenerProductos()
    this.productosService.productos$.subscribe(productos => {
      this.productos = productos;
    });
  }

  onDelete(id: number) {
    this.productosService.eliminarProducto(id).subscribe(() => {
      this.productos = this.productos.filter((p: { id: number; }) => p.id !== id);
    });
  }

  onEdit(id: number) {
    console.log('Editar producto con ID:', id);
    // Lógica para editar el producto
  }
}
