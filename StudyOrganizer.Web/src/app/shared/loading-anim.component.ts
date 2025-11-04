// src/app/shared/loading-anim.component.ts
import { Component, Input, OnInit, OnDestroy, ElementRef } from '@angular/core';
import lottie from 'lottie-web';

@Component({
    selector: 'app-loading-anim',
    standalone: true,
    template: `<div class="lottie-container" #wrap aria-hidden="true"></div>`,
    styles: [`
    .lottie-container { width: 320px; height: 320px; display:inline-block; }
  `]
})
export class LoadingAnimComponent implements OnInit, OnDestroy {
    @Input() path = 'assets/animations/loading-books.json'; // default path
    private anim: any;

    constructor(private el: ElementRef) { }

    ngOnInit() {
        const container = this.el.nativeElement.querySelector('.lottie-container');
        this.anim = lottie.loadAnimation({
            container,
            renderer: 'svg',
            loop: true,
            autoplay: true,
            path: this.path
        });
    }
    ngOnDestroy() {
        if (this.anim) this.anim.destroy();
    }
}
