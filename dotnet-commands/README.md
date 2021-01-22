
# Some DOTNET commands to use


### Dotnet new

Create .gitignore file for .NET projects:
```bash
dotnet new gitignore
```


Create .NET core grpc project
```bash
dotnet new grpc
```


Create .NET console application
```bash
dotnet new console
```

Create ASP.NET Core Web App (Model-View-Controller)
```bash
dotnet new mvc
```

Create NuGet Config file
```bash
dotnet new nugetconfig
```

### Dotnet Restore

Restore dependencies and tools for the `app` project:
```bash
dotnet restore ./projects/app/app.csproj
```

### Dotnet Build

Build a project and its dependencies using `Release` config:
```bash
dotnet build --configuration Release
```


### Dotnet Test

Run the tests in the `test` project:
```bash
dotnet test ./projects/test/test.csproj
```

Run tests with verbosity:
```bash
dotnet test --logger "console;verbosity=detailed"
```