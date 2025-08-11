# RBAC Authentication API

## Overview

This project implements a Role-Based Access Control (RBAC) authentication system using JWT tokens in a .NET 8 Web API. It includes:

- JWT authentication with tokens stored in HttpOnly cookies.

- Role-based authorization using enums and policies.

- Clean separation of concerns with token generation and current user services.

- Support for bearer token and cookie authentication.

- EF Core for database access and migrations.

- SQL Server as the database backend.

## Features

- JWT Token Generation: Create tokens with user ID and roles, configurable expiration.

- Authentication Middleware: Validates JWT tokens from cookies or Authorization headers.

- Role-based Authorization: Use [Authorize(Roles = "...")] or policies with enum roles.

- Auth Service: Easily get current user ID, roles, and role checks from claims.

- Login and Logout Endpoints: Issue and clear JWT cookies securely.

- Controller-wide Authorization: Apply [Authorize] globally with exceptions via [AllowAnonymous].

- EF Core Migrations: Database schema management with migrations.

- SQL Server Support: Using Microsoft SQL Server as the database.

## Technologies

- .NET 8 Web API

- Entity Framework Core (EF Core) for ORM and migrations

- Microsoft.AspNetCore.Authentication.JwtBearer

- SQL Server

- Dependency Injection (DI)

- Enums for Roles

- HttpOnly Cookies for token storage

## Getting Started

### Prerequisites

- .NET 8 SDK

- SQL Server instance (local or remote)

- IDE such as Visual Studio 2022 / VS Code

### Setup

1.  Clone the Reop

    ```bash
    git clone https://github.com/Sahil2k07/RBAC-ASP.NET-Core.git

    cd RBAC-ASP.NET-Core
    ```

2.  Configure your appsettings.json with SQL Server connection string and JWT settings:

    ```json
    {
      "Logging": {
        "LogLevel": {
          "Default": "Information",
          "Microsoft.AspNetCore": "Warning"
        }
      },
      "DbSettings": {
        "Host": "localhost",
        "Database": "RBAC",
        "User": "SA",
        "Password": "$hahil00"
      },
      "JwtSettings": {
        "TokenName": "rbac-token",
        "SecretKey": "aP7s9vE1xY4rT0zLqW8mNjHbCdFgUkIr",
        "Issuer": "http://localhost:5205",
        "Audience": "rbac-local",
        "ExpiryHours": 24
      }
    }
    ```

3.  Navigate to the `classlib` project to run migraion

    ```bash
    cd rbac-core
    ```

4.  Add the DB Creds in `rbac-core/migration.json` file as well as it will be needed to run migraion

    ```json
    {
      "DbSettings": {
        "Host": "localhost",
        "Database": "RBAC",
        "User": "SA",
        "Password": "$hahil00"
      }
    }
    ```

5.  Run EF Core migrations to create/update the database schema:

    ```bash
    dotnet ef database update
    ```

6.  Navigate the the `webapi` to start the project

    ```bash
    cd ..

    cd rbac-web
    ```

7.  Start the project in `Development` mode

    ```bash
    dotnet run
    ```

8.  Additionally you can make the Release build of the project using the command

    ```bash
    dotnet publish -c Release
    ```

- You can find the release build in the location `rbac-web/bin/Release/net8.0/publish`
