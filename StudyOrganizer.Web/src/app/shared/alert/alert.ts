import { Component, OnInit } from '@angular/core';
import { MessageService } from '../../services/message.service';
import { filter, tap } from 'rxjs';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-alert',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './alert.html',
  styleUrl: './alert.css'
})
export class AlertComponent implements OnInit {
  message: string = '';
  alertClass: string = '';

  constructor(private messageService: MessageService) { }

  ngOnInit(): void {
    this.messageService.message$.pipe(
      filter(msg => !!msg.message),
      tap(msg => {
        this.message = msg.message;
        this.alertClass = msg.type === 'success' ? 'alert-success' : 'alert-danger';
        setTimeout(() => this.clearMessage(), 5000);
      })
    ).subscribe();
  }

  clearMessage(): void {
    this.message = '';
    this.alertClass = '';
  }
}
