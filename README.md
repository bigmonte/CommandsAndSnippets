# Commands and Snippets

## Description:

An REST API that provides useful commands and snippets.


---------------
Firing up docker with SQL Server (Microsoft):

```bash
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=<YourStrong@Passw0rd>" \\n   -p 1433:1433 --name sql1 -h sql1 \\n   -d mcr.microsoft.com/mssql/server:2019-latest
```

Using user secrets:
(Change the password)

```bash
dotnet user-secrets set "UserID" "SA"
dotnet user-secrets set "Password" "<YourStrong@Passw0rd>"

```

Setting Up Automapper

`dotnet add package AutoMapper.Extensions.Microsoft.DependencyInjection`

Dependencies for PATCH Request

```bash
dotnet add package Microsoft.AspNetCore.JsonPatch
dotnet add package Microsoft.AspNetCore.Mvc.NewtonsoftJson
```

### Extras examples

Create new project
```bash
dotnet new xunit -n CommandAPI.Tests
```

Create new solution
```bash
dotnet new sln --name CommandAPISolution
```

Add project to solution
```bash
dotnet sln CommandAPISolution.sln add src/CommandAPI/CommandAPI.csproj test/CommandAPI.Tests/CommandAPI.Tests.csproj
```

Add reference to CommandAPI.Tests
```bash
dotnet add test/CommandAPI.Tests/CommandAPI.Tests.csproj reference src/ CommandAPI/CommandAPI.csproj
```