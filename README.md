## ASP.NET Web API & Angular 10 Clean Architecture

I recently implemented an application using Jason Taylor Clean Architecture with a [.NET Core](https://jasontaylor.dev/clean-architecture-getting-started/) article and thought it would be a good idea to write another article to break it down into steps so that it would be easy to follow for beginners. Obisvoulsy, there would be some differences e.g. I will be using Dapper micro ORM, repository, and unit of work design pattern instead of Entity Framework .NET Core and will use a separate database for integration testing instead of an in-memory database and also if time permits, I will add more detailed Angular unit and integration tests.  

We are going to develop one of a favorite application called User Management where you would be able to add, update, delete and view the users, currently, I am going to skip user authentication, you can find it in Jason’s [Github repository](https://github.com/jasontaylordev/CleanArchitecture).

Learning Path Link: https://fullstackhub.io/category/learning-path/asp-net-web-api-angular-10-clean-architecture/
