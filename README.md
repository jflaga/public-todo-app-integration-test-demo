# Integration test (or functional test) demo using a simple todo app

Apps/Services:

1. Public Todo App

   A publicly visible todo list.

   Uses SQL Server for DB.

   In integration tests, these are used: 
   `MsSqlContainer` (TestContainers for SQL Server), 
   `Respawn` for resetting db between tests

2. Data Collector Service

   Let's say our Todo app has become famous, and a book publisher has contacted us for data on 
   books being read by our users, to help them decide on what kinds of books to publish next.
   
   Uses MassTransit and RabbitMQ for messaging
   
   In integration tests, these are used: 
   `RabbitMqContainer` (TestContainers for RabbitMQ)

3. Task Suggestions Service

   An external service which gives suggestions on how to do a task, or it gives information 
   about books if a task looks like "Read '<title of book>'" for example.

   This is supposed to be an external service, but because there is no such service in existence,
   I will create a separate service for this.


# How to run integration tests project

0. Install MS SQL Server, RabbitMQ, and Docker Desktop

1. Make sure Docker Desktop is running and that you are connected to the internet

2. Run the tests in Visual Studio 2022 
   
   - by using Test Explorer 
   - or by right-clicking on a test method then clicking Run Tests or Debug Tests on ton context menu

   NOTE: On first run, TestContainers downloads the Docker image(s) being used in the tests.
   Wait for the download(s) to finish.
