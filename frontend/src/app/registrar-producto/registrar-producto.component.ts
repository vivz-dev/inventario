import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ProductoService } from '../services/producto.service';


@Component({
  selector: 'app-registrar-producto',
  templateUrl: './registrar-producto.component.html',
  imports: [FormsModule, ReactiveFormsModule, CommonModule],
  styleUrls: ['./registrar-producto.component.scss']
})

export class RegistrarProductoComponent {

  producto = {
    nombre: '',
    precio: 0,
    imagenUrl: '',
    stock: 1
  };

  errorMessage = '';

  // Creamos el FormGroup con FormBuilder
  productoForm: FormGroup;

  constructor(private fb: FormBuilder, private productoService: ProductoService) {
    // Inicializamos el formulario reactivo
    this.productoForm = this.fb.group({
      nombre: ['', Validators.required],  // Campo para nombre
      precio: [0, [Validators.required, Validators.min(0)]],  // Campo para precio
      imagenUrl: ['', [Validators.required]],  // Campo para imagen URL
      stock: [1, [Validators.required, Validators.min(1)]]  // Campo para stock
    });
  }

  // Método para manejar el envío del formulario
  onSubmit() {

    const producto = this.productoForm.value;

    this.productoService.agregarProducto(producto).subscribe(response => {
      console.log('Producto registrado:', response);
      
      // Después de registrar el producto, actualiza la lista de productos
      this.productoService.obtenerProductos();  // Esto actualizará la tabla automáticamente

      // Limpiar el formulario después de guardar
      this.productoForm.reset();
    }, error => {
      console.error('Error al registrar el producto:', error);
    });
  }
}
