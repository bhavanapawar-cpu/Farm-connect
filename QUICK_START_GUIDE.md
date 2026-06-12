# .NET 8 & Angular Migration - Quick Start Guide

## Prerequisites

### System Requirements
- **.NET 8 SDK** (Windows, macOS, Linux)
- **Node.js 18+** with npm
- **Angular CLI 18+**
- **SQL Server** (LocalDB or full version)
- **Visual Studio Code** or **Visual Studio 2022**

### Installation

#### 1. Install .NET 8 SDK
```bash
# Download from: https://dotnet.microsoft.com/en-us/download/dotnet/8.0
dotnet --version
```

#### 2. Install Angular CLI
```bash
npm install -g @angular/cli@18
ng version
```

---

## Backend Setup (.NET 8)

### Step 1: Create Project Structure
```bash
cd Backend
dotnet new sln -n FarmConnect
dotnet new webapi -n FarmConnect.API
dotnet new classlib -n FarmConnect.Core
dotnet new classlib -n FarmConnect.Infrastructure
dotnet new xunit -n FarmConnect.Tests

# Add projects to solution
dotnet sln FarmConnect.sln add FarmConnect.API
dotnet sln FarmConnect.sln add FarmConnect.Core
dotnet sln FarmConnect.sln add FarmConnect.Infrastructure
dotnet sln FarmConnect.sln add FarmConnect.Tests
```

### Step 2: Add Project References
```bash
cd FarmConnect.API
dotnet add reference ../FarmConnect.Core
dotnet add reference ../FarmConnect.Infrastructure

cd ../FarmConnect.Infrastructure
dotnet add reference ../FarmConnect.Core

cd ..
```

### Step 3: Install NuGet Packages
```bash
cd FarmConnect.API

# Entity Framework
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Tools

# Authentication
dotnet add package System.IdentityModel.Tokens.Jwt
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer

# AI/Services
dotnet add package Azure.AI.OpenAI
dotnet add package FirebaseAdmin

# SignalR
dotnet add package Microsoft.AspNetCore.SignalR

# Utilities
dotnet add package AutoMapper.Extensions.Microsoft.DependencyInjection
dotnet add package FluentValidation.DependencyInjectionExtensions
dotnet add package Swashbuckle.AspNetCore

cd ..
```

### Step 4: Database Migration
```bash
cd FarmConnect.API
dotnet ef migrations add InitialCreate -p ../FarmConnect.Infrastructure -s .
dotnet ef database update
```

### Step 5: Run Backend
```bash
cd FarmConnect.API
dotnet run
# Server running at: https://localhost:5000
```

---

## Frontend Setup (Angular 18)

### Step 1: Create Angular Project
```bash
ng new FarmConnect --routing --style=scss --package-manager=npm
cd FarmConnect
```

### Step 2: Install Dependencies
```bash
npm install @angular/material @ngrx/store @ngrx/effects rxjs socket.io-client axios

# Tailwind CSS (optional but recommended)
ng add @ngneat/tailwind
```

### Step 3: Project Structure
```bash
# Create directories
mkdir -p src/app/modules/{auth,dashboard,ai-assistant,marketplace,community,health,weather}
mkdir -p src/app/shared/{services,interceptors,guards}
mkdir -p src/environments
```

### Step 4: Generate Components
```bash
ng generate component modules/auth/components/login
ng generate component modules/auth/components/register
ng generate component modules/dashboard/pages/dashboard
ng generate service shared/services/auth
ng generate guard shared/guards/auth
```

### Step 5: Update Environment Configuration

Edit `src/environments/environment.ts`:
```typescript
export const environment = {
  production: false,
  apiUrl: 'http://localhost:5000/api',
  wsUrl: 'http://localhost:5000'
};
```

### Step 6: Run Frontend
```bash
ng serve
# Application running at: http://localhost:4200
```

---

## API Endpoints Reference

### Authentication
```
POST   /api/auth/register
POST   /api/auth/login
GET    /api/auth/profile
PUT    /api/auth/profile
```

### Dashboard
```
GET    /api/dashboard/overview
GET    /api/dashboard/analytics
GET    /api/dashboard/activities
```

### AI Features
```
POST   /api/ai/chat
POST   /api/ai/yield-prediction
POST   /api/ai/farm-health
```

### Farms
```
GET    /api/farms
GET    /api/farms/{id}
POST   /api/farms
PUT    /api/farms/{id}
DELETE /api/farms/{id}
```

### Weather
```
GET    /api/weather/current/{location}
GET    /api/weather/forecast/{location}
GET    /api/weather/alerts/{location}
```

### Marketplace
```
GET    /api/marketplace/listings
POST   /api/marketplace/listings
GET    /api/marketplace/listings/{id}
PUT    /api/marketplace/listings/{id}
DELETE /api/marketplace/listings/{id}
```

### Community
```
GET    /api/community/posts/{category}
POST   /api/community/posts
GET    /api/community/posts/{id}
POST   /api/community/posts/{id}/replies
DELETE /api/community/posts/{id}
```

---

## Configuration Files

### appsettings.json (Backend)
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=FarmConnectDb;Trusted_Connection=true;"
  },
  "Jwt": {
    "SecretKey": "your-super-secret-key",
    "Issuer": "FarmConnect",
    "Audience": "FarmConnectUsers"
  },
  "Cors": {
    "AllowedOrigins": ["http://localhost:4200"]
  }
}
```

### environment.ts (Frontend)
```typescript
export const environment = {
  production: false,
  apiUrl: 'http://localhost:5000/api'
};
```

---

## Development Workflow

### Backend Development
```bash
# Open in Visual Studio or VS Code
code Backend/

# Or use CLI
dotnet watch run  # Auto-recompiles on changes
```

### Frontend Development
```bash
cd Frontend/

# Start dev server with auto-reload
ng serve

# Run tests
ng test

# Build for production
ng build --configuration production
```

---

## Testing

### Backend Unit Tests
```bash
cd Backend/FarmConnect.Tests
dotnet test
```

### Frontend Unit Tests
```bash
cd Frontend/
ng test
```

### Integration Tests
```bash
# API Integration Tests
cd Backend/FarmConnect.Tests
dotnet test --filter "Category=Integration"

# E2E Tests
cd Frontend/
ng e2e
```

---

## Docker Deployment

### Backend Dockerfile
```dockerfile
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["FarmConnect.API/FarmConnect.API.csproj", "FarmConnect.API/"]
RUN dotnet restore "FarmConnect.API/FarmConnect.API.csproj"
COPY . .
RUN dotnet build "FarmConnect.API/FarmConnect.API.csproj" -c Release

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/bin/Release/net8.0/publish .
EXPOSE 5000
ENTRYPOINT ["dotnet", "FarmConnect.API.dll"]
```

### Frontend Dockerfile
```dockerfile
FROM node:18 AS build
WORKDIR /app
COPY package*.json ./
RUN npm ci
COPY . .
RUN npm run build

FROM nginx:alpine
COPY --from=build /app/dist/farmconnect /usr/share/nginx/html
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
```

---

## Troubleshooting

### Database Connection Issues
```bash
# Check SQL Server is running
# Windows: Services.msc
# Linux/Mac: Check Docker or local installation

# Update connection string in appsettings.json
```

### Port Conflicts
```bash
# Change backend port
# In Program.cs: .UseUrls("http://localhost:5001")

# Change frontend port
# Run: ng serve --port 4300
```

### CORS Errors
```bash
# Ensure CORS is configured in Program.cs
# Update AllowedOrigins in appsettings.json
```

---

## Support & Resources

- [.NET Documentation](https://docs.microsoft.com/en-us/dotnet/)
- [Angular Documentation](https://angular.io/docs)
- [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/)
- [SignalR Guide](https://docs.microsoft.com/en-us/aspnet/core/signalr/)

---

**Happy Coding! 🚀**
