import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Module } from '../models/modulo.model';

@Injectable({
  providedIn: 'root'
})
export class ModulesService {
  private apiUrl = '/api/modules';

  constructor(private http: HttpClient) { }

  getModules(): Observable<Module[]> {
    return this.http.get<Module[]>(this.apiUrl);
  }

  getModuleById(id: number): Observable<Module> {
    return this.http.get<Module>(`${this.apiUrl}/${id}`);
  }

  // Método para criar um novo módulo
  createModule(module: Omit<Module, 'id'>): Observable<Module> {
    return this.http.post<Module>(this.apiUrl, module);
  }

  // Método para atualizar um módulo existente
  updateModule(module: Module): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${module.Id}`, module);
  }

  // Método para deletar um módulo
  deleteModule(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
