# KOMAS API

Visit the `/swagger` endpoint for the SwaggerUI documentation

## Set Up

- `dotnet restore` to install the necessary packages
- `dotnet ef migrations add Initial` to perform database migrations
- `dotnet ef database update` to update the database based on the migrations above
- `dotnet run` | `dotnet watch` to start the server with or without _hot reload_
