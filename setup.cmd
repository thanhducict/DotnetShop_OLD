@echo off

echo ======================================
echo Execute Db migrations and data seeding
echo ======================================

echo === Migrations ===
if exist DotnetShop.Migrations.Sqlite\Migrations\ (
	rmdir DotnetShop.Migrations.Sqlite\Migrations\ /s /q
)
dotnet ef migrations add InitialCreate -p DotnetShop.Migrations.Sqlite\DotnetShop.Migrations.Sqlite.csproj -s DotnetShop.WebApp\DotnetShop.WebApp.csproj

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
