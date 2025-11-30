
# BloodLink

**Author:** Hadeer Sabry  
**Repository:** [marwa-gameil/BloodLink](https://github.com/marwa-gameil/BloodLink)  

BloodLink is a multi-tier .NET application designed to manage blood bank operations, hospital requests tracking. The system provides APIs for data management and a web interface for user interaction.

---

## Table of Contents

- [Features](#features)  
- [Technology Stack](#technology-stack)  
- [System Requirements](#system-requirements)  
- [Installation](#installation)  
- [Configuration](#configuration)  
- [Execution Guide](#execution-guide)  
- [API Documentation](#api-documentation)  
- [Deployment](#deployment)  
- [Contributing](#contributing)  
- [License](#license)  

---

## Features

- User authentication and role-based access (Admin, Hospital, Blood Bank).  
- Manage blood stocks and requests.  
- Send notifications for blood requirements.
- user can see the nearest Blood Bank
- Audit logs for system operations.  

---

## Technology Stack

- **Backend:** .NET 7, C#  
- **Database:** SQL Server  
- **Frontend:** ASP.NET MVC / Razor Pages  
- **ORM:** Entity Framework Core  
- **Authentication:** ASP.NET Identity  
- **Testing:** xUnit / MSTest  

---

## System Requirements

- Windows 10 or higher / macOS / Linux  
- [.NET SDK 7.0](https://dotnet.microsoft.com/download/dotnet/7.0)  
- SQL Server 2019 or higher  
- Visual Studio 2022 or VS Code  

---

## Installation

1. Clone the repository:  
   ```bash
   git clone https://github.com/marwa-gameil/BloodLink.git
   cd BloodLink
   ```
 2. Restore dependencies:
```bash
   dotnet restore
```
3. Apply database migrations:
   
```bash
dotnet ef database update --project App.Infrastructure
```

4. Build the solution:
```bash
dotnet build
```

---

## Configuration

1. Copy `.env.example` to `.env` (or configure `appsettings.json` in `App.API`):

   ```env
   CONNECTION_STRING=<Your SQL Server Connection String>
   JWT_SECRET=<Your JWT Secret>
   ```

2. Update roles, email settings, or other configurations in `appsettings.json`.

---

## Execution Guide

1. Navigate to the API project and run:

   ```bash
   dotnet run --project App.API
   ```

2. Navigate to the Web project and run:

   ```bash
   dotnet run --project App.Web
   ```

3. Access the web application:

   ```
   http://localhost:5000
   ```

---

## API Documentation

* Base URL: `http://localhost:5000/api/`
* Example endpoints:

  * `GET /api/users` – Retrieve all users
  * `POST /api/users` – Create a new user
  * `GET /api/bloodrequests` – Retrieve all blood requests
  * `POST /api/bloodrequests` – Create a new blood request

> For full API documentation, see [API Documentation](docs/API.md) (if available).

---

## Deployment

1. Build the application for production:

   ```bash
   dotnet publish -c Release
   ```

2. Deploy to a web server or cloud platform (Azure, AWS, etc.).

3. Ensure database connection strings and secrets are configured properly.

---

## Contributing

1. Fork the repository.
2. Create a new branch:

   ```bash
   git checkout -b feature/YourFeature
   ```
3. Commit your changes:

   ```bash
   git commit -m "Add some feature"
   ```
4. Push to your branch:

   ```bash
   git push origin feature/YourFeature
   ```
5. Open a Pull Request.

---

## License

This project is licensed under the MIT License.

---

**BloodLink** – Simplifying blood request management for hospitals and blood banks.


![logo Red](https://github.com/user-attachments/assets/7740071a-2b15-4063-85a6-e1c86a3e95d1)

