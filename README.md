
#  Clean Architecture Web API (.NET)

This project is a scalable and maintainable **ASP.NET Core Web API** built using the **Clean Architecture pattern**, which separates concerns across different layers: **API**, **Application/Service**, **Domain**, and **Infrastructure**.

It follows modern best practices like **CQRS**, **AutoMapper**, **custom exception handling**, **localization**, and **Entity Framework (Database-First)**.

---

## 🔧 Technologies & Tools

- ASP.NET Core Web API  
- Clean Architecture  
- CQRS (Command and Query Responsibility Segregation)  
- AutoMapper  
- EF Core (Database-First)  
- Generic Repository Pattern  
- Custom Response Wrapper  
- Global Error & Exception Handling Middleware  
- Localization (Multi-language support)  
- Dependency Injection  
- Swagger API Documentation

---

## 📁 Solution Structure

```
/YourProject
│
├── API                     # Presentation Layer (Routing, Controllers, Filters, Middlewares)
├── Application / Services  # Application Layer (CQRS Handlers, Interfaces, DTOs, Mapping)
├── Domain                  # Core Business Models (Entities, Enums, Interfaces)
├── Infrastructure          # External Services (DB, Repositories, Email, FileStorage)
│
├── YourProject.sln
```

---

## ✅ Features

- ✅ Clean architecture with proper separation of concerns  
- ✅ Command/Query patterns for handling logic (CQRS)  
- ✅ Generic Repository & Unit of Work  
- ✅ AutoMapper for DTO mapping  
- ✅ Global Exception Middleware with custom error response  
- ✅ Multilingual support using resource files  
- ✅ Swagger for API documentation  
- ✅ Database-First approach using EF Core  
- ✅ Custom API response model with metadata (status, data, message)

---

## 🚀 How to Run

1. **Configure Database Connection**  
   In `appsettings.json`, set your DB connection string:

   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=.;Database=YourDbName;Trusted_Connection=True;"
   }
   ```

2. **Restore Packages**

   ```bash
   dotnet restore
   ```

3. **Build the Project**

   ```bash
   dotnet build
   ```

4. **Run the API**

   ```bash
   dotnet run --project YourApiProjectName
   ```

5. **Access API Docs**

   Navigate to:  
   ```
   https://localhost:5001/swagger
   ```

---

## 📌 CQRS Example

```csharp
// Query
public class GetAllUsersQuery : IRequest<List<UserDto>> { }

// Handler
public class GetAllUsersHandler : IRequestHandler<GetAllUsersQuery, List<UserDto>>
{
    private readonly IUserRepository _repo;
    private readonly IMapper _mapper;

    public GetAllUsersHandler(IUserRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<List<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _repo.GetAllAsync();
        return _mapper.Map<List<UserDto>>(users);
    }
}
```

---

## 🌐 Localization

Localization is handled via `.resx` resource files. You can switch languages dynamically via request headers.

```http
GET /api/values
Accept-Language: ar
```

---

## 📥 Response Wrapper

Every API response is wrapped in a consistent format:

```json
{
  "success": true,
  "message": "Data retrieved successfully",
  "data": {
    "id": 1,
    "name": "Test Item"
  }
}
```

---

## 📌 Error Handling

Centralized error middleware handles exceptions and returns proper error response formats.

---

## 👩‍💻 Author

Created by Aya Nafed
MIT License

---
