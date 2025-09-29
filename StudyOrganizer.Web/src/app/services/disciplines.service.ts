import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Discipline } from '../models/discipline.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DisciplinesService {
  private apiUrl = '/api/disciplines';

  constructor(private http: HttpClient) { }

  getDisciplinesByModuleId(moduleId: number): Observable<Discipline[]> {
    return this.http.get<Discipline[]>(`${this.apiUrl}?moduleId=${moduleId}`);
  }

  getDisciplineById(id: number): Observable<Discipline> {
    return this.http.get<Discipline>(`${this.apiUrl}/${id}`);
  }

  // Método para criar uma nova disciplina
  createDiscipline(discipline: Omit<Discipline, 'id'>): Observable<Discipline> {
    return this.http.post<Discipline>(this.apiUrl, discipline);
  }

  // Método para atualizar uma disciplina existente
  updateDiscipline(discipline: Discipline): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${discipline.Id}`, discipline);
  }

  // Método para deletar uma disciplina
  deleteDiscipline(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
