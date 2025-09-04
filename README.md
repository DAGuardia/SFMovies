# SFMovies – Film Locations in San Francisco

Servicio + UI que muestran en un **mapa** dónde se filmaron películas en San Francisco.  
Permite **filtrar** por título con **autocompletado**.

> **Backend**: .NET 8 · **Frontend**: Angular (standalone) · **Mapas**: Leaflet · **Datos**: DataSF `yitu-d5am`

## Demo
- **UI**: https://sfmoviesmap.azurewebsites.net  
- **API**: https://sfmovies.azurewebsites.net/swagger  

## Estructura
```bash
SFMovies/
├─ backend/
│ ├─ SFMoviesBackend.sln
│ ├─ SFMovies.Api/ # ASP.NET Core Web API
│ ├─ SFMovies.Application/ # Casos de uso / Servicios
│ ├─ SFMovies.Domain/ # Entidades de dominio
│ ├─ SFMovies.Infrastructure/ # Integrations + Mappers (HttpClient a DataSF)
│ └─ SFMovies.Tests/ # xUnit
└─ ui/
└─ sfmovies-ui/ # Angular app (Leaflet, NSwag client)
```
Requisitos
.NET SDK 8.x

Node 20+ (PNPM o NPM)

Token Socrata App Token (DataSF)

## Ejecución local (dev)
### Backend
```bash
cd backend
cp SFMovies.Api/appsettings.json.example SFMovies.Api/appsettings.Development.json
# editar AppToken y CORS
dotnet run --project SFMovies.Api
# Swagger en https://localhost:7192/swagger (revisar puerto)
```

### Frontend
```bash
cd ui/sfmovies-ui
npm install
npm start
# http://localhost:4200
```

En dev la UI usa proxy `/api` → backend local.

## Build
### Backend
```bash
cd backend
dotnet publish SFMovies.Api -c Release -o out
```

### Frontend
```bash
cd ../ui/sfmovies-ui
ng build --configuration production
```

## Testing
### Backend
```bash
cd backend && dotnet test
```

### Frontend (si configuraste Karma/Jest)
```bash
cd ../ui/sfmovies-ui && npm test
```


