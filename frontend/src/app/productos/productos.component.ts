import { Component, OnInit } from '@angular/core';
import { ProductoService } from '../services/producto.service';

@Component({
  selector: 'app-productos',
  templateUrl: './productos.component.html'
})
export class ProductosComponent implements OnInit {
  constructor(private productoService: ProductoService) {}

  ngOnInit(): void {
    this.productoService.obtenerTodos().subscribe(data => {
      console.log('Productos desde el backend:', data);
    });
  }
}
