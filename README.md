# **Factory Management System**

This project is a web-based management system developed to digitize a factory's production processes, personnel management, and data flow. The project has a modular structure and is developed using C# language and ASP.NET Core technologies.


# Project Structure



**Project Structure**

The project consists of the following layers:

-   **FabrikaYonetimSistemi.Core**: Contains business logic and service interfaces.
-   **FabrikaYonetimSistemi.Data**: Data access layer; database operations are performed with Entity Framework Core.
-   **FabrikaYonetimSistemi.Entity**: Data models and entity classes are defined in this layer.
-   **FabrikaYonetimSistemi.Service**: Contains concrete implementations of business logic services.
-   **FabrikaYonetimSistemi.Web**: User interface; developed using ASP.NET Core MVC.

## Setup and Running

-   **Clone the Repository:**
    

    
    ```
    git clone https://github.com/zikrullahcelep611/Factory-Management-System.git
    
    ```
    
-   **Required Tools:**
    -   .NET 6 SDK or a newer version
    -   Visual Studio 2022 or Visual Studio Code
    -   PostgreSQL 
-   **Database Configuration:**
    -   In the `appsettings.json` file, update the database connection string according to your Server settings.
    -   Run the following command in the Package Manager Console to create the database:
        
        Bash
        
        ```
        Update-Database
        
        ```
        
-   **Run the Project:**
    -   Open the solution file (`FabrikaYonetimSistemi.sln`) with Visual Studio.
    -   Set `FabrikaYonetimSistemi.Web` as the startup project.
    -   Run the project to view the application in your browser.

## Features

-   **Personnel Management:** Adding, updating, and listing employee information.
-   **Production Tracking:** Monitoring and reporting of production processes.
-   **Data Analysis:** Analysis of production and personnel data.
-   **User Interface:** User-friendly and responsive design.


## Contributing

If you want to contribute:

2.  Fork the repository.
4.  Create a new branch: `git checkout -b new-feature`
6.  Make your changes and commit them: `git commit -m 'Added new feature'`
7.  Push your branch: `git push origin new-feature`
8.  Create a Pull Request.


```
