// producto.service.ts
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';


@Injectable({ providedIn: 'root' })
export class ProductoService {
  private apiUrl = 'http://localhost:5262/api/Productos';
  private productosSubject = new BehaviorSubject<any[]>([]);
  productos$ = this.productosSubject.asObservable(); 


  constructor(private http: HttpClient) {}

  obtenerProductos() {
    const token = localStorage.getItem('token');  // Asegúrate de tener el JWT guardado
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`,
      'Content-Type': 'application/json'
    });

    this.http.get<any[]>(this.apiUrl, { headers }).subscribe(
      data => {
        this.productosSubject.next(data);  // Actualiza el Subject cuando obtenga los productos
      },
      error => {
        console.error('Error al obtener los productos:', error);
      }
    );
  }

  buscarProductos(query: string): Observable<any[]> {

    return this.http.get<any[]>(`${this.apiUrl}?search=${query}`);
  }

  eliminarStock(producto: any): any {
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`,
      'Content-Type': 'application/json'
    });
    const url_producto = this.apiUrl + "/" + producto.id;

    const body ={
      "id": producto.id,
      "nombre": producto.nombre,
      "precio": producto.precio,
      "imagenUrl": producto.imagenUrl,
      "stock": producto.stock - 1,
    }

    console.log(body)
    console.log(producto.id)
    return this.http.put<any>(url_producto, { headers, body });
  }

  buscarProductoId(id: number): any{
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`,
      'Content-Type': 'application/json'
    });
    const url_producto = this.apiUrl + id;
    return this.http.get<any>(url_producto);
  }

  agregarProducto(producto: any): Observable<any> {
    const token = localStorage.getItem('token');
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`,
      'Content-Type': 'application/json'
    });

    return this.http.post(this.apiUrl, producto, { headers });
  }

  eliminarProducto(id: number): Observable<void> {
    const url = `${this.apiUrl}/${id}`;  // URL para eliminar un producto
    return this.http.delete<void>(url);  // Realizamos una solicitud DELETE
  }
}
