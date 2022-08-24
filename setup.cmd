@echo off

echo =========================================
echo Execute Db migrations and model seed data
echo =========================================

rem echo === Sqlite Migrations ===
if exist DotnetShop.Migrations.Sqlite\Migrations\ (
	rmdir DotnetShop.Data.Migrations.Sqlite\Migrations /s /q
)
dotnet ef migrations add InitialCreate -p DotnetShop.Migrations.Sqlite -s DotnetShop.WebApp\DotnetShop.WebApp.csproj

echo === SqlServer Migrations ===
if exist DotnetShop.Migrations.SqlServer\Migrations\ (
	rmdir DotnetShop.Migrations.SqlServer\Migrations /s /q
)
rem dotnet ef migrations add InitialCreate -p DotnetShop.Migrations.SqlServer -s DotnetShop.WebApp\DotnetShop.WebApp.csproj

echo === Generate database ===
dotnet ef database drop -f -s DotnetShop.WebApp/DotnetShop.WebApp.csproj
if not exist DotnetShop.WebApp\App_Data\ (
	mkdir DotnetShop.WebApp\App_Data
)
dotnet ef database update -s DotnetShop.WebApp/DotnetShop.WebApp.csproj

echo === Build solution ===
dotnet build
call yarn --cwd DotnetShop.Admin.Extensions\ClientApp install
call yarn --cwd DotnetShop.Admin.Extensions\ClientApp build:debug

pause
