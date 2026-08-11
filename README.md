# InternetShop API

REST API интернет-магазина на **ASP.NET Core Web API**.

Проект создан в учебных целях для практики backend-разработки на **C#, ASP.NET Core, Entity Framework Core, PostgreSQL, JWT и Docker**.

---

## 📌 Возможности

API поддерживает:

- регистрацию и авторизацию пользователей;
- JWT-аутентификацию;
- разграничение доступа по ролям;
- управление категориями;
- управление товарами;
- создание, изменение и удаление заказов;
- получение заказов текущего пользователя;
- связь пользователей и заказов;
- валидацию входящих данных;
- централизованную обработку исключений;
- логирование;
- пагинацию;
- поиск товаров;
- сортировку и фильтрацию;
- PostgreSQL в Docker;
- запуск проекта через Docker Compose.

---

## 🛠️ Стек технологий

| Технология | Назначение |
|---|---|
| **C#** | Основной язык разработки |
| **.NET** | Платформа |
| **ASP.NET Core Web API** | Создание REST API |
| **Entity Framework Core** | Работа с базой данных |
| **PostgreSQL** | СУБД |
| **Npgsql** | PostgreSQL provider для EF Core |
| **JWT Bearer Authentication** | Аутентификация |
| **BCrypt** | Хеширование паролей |
| **FluentValidation** | Валидация DTO |
| **Swagger / OpenAPI** | Документация и тестирование API |
| **Docker** | Контейнеризация |
| **Docker Compose** | Запуск API и PostgreSQL |

---

## 🏗️ Архитектура

Проект построен с разделением ответственности на несколько слоёв:

```text
InternetShop
│
├── Controllers
│   ├── AuthController
│   ├── CategoriesController
│   ├── ProductsController
│   └── OrdersController
│
├── Services
│   ├── AuthService
│   ├── CategoryService
│   ├── ProductService
│   └── OrderService
│
├── Repositories
│   ├── UserRepository
│   ├── CategoryRepository
│   ├── ProductRepository
│   └── OrderRepository
│
├── Models
│   ├── User
│   ├── Category
│   ├── Product
│   ├── Order
│   └── OrderItem
│
├── DTOs
│
├── Mappers
│
├── Middleware
│   └── ExceptionMiddleware
│
├── Validators
│
├── Data
│   └── AppDbContext
│
├── Program.cs
├── appsettings.json
├── Dockerfile
└── docker-compose.yml
```

### Разделение ответственности

```text
HTTP Request
     │
     ▼
Controller
     │
     ▼
Service
     │
     ▼
Repository
     │
     ▼
Database
```

### Controller

Контроллер отвечает за HTTP-запросы и HTTP-ответы.

Контроллер не должен содержать бизнес-логику или напрямую работать с базой данных.

### Service

Сервис содержит бизнес-логику приложения.

Например:

- создание пользователя;
- проверка пароля;
- создание JWT;
- создание заказа;
- расчёт стоимости заказа;
- проверка существования товара;
- преобразование DTO.

### Repository

Repository отвечает за работу с Entity Framework Core и базой данных.

### DTO

DTO используются для передачи данных между клиентом и API.

Это позволяет не отдавать клиенту напрямую Entity-модели базы данных.

### Mapper

Для преобразования Entity в DTO используются отдельные Mapper-классы.

```text
Category
    │
    ▼
CategoryMapper
    │
    ▼
ResponseCategoryDto
```

Такой подход позволяет не размещать большое количество кода преобразования внутри сервисов.

---

# 🔐 Аутентификация и авторизация

## JWT

Для аутентификации используется **JWT Bearer Authentication**.

После регистрации пользователь может выполнить вход:

```http
POST /api/Auth/login
```

В ответ API возвращает JWT:

```json
{
  "token": "..."
}
```

Полученный токен передаётся в последующих защищённых запросах:

```http
Authorization: Bearer <token>
```

ASP.NET Core проверяет:

- подпись токена;
- срок действия;
- issuer;
- audience.

## Авторизация

Для ограничения доступа используются атрибуты:

```csharp
[Authorize]
```

Доступ будет разрешён только авторизованным пользователям.

Для ограничения доступа по роли:

```csharp
[Authorize(Roles = "Admin")]
```

---

# 👤 Пользователи и заказы

Каждый заказ принадлежит пользователю.

Связь между сущностями:

```text
User
 │
 │ 1:N
 ▼
Order
 │
 │ 1:N
 ▼
OrderItem
```

Один пользователь может иметь несколько заказов.

Каждый заказ принадлежит одному пользователю.

При получении заказов пользователь должен получать только свои заказы.

Идентификатор пользователя берётся из JWT:

```csharp
User.FindFirstValue(ClaimTypes.NameIdentifier)
```

Таким образом, клиент не должен самостоятельно передавать `UserId` для определения владельца заказа.

---

# 🗄️ Модели

## User

Пользователь содержит:

- `Id`
- `Username`
- `Email`
- `PasswordHash`
- `Role`
- `Orders`

Пароли не хранятся в открытом виде.

Для хранения используется **BCrypt**.

## Category

Категория содержит:

- `Id`
- `Name`
- `Products`

## Product

Товар содержит:

- `Id`
- `Name`
- `Price`
- `Stock`
- `CategoryId`
- `Category`

## Order

Заказ содержит:

- `Id`
- `UserId`
- `User`
- `TotalPrice`
- `Status`
- `Items`

## OrderItem

Позиция заказа содержит:

- `Id`
- `OrderId`
- `ProductId`
- `ProductName`
- `Quantity`
- `UnitPrice`

---

# ✅ Валидация

Для проверки входящих DTO используется **FluentValidation**.

Например, при создании товара проверяются:

- название;
- цена;
- количество;
- категория.

При создании заказа:

- наличие товаров;
- количество товара;
- корректность идентификаторов.

---

# ⚠️ Обработка исключений

В проекте используется собственный middleware:

```text
ExceptionMiddleware
```

Он перехватывает необработанные исключения и формирует единый HTTP-ответ.

Пример ответа:

```json
{
  "status": 500,
  "message": "Internal server error",
  "path": "/api/products",
  "timeStamp": "2026-08-09T00:00:00Z"
}
```

Это позволяет не дублировать обработку исключений в каждом контроллере.

---

# 📝 Логирование

Для логирования используется встроенный:

```csharp
ILogger<T>
```

Например:

```csharp
_logger.LogInformation(
    "Категория {Name} успешно создана с Id = {Id}",
    category.Name,
    category.Id);
```

Ошибки записываются через:

```csharp
_logger.LogError(
    exception,
    "Ошибка при обработке запроса");
```

---

# 🐘 PostgreSQL

Проект использует **PostgreSQL** в качестве базы данных.

При запуске через Docker база данных запускается автоматически через Docker Compose.

Основная схема:

```text
User
 │
 │ 1:N
 ▼
Order
 │
 │ 1:N
 ▼
OrderItem
 │
 │ N:1
 ▼
Product
 │
 │ N:1
 ▼
Category
```

---

# 🐳 Docker

Для проекта используются:

- `Dockerfile`
- `docker-compose.yml`

Docker Compose используется для запуска необходимых контейнеров.

Основные сервисы:

```text
API
 │
 └── PostgreSQL
```

## Запуск через Docker

Убедитесь, что **Docker Desktop запущен**.

Перейдите в директорию проекта:

```powershell
cd C:\Users\ritok\source\repos\Training\InternetShop
```

Запустите проект:

```powershell
docker compose up --build
```

Для запуска в фоне:

```powershell
docker compose up --build -d
```

Проверить контейнеры:

```powershell
docker compose ps
```

Посмотреть логи:

```powershell
docker compose logs
```

Логи API:

```powershell
docker compose logs api
```

Логи PostgreSQL:

```powershell
docker compose logs postgres
```

Остановить контейнеры:

```powershell
docker compose down
```

Остановить контейнеры и удалить связанные volumes:

```powershell
docker compose down -v
```

---

# 📚 Swagger

После запуска API Swagger доступен по адресу:

```text
http://localhost:8080/swagger
```

Либо по HTTPS-адресу, который указан в Docker Compose.

Swagger позволяет:

- просматривать endpoints;
- отправлять HTTP-запросы;
- тестировать регистрацию;
- тестировать авторизацию;
- передавать JWT;
- создавать категории;
- создавать товары;
- создавать заказы.

---

# 🔑 Работа с JWT в Swagger

Сначала необходимо выполнить:

```http
POST /api/Auth/login
```

Получить:

```json
{
  "token": "..."
}
```

Затем нажать кнопку:

```text
Authorize
```

и указать:

```text
Bearer <полученный JWT>
```

После этого Swagger сможет выполнять защищённые endpoints от имени авторизованного пользователя.

---

# 📡 Примеры API

## Регистрация

```http
POST /api/Auth/register
Content-Type: application/json
```

Пример тела:

```json
{
  "username": "mikhail",
  "email": "mikhail@example.com",
  "password": "Password123!"
}
```

---

## Авторизация

```http
POST /api/Auth/login
Content-Type: application/json
```

Пример тела:

```json
{
  "username": "mikhail",
  "password": "Password123!"
}
```

Ответ:

```json
{
  "token": "JWT_TOKEN"
}
```

---

## Создание категории

```http
POST /api/Categories
Authorization: Bearer JWT_TOKEN
Content-Type: application/json
```

Пример тела:

```json
{
  "name": "Electronics"
}
```

---

## Создание товара

```http
POST /api/Products
Authorization: Bearer JWT_TOKEN
Content-Type: application/json
```

Пример тела:

```json
{
  "name": "Laptop",
  "price": 100000,
  "stock": 10,
  "categoryId": 1
}
```

---

## Создание заказа

```http
POST /api/Orders
Authorization: Bearer JWT_TOKEN
Content-Type: application/json
```

Пример тела:

```json
{
  "items": [
    {
      "productId": 1,
      "quantity": 2
    }
  ]
}
```

---

# 🔄 Entity Framework Core

Для работы с базой данных используется **Entity Framework Core**.

Создание миграции:

```powershell
dotnet ef migrations add InitialCreate
```

Применение миграций:

```powershell
dotnet ef database update
```

Если база данных была удалена, её можно создать заново применением существующих миграций:

```powershell
dotnet ef database update
```

---

# 🐳 Полезные Docker-команды

Показать запущенные контейнеры:

```powershell
docker ps
```

Показать все контейнеры:

```powershell
docker ps -a
```

Показать Docker images:

```powershell
docker images
```

Пересобрать проект:

```powershell
docker compose build
```

Запустить:

```powershell
docker compose up -d
```

Остановить:

```powershell
docker compose down
```

Перезапустить:

```powershell
docker compose restart
```

---

# ⚙️ Конфигурация

Основные настройки находятся в:

```text
appsettings.json
```

и:

```text
docker-compose.yml
```

JWT использует следующие параметры:

```json
{
  "Jwt": {
    "Key": "...",
    "Issuer": "InternetShop",
    "Audience": "InternetShopUsers",
    "ExpireMinutes": 60
  }
}
```

> В реальном production-приложении секретный JWT-ключ не следует хранить непосредственно в Git-репозитории.

Для production рекомендуется использовать:

- environment variables;
- Docker secrets;
- Secret Manager;
- специализированные системы хранения секретов.

---

# 💻 Запуск без Docker

Если PostgreSQL установлен локально, проект можно запустить напрямую:

```powershell
dotnet restore
dotnet build
dotnet run
```

Перед запуском необходимо убедиться, что PostgreSQL запущен и строка подключения в `appsettings.json` соответствует локальной базе данных.

---

# 🎯 Цель проекта

Основная цель проекта — практическое изучение backend-разработки на ASP.NET Core.

В проекте используются следующие концепции:

- C#;
- ООП;
- Dependency Injection;
- ASP.NET Core Web API;
- REST;
- HTTP;
- DTO;
- Repository Pattern;
- Service Layer;
- Entity Framework Core;
- PostgreSQL;
- LINQ;
- async/await;
- JWT;
- Authentication;
- Authorization;
- Role-based authorization;
- FluentValidation;
- Middleware;
- Logging;
- пагинация;
- поиск товаров;
- сортировка;
- фильтрация;
- Docker;
- Docker Compose.

---

# 🚀 Что можно улучшить

Возможные дальнейшие улучшения проекта:

- полноценное управление ролями;
- refresh tokens;
- подтверждение email;
- восстановление пароля;
- обработка конкурентного изменения `Stock`;
- транзакции при создании заказа;
- уменьшение `Stock` после оформления заказа;
- отмена заказа;
- дополнительные статусы заказа;
- глобальные Result-модели;
- unit-тесты;
- integration-тесты;
- CI/CD;
- deployment;
- Redis;
- кэширование;
- PostgreSQL indexes;
- OpenTelemetry;
- более подробное структурированное логирование.

---

# 👨‍💻 Автор

**Domino**  
Junior C# / .NET Developer

GitHub: https://github.com/ritokral2008-spec

Учебный проект для практики backend-разработки на C# и ASP.NET Core.
