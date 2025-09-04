```md
# Arquitectura

## Visión general
Arquitectura **Clean** con separación de capas y dependencias unidireccionales:

```mermaid
flowchart LR
UI[Angular] -->|HTTP| API[SFMovies.Api]
API --> APP[SFMovies.Application]
APP --> DOM[SFMovies.Domain]
APP --> ADP[Infrastructure.Adapters (Interfaces externas)]
ADP --> INT[Infrastructure.Integrations (DataSF HttpClient)]
Domain: Movie, MovieLocation (modelo propio). Sin dependencias.

Application: IMovieService / MovieService (casos de uso). Mapea dominio ? DTOs de salida.

Infrastructure

Integrations: DataSfClient (HttpClient ? DataSF), mapea Row externo ? Dominio.

Adapters: interfaces/puertos para desacoplar Application de Integrations si creciera.

Api: controladores REST, DI, caching y swagger.

Decisiones
Sin DB: lectura directa de DataSF (Socrata). Se mapea a dominio y se expone como DTO propio.

Autocomplete: endpoint /api/movies/suggest con prefix y limit, Distinct case-insensitive.

Mapa: Leaflet en UI; markers por MovieLocation con popup (título, año, director, cast).

CORS: configurado por Cors:AllowedOrigins.

Caching: In-memory en MovieService (TTL breve) para aliviar rate limits.

Configuración
Socrata:BaseUrl, Socrata:ResourcePath, Socrata:AppToken

Cors:AllowedOrigins: [ "https://<tu-ui>" ]

Swagger:Enabled (permite activar en producción para validación)

Errores/edge-cases
release_year puede venir como string ? se parsea seguro.

Lat/long a veces ausentes ? se omiten markers sin coordenadas.

Dataset puede devolver duplicados de título/locación ? se agrupan en UI.

php
Copiar código
