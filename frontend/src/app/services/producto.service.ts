// producto.service.ts
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({ providedIn: 'root' })
export class ProductoService {
  private apiUrl = 'http://localhost:5262/api/Productos';

  constructor(private http: HttpClient) {}

  obtenerTodos() {
    return this.http.get(this.apiUrl);
  }
}
