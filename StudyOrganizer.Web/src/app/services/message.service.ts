import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MessageService {
  private messageSubject = new Subject<{ message: string; type: 'success' | 'danger' }>();
  message$ = this.messageSubject.asObservable();

  constructor() { }

  showSuccess(message: string): void {
    this.messageSubject.next({ message, type: 'success' });
  }

  showError(message: string): void {
    this.messageSubject.next({ message, type: 'danger' });
  }

  clear(): void {
    this.messageSubject.next({ message: '', type: 'success' });
  }
}