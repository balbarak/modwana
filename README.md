<img src="https://dev.azure.com/balbarak/Modwana/_apis/build/status/Master%20Build%20CI"/>

## Modwana
Cross platform web blog app based on C# and .NET Core 3.1 that, can use the followings databases
* SqlServer
* Postgress
* Sqlite
* MySql

And works on
* Windows 
* Linux
* Mac
## Installation

Install [.NET Core](https://dot.net) for your OS

Make sure you have dotnet installed by running the following command

`dotnet --version`

* Clone the repo

`git clone https://github.com/balbarak/modwana.git`

* Go to web Path

`cd /modwana/src/Modwana.Web/`

* Run the app

`dotnet run`

now your app is runing ...

open the browser at `http://localhost:5010`

### Login Information

user: admin@admin.com

pass: 1122

### Databse Configuration
Modwana app use Sqlite as default database, if you want to change the database open

`src/Modwana.Web/appsettings.json`

and make the changes that, suite for you
```js
  "DatabaseSettings": {
    "UseConnectionString": false,
    "Type": 1, // 1=Sqlite, 2=Postgress, 3=MSSQL, 4=MySql
    "Host": "127.0.0.1",
    "Port": 3306, // Postgree=5432, MySql=3306
    "Database": "ModwanaDb",
    "User": "balbarak",
    "Password": "1122",
    "FilePath": "Database/ModwanaSqlite.db"
  },
```


## Architecture 
Domain Driver Design Architecture with the followings designs and concepts

* Repository Pattern
* Dependency Injection
* ORM (Entity Framework Core)
* MVC (Model View Controller)
* Event Driven Architecture

It is over engineered for simple blog !

It can be extended for whatever purpose 

