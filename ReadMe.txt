In order to run this project - you will need to link it to a SQL server and build the database first.
The steps are as follow: 

1. Obviously.. clone the project to VS 
2. In the appsettings.json file - you will need to change your SQL server connection. Mine is written as DORONSURFACE\\SQLEXPRESS. 
The Database name would be CamhsFinal, unless you want to change it
3. Via the Nuget Pacage Manager , install Microsoft.EntityFrameworkCore.SqlServer (version 2.1.4)
4. Time to build: via the NuGet Console
  EntityFrameworkCore\Add-Migration WhatEverNameYouWant 
  
  and then: 
  
  EntityFrameworkCore\Update-Database

5. It should be working now...

  
