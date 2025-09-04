```md
# API
```
Base URL (prod): `https://sfmovies.azurewebsites.net/api`

## Endpoints

### GET /movies
Lista películas (paginable) con locations geocodificadas si están disponibles.

**Query**
- `title` (opcional): filtro contains (case-insensitive)
- `limit` (int, default 100, max 500)
- `offset` (int, default 0)

**200 – Ejemplo**
```json
[
  {
    "title": "Ant-Man and the Wasp",
    "releaseYear": 2018,
    "productionCompany": "Marvel Studios",
    "director": "Peyton Reed",
    "writer": "Chris McKenna",
    "cast": ["Paul Rudd","Evangeline Lilly","Michael Douglas"],
    "locations": [
      {
        "address": "City Hall",
        "funFact": null,
        "latitude": 37.779275,
        "longitude": -122.4192417
      }
    ]
  }
]
```
# GET /movies/suggest

Autocomplete de títulos de películas.

## Query Parameters
- **prefix** (string, requerido, mínimo 2 caracteres)  
- **limit** (int, opcional, por defecto 10, máximo 20)  

## Respuesta `200 OK`
```json
[
  { "value": "Ant-Man" },
  { "value": "Ant-Man and the Wasp" }
]
```
