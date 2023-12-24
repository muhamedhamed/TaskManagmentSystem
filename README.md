Here's the structure that my solution goes through:

1. **Domain Layer (`TaskManagementSystem.Domain`):**
    - I Define entities representing core domain concepts ( **`User`**, **`Task`**).
    - Define repository interfaces for each entity (e.g., **`IUserRepository`**, **`ITaskRepository`**).
    - Implement a generic repository and unit of work patterns.
2. **Application Layer (`TaskManagementSystem.Application`):**
    - Define DTOs (Data Transfer Objects) for communication between my application and the Presentation layer (API).
    - Define service interfaces (e.g., **`IUserService`**, **`ITaskService`**) that encapsulate the application logic.
    - The interfaces defined here are simple and just cover basic process-related to the application
    - Implement service classes that use the repository interfaces from the domain layer.
    - Implement AutoMapper to map between entities and DTOs.
3. **Infrastructure Layer (`TaskManagementSystem.Infrastructure`):**
    - Implement concrete repository classes (e.g., **`UserRepository`**, **`TaskRepository`**) from the repository interfaces defined in the domain layer.
    - The first commit of the project focuses on the **`GenericRepository`**.
    - Implement the database context class (**`ApplicationDbContext`**) that inherits from **`DbContext`**.
    - after that, I go to the Dependency Injection Container in the API and Register the database context and repository implementations in the dependency injection container.
4. **API Layer (`TaskManagementSystem.Api`):**
    - Implement API controllers (e.g., **`UserController`**, **`TaskController`**) that use the service interfaces from the application layer.
    - Configure dependency injection for services in the API project's **`Program.cs`**.
    - Configure Swagger for API documentation.
    
    **DB used is MS SQL server.**
    
    **Simple clean architecture is applied.**
    
    **Serilog is used as a Logger.**
    
    **Added JWt Token-Based Authentication.**
    
    ---
    
    To Run the Project:
    
    - Need to use VS Code as it is created and configured on VS Code, It will work on Visual Studio Also but may need some additional Minner fixes.
    - Use SQL Server or SQL Server on Docker and update the Connection String, run the database update command to get the newly created database.
    - Use Postman as API Documentation or Swagger as it configured with Swagger UI.
    - Additional Apis are created as mentioned.
