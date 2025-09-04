# SFMovies – Film Locations in San Francisco

Service + UI que muestran en un **mapa** dónde se filmaron películas en SF.  
Permite **filtrar** por título con **autocompletado** (DataSF).

## Tech
- Backend: .NET 8, ASP.NET Core, HttpClient, Caching in-memory, Swagger.
- Frontend: Angular, Standalone API, NSwag TypeScript client, Leaflet.
- Data: https://data.sfgov.org/ (dataset `yitu-d5am`).

## Estructura
- `/backend` – solución .NET (Api / Application / Domain / Infrastructure / Tests)
- `/ui/sfmovies-ui` – Angular app

## Cómo correr (dev)
```bash
# Backend
cd backend
cp SFMovies.Api/appsettings.json.example SFMovies.Api/appsettings.Development.json
dotnet build
dotnet run --project SFMovies.Api

# Frontend
cd ../ui/sfmovies-ui
npm install
npm start
