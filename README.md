# ISR Route Optimization App

This project is a web application that helps ISRs (Inside Sales Representatives) plan the optimal order to visit their leads, minimizing total travel distance.

The application allows ISRs to:
- Upload lead lists provided by their manager and their own personal list (CSV)
- Define their home address as the starting location
- Generate an optimized route that orders all leads efficiently

---

## Architecture Overview

The solution follows **Clean Architecture** principles:

### Backend (.NET)
The backend is built with **ASP.NET Core** and organized into clear layers:

- **Domain**
  - Core entities and business rules
  - Route optimization abstractions

- **Application**
  - Use cases implemented using **CQRS**
  - Commands and queries handled via **MediatR**
  - Input validation with **FluentValidation**
  - Repository contracts (interfaces)

- **Infrastructure**
  - Repository implementations
  - **In-memory database** used to simplify setup and allow easy testing without external dependencies

- **API**
  - REST endpoints
  - Request and response DTOs

This structure ensures the business logic is independent from persistence and UI concerns.

---

## Frontend (React)

The frontend is built with **React + TypeScript**, using a feature-based organization.

Key concepts:
- Components and pages grouped by feature
- API communication using **Axios**
- Interactive maps implemented with **Leaflet / React-Leaflet**
- Local state management using React hooks

Main features:
- Home address selection through an interactive map
- Lead upload via CSV files
- Optimized route visualization

---

## Technologies Used

### Backend
- ASP.NET Core
- MediatR (CQRS)
- FluentValidation
- In-memory database
- NUnit (unit tests)

### Frontend
- React
- TypeScript
- Vite
- React Router
- Axios
- Leaflet / React-Leaflet
- Bootstrap

---

## Notes

The use of an in-memory database allows the project to be run and tested quickly without additional setup.  
The architecture is designed to be easily extensible for future enhancements such as persistent storage, authentication, or more advanced routing algorithms.

## Sample CSV Files

The repository includes sample CSV files under `/samples/csv` that can be used to test the lead upload functionality without creating custom data.

These files are provided for demonstration purposes only and are not used by the application at runtime.
