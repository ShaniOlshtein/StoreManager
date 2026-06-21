# StoreManager API

A RESTful Web API built with ASP.NET Core 8.0 for managing a store's products, categories, and orders.

---

## 🏗️ Project Architecture

The project follows a layered architecture:

```
API            → Controllers, DTOs, Middleware (Presentation Layer)
Services       → Business logic, AutoMapper profiles
Repositories   → Data access interfaces and implementations
Entities       → EF Core models, DbContext
Common         → Shared DTOs used across layers
```

Each layer depends only on the layer below it. The API never accesses the database directly.

---

## 🗄️ Database

The project uses **MySQL** hosted on **Aiven Cloud**.

### Tables

| Table | Description |
|---|---|
| `Categories` | Product categories |
| `Products` | Store products, each belonging to a category |
| `Orders` | Customer orders |
| `OrderItems` | Join table between Orders and Products (Many-to-Many) with Quantity and UnitPrice |

### Relationships
- `Product` → `Category` (Many-to-One)
- `OrderItem` → `Order` (Many-to-One)
- `OrderItem` → `Product` (Many-to-One)
- `Order` ↔ `Product` via `OrderItem` (Many-to-Many)

---

## ⚙️ Installation & Setup

### Prerequisites
- [.NET 8.0 SDK](https://dotnet.microsoft.com/download)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or later
- Access to a MySQL server (local or cloud)

### Steps

1. Clone the repository:
```bash
git clone <YOUR_GITHUB_REPO_URL>
cd StoreManager
```

2. Update the connection string in `API/appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=<YOUR_SERVER>;Port=<YOUR_PORT>;Database=<YOUR_DB>;User=<YOUR_USER>;Password=<YOUR_PASSWORD>;SslMode=Required;"
  }
}
```

3. Also update `Entities/StoreDbContextFactory.cs` with the same connection string (used only for EF CLI migrations).

4. Apply the database migrations from the solution root:
```bash
dotnet ef database update --project Entities --startup-project API
```

5. Run the project:
```bash
dotnet run --project API
```
Or press `F5` / `Ctrl+F5` in Visual Studio.

---

## 🛠️ Testing the API with Swagger

1. Run the project in Visual Studio by pressing `F5`.

2. The browser will open automatically at the Swagger UI, or navigate manually to:
```
https://localhost:XXXX/swagger
```
(replace `XXXX` with the port shown in the console output)

3. You will see all available endpoints grouped by entity:
   - **Categories** — GET, POST, PUT, DELETE
   - **Products** — GET, POST, PUT, DELETE
   - **Orders** — GET, POST, PUT, DELETE
   - **OrderItems** — GET, POST, PUT, DELETE

4. To test an endpoint:
   - Click on the endpoint (e.g. `GET /api/products`)
   - Click **Try it out**
   - Click **Execute**
   - The response body and status code (e.g. `200 OK`) will appear below

5. To create a new product (POST example):
   - Click on `POST /api/products`
   - Click **Try it out**
   - Replace the request body with:
   ```json
   {
     "name": "Laptop",
     "price": 2999.99,
     "categoryId": 1
   }
   ```
   - Click **Execute** — you should receive `201 Created` with the new product

---

## 📦 Key Technologies

- ASP.NET Core 8.0
- Entity Framework Core 8.0
- Pomelo.EntityFrameworkCore.MySql
- AutoMapper
- Swagger / Swashbuckle
