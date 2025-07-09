
dotnet ef migrations add InitialCreate --project TaskManager.Infra --startup-project TaskManager.Api
dotnet ef database update --project TaskManager.Infra --startup-project TaskManager.APIServer