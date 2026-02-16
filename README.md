# Sprint 1 Capstone: SolarMonitor

## 🏛️ Clean Architecture Structure

### 1. SolarMonitor.Domain 🧠
- **Role:** The Enterprise Business Rules.
- **Dependencies:** None.
- **Contents:** Entities, Enums, Value Objects, Domain Exceptions.

### 2. SolarMonitor.Application ⚙️
- **Role:** The Application Business Rules (Use Cases).
- **Dependencies:** Domain.
- **Contents:** Interfaces (Repo/Services), DTOs, CQRS Handlers, Validators.

### 3. SolarMonitor.Infrastructure 🔌
- **Role:** External concerns (The Plumbing).
- **Dependencies:** Application, Domain.
- **Contents:** EF Core DbContext, SQL Migrations, Email Service Implementation.

### 4. SolarMonitor.Api 🌐
- **Role:** The Entry Point.
- **Dependencies:** Application, Infrastructure.
- **Contents:** Controllers, Middleware, DI Configuration.