// For installing dotnet-ef tools
dotnet tool install --global dotnet-ef --version 7.*

cd EventsDemoAPI

dotnet ef migrations add events -o Data/Migrations
dotnet ef migrations remove
dotnet ef database update

dotnet build
dotnet watch

