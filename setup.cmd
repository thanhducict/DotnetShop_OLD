@echo off

echo === Sqlite Migrations ===
if exist IngredientSupplier.Migrations.Sqlite\Migrations\ (
	rmdir IngredientSupplier.Data.Migrations.Sqlite\Migrations /s /q
)
rem dotnet ef migrations add InitialCreate -p IngredientSupplier.Migrations.Sqlite -s IngredientSupplier.WebApp\IngredientSupplier.WebApp.csproj

echo === SqlServer Migrations ===
if exist IngredientSupplier.Migrations.SqlServer\Migrations\ (
	rmdir IngredientSupplier.Migrations.SqlServer\Migrations /s /q
)
dotnet ef migrations add InitialCreate -p IngredientSupplier.Migrations.SqlServer -s IngredientSupplier.WebApp\IngredientSupplier.WebApp.csproj

echo === Generate database ===
dotnet ef database drop -f -s IngredientSupplier.WebApp/IngredientSupplier.WebApp.csproj
if not exist IngredientSupplier.WebApp\App_Data\ (
	mkdir IngredientSupplier.WebApp\App_Data
)
dotnet ef database update -s IngredientSupplier.WebApp/IngredientSupplier.WebApp.csproj

echo === Build solution ===
dotnet build

pause