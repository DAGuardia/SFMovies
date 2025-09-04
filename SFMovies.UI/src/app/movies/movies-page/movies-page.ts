import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Client, MovieDto, TittleSuggestionDto } from '../../core/clients/sfmovies-client';
import { TitleAutocompleteComponent } from '../../shared/title-autocomplete/title-autocomplete';
import { MapComponent, MapPoint } from '../../shared/map.component/map.component';

@Component({
  selector: 'MoviesPage',
  standalone: true,
  imports: [CommonModule, FormsModule, TitleAutocompleteComponent, MapComponent],
  templateUrl: './movies-page.html',
  styleUrls: ['./movies-page.scss']
})
export class MoviesPageComponent {
  private api = inject(Client);

  query = '';
  movies: MovieDto[] = [];
  loading = false;
  error = '';
  titles: TittleSuggestionDto[] = [];
  mapPoints: MapPoint[] = [];

  private buildMapPoints(): void {
    const pts: MapPoint[] = [];
  
    for (const m of this.movies) {
      for (const loc of (m.locations ?? [])) {
        if (loc.latitude == null || loc.longitude == null) continue;
  
        pts.push({
          title: m.title ?? 'Unknown',
          releaseYear: m.releaseYear ?? null,
          director: m.director ?? null,
          cast: (m.cast ?? []).filter(Boolean),
          address: loc.address ?? '',
          latitude: Number(loc.latitude),
          longitude: Number(loc.longitude),
        });
      }
    }
  
    this.mapPoints = pts;
  }
  
  search(): void {
    this.error = '';
    this.loading = true;
    const title = this.query.trim() || undefined;
  
    this.api.movies(title).subscribe({
      next: res => { 
        this.movies = res ?? [];
        this.loading = false; 
        this.buildMapPoints();
      },
      error: err => { this.error = 'Error fetching movies.'; console.error(err); this.loading = false; }
    });
  }

  loadSuggestions(term: string) {
    const q = (term ?? '').trim();
    if (q.length < 2) { this.titles = []; return; }
    this.api.suggest(q, 50).subscribe(res => this.titles = res ?? []);
  }
  
  onPickFromAutocomplete(title: string) {
    this.query = title;
    this.search();
  }  

  clear(): void {
    this.query = '';
    this.movies = [];
    this.error = '';
  }
}