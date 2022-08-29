@echo off

echo =========================================
echo Execute Db migrations and model seed data
echo =========================================

echo === Sqlite Migrations ===
if exist DotnetShop.Migrations.Sqlite\Migrations\ (
	rmdir DotnetShop.Migrations.Sqlite\Migrations /s /q
)
dotnet ef migrations add InitialCreate -p DotnetShop.Migrations.Sqlite -s DotnetShop.WebApp\DotnetShop.WebApp.csproj

rem echo === SqlServer Migrations ===
rem if exist DotnetShop.Migrations.SqlServer\Migrations\ (
rem 	rmdir DotnetShop.Migrations.SqlServer\Migrations /s /q
rem )
rem dotnet ef migrations add InitialCreate -p DotnetShop.Migrations.SqlServer -s DotnetShop.WebApp\DotnetShop.WebApp.csproj

echo === Generate database ===
dotnet ef database drop -f -s DotnetShop.WebApp/DotnetShop.WebApp.csproj
if not exist DotnetShop.WebApp\App_Data\ (
	mkdir DotnetShop.WebApp\App_Data
)
dotnet ef database update -s DotnetShop.WebApp/DotnetShop.WebApp.csproj

echo === Build solution ===
dotnet build

echo === Build client app ===
call yarn --cwd DotnetShop.WebClient\ClientApp install
call yarn --cwd DotnetShop.WebClient\ClientApp build:debug

pause
