import { Component, Input, OnChanges, OnDestroy, SimpleChanges, ElementRef, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import * as L from 'leaflet';

export type MapPoint = {
  title: string;
  address: string;
  latitude: number;
  longitude: number;
  releaseYear?: number | null;
  director?: string | null;
  cast?: string[];
};

@Component({
  selector: 'app-map',
  standalone: true,
  imports: [CommonModule],
  template: `<div #map class="leaflet-map"></div>`,
})
export class MapComponent implements OnChanges, OnDestroy {
  @Input() points: MapPoint[] = [];

  @ViewChild('map', { static: true }) mapEl!: ElementRef<HTMLDivElement>;

  private map?: L.Map;
  private layerGroup?: L.LayerGroup;

  ngOnChanges(changes: SimpleChanges): void {
    if (!this.map) {
      this.initMap();
    }
    if (changes['points']) {      
      this.renderMarkers();
    }
  }

  ngOnDestroy(): void {
    this.map?.remove();
  }

  private initMap(): void {
    L.Icon.Default.mergeOptions({
      iconRetinaUrl: 'assets/leaflet/marker-icon-2x.png',
      iconUrl: 'assets/leaflet/marker-icon.png',
      shadowUrl: 'assets/leaflet/marker-shadow.png',
    });

    // Centro aproximado de San Francisco
    this.map = L.map(this.mapEl.nativeElement, {
      center: [37.7749, -122.4194],
      zoom: 12
    });

    L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
      attribution:
        '&copy; <a href="https://www.openstreetmap.org/copyright">OSM</a> contributors'
    }).addTo(this.map);

    this.layerGroup = L.layerGroup().addTo(this.map);
  }

  private renderMarkers(): void {
    if (!this.map || !this.layerGroup) return;
  
    this.layerGroup.clearLayers();
    const bounds: L.LatLngTuple[] = [];
  
    for (const p of this.points) {
      if (p.latitude == null || p.longitude == null) continue;
  
      const year = p.releaseYear != null ? ` (${p.releaseYear})` : '';
      const director = p.director ? `<div><b>Director:</b> ${this.escape(p.director)}</div>` : '';
      const castList = (p.cast && p.cast.length)
        ? `<div><b>Cast:</b> ${p.cast.slice(0, 5).map(c => this.escape(c)).join(', ')}${p.cast.length > 5 ? '…' : ''}</div>`
        : '';
      const address = p.address ? `<div class="addr"><b>Address:</b> ${this.escape(p.address)}</div>` : '';
  
      const html = `
        <div class="popup">
          <div><b>${this.escape(p.title)}</b>${year}</div>
          ${director}
          ${castList}
          ${address}
        </div>
      `;
  
      const marker = L
        .marker([p.latitude, p.longitude] as L.LatLngTuple)
        .bindPopup(html);
  
      marker.addTo(this.layerGroup);
      bounds.push([p.latitude, p.longitude] as L.LatLngTuple);
    }
  
    if (bounds.length > 0) {
      this.map.fitBounds(L.latLngBounds(bounds), { padding: [20, 20] });
    }
  }
  
  private escape(s: string): string {
    const div = document.createElement('div');
    div.innerText = s ?? '';
    return div.innerHTML;
  } 
}