# Joyce Nguyen Contact Lens

An e-commerce platform for managing and selling contact lenses, built with a .NET 9 backend and a React admin portal. All services are containerised with Docker Compose.

## Architecture

```
docker-compose.yml
├── BackendApplication/    .NET 9 GraphQL API (HotChocolate)
├── admin-portal/          React 19 admin dashboard (TypeScript, MUI)
└── PostgreSQL 17           database
```

### Backend (`BackendApplication/`)

- **.NET 9** Web API exposing a **GraphQL** endpoint via [HotChocolate](https://chillicream.com/docs/hotchocolate)
- **Entity Framework Core 9** with PostgreSQL (Npgsql)
- **JWT authentication** (admin login, user registration/login)
- Domain models: `ContactLensType`, `User`, `Order`, `OrderItem`, `Address`
- Product image upload support via GraphQL file uploads

### Admin Portal (`admin-portal/`)

- **React 19** with **TypeScript**, bootstrapped with Create React App
- **Apollo Client** for GraphQL communication
- **MUI 7** (Material UI) component library with DataGrid
- Pages: Admin Login, Dashboard, Manage Products (CRUD + image upload), Manage Orders
- Protected routes with JWT-based auth

### Database

- **PostgreSQL 17**, provisioned automatically by Docker Compose
- Schema managed through EF Core migrations

## Getting Started

### Prerequisites

- [Docker](https://docs.docker.com/get-docker/) and Docker Compose

### Run

```bash
docker compose up --build
```

| Service         | URL                            |
| --------------- | ------------------------------ |
| GraphQL API     | http://localhost:8080/graphql   |
| Admin Portal    | http://localhost:3000           |
| PostgreSQL      | localhost:5432                  |

The database is created and migrated automatically on first startup in development mode.

## Tech Stack

| Layer     | Technology                                          |
| --------- | --------------------------------------------------- |
| Backend   | .NET 9, HotChocolate (GraphQL), EF Core 9, Dapper   |
| Frontend  | React 19, TypeScript, Apollo Client, MUI 7           |
| Database  | PostgreSQL 17                                        |
| Auth      | JWT (HS512)                                          |
| Infra     | Docker, Docker Compose                               |

## License

[MIT](LICENSE)
