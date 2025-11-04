import { Component, OnInit } from '@angular/core';
import { BehaviorSubject, delay, Observable, switchMap } from 'rxjs';
import { Module } from '../models/modulo.model';
import { ModulesService } from '../services/modules.service';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { AlertComponent } from '../shared/alert/alert';
import { MessageService } from '../services/message.service';
import { BreadcrumbComponent, BreadcrumbItem } from '../shared/breadcrumb/breadcrumb';
import { LoadingAnimComponent } from "../shared/loading-anim.component";

@Component({
  selector: 'app-modules-list',
  standalone: true,
  imports: [CommonModule, RouterModule, BreadcrumbComponent, FormsModule, AlertComponent, LoadingAnimComponent],
  providers: [ModulesService],
  templateUrl: './modules-list.html',
  styleUrl: './modules-list.css'
})
export class ModulesListComponent implements OnInit {
  private refreshModules$ = new BehaviorSubject<void>(undefined);
  modules$!: Observable<Module[]>;
  newModuleName: string = '';
  breadcrumbs: BreadcrumbItem[] = [
    { label: 'Home', path: '/' },
    { label: 'Módulos' }
  ];

  constructor(
    private modulesService: ModulesService,
    private messageService: MessageService
  ) { }

  ngOnInit(): void {
    this.modules$ = this.refreshModules$.pipe(
      delay(5500),
      switchMap(() => this.modulesService.getModules())
    );
  }

  onAddModule(): void {
    if (!this.newModuleName) return;

    const newModule: Omit<Module, 'id'> = {
      Name: this.newModuleName,
      Disciplines: [],
      Id: 0
    };

    this.modulesService.createModule(newModule).subscribe(() => {
      this.newModuleName = '';
      this.refreshModules$.next();
      this.messageService.showSuccess('Módulo adicionado com sucesso!');
    }, () => {
      this.messageService.showError('Erro ao adicionar módulo.');
    });
  }

  onDeleteModule(id: number): void {
    this.modulesService.deleteModule(id).subscribe(() => {
      this.refreshModules$.next();
      this.messageService.showSuccess('Módulo removido com sucesso!');
    }, () => {
      this.messageService.showError('Erro ao remover módulo.');
    });
  }
}
