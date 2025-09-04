# Deploy

## Backend – Azure Web App (Linux, .NET 8)
1. `dotnet publish SFMovies.Api -c Release -o ./publish`
2. Portal → Web App (.NET 8 Linux).
3. **Configuration → Application settings**:
   - `Socrata__BaseUrl`
   - `Socrata__ResourcePath`
   - `Socrata__AppToken`
   - `Cors__AllowedOrigins__0 = https://<tu-ui>.azurewebsites.net`
   - `Swagger__Enabled = true` (opcional)
4. Subir artefacto (Zip Deploy, VS Publish o GitHub Action).
5. Probar `/swagger` y `/api/movies?title=ant`.

## Frontend – Azure Web App (Linux, Node 20)
1. `ng build --configuration production`
2. **Startup Command**:
   ```bash
   pm2 serve /home/site/wwwroot --no-daemon --spa
rrr
3. Subir `dist/<app>/` a `/home/site/wwwroot` (Kudu, FTP o ZipDeploy).
4. Probar `https://<tu-ui>.azurewebsites.net/`.

## CORS
En backend, permitir el origen de la UI:

```json
"Cors": {
  "AllowedOrigins": [
    "https://<tu-ui>.azurewebsites.net"
  ]
}
```

O bien, como variables de entorno:

```ini
Cors__AllowedOrigins__0 = https://<tu-ui>.azurewebsites.net
```
