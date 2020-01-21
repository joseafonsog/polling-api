# Polling API
Created with Net Core and Mongo Db

---
### Requirements:
* Net core 2.2
* MongoDb 4+
* Visual Studio 2017 CE
---
### Configuration:
1. Verify that **_MongoDb_** is running in your enviroment or is accesible from it.
2. Edit the config file **_appsettings.json_** inside the **_PollingApp.Api_** project.
3. Change the **_QuestionsDatabaseSettings_** key accordinly to your enviroment.
4. Change in **_EmailSettings_** key the **_LocalDirectoryPath_** for the correct funtioning of the email service.

### Execution:
Execute the solution with the **_PollingApp.Api_** project as the startup, or run the command ```dotnet run PollingApp.Api.csproj``` in a terminal application inside the folder of the **_PollingApp.Api_** project.

At the first run the database will be populated with test data, you can check the data querying the collection **_Questions_**.