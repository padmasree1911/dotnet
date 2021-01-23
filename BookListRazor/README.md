
# General Information

## Database Migrations in Visual Studio 2019

In order to start to do migrations click `Tools->NuGet Package Manager->Package Manager Console` in Visual Studio 2019.


Run following command to add migrations. The parameter is the name of the migration.
```bash
PM> add-migration AddBookToDb
```

Run following command to update all migrations to the Database Server.
```bash
PM> update-database
```