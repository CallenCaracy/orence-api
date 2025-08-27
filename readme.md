# OrenceWebApi
This is the API backend for Orence, built with .NET and C#.

---

## Overview
OrenceWebApi provides the foundational backend services for the Orence project, focusing on authentication and user management.  
It leverages C# and ASP.NET Core for robust and scalable API design.

---

## Project Structure

### Controllers
Handles HTTP requests and routes (e.g., `Auth.cs` for authentication).

### Data
Contains data context classes (e.g., `AppDbContext.cs`).

### Models
Defines data transfer objects (DTOs) and core models (`JwtSettings.cs`, `User.cs`).

### Services
Business logic and utilities such as JWT handling (`JwtService.cs`).

### Configuration
- `appsettings.json`: Configuration settings for the application.

### Entry Point
- `Program.cs` and `Setup.cs`: Application startup configuration.

---

## Getting Started

1. Install .NET SDK (latest version recommended).
2. Restore dependencies:
   ```bash
   dotnet restore
   ```
3. Update `appsettings.json` with your configuration preferences.
4. Run the API:
   ```bash
   dotnet run
   ```

---

## Features
- JWT-based authentication  
- Modular service design  
- Clean separation of concerns  

---

## Technologies Used
- C#  
- ASP.NET Core  

---

## License
This project is intended for personal or educational use.
