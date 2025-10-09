import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { Observable, switchMap, map, finalize } from 'rxjs';

import { SubtopicsService } from '../services/subtopics.service';
import { DisciplinesService } from '../services/disciplines.service';
import { AiContentService } from '../services/ai-content.service';
import { MessageService } from '../services/message.service';

import { Subtopic } from '../models/discipline.model';

// Importe a biblioteca pdf.js para a extração de conteúdo
import * as pdfjsLib from 'pdfjs-dist';
import { BreadcrumbComponent, BreadcrumbItem } from '../shared/breadcrumb/breadcrumb';
import { AlertComponent } from '../shared/alert/alert';
import { AfterViewInit } from '@angular/core';
import * as bootstrap from 'bootstrap';
import { SoundService } from '../services/sound.service';
import { LoadingAnimComponent } from "../shared/loading-anim.component";

// Configura o caminho para o worker de pdf.js
pdfjsLib.GlobalWorkerOptions.workerSrc = `assets/js/pdf.worker.mjs`;

@Component({
  selector: 'app-subtopics',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule, BreadcrumbComponent, AlertComponent, LoadingAnimComponent],
  providers: [SubtopicsService, DisciplinesService, AiContentService, MessageService],
  templateUrl: './subtopics.html',
  styleUrl: './subtopics.css'
})
export class SubtopicsComponent implements OnInit, AfterViewInit {
  disciplineId!: number;
  subtopics$!: Observable<Subtopic[]>;
  newSubtopicDescription: string = '';
  breadcrumbs: BreadcrumbItem[] = [];

  // Paginação e filtro
  pageSize: number = 5;
  currentPage: number = 1;
  totalPages: number = 0;
  paginatedSubtopics: Subtopic[] = [];
  filterText: string = '';

  // Subtópico selecionado
  selectedSubtopic: Subtopic | null = null;
  content: string = ''; // campo único para IA + PDF + edição manual
  aiLoading: boolean = false;

  constructor(
    private route: ActivatedRoute,
    private subtopicsService: SubtopicsService,
    private disciplinesService: DisciplinesService,
    private aiContentService: AiContentService,
    private messageService: MessageService,
    private soundService: SoundService
  ) { }

  ngOnInit(): void {
    this.subtopics$ = this.route.paramMap.pipe(
      map(params => Number(params.get('disciplineId'))),
      switchMap(disciplineId => {
        this.disciplineId = disciplineId;
        this.buildBreadcrumbs();
        return this.subtopicsService.getSubtopicsByDisciplineId(disciplineId);
      }),
      map(subtopics => {
        const filteredSubtopics = this.filterSubtopics(subtopics);
        this.totalPages = Math.ceil(filteredSubtopics.length / this.pageSize);
        const startIndex = (this.currentPage - 1) * this.pageSize;
        this.paginatedSubtopics = filteredSubtopics.slice(startIndex, startIndex + this.pageSize);
        return filteredSubtopics;
      })
    );
  }

  ngAfterViewInit(): void {
    const tooltipTriggerList = Array.from(document.querySelectorAll('[data-bs-toggle="tooltip"]'));
    tooltipTriggerList.forEach(el => new bootstrap.Tooltip(el));
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
        Id: 0,
        Description: this.newSubtopicDescription,
        DisciplineId: this.disciplineId,
        Status: 0,
        Notes: null,
        StartDate: null,
        EndDate: null,
        MasteryLevel: null,
        MaterialUrl: null,
        Content: ''
      };

      this.subtopicsService.createSubtopic(newSubtopic).subscribe({
        next: () => {
          this.soundService.playAdd();
          this.messageService.showSuccess('Subtópico adicionado com sucesso!');
          this.newSubtopicDescription = '';
          this.reloadSubtopics();
        },
        error: () => {
          this.soundService.playError();
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
          this.selectedSubtopic = null;
          this.content = '';
          this.reloadSubtopics();
        },
        error: () => {
          this.messageService.showError('Erro ao remover o subtópico.');
        }
      });
    }
  }

  protected reloadSubtopics(): void {
    this.subtopics$ = this.subtopicsService.getSubtopicsByDisciplineId(this.disciplineId).pipe(
      map(subtopics => {
        const filteredSubtopics = this.filterSubtopics(subtopics);
        this.totalPages = Math.ceil(filteredSubtopics.length / this.pageSize);
        const startIndex = (this.currentPage - 1) * this.pageSize;
        this.paginatedSubtopics = filteredSubtopics.slice(startIndex, startIndex + this.pageSize);
        return filteredSubtopics;
      })
    );
  }

  filterSubtopics(subtopics: Subtopic[]): Subtopic[] {
    if (!this.filterText) return subtopics;
    return subtopics.filter(subtopic =>
      subtopic.Description.toLowerCase().includes(this.filterText.toLowerCase())
    );
  }

  goToPage(page: number): void {
    if (page >= 1 && page <= this.totalPages) {
      this.currentPage = page;
      this.reloadSubtopics();
    }
  }

  selectSubtopic(subtopic: Subtopic): void {
    this.selectedSubtopic = subtopic;
    this.content = subtopic.Content || '';
  }

  generateAiContent(prompt?: string): void {
    if (!this.selectedSubtopic) {
      this.messageService.showError('Nenhum subtópico selecionado.');
      return;
    }

    this.aiLoading = true;
    const query = prompt && prompt.trim() ? prompt : this.selectedSubtopic.Description;

    this.aiContentService.generateContent(query)
      .pipe(finalize(() => this.aiLoading = false))
      .subscribe({
        next: (generated) => {
          this.content += (this.content ? '\n\n' : '') + generated;
        },
        error: () => {
          this.messageService.showError('Erro ao gerar conteúdo de IA.');
        }
      });
  }

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
      debugger;
      try {
        const pdf = await loadingTask.promise;
        let fullText = '';
        for (let i = 1; i <= pdf.numPages; i++) {
          const page = await pdf.getPage(i);
          const textContent = await page.getTextContent();
          const pageText = textContent.items.map(item => (item as any).str).join(' ');
          fullText += pageText + '\n';
        }
        this.aiContentService.adaptText(fullText).subscribe({
          next: (response) => {
            this.content += (this.content ? '\n\n' : '') + response.content;
          },
          error: () => {
            this.messageService.showError('Erro ao adaptar conteúdo do PDF.');
          }
        });
        // this.content += (this.content ? '\n\n' : '') + fullText;
        this.messageService.showSuccess('Conteúdo do PDF extraído com sucesso!');
      } catch (error) {
        this.messageService.showError('Erro ao extrair conteúdo do PDF.');
        console.error('Erro na extração de PDF:', error);
      }
    };
    fileReader.readAsArrayBuffer(file);
  }

  saveContent(): void {
    if (!this.selectedSubtopic) {
      this.messageService.showError('Nenhum subtópico selecionado.');
      return;
    }

    const updatedSubtopic = { ...this.selectedSubtopic, Content: this.content };

    this.subtopicsService.updateSubtopic(updatedSubtopic).subscribe({
      next: () => {
        this.soundService.playSuccess();
        this.messageService.showSuccess('Conteúdo salvo com sucesso!');
        this.reloadSubtopics();
      },
      error: () => {
        this.soundService.playError();
        this.messageService.showError('Erro ao salvar o conteúdo.');
      }
    });
  }
}
