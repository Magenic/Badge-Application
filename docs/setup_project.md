## Setting up the Project

### Getting the source from repository and removing all errors (in case there are any)

#### Preconditions:

1. You must have installed the following:
	1.1. Visual Studio 2017 updated to version 15.8.4 or later.
	1.2. SQL Server 2017 with SQL Server Management Studio v17.9.

#### Setup Steps

1. Clone the project from the Github repository.
2. Build the solution. Make sure that no errors are encountered.

Important note related to Nuget Packages under the Mvc and WebApi projects:
* Update Microsoft.AspNet.Mvc to version 5.2.6
* Update Autofac to version 4.1.0 only (not the latest which is 4.8.1)