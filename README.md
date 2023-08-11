# Sample Application

This repository is being used for several presentations, including the [A Lap Around Microsoft Azure](https://www.josephguadagno.net/presentations/a-lap-around-microsoft-azure) talk.  

## Setup

We'll include any setup required to get going with this project

### Software Tools

* [Visual Studio](https://visualstudio.microsoft.com/) [Code](https://code.visualstudio.com/?wt.mc_id=DX_841432) ([Mac](https://code.visualstudio.com/docs/?dv=osx) / [Windows](https://code.visualstudio.com/?wt.mc_id=DX_841432#))
* [Visual Studio](https://visualstudio.microsoft.com/) Community ([Windows](https://visualstudio.microsoft.com/thank-you-downloading-visual-studio/?sku=Community&rel=16))
* [JetBrains](https://www.jetbrains.com/) [Rider](https://www.jetbrains.com/rider/) ([Mac](https://www.jetbrains.com/rider/download/download-thanks.html/) / [Windows](https://www.jetbrains.com/rider/download/download-thanks.html))

### Databases

#### Microsoft SQL Server

[Microsoft SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) for local development. See the file [sqlserver\readme.md](./sqlserver/readme.md) for steps to set up the database.

To configure the application to use Microsoft SQL Server,
update the `ConnectionStrings` `ContactsDatabaseSqlServer` setting in your *appsettings.json* file(s) with the connection string to your instance of Microsoft SQL Server.

The connection string is in the format of `Server=<server>;Database=<database>;User Id=<user>;Password=<password>;`.

Example:

```json
{
  "ConnectionStrings": {
    "ContactsDatabaseSqlServer": "Server=localhost;Database=Contacts;User Id=contacts_user;Password=Password123!;"
  }
}
```

The `appsettings.json` file(s) are located in the *./src/Contacts.Api* folder.

Files:

* [appsettings.Development.json](./src/Contacts.Api/appsettings.Development.json)
* [appsettings.json](./src/Contacts.Api/appsettings.json)

In the [Program.cs](./src/Contacts.Api/Program.cs) file, you will need to uncomment the following line:

```csharp
builder.Services.AddTransient<IContactDataStore, SqlServerDataStore>();
```

and comment the following line:

```csharp
builder.Services.AddTransient<IContactDataStore, SqliteDataStore>();
```

#### SQLite

[SQLite](https://www.sqlite.org/index.html) for local development. See the file [sqlite\readme.md](./sqlite/readme.md) for steps to set up the database.

To configure the application to use Microsoft SQL Server,
update the `ConnectionStrings` `ContactsDatabaseSqlite` setting in your *appsettings.json* file(s) with the connection string to your instance of Microsoft SQL Server.

The connection string is in the format of `Data Source=C:\databases\contacts.db;`.

Example:

```json
{
  "ConnectionStrings": {
    "ContactsDatabaseSqlite": "Data Source=<fully_qualified_path_and_filename>;"
  }
}
```

The `appsettings.json` file(s) are located in the *./src/Contacts.Api* folder.

Files:

* [appsettings.Development.json](./src/Contacts.Api/appsettings.Development.json)
* [appsettings.json](./src/Contacts.Api/appsettings.json)

In the [Program.cs](./src/Contacts.Api/Program.cs) file, you will need to uncomment the following line:


```csharp
builder.Services.AddTransient<IContactDataStore, SqliteDataStore>();
```

and comment the following line:

```csharp
builder.Services.AddTransient<IContactDataStore, SqlServerDataStore>();
```