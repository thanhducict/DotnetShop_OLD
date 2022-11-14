@echo off

echo ===========================================
echo Execute Db migrations and data seeding
echo ===========================================

echo === Migrations ===
if exist DotnetShop.WebApp\App_Data\Migrations\ (
	rmdir DotnetShop.WebApp\App_Data\Migrations\ /s /q
)
dotnet ef migrations add InitialCreate -p DotnetShop.WebApp\DotnetShop.WebApp.csproj -s DotnetShop.WebApp\DotnetShop.WebApp.csproj -o App_Data\Migrations

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
