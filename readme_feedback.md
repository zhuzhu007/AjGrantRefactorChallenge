# Coding Challenge - Feedback

Thanks for giving this opportunity to do the refactoring exercise. 
I am not quite familiar with GitHub, just pushed to master branch directly in my own repository.
But I did the changes in several commits.

 - The first commit is the code I got
 - Tried to run Unit tests first, seen none of them pass, so fix bugs and unit tests in the second commit. Also Created constant for receipt
 - The third commit created interface for Policy and Lines. Line classes also implemented as polymorphism, add/updates unit tests
 - The following commit split receipt logic out from Order class, create interface for order and receipt. Created receipt template classes, updated unit tests
 - I will commit again once I complete this file

To use the interfaces with multiple implementations, there are mainly two ways to do it
 - following simple/abstract factory pattern so that the factory can generate the instance dynamically
 - use IoC container with input parameter to tell the container which instance it needs to generate

Usually factory pattern is still tight coupling than IoC container. If later on need to add a new receipt template, as an example, the code in factory or IoC register need to be updated a little bit as well. 
We can use C# reflection to help minimise the code changes like this, but it may reduce the performance, just need to keep this in mind when make the decision.

There are still areas can be improved but I didn't touch. For example:
 - create an interface for logger, and use dependency injection to inject logger instance into the classes we want to log. This way will give flexibility if we want to use third party logging libraries
 - add validation for some classes. e.g. policy and line. So policy won't have duplicates, price must be positive number, and line quantity is not negative
 - load some values from external resources, instead of hard coded e.g. Configuration file, the tax rate is a good example, as it rarely changes
 - Order can have more methods, like remove line, find line or clear all lines. May need to add a line Id for these logic.
 - I just copy paste some constant for all unit tests classes, these values can be stored in one place.
 - Text and HTML templates are also hard coded, they can be put in external resources as well, then load into memory once the application start. For example, using singleton pattern
 - I prefer to hard coded the line amount calculation logic in code, it can be very complex. It may not as simple as percentage or value discount, something like buy 2 get the third free or 50% off. The other way is using a formula to handle it, more efforts to implement.

# Skills Matrix

|Level |Description |
|--|--|
|0 |No knowledge or capability |
|1 |Basic level of understanding |
|2 |Basic level of Application |
|3 |Comprehensive level of Application |
|4 |Advanced level of Application |


|Skill |Level| Notes|
|--|--|--|
|**Server**||
|C# |4| More than 10 years
|ASP.NET Core / .NET 6 |4| More than 5 years
|ASP.NET MVC / Web Forms|3| Not used recently
|ASP.NET Web Forms |2| Not used recently
|Web API |4| More than 5 years
|AngularJS |3| Not used recently, only use Angular in the last 5 years
|ORMs / EF |4| EF, EF Core and Dapper
|WinForms |3| Not used recently
|WCF |3| Not used recently
|NoSql |2| MangoDB
|SQL / T-SQL |4| SQL Server and Postgres
|Unit Testing |4| MS Unit test, NUnit and XUnit
|Service Bus |1| Azure Service Bus
|Mobile App development |1| Ionic, Xamarin
|**Browser** ||
|SPA Frameworks |4| 
|JavaScript |4|
|CSS pre processors |3| scss, less
|Unit testing |3|
|E2E UI testing |2| cypress, selemnium
|**Design Patterns** ||
|OO/SOLID |4|
|CQRS |3|
|IoC / DI |4| Ninject, StructureMap, autofac
|Domain Driven Design |2|
|Event Sourcing |2|
|Microservices and SOA |2|
|Distributed Systems |2|
|**Build / Deployment**
|Build Tools |3|
|CI/CD Tools |3| Azure Devops, use octopus several years ago
|Deployment Strategies |1|
|Infrastructure as Code |1|
|GitHub |2| Mainly Azure Devops
|**Cloud Platforms** ||
|AWS |1|
|Azure |2| Not used recently
|GCP or other |0|