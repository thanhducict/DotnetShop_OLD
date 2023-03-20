@echo off

echo ======================================
echo Execute Db migrations and data seeding
echo ======================================

echo === Migrations ===
if exist DotnetShop.Data\Migrations\ (
	rmdir DotnetShop.Data\Migrations\ /s /q
)
dotnet ef migrations add InitialCreate -p DotnetShop.Data\DotnetShop.Data.csproj -s DotnetShop.WebApp\DotnetShop.WebApp.csproj

echo === Generate database ===
dotnet ef database drop -f -s DotnetShop.WebApp\DotnetShop.WebApp.csproj
if not exist DotnetShop.WebApp\App_Data\ (
	mkdir DotnetShop.WebApp\App_Data
)
dotnet ef database update -s DotnetShop.WebApp\DotnetShop.WebApp.csproj

echo === Build solution ===
dotnet build

rem echo === Build client app ===
rem call yarn --cwd DotnetShop.WebClient\ClientApp install
rem call yarn --cwd DotnetShop.WebClient\ClientApp build:debug

pause
