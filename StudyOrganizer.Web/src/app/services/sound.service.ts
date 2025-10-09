// src/app/services/sound.service.ts
import { Injectable } from '@angular/core';

@Injectable({ providedIn: 'root' })
export class SoundService {
    ctx: AudioContext | null = null;
    gainNode: GainNode | null = null;
    public enabled = true;

    private ensureContext() {
        if (!this.ctx) {
            const AudioCtx = (window as any).AudioContext || (window as any).webkitAudioContext;
            if (!AudioCtx) return null;
            const ctx = new AudioCtx();
            this.ctx = ctx;
            const gain = ctx.createGain();
            this.gainNode = gain;
            gain.connect(ctx.destination);
            gain.gain.value = 0.12; // volume
        }
        return this.ctx;
    }

    toggle(enabled: boolean) {
        this.enabled = enabled;
    }

    // simple tone
    private playTone(freq: number, duration = 0.12, type: OscillatorType = 'sine') {
        if (!this.enabled) return;
        const ctx = this.ensureContext();
        if (!ctx || !this.gainNode) return;
        const o = ctx.createOscillator();
        o.type = type;
        o.frequency.value = freq;
        o.connect(this.gainNode);
        o.start();
        o.stop(ctx.currentTime + duration);
    }

    // success chime (two tones)
    playSuccess() {
        if (!this.enabled) return;
        const ctx = this.ensureContext();
        if (!ctx) return;
        // two quick tones
        this.playTone(880, 0.08, 'sine');
        setTimeout(() => this.playTone(1320, 0.12, 'sine'), 100);
    }

    // add/creation sound
    playAdd() {
        this.playTone(660, 0.12, 'triangle');
    }

    // error sound
    playError() {
        if (!this.enabled) return;
        const ctx = this.ensureContext();
        if (!ctx) return;
        this.playTone(220, 0.16, 'sawtooth');
    }

    // small click
    playClick() {
        this.playTone(1000, 0.06, 'square');
    }
}
