import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Subtopic } from '../models/discipline.model';

@Injectable({
  providedIn: 'root'
})
export class SubtopicsService {
  private apiUrl = '/api/subtopics';

  constructor(private http: HttpClient) { }

  getSubtopicsByDisciplineId(disciplineId: number): Observable<Subtopic[]> {
    return this.http.get<Subtopic[]>(`${this.apiUrl}?disciplineId=${disciplineId}`);
  }

  // Método para criar um novo subtópico
  createSubtopic(subtopic: Subtopic): Observable<Subtopic> {
    return this.http.post<Subtopic>(this.apiUrl, subtopic);
  }

  // Método para deletar um subtópico
  deleteSubtopic(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }

  // Método para atualizar um subtópico
  updateSubtopic(subtopic: Subtopic): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${subtopic.Id}`, subtopic);
  }
}
