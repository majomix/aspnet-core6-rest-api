# ASP.NET Core 6 REST API

## Task

Given the provided IDataProcesserService interface and supporting classes, create an ASP.NET API Solution providing an implementation of the interface against an in-memory list, as well as relevant API Endpoints for exposing the functionality of the service externally.

## Solution

* Project organization is based on Clean Architecture.
* Running DataProcessing.Api opens Swagger with API definition. All endpoints are implemented.
* 3rd party libraries used: AutoMapper, Swashbuckle, Newtonsoft.Json, NUnit, NCrunch, Moq, FluentAssertions.
* Unit and integration test coverage approx. 60%.
* API-level validation was done through attribute annotations.
* Error handling was done through exception catching middleware.
* For technical information, please see comments on interface level.
