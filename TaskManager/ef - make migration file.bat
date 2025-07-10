
dotnet ef migrations add InitialCreate --project TaskManager.Infra --startup-project TaskManager.Api
dotnet ef database update --project TaskManager.Infra --startup-project TaskManager.APIServer

dotnet ef migrations add 08-07-2025-19-09 --project TaskManager.Infra --startup-project TaskManager.APIServer
