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

  constructor(private productoService: ProductoService) {}

  ngOnInit() {
    this.productoService.obtenerProductos()
    this.productoService.productos$.subscribe(productos => {
      this.productos = productos;
    });
  }

  onDelete(id: number) {
    this.productoService.eliminarProducto(id).subscribe(() => {
      this.productos = this.productos.filter((p: { id: number; }) => p.id !== id);
    });
  }

  obtenerProductos(): void {
    this.productoService.obtenerProductos();
  };


  onEdit(id: number) {
    console.log('Editar producto con ID:', id);
    // Lógica para editar el producto
  }

  eliminarProducto(id: number): void {
    if (confirm('¿Estás seguro de eliminar este producto?')) {
      this.productoService.eliminarProducto(id).subscribe(() => {
        this.obtenerProductos();  // Volver a cargar la lista de productos
        alert('Producto eliminado con éxito');
      }, (error) => {
        alert('Error al eliminar el producto');
        console.error(error);
      });
    }
  }
}
