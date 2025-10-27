# 🧩 Entity Framework Core 8 - Database Replication (Read/Write)

Este projeto demonstra como implementar **replicação de banco de dados (Read/Write Split)** utilizando **Entity Framework Core 8**.  
A ideia é simples, mas poderosa:  
👉 **Escrever no banco principal** e **ler nas réplicas**, melhorando a performance e a escalabilidade da aplicação.

---

## 🚀 Objectivo

Muitos sistemas enfrentam gargalos de performance porque todas as operações (leitura e escrita) são direcionadas ao mesmo servidor.  
Com **replicação de banco de dados** e o suporte do **EF Core 8**, podemos:

- Reduzir a carga no servidor principal  
- Aumentar a disponibilidade do sistema  
- Melhorar o tempo de resposta das consultas  
- Evitar downtime em cenários de alta demanda  

---

## ⚙️ Tecnologias Utilizadas

- [.NET 8](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Entity Framework Core 8](https://learn.microsoft.com/en-us/ef/core/)
- [SQL Server](https://www.microsoft.com/sql-server)
- [Docker](https://www.docker.com/) (opcional, para subir bancos e ambiente local)
- [Dependency Injection](https://learn.microsoft.com/en-us/dotnet/core/extensions/dependency-injection)
- [Configuration via appsettings.json](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/configuration/)

---

## 🧠 Conceito

A ideia é separar os contextos de **leitura** e **escrita** dentro da aplicação, usando diferentes *connection strings*:

```json
ConnectionStrings": {
  "PrimaryDatabase": "Server=primary.db;Database=AppDB;User Id=sa;Password=123;",
  "ReadReplicas": [
    "Server=replica1.db;Database=AppDB;User Id=sa;Password=123;",
    "Server=replica2.db;Database=AppDB;User Id=sa;Password=123;"
  ]
}
