import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { Observable, BehaviorSubject, switchMap, finalize, map, Subject } from 'rxjs';

import { SubtopicsService } from '../services/subtopics.service';
import { DisciplinesService } from '../services/disciplines.service';
import { AiContentService } from '../services/ai-content.service';
import { MessageService } from '../services/message.service';

import { Subtopic } from '../models/discipline.model';
import { AlertComponent } from '../shared/alert/alert';

import * as pdfjsLib from 'pdfjs-dist';
import { BreadcrumbComponent, BreadcrumbItem } from '../shared/breadcrumb/breadcrumb';

pdfjsLib.GlobalWorkerOptions.workerSrc = `assets/js/pdf.worker.js`;

@Component({
  selector: 'app-subtopics',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule, BreadcrumbComponent, AlertComponent],
  providers: [SubtopicsService, DisciplinesService, AiContentService, MessageService],
  templateUrl: './subtopics.html',
  styleUrl: './subtopics.css'
})
export class SubtopicsComponent implements OnInit {
  disciplineId!: number;
  public refreshSubtopics$: Subject<void> = new Subject<void>();
  subtopics$!: Observable<Subtopic[]>;
  newSubtopicDescription: string = '';
  breadcrumbs: BreadcrumbItem[] = [];

  pageSize: number = 5;
  currentPage: number = 1;
  totalPages: number = 0;
  paginatedSubtopics: Subtopic[] = [];
  filterText: string = '';

  selectedSubtopic: Subtopic | null = null;

  // Propriedades unificadas e novas
  contentToSave: string = '';
  aiPrompt: string = '';
  aiLoading: boolean = false;

  constructor(
    private route: ActivatedRoute,
    private subtopicsService: SubtopicsService,
    private disciplinesService: DisciplinesService,
    private aiContentService: AiContentService,
    private messageService: MessageService
  ) { }

  ngOnInit(): void {
    // this.subtopics$ = this.refreshSubtopics$.pipe(
    //   switchMap(() => this.subtopicsService.getSubtopicsByDisciplineId(this.disciplineId)),
    //   map(subtopics => {
    //     const filteredSubtopics = this.filterSubtopics(subtopics);
    //     this.totalPages = Math.ceil(filteredSubtopics.length / this.pageSize);

    //     const startIndex = (this.currentPage - 1) * this.pageSize;
    //     this.paginatedSubtopics = filteredSubtopics.slice(startIndex, startIndex + this.pageSize);

    //     return filteredSubtopics;
    //   })
    // );

    this.route.paramMap.subscribe(params => {
      this.disciplineId = Number(params.get('disciplineId'));
      this.buildBreadcrumbs();
      this.subtopics$ = this.refreshSubtopics$.pipe(
        switchMap(() => this.subtopicsService.getSubtopicsByDisciplineId(this.disciplineId)),
        map(subtopics => {
          console.log(subtopics);
          const filteredSubtopics = this.filterSubtopics(subtopics);
          this.totalPages = Math.ceil(filteredSubtopics.length / this.pageSize);

          const startIndex = (this.currentPage - 1) * this.pageSize;
          this.paginatedSubtopics = filteredSubtopics.slice(startIndex, startIndex + this.pageSize);

          return filteredSubtopics;
        })
      );

      //this.refreshSubtopics$.next();
    });
  }

  private buildBreadcrumbs(): void {
    this.disciplinesService.getDisciplineById(this.disciplineId).subscribe(discipline => {
      this.breadcrumbs = [
        { label: 'Home', path: '/' },
        { label: 'Módulos', path: '/modules' },
        { label: discipline.Name, path: `/disciplines/${discipline.ModuleId}` },
        { label: 'Subtópicos' }
      ];
    });
  }

  onAddSubtopic(): void {
    if (this.newSubtopicDescription.trim()) {
      const newSubtopic: Subtopic = {
        Id: 0, // Assuming backend will assign a new Id
        Description: this.newSubtopicDescription,
        DisciplineId: this.disciplineId,
        Status: 0,
        Notes: null,
        StartDate: null,
        EndDate: null,
        MasteryLevel: null,
        MaterialUrl: null,
        Content: '' // Add this if your Subtopic model includes 'content'
      };

      this.subtopicsService.createSubtopic(newSubtopic).subscribe({
        next: () => {
          this.messageService.showSuccess('Subtópico adicionado com sucesso!');
          this.newSubtopicDescription = '';
          this.refreshSubtopics$.next();
        },
        error: () => {
          this.messageService.showError('Erro ao adicionar o subtópico.');
        }
      });
    }
  }

  onDeleteSubtopic(id: number): void {
    if (confirm('Tem certeza que deseja remover este subtópico?')) {
      this.subtopicsService.deleteSubtopic(id).subscribe({
        next: () => {
          this.messageService.showSuccess('Subtópico removido com sucesso!');
          this.refreshSubtopics$.next();
          this.selectedSubtopic = null;
          this.contentToSave = '';
        },
        error: () => {
          this.messageService.showError('Erro ao remover o subtópico.');
        }
      });
    }
  }

  filterSubtopics(subtopics: Subtopic[]): Subtopic[] {
    if (!this.filterText) {
      return subtopics;
    }
    return subtopics.filter(subtopic =>
      subtopic.Description.toLowerCase().includes(this.filterText.toLowerCase())
    );
  }

  goToPage(page: number): void {
    if (page >= 1 && page <= this.totalPages) {
      this.currentPage = page;
      this.refreshSubtopics$.next();
    }
  }

  // Método de seleção agora carrega o conteúdo existente
  selectSubtopic(subtopic: Subtopic): void {
    this.selectedSubtopic = subtopic;
    this.contentToSave = subtopic.Content || '';
    this.aiPrompt = ''; // Limpa o campo de prompt ao selecionar um novo subtópico
  }

  // Novo método para salvar o conteúdo unificado
  saveContent(): void {
    if (!this.selectedSubtopic) {
      this.messageService.showError('Nenhum subtópico selecionado.');
      return;
    }

    const updatedSubtopic = { ...this.selectedSubtopic, content: this.contentToSave };

    this.subtopicsService.updateSubtopic(updatedSubtopic).subscribe({
      next: () => {
        this.messageService.showSuccess('Conteúdo salvo com sucesso!');
        this.refreshSubtopics$.next();
      },
      error: () => {
        this.messageService.showError('Erro ao salvar o conteúdo.');
      }
    });
  }

  // Método de geração de conteúdo para IA agora usa o novo prompt
  generateAiContent(): void {
    if (!this.selectedSubtopic || !this.aiPrompt.trim()) {
      this.messageService.showError('Por favor, digite uma pergunta para a IA.');
      return;
    }

    this.aiLoading = true;
    this.aiContentService.generateContent(this.aiPrompt)
      .pipe(finalize(() => this.aiLoading = false))
      .subscribe({
        next: (content) => {
          const separator = this.contentToSave ? '\n\n---\n\n' : '';
          this.contentToSave += separator + content;
          this.aiPrompt = ''; // Limpa o prompt após a geração
        },
        error: (err) => {
          this.messageService.showError('Erro ao gerar conteúdo de IA. Por favor, tente novamente.');
          console.error('Erro na requisição da IA:', err);
        }
      });
  }

  // Método de extração de PDF agora concatena o conteúdo
  async extractPdfContent(files: FileList | null): Promise<void> {
    if (!files || files.length === 0) {
      this.messageService.showError('Nenhum arquivo selecionado.');
      return;
    }

    const file = files[0];
    const fileReader = new FileReader();

    fileReader.onload = async () => {
      const arrayBuffer = fileReader.result as ArrayBuffer;
      const loadingTask = pdfjsLib.getDocument({ data: arrayBuffer });

      try {
        const pdf = await loadingTask.promise;
        let fullText = '';
        for (let i = 1; i <= pdf.numPages; i++) {
          const page = await pdf.getPage(i);
          const textContent = await page.getTextContent();
          const pageText = textContent.items.map(item => (item as any).str).join(' ');
          fullText += pageText + '\n';
        }

        const separator = this.contentToSave ? '\n\n---\n\n' : '';
        this.contentToSave += separator + fullText.trim();

        this.messageService.showSuccess('Conteúdo do PDF extraído com sucesso!');
      } catch (error) {
        this.messageService.showError('Erro ao extrair conteúdo do PDF.');
        console.error('Erro na extração de PDF:', error);
      }
    };
    fileReader.readAsArrayBuffer(file);
  }
}