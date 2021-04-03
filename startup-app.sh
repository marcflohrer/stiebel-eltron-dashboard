#!/bin/bash
. .env
cd mssql/logs/ && find . ! -name . -prune ! -name ../Models -exec rm {} \; && cd ../..
dotnet user-secrets init
dotnet user-secrets set ConnectionStrings:DefaultConnection "${DatabaseConnectionString}" --project .
dotnet user-secrets set ServiceWeltPassword "${ServiceWeltPassword}" --project .
docker-compose up --build --remove-orphans
dotnet user-secrets clear
cd mssql/logs/ && find . ! -name . -prune ! -name ../Models -exec rm {} \; && cd ../..
