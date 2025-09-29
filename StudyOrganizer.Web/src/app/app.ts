// import { Component, signal } from '@angular/core';
// import { RouterModule, RouterOutlet } from '@angular/router';
// import { Observable } from 'rxjs';
// import { ModulesService } from './services/modules.service';
// import { CommonModule } from '@angular/common';
// import { Module } from './models/modulo.model';
// import { AppComponent } from "./app.component";

// @Component({
//   selector: 'app-root',
//   imports: [CommonModule, AppComponent, RouterModule],
//   templateUrl: './app.html',
//   styleUrl: './app.css'
// })
// export class App {
//   protected readonly title = signal('StudyOrganizer.Web');
//   modules$!: Observable<Module[]>;

//   constructor(private modulesService: ModulesService) { }

//   ngOnInit(): void {
//     this.modules$ = this.modulesService.getModules();
//     console.log(this.modulesService.getModules());
//     console.log(this.modules$);
//   }
// }
