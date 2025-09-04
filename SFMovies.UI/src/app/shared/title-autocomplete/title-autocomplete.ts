import { Component, Input, Output, EventEmitter, OnChanges, SimpleChanges } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormControl, ReactiveFormsModule } from '@angular/forms';
import { debounceTime, distinctUntilChanged, filter, switchMap } from 'rxjs/operators';
import { MatAutocompleteModule, MatAutocompleteSelectedEvent } from '@angular/material/autocomplete';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { TittleSuggestionDto } from '../../core/clients/sfmovies-client';

@Component({
  selector: 'app-title-autocomplete',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, MatFormFieldModule, MatInputModule, MatAutocompleteModule],
  templateUrl: './title-autocomplete.html',
  styleUrls: ['./title-autocomplete.scss']
})
export class TitleAutocompleteComponent implements OnChanges {
  @Input() suggestions: TittleSuggestionDto[] = [];

  @Input() value: string = '';
  @Output() valueChange = new EventEmitter<string>();

  @Output() pick = new EventEmitter<string>();

  control = new FormControl<string>('', { nonNullable: true });

  constructor() {
    this.control.valueChanges.subscribe(v => this.valueChange.emit((v ?? '').trim()));
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['value'] && this.control.value !== this.value) {
      this.control.setValue(this.value ?? '', { emitEvent: false });
    }
  }

  optionSelected(ev: MatAutocompleteSelectedEvent) {
    const title = (ev.option.value ?? '').toString().trim();
    this.pick.emit(title);
  }

  enterPick() {
    this.pick.emit((this.control.value ?? '').toString().trim());
  }
}