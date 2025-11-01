     dotnet restore
     dotnet tool install --global dotnet-ef
     cd App.Web
     dotnet ef migrations add InitialInfrastructure --project ../App.Infrastructure --context Infrastructure.Data.ApplicationDbContext --output-dir ../App.Infrastructure/Data/Migrations
     dotnet ef database update --project ../App.Infrastructure --context Infrastructure.Data.ApplicationDbContext
     dotnet run
     ```
  3. The app will start at the URLs in `App.Web/Properties/launchSettings.json`:
     - HTTP: `http://localhost:5149`
     - HTTPS: `https://localhost:7105`

- **Run with Visual Studio**
  - Open the solution, select the `App.Web` profile, press F5.

- **Notes**
  - The app is an ASP.NET Core MVC app with Identity. Entry point: `App.Web/Program.cs`.
  - DB context: `App.Web/Data/ApplicationDbContext.cs` with migrations included.

### Where is the frontend and how to build it

- **Frontend type**: Razor MVC (server-rendered) with **Tailwind CSS** for styling.
  - Views live under `App.Web/Views` (e.g., `Views/Home/Index.cshtml`).
  - Layout and shared UI in `Views/Shared` (e.g., `_Layout.cshtml`).
  - Static assets (CSS/JS/libs) in `App.Web/wwwroot`.
  - Tailwind CSS is loaded via CDN in `_Layout.cshtml` - no build step needed!
- **Build**: Happens automatically with the backend:
  - `dotnet build` or `dotnet run` builds both backend and Razor views.
  - No separate `npm install` / `npm run build` is required.
  - Tailwind CSS classes compile in the browser via CDN.

### How to expose/use API endpoints from the frontend

Right now, there are only MVC controllers (e.g., `HomeController`). If you want JSON APIs:

- **Add an API controller**
  - Create `Controllers/Api/DonorsController.cs`:
    ```csharp
    using Microsoft.AspNetCore.Mvc;

    namespace App.Web.Controllers.Api
    {
        [ApiController]
        [Route("api/[controller]")]
        public class DonorsController : ControllerBase
        {
            [HttpGet]
            public IActionResult GetAll() => Ok(new[] { new { id = 1, name = "Alice" } });

            [HttpGet("{id:int}")]
            public IActionResult GetById(int id) => Ok(new { id, name = "Alice" });
        }
    }
    ```
  - This works immediately because `Program.cs` already has `builder.Services.AddControllersWithViews();` and `app.MapControllerRoute(...)`.

- **Call the API from a Razor view (same origin)**
  - In `Views/Home/Index.cshtml`:
    ```html
    <div id="donors"></div>
    <script>
      fetch('/api/donors')
        .then(r => r.json())
        .then(items => {
          document.getElementById('donors').innerHTML =
            items.map(x => `<div>${x.id} - ${x.name}</div>`).join('');
        })
        .catch(console.error);
    </script>
    ```
  - Same-origin calls don’t need CORS.

- **If you later add a separate SPA (different origin)**
  - Enable CORS in `Program.cs`:
    ```csharp
    var MyCors = "_myCors";
    builder.Services.AddCors(o => o.AddPolicy(MyCors, p =>
      p.WithOrigins("http://localhost:5173").AllowAnyHeader().AllowAnyMethod()));
    var app = builder.Build();
    app.UseCors(MyCors);
    ```
  - Then call `https://localhost:7105/api/donors` from the SPA.

- **Alternative without a dedicated API**
  - You can return JSON from an MVC action:
    ```csharp
    public IActionResult DonorList() => Json(new[] { new { id = 1, name = "Alice" } });
    ```
  - Fetch from `/Home/DonorList`.

### Quick checklist
- Run DB migrations: `dotnet ef database update`
- Start the app: `dotnet run` (use HTTPS `https://localhost:7105`)
- Frontend edits go in `App.Web/Views` and `App.Web/wwwroot`
- Add API controllers under `App.Web/Controllers` (or `Controllers/Api`) and fetch with `/api/...`

- Made sure where to run it (`App.Web`), where the frontend lives (`Views`/`wwwroot`), and how to add/use API endpoints both same-origin and cross-origin.