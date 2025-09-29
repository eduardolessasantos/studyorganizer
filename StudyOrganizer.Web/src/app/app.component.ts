import { CommonModule } from "@angular/common";
import { Component, OnInit } from "@angular/core";
import { ModulesService } from "./services/modules.service";
import { RouterModule } from "@angular/router";
import { BreadcrumbComponent } from "./shared/breadcrumb/breadcrumb";
import { HeaderComponent } from "./shared/header/header";
import { FooterComponent } from "./shared/footer/footer";

@Component({
    selector: 'app-root',
    standalone: true, // O componente já é autônomo por padrão
    imports: [CommonModule, RouterModule, HeaderComponent, FooterComponent], // Adicione CommonModule aqui
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css'],
    providers: [ModulesService] // Adicione o serviço aqui
})
export class AppComponent implements OnInit {
    // modules$!: Observable<Module[]>;

    // constructor(private modulesService: ModulesService) { }

    ngOnInit(): void {
        // this.modules$ = this.modulesService.getModules();
    }
}