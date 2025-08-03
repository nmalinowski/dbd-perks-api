# Dead by Daylight Perks API

<img alt=".NET" src="https://github.com/nmalinowski/dbd-perks-api/actions/workflows/dotnet.yml/badge.svg">
<img alt="Export Swagger UI and Publish to GitHub Pages" src="https://github.com/nmalinowski/dbd-perks-api/actions/workflows/swagger-export.yml/badge.svg">

[![GitHub Pages](https://img.shields.io/badge/GitHub%20Pages-Swagger%20UI-blue?logo=github)](https://nmalinowski.github.io/dbd-perks-api/)

**Live Demo & Documentation:**

- [Swagger UI (GitHub Pages)](https://nmalinowski.github.io/dbd-perks-api/)
- [Swagger (live)](http://dbdperksapi.bgdwd9dxb7hfezd5.centralus.azurecontainer.io:8080/swagger/index.html)
- [Main API/Frontend](http://dbdperksapi.bgdwd9dxb7hfezd5.centralus.azurecontainer.io:8080)
- [Perk Database](http://dbdperksapi.bgdwd9dxb7hfezd5.centralus.azurecontainer.io:8080/index.html)
- [Tier List](http://dbdperksapi.bgdwd9dxb7hfezd5.centralus.azurecontainer.io:8080/tier-list.html)
- [Random Build Generator](http://dbdperksapi.bgdwd9dxb7hfezd5.centralus.azurecontainer.io:8080/random-build.html)
- [Create Your Own Build](http://dbdperksapi.bgdwd9dxb7hfezd5.centralus.azurecontainer.io:8080/create-build.html)
- [Meta Builds](http://dbdperksapi.bgdwd9dxb7hfezd5.centralus.azurecontainer.io:8080/meta-builds.html)
- [Otzdarva Builds](http://dbdperksapi.bgdwd9dxb7hfezd5.centralus.azurecontainer.io:8080/otzdarva-builds.html)

## Features

- **Data-driven:** All perk data is managed in a single JSON file (`DbdPerksApi/Data/perks.json`).
- **Local icon hosting:** All perk icons are served statically from `DbdPerksApi/wwwroot/perk_icons`.
- **Automatic loading:** The API loads all perks from the JSON file at startup.
- **Easy updates:** Add or update perks by editing the JSON and running the `perk_downloader.py` script.
- **Modern C#:** Uses .NET 9 and C# 13 features where appropriate.

## Project Structure

- `DbdPerksApi/` - Main API project
  - `Models/Perk.cs` - Perk model and enums
  - `Data/perks.json` - All perk data
  - `Data/PerkDatabase.cs` - Loads perks from JSON
  - `Services/IPerkService.cs` - Perk service interface
  - `Services/PerkService.cs` - Perk service implementation
  - `Controllers/PerksController.cs` - API controller
  - `Program.cs` - Entry point and configuration
  - `wwwroot/perk_icons/` - All perk icon images (PNG)
- `DbdPerksApi.Tests/` - Test project
- `.github/copilot-instructions.md` - Workspace instructions
- `perk_downloader.py` - Script for icon download and JSON update

## How to Add or Update Perks

1. **Edit `perks.json`:** Add the new perk with its name, description, character, type, and icon filename.
2. **Run the script:** Execute `python3 perk_downloader.py` to download/copy the icon and update the JSON with the correct URL.
3. **Icon URL format:** All icon URLs in `perks.json` must be `/perk_icons/{sanitized_name}.png` (lowercase, hyphens for spaces/specials, apostrophes/ampersands removed).
4. **No individual perk classes:** Do not create or edit files like `CharacterNamePerks.cs` or `GenericPerks.cs`. All logic is data-driven.

## API Endpoints

- `GET /api/perks` - List all perks
- `GET /api/perks/{name}` - Get details for a specific perk
- Static icons: `GET /perk_icons/{filename}.png`

## Icon Hosting & Static Files

- All icons are served from `wwwroot/perk_icons` via ASP.NET Core's static file middleware.
- Ensure new icons are placed in this directory and referenced in the JSON as `/perk_icons/{filename}.png`.

## API Documentation (Swagger)

- Swagger UI is available at [http://localhost:5265/swagger](http://localhost:5265/swagger) when running the API locally or in a container.
- The OpenAPI spec is served at `/swagger/v1/swagger.json`.
- Use Swagger UI to explore, test, and document all API endpoints interactively.

### Exporting Swagger UI for GitHub Pages

1. Run the API locally and visit `/swagger` to verify the documentation.
2. Export the OpenAPI spec:
   ```sh
   curl http://localhost:5265/swagger/v1/swagger.json -o openapi.json
   ```
3. Use a tool like [`swagger-ui-dist`](https://www.npmjs.com/package/swagger-ui-dist) or [Swagger Editor](https://editor.swagger.io/) to generate static HTML files from `openapi.json`.
4. Place the generated static Swagger UI files in a `docs/` folder in your repository.
5. Enable GitHub Pages for the `docs/` folder in your repository settings.
6. Your API documentation will be publicly available via GitHub Pages.

## Development

- Requires .NET 9 SDK
- Python 3 for running the downloader script
- To run the API:
  ```sh
  dotnet run --project DbdPerksApi
  ```
- To run tests:
  ```sh
  dotnet test
  ```

## Contributing

- Add new perks only via `perks.json` and the script.
- Keep icon filenames sanitized and consistent.
- Update documentation if API endpoints or workflow changes.

## Acknowledgments 🙌

https://www.nightlight.gg, thanks for the icons :X
https://www.deadbydaylight.com
https://www.bhvr.com

## License

MIT
