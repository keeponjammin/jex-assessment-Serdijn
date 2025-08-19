
# JEX Assessment


This project is a full-stack web application built with ASP.NET Core (backend API) and Vue 3 (frontend, in `ClientApp`), designed for managing companies and vacancies.

## Features
- View, add, and manage companies and vacancies
- Modern Vue 3 frontend (Vite)
- API endpoints for CRUD operations

## Getting Started


### Prerequisites
- [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)
- [Node.js (LTS)](https://nodejs.org/)

### Requirements
- You need a local database (e.g., SQL Server or SQLite) set up and configured. Update your `appsettings.json` with the correct connection string for your environment.

### Generating TypeScript Interfaces from DTOs
You can use [NSwag](https://github.com/RicoSuter/NSwag) to generate TypeScript interfaces from your C# DTOs. To do this:
1. Make sure your back-end is running
2. Use the NSwag CLI or Studio to generate TypeScript interfaces:
	 - Example command:
		 ```sh
		 cd Clientapp
         npm run generate-types
		 ```
	 - This will generate TypeScript interfaces in the location specified in your `nswag.json` file.
For more details, see the [NSwag documentation](https://github.com/RicoSuter/NSwag).


### Backend (API)
1. Restore dependencies:
	```powershell
	dotnet restore
	```
2. Run the backend:
	```powershell
	dotnet run
	```
	The API will be available at `https://localhost:5001` (see `Properties/launchSettings.json`).

### Frontend (Vue 3)
1. Open a new terminal and go to the client folder:
	```powershell
	cd ClientApp
	npm install
	npm run dev
	```
	> NOTE: This is optional for development. The backend will build the production frontend for you when publishing.
2. Visit [http://localhost:3000](http://localhost:3000) in your browser. API requests to `/api` are proxied to the backend.

## API Overview
See Swagger UI at `https://localhost:5001/api`

## Wishlist / Improvements
If given more time, I would add or improve:
- **Proper error handling**: More robust error messages and user feedback in both backend and frontend.
- **Proper styling**: Consistent, responsive, and accessible UI/UX with a design system or component library.
- **Authorization**: Secure endpoints and UI with authentication and role-based access control.
- **Testing**: Automated tests for backend (unit/integration) and frontend (unit/e2e).