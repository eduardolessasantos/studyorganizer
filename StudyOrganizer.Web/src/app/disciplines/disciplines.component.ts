import { Component, OnInit } from '@angular/core';
import { BehaviorSubject, Observable, switchMap } from 'rxjs';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { DisciplinesService } from '../services/disciplines.service';
import { CommonModule } from '@angular/common';
import { Discipline } from '../models/discipline.model';
import { BreadcrumbItem, BreadcrumbComponent } from "../shared/breadcrumb/breadcrumb";
import { MessageService } from '../services/message.service';
import { AlertComponent } from "../shared/alert/alert";
import { FormsModule } from '@angular/forms';
import { ModulesService } from '../services/modules.service';

@Component({
  selector: 'app-disciplines',
  standalone: true,
  imports: [CommonModule, RouterModule, FormsModule, BreadcrumbComponent, AlertComponent],
  providers: [DisciplinesService],
  templateUrl: './disciplines.component.html',
  styleUrl: './disciplines.component.css'
})
export class DisciplinesComponent implements OnInit {
  private moduleId!: number;
  private refreshDisciplines$ = new BehaviorSubject<void>(undefined);
  disciplines$!: Observable<Discipline[]>;
  newDisciplineName: string = '';
  breadcrumbs: BreadcrumbItem[] = [];

  constructor(
    private route: ActivatedRoute,
    private disciplinesService: DisciplinesService,
    private messageService: MessageService,
    private modulesService: ModulesService
  ) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      this.moduleId = Number(params.get('moduleId'));
      this.buildBreadcrumbs();
      this.refreshDisciplines$.next();
    });

    this.disciplines$ = this.refreshDisciplines$.pipe(
      switchMap(() => this.disciplinesService.getDisciplinesByModuleId(this.moduleId))
    );
  }

  private buildBreadcrumbs(): void {
    this.modulesService.getModuleById(this.moduleId).subscribe(module => {
      this.breadcrumbs = [
        { label: 'Home', path: '/' },
        { label: 'Módulos', path: '/modules' },
        { label: module.Name }
      ];
    });
  }

  onAddDiscipline(): void {
    if (!this.newDisciplineName) return;

    const newDiscipline: Omit<Discipline, 'id'> = {
      Name: this.newDisciplineName,
      ModuleId: this.moduleId,
      Subtopics: [],
      Id: 0
    };

    this.disciplinesService.createDiscipline(newDiscipline).subscribe(() => {
      this.newDisciplineName = '';
      this.refreshDisciplines$.next();
      this.messageService.showSuccess('Disciplina adicionada com sucesso!');
    }, () => {
      this.messageService.showError('Erro ao adicionar disciplina.');
    });
  }

  onDeleteDiscipline(id: number): void {
    this.disciplinesService.deleteDiscipline(id).subscribe(() => {
      this.refreshDisciplines$.next();
      this.messageService.showSuccess('Disciplina removida com sucesso!');
    }, () => {
      this.messageService.showError('Erro ao remover disciplina.');
    });
  }
}
