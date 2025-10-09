import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AiContentService {
  // A requisição agora aponta para o seu servidor Python
  private apiUrl = 'http://localhost:5000/api';

  constructor(private http: HttpClient) { }

  /**
   * Gera conteúdo para um tópico através do servidor intermediário em Python.
   * @param topic O tema sobre o qual o conteúdo deve ser gerado.
   * @returns Um Observable que emite o conteúdo gerado em formato de string.
   */
  generateContent(topic: string): Observable<string> {
    const body = {
      topic: topic
    };

    return this.http.post<any>(this.apiUrl + '/generate-content', body).pipe(
      map(res => {
        return res.content || 'Não foi possível gerar conteúdo.';
      })
    );
  }

  adaptText(text: string): Observable<{ content: string }> {
    return this.http.post<{ content: string }>(this.apiUrl + '/adapt-pdf-content', { pdf_text: text });
  }
}