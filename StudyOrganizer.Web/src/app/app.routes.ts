import { Routes } from '@angular/router';
import { DisciplinesComponent } from './disciplines/disciplines.component';
import { AppComponent } from './app.component';
import { ModulesListComponent } from './modules-list/modules-list';
import { SubtopicsComponent } from './subtopics/subtopics';
import { HomeComponent } from './home/home';

export const routes: Routes = [
    { path: '', component: HomeComponent, title: 'Home - Organizador de Estudos' },
    { path: 'modules', component: ModulesListComponent, title: 'Módulos' },
    { path: 'disciplines/:moduleId', component: DisciplinesComponent, title: 'Disciplinas' },
    { path: 'subtopics/:disciplineId', component: SubtopicsComponent, title: 'Subtópicos' },
];