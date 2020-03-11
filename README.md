# Seed code - Boilerplate for News-App Step4 Assignment

## Assignment Step Description

In this Assignment: News-App Step 4, we will create a RESTful application with Logger, Exception Handler. We will also add .gitlab-ci.yml file for CI in Gitlab.

Representational State Transfer (REST) is an architectural style that specifies constraints. 
In the REST architectural style, data and functionality are considered resources and are accessed using Uniform Resource Identifiers (URIs), typically links on the Web.

Resources are usually manipulated using a set of four standard operations and Http Verbs: create (POST), read(GET), update(PUT), delete(DELETE).

<b>
1. Refactor the solution prepared in previous step to implement Exception handling using custom Filter and Logging using custom middleware.
2. CI must be completed successfully and published atrifacts must be downloadable.
</b>

### Problem Statement

In this assignment, we will develop a RESTful application with which we will allow to perform CRUD operations on User,News and Reminder with the help of URI. Check the correctness of the operations with the help of Postman API.

1. Design the database using EF Core code first without any seed data.
2. Design the REST API and define endpoints to manipuate the respources (User,News,Reminder).
3. All types of exceptions must be handled by the application and return appropriate status codes.

<b> Note: For detailed clarity on the class files, kindly go thru the Project Structure </b>

### Expected Solution:

REST API must expose the endpoints for the following operations:

- Create a new user, update the user, retrieve a single user, delete the user.
- Create a News, delete a news, get all news of a specific userId.
- Create a Reminder, delete a Reminder, get the Reminder for specific news.
- Add logger for all the controller methods.
- It Should log "Request Incoming Time", "Processing Time","URI", "Http Verb" and "status" of the operation.
- Add exception handler for all the controller methods.
- Add .gitlab-ci.yml file for CI

### Steps to be followed:

    Step 1: Fork and Clone the boilerplate in a specific folder on your local machine.
    Step 2: Define the data model classes (UserProfile, Reminder, News)
    Step 3: See the methods mentioned in the Repository interface.
    Step 4: Implement all the methods of Repository interface.
    Step 5: Test each and every Repository with appropriate test cases.
    Step 6: See the methods mentioned in the service interface.
    Step 7: Implement all the methods of service interface.
    Step 8: Test each and every service with appropriate test cases.
    Step 9: Write controllers to work with RESTful API.  
    Step 10: Test each and every controller with appropriate test cases.
    Step 11: Write loggers for each of the methods of controller.
    Step 12: Test LoggingAspect with LoggerTest cases.
    Step 13: Write ExceptionFilter and associate with each controller.
    Step 14: Check all the functionalities using URI's mentioned in the controllers with the help of Postman for final output.

`Add valid .gitlab-ci.yml file in root directory for CI.`


### Project structure

The folders and files you see in this repositories is how it is expected to be in projects, which are submitted for automated evaluation by Hobbes
```
📦News-Step-4
 ┣ 📂DAL
 ┃ ┣ 📜DAL.csproj
 ┃ ┣ 📜INewsRepository.cs //Interface to define contract for News
 ┃ ┣ 📜IReminderRepository.cs //Interface to define contract for Reminder
 ┃ ┣ 📜IUserRepository.cs //Interface to define contract for User
 ┃ ┣ 📜NewsDbContext.cs //class to define DbContext and configuring entities
 ┃ ┣ 📜NewsRepository.cs //Implementation of INewsRepository
 ┃ ┣ 📜ReminderRepository.cs //Implementation of IReminderRepository
 ┃ ┗ 📜UserRepository.cs //Implementation of IUserRepository
 ┣ 📂Entities //Project to define Model classes
 ┃ ┣ 📜Entities.csproj
 ┃ ┣ 📜News.cs //Model class For News Entity
 ┃ ┣ 📜Reminder.cs //Model class For Reminder Entity
 ┃ ┗ 📜UserProfile.cs //Model class For UserProfile Entity
 ┣ 📂NewsAPI //Project to define REST API
 ┃ ┣ 📂Aspect
 ┃ ┃ ┗ 📜ExceptionHandler.cs //custom filter implementation for exception handling
 ┃ ┣ 📂Controllers
 ┃ ┃ ┣ 📜NewsController.cs //class to define endpoints for News
 ┃ ┃ ┣ 📜ReminderController.cs //class to define endpoints for Reminder
 ┃ ┃ ┗ 📜UserController.cs //class to define endpoints for User
 ┃ ┣ 📂Middleware
 ┃ ┃ ┗ 📜LoggingMiddleware.cs //custom middleware implementation for Logging
 ┃ ┣ 📂Properties
 ┃ ┃ ┗ 📜launchSettings.json
 ┃ ┣ 📜appsettings.Development.json
 ┃ ┣ 📜appsettings.json
 ┃ ┣ 📜NewsAPI.csproj
 ┃ ┣ 📜NewsAPI.csproj.user
 ┃ ┣ 📜Program.cs
 ┃ ┗ 📜Startup.cs
 ┣ 📂Service
 ┃ ┣ 📂Exceptions
 ┃ ┃ ┣ 📜NewsAlreadyExistsException.cs
 ┃ ┃ ┣ 📜NewsNotFoundException.cs
 ┃ ┃ ┣ 📜ReminderAlreadyExistsException.cs
 ┃ ┃ ┣ 📜ReminderNotFoundException.cs
 ┃ ┃ ┣ 📜UserAlreadyExistsException.cs
 ┃ ┃ ┗ 📜UserNotFoundException.cs
 ┃ ┣ 📜INewsService.cs //Interface to define Business Rules for News
 ┃ ┣ 📜IReminderService.cs //Interface to define Business Rules for News
 ┃ ┣ 📜IUserService.cs //Interface to define Business Rules for News
 ┃ ┣ 📜NewsService.cs //Implementation of INewsService
 ┃ ┣ 📜ReminderService.cs //Implementation of IReminderService
 ┃ ┗ 📜UserService.cs //Implementation of IUserService
 ┃ ┣ 📜Service.csproj
 ┣ 📂test
 ┃ ┣ 📂ControllerTests
 ┃ ┃ ┣ 📂IntegrationTest
 ┃ ┃ ┃ ┣ 📜NewsControllerTest.cs
 ┃ ┃ ┃ ┣ 📜NewsWebApplicationFactory.cs
 ┃ ┃ ┃ ┣ 📜ReminderControllerTest.cs
 ┃ ┃ ┃ ┗ 📜UserControllerTest.cs
 ┃ ┃ ┗ 📂UnitTest
 ┃ ┃ ┃ ┣ 📜NewsControllerTest.cs
 ┃ ┃ ┃ ┣ 📜ReminderControllerTest.cs
 ┃ ┃ ┃ ┗ 📜UserControllerTest.cs
 ┃ ┣ 📜PriorityOrderer.cs
 ┃ ┗ 📜test.csproj
 ┣ 📜News-Step-4.sln
 ```