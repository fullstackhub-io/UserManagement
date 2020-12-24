## ASP.NET Web API & Angular 10 Clean Architecture - Part 4

In the last part, we implemented the database logic by Repository and unit of Work design patterns with Dapper & Dapper.Contrib packages. In this part, let’s implement the business logic in **UserManagement.Application**. As discussed in [Part 1](https://fullstackhub.io/asp-net-core-with-angular-application-architecture-part-1/), we will be using CQRS (Command Query Segregation Principle) and Mediator pattern to loosely couple the API and Application layer connection. We will see the implementation in action but first let’s create the base classes e.g. logging, validation behavior, exception handling, etc in this part.

Learning Path Link: https://fullstackhub.io/asp-net-core-with-angular-application-architecture-part-4/
