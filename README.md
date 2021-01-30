# Employee Benefits Web App


## Installation

To run locally, follow these instructions:

1. You will need VS2019, .Net Core 3.1, Node and MS SQL Server or SQL Server Express installed.
2. Clone or download the zip to your local computer.
3. Run "NPM Install" in the EmployeeBenefitsSolution/EmployeeBenefits.Web/ClientApp/ directory to download NPM packages.
4. You will execute the InitializeDB.sql script against your local MS SQL Server or SQL Server Express. The script is in the EmployeeBenefitsSolution/EmployeeBenefits.Database/Scripts directory. It creates the database, tables and populates some initial data.
5. Open the EmployeeBenefitsSolution.sln file in Visual Studio 2019.
6. Update the "DefaultConnection" connection string with your local sql instance. The appsettings.json is located in the EmployeeBenefitsSolution/EmployeeBenefits.Web/ directory.

Good luck!


## To Do

1. Error logging.
2. Authentication and Authorization.
3. Cors implementation.
4. Security - Anti XSS
