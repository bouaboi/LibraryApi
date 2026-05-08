# Library Management System API

A RESTful API for managing a library system, built with ASP.NET Core following a 3-tier architecture.

## Tech Stack

- **ASP.NET Core** — Web API framework
- **ADO.NET** — Data access with stored procedures
- **SQL Server** — Database
- **Swagger** — API documentation and testing

## Architecture

The project is structured into 5 layers:

- **LibraryApi** — Controllers and API endpoints
- **LibraryBusinessLayer** — Business logic and services
- **LibraryDataAccessLayer** — Database access using ADO.NET and stored procedures
- **LibraryDTOs** — Data transfer objects
- **LibraryModuls** — Entity models matching database tables

## Features

- Full CRUD for Members and Books
- Borrow and return book management
- Automatic fine generation for late returns
- Configurable settings (borrow limit, duration, fine rate)
- Member search by name or national ID
- Book search by title, author, or ISBN

## Getting Started

### Prerequisites

- Visual Studio 2022
- SQL Server
- .NET 8

### Setup

1. Clone the repository
2. Open `LibraryApi.sln` in Visual Studio
3. Run `Database/Library_MS.sql` in SQL Server Management Studio
4. Update the connection string in `DbConnection.cs`
5. Run the project and open Swagger

## API Endpoints

### Members
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | /api/Members | Get all members |
| GET | /api/Members/{memberId} | Get member by ID |
| GET | /api/Members/Search?query= | Search members |
| POST | /api/Members | Add new member |
| PUT | /api/Members/{memberId}/Update | Update member |
| PUT | /api/Members/{memberId}/reactivate | Reactivate member |
| DELETE | /api/Members/{memberId} | Deactivate member |

### Books
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | /api/Books | Get all books |
| GET | /api/Books/{bookId} | Get book by ID |
| GET | /api/Books/Search?query= | Search books |
| POST | /api/Books | Add new book |
| PUT | /api/Books/{bookId} | Update book |
| DELETE | /api/Books/{bookId} | Delete book |

### Borrows
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | /api/Borrows | Get all borrows |
| GET | /api/Borrows/{borrowId} | Get borrow by ID |
| GET | /api/Borrows/Active | Get active borrows |
| GET | /api/Borrows/Member/{memberId} | Get borrows by member |
| POST | /api/Borrows | Add new borrow |
| PUT | /api/Borrows/{borrowId} | Return book |

### Fines
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | /api/Fine | Get all fines |
| GET | /api/Fine/Member/{memberId} | Get fines by member |
| PUT | /api/Fine/{fineId} | Settle fine |

### Settings
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | /api/Settings | Get settings |
| PUT | /api/Settings | Update settings |

## Future Improvements

- JWT authentication and role-based authorization
- Migration from ADO.NET to EF Core