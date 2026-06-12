# FarmConnect Migration Guide: Node.js/React → .NET 8/Angular

## Overview
This guide details the complete migration from Node.js/Express + React to ASP.NET Core 8 + Angular 18.

## Timeline
- **Phase 1**: Backend migration (2-3 weeks)
- **Phase 2**: Frontend migration (2-3 weeks)
- **Phase 3**: Integration & testing (1-2 weeks)
- **Phase 4**: Deployment (1 week)

---

## Phase 1: Backend Migration (.NET 8)

### Prerequisites
```bash
# Install .NET 8 SDK
# Visit: https://dotnet.microsoft.com/en-us/download/dotnet/8.0

# Verify installation
dotnet --version  # Should show 8.x.x
```

### Project Setup
```bash
# Create solution
dotnet new sln -n FarmConnect

# Create projects
dotnet new webapi -n FarmConnect.API
dotnet new classlib -n FarmConnect.Core
dotnet new classlib -n FarmConnect.Infrastructure
dotnet new classlib -n FarmConnect.Services
dotnet new xunit -n FarmConnect.Tests

# Add to solution
dotnet sln FarmConnect.sln add FarmConnect.API
dotnet sln FarmConnect.sln add FarmConnect.Core
dotnet sln FarmConnect.sln add FarmConnect.Infrastructure
dotnet sln FarmConnect.sln add FarmConnect.Services
dotnet sln FarmConnect.sln add FarmConnect.Tests
```

### Core NuGet Packages
```bash
cd FarmConnect.API

# EF Core & Database
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Tools

# Firebase (or migrate to Cosmos DB)
dotnet add package FirebaseAdmin

# Authentication
dotnet add package System.IdentityModel.Tokens.Jwt
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer

# AI/ML
dotnet add package Azure.AI.OpenAI
dotnet add package Newtonsoft.Json

# Real-time
dotnet add package Microsoft.AspNetCore.SignalR

# Utilities
dotnet add package AutoMapper.Extensions.Microsoft.DependencyInjection
dotnet add package FluentValidation.DependencyInjectionExtensions
dotnet add package Serilog.AspNetCore

# API Documentation
dotnet add package Swashbuckle.AspNetCore
```

### API Endpoint Mapping

| Express Route | .NET 8 Endpoint | Controller | Status |
|---------------|-----------------|-----------|--------|
| `POST /api/auth/register` | `POST /api/auth/register` | AuthController | ⏳ |
| `POST /api/auth/login` | `POST /api/auth/login` | AuthController | ⏳ |
| `GET /api/auth/profile` | `GET /api/auth/profile` | AuthController | ⏳ |
| `PUT /api/auth/profile` | `PUT /api/auth/profile` | AuthController | ⏳ |
| `POST /api/ai/chat` | `POST /api/ai/chat` | AIController | ⏳ |
| `POST /api/ai/yield-prediction` | `POST /api/ai/yield-prediction` | AIController | ⏳ |
| `POST /api/ai/farm-health` | `POST /api/ai/farm-health` | AIController | ⏳ |
| `GET /api/dashboard/overview` | `GET /api/dashboard/overview` | DashboardController | ⏳ |
| `GET /api/weather/current/:location` | `GET /api/weather/current/{location}` | WeatherController | ⏳ |
| `GET /api/marketplace/listings` | `GET /api/marketplace/listings` | MarketplaceController | ⏳ |
| `GET /api/community/posts/:category` | `GET /api/community/posts/{category}` | CommunityController | ⏳ |

---

## Phase 2: Frontend Migration (Angular 18)

### Prerequisites
```bash
# Install Node.js 18+ and npm
# Install Angular CLI
npm install -g @angular/cli@18

# Verify installation
ng version
```

### Project Setup
```bash
# Create Angular project
ng new FarmConnect.Client --routing --style=css --package-manager=npm

cd FarmConnect.Client

# Add Tailwind CSS
ng add @ngneat/tailwind

# Install dependencies
npm install @angular/material @ngrx/store @ngrx/effects rxjs socket.io-client axios
```

### Project Structure
```
src/
├── app/
│   ├── modules/
│   │   ├── dashboard/
│   │   │   ├── components/
│   │   │   ├── services/
│   │   │   ├── store/
│   │   │   └── dashboard.module.ts
│   │   ├── auth/
│   │   ├── ai-assistant/
│   │   ├── marketplace/
│   │   ├── community/
│   │   ├── health/
│   │   └── weather/
│   ├── shared/
│   │   ├── services/
│   │   │   ├── api.service.ts
│   │   │   ├── auth.service.ts
│   │   │   └── websocket.service.ts
│   │   ├── interceptors/
│   │   ├── guards/
│   │   └── shared.module.ts
│   ├── app.module.ts
│   ├── app-routing.module.ts
│   └── app.component.ts
├── environments/
└── styles/
    └── global.css
```

### Component Mapping

| React Component | Angular Component | Status |
|-----------------|-------------------|--------|
| Dashboard | DashboardComponent | ⏳ |
| ChatInterface | ChatComponent | ⏳ |
| Marketplace | MarketplaceComponent | ⏳ |
| Community | CommunityComponent | ⏳ |
| Weather | WeatherComponent | ⏳ |
| Profile | ProfileComponent | ⏳ |

---

## Phase 3: Integration Checklist

- [ ] Database migration (Firebase → Cosmos DB/SQL Server)
- [ ] Authentication flow (JWT implementation)
- [ ] API integration (.NET backend ↔ Angular frontend)
- [ ] Real-time communication (SignalR setup)
- [ ] File uploads (Azure Blob Storage)
- [ ] Environment configuration
- [ ] Unit tests
- [ ] Integration tests

---

## Phase 4: Deployment

### Backend Deployment (.NET 8)
```bash
# Publish release build
dotnet publish -c Release -o ./publish

# Deploy to Azure App Service, AWS, or Docker
```

### Frontend Deployment (Angular)
```bash
# Build production
ng build --configuration production

# Deploy to Vercel, Netlify, or Azure Static Web Apps
```

---

## Key Improvements in Migration

### Backend (.NET 8)
✅ Better performance & scalability
✅ Strong typing with C#
✅ Built-in dependency injection
✅ Entity Framework Core for ORM
✅ Integrated logging & monitoring

### Frontend (Angular)
✅ Stronger typing with TypeScript
✅ Better state management (NgRx)
✅ Modular architecture
✅ Built-in testing framework (Jasmine/Karma)
✅ Better SEO support (SSR ready)

---

## Rollback Plan

If issues arise:
1. Keep both stacks running in parallel for 2-4 weeks
2. Use feature flags to switch between old/new implementations
3. Maintain database synchronization
4. Monitor error rates closely

---

## Resources

- [ASP.NET Core 8 Documentation](https://learn.microsoft.com/en-us/aspnet/core/)
- [Angular 18 Documentation](https://angular.io/docs)
- [Entity Framework Core](https://learn.microsoft.com/en-us/ef/core/)
- [SignalR Documentation](https://learn.microsoft.com/en-us/aspnet/core/signalr/)
- [Azure Services](https://azure.microsoft.com/en-us/services/)

---

## Support & Questions

For detailed implementation files, refer to:
- `Backend/` - .NET 8 backend implementation
- `Frontend/` - Angular 18 frontend implementation
- `Database/` - Migration scripts
