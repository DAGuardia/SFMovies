```md
# Arquitectura

## Visi�n general
Arquitectura **Clean** con separaci�n de capas y dependencias unidireccionales:

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

Mapa: Leaflet en UI; markers por MovieLocation con popup (t�tulo, a�o, director, cast).

CORS: configurado por Cors:AllowedOrigins.

Caching: In-memory en MovieService (TTL breve) para aliviar rate limits.

Configuraci�n
Socrata:BaseUrl, Socrata:ResourcePath, Socrata:AppToken

Cors:AllowedOrigins: [ "https://<tu-ui>" ]

Swagger:Enabled (permite activar en producci�n para validaci�n)

Errores/edge-cases
release_year puede venir como string ? se parsea seguro.

Lat/long a veces ausentes ? se omiten markers sin coordenadas.

Dataset puede devolver duplicados de t�tulo/locaci�n ? se agrupan en UI.

php
Copiar c�digo
