#!/bin/bash
. .env
cd mssql/logs/ && find . ! -name . -prune ! -name ../Models -exec rm {} \; && cd ../..
dotnet user-secrets init
dotnet user-secrets set ConnectionStrings:DefaultConnection "${DatabaseConnectionString}" --project .
dotnet user-secrets set ServiceWeltUser "${ServiceWeltUser}" --project .
dotnet user-secrets set ServiceWeltPassword "${ServiceWeltPassword}" --project .
dotnet user-secrets set ServiceWeltUrl "${ServiceWeltUrl}" --project .
docker-compose up --build --remove-orphans
dotnet user-secrets clear
cd mssql/logs/ && find . ! -name . -prune ! -name ../Models -exec rm {} \; && cd ../..
docker system prune -y
