## Fundraising System API

This Project is an API (Application Programming Interface) for a fundraising system developed using ASP.NET Core. The project aims to manage donations, projects, and users while providing high security and scalability.

## Overview

The fundraising system allows users to efficiently manage donations, including creating donations, viewing donations by project, and managing users. The system focuses on security and ease of use.

## Technologies and Tools Used

- **ASP.NET Core**: The primary framework for building web applications, offering high performance and flexibility in development.
- **Entity Framework Core**: An ORM (Object-Relational Mapping) library used to facilitate database interactions.
- **AutoMapper**: A library for mapping objects between different models, simplifying data transfer between layers.
- **JWT (JSON Web Tokens)**: An authentication system used to secure APIs through tokens generated upon login.
- **Serilog**: A logging library supporting writing logs to databases and log files.
- **Swagger**: A tool for documenting APIs, making them easier to test and understand.
- **MSSQL**: The database used to store data.

## Architecture Structure

### 1. **Infrastructure**

This layer includes all the code related to database interaction:

- **ApplicationDbContext**: The database context that uses Entity Framework to interact with the database. It includes table definitions such as `Donations` and `Projects`.
  
- **Repositories**: Implements the repository pattern to separate data access logic from business logic:
  - `IDonationRepository`: Interface for managing donation operations.
  - `IProjectRepository`: Interface for managing projects.
  
- **Repository Implementations**: Implementations of the interfaces in `Infrastructure.RepositoryImplementation`, such as `DonationRepository` and `ProjectRepository`.

### 2. **Application Services**

Contains business logic and data coordination:

- **DTOs (Data Transfer Objects)**: Objects used to transfer data between layers, including:
  - `DonationDto`: Donation data.
  - `ProjectDto`: Project data.
  - `AuthDto`: Authentication data.
  
- **Business Services**: Services containing business logic:
  - `DonationService`: Manages donation operations.
  - `ProjectService`: Manages project operations.
  - `IdentityService`: Manages authentication and user management.

### 3. **Controllers**

Contains the APIs that handle incoming client requests:

- **DonationController**: Manages donations, including creating, updating, and deleting donations.
- **ProjectController**: Manages projects, including creating, updating, and deleting projects.
- **IdentityController**: Manages authentication, registration, and role management.

### 4. **Configurations**

Includes necessary configuration settings such as JWT settings, database settings, and Serilog settings. These settings are stored in the `appsettings.json` file.

### 5. **Error Handling**

`GlobalExceptionHandlingMiddleware` is used for centralized error handling in the application. Errors are logged, and appropriate responses are returned to the user.

## How to Run

1. **Install Requirements**:
   - Ensure the appropriate .NET SDK is installed on your machine.

2. **Set Up Database**:
   - Update the connection settings in `appsettings.json` if necessary.

3. **Run the Application**:
   - Open the project in Visual Studio or use the command line.
   - Run the project using F5 or from the command line using `dotnet run`.

4. **Test the API**:
   - Use Swagger to test the APIs or tools like Postman.

## How to Use

- **Register a New User**: Use the endpoint `POST api/identity/register` to register a new user.
- **Log In**: Use the endpoint `POST api/identity/token` to obtain a JWT token.
- **Create a Donation**: Use the endpoint `POST api/donation` to create a new donation.
- **View All Donations**: Use the endpoint `GET api/donation` to view all donations.
- **Create a Project**: Use the endpoint `POST api/projects` to create a new project.

## Conclusion

This project is a comprehensive example of how to build an integrated fundraising system using modern technologies. You can expand or modify the project according to your specific needs. Feel free to contribute or ask questions!
