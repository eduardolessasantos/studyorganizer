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
        if (!ctx || !this.gainNode) return;

        const now = ctx.currentTime;

        const makeTone = (freq: number, startOffset: number, duration: number, type: OscillatorType = 'triangle') => {
            const osc = ctx.createOscillator();
            osc.type = type;
            osc.frequency.value = freq;

            const env = ctx.createGain();
            // soft attack and smooth release
            env.gain.setValueAtTime(0.0, now + startOffset);
            env.gain.linearRampToValueAtTime(0.9, now + startOffset + 0.06); // attack ~60ms
            env.gain.linearRampToValueAtTime(0.0001, now + startOffset + duration); // release

            osc.connect(env);
            env.connect(this.gainNode as GainNode);

            osc.start(now + startOffset);
            osc.stop(now + startOffset + duration + 0.02);
        };

        // two overlapping, slightly longer and softer tones
        makeTone(660, 0, 0.36, 'triangle');
        makeTone(990, 0.12, 0.44, 'triangle');
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
