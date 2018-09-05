#Criar um novo projeto api
dotnet new webapi -o DatingApp.API -n DatingApp.API

# Extensions
C#
C# Extensions
Nuget Package Manager

# Watch do dot.net
dotnet watch run

# Database commands

#Intalando o entity core sqlite
Nuget > Microsoft.EntityFrameworkCore.Sqlite

# Criando uma migração do zero
dotnet ef migrations add InitialCreate

# Cria um database, se ele não existir, e roda as migrations
dotnet ef database update