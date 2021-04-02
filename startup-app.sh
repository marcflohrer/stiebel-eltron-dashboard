#!/bin/bash
. .env
cd mssql/logs/ && find . ! -name . -prune ! -name ../Models -exec rm {} \; && cd ../..
dotnet user-secrets init
dotnet user-secrets set DatabasePassword "${DatabasePassword}" --project .
docker-compose up --build --remove-orphans
dotnet user-secrets remove "DatabasePassword"
cd mssql/logs/ && find . ! -name . -prune ! -name ../Models -exec rm {} \; && cd ../..
