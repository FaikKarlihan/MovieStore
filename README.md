# Movie Store Web API
## Overview
####  This project is a Web API built with .NET and Entity Framework to manage a Movie Store. It includes authentication, authorization, logging, validation, and dependency injection.

#### The application allows users to manage movies, directors, actors, and customers. Customers can purchase movies, and all transactions are logged. The system ensures data consistency and follows best practices such as using DTOs instead of entity objects.

## Features
* CRUD Operations for movies, directors, actors, orders and customers.

* Purchase System for customers to buy movies.

* Data Validation using FluentValidation.

* DTO & View Models to prevent direct entity exposure.

* AutoMapper Integration to map between different object types.

* Logging Middleware for tracking requests, responses, and errors.

* Authentication & Authorization access control.

* Dependency Injection for managing dependencies.

* Unit Testing to ensure system reliability.

* Swagger Integration with JWT authentication button for easy API testing.

## Technologies Used
* C# - Primary programming language

* .NET 5 - Framework for building the Web API

* Entity Framework Core - ORM for database operations

* FluentValidation - For request validation

* AutoMapper - For transforming objects

* ASP.NET Core Identity - For authentication and authorization

* Middleware Logging - To log requests and errors to the console and database

* Dependency Injection (DI) - For managing service lifetimes

* Unit Testing with xUnit & Moq - For writing automated tests

* Swagger UI - For interactive API documentation and testing
