
#!/bin/bash

set -e

mkdir -p mssql/data
chown 10001 mssql/data
mkdir -p mssql/log
chown 10001 mssql/log
mkdir -p mssql/secrets
chown 10001 mssql/secrets

run_cmd="dotnet stiebel-eltron-dashboard.dll"

>&2 echo "!!!11!!!!!!11!!!!!!11!!!!!!11!!!!!!11!!!"
>&2 echo "Running entrypoint.sh !!!11!!!!!!11!!!!!"
>&2 echo "!!!11!!!!!!11!!!!!!11!!!!!!11!!!!!!11!!!"

until PATH="$PATH:/root/.dotnet/tools"; do
>&2 echo "Setting up env variables..."
sleep 1
done

>&2 echo "!!!11!!!!!!11!!!!!!11!!!!!!11!!!!!!11!!!1!!!!!!11!!!!!"
>&2 echo "Running entrypoint.sh :: PATH IS SET!!!11!!!!!!11!!!!!"
>&2 echo "!!!11!!!!!!11!!!!!!11!!!!!!11!!!!!!11!!!1!!!!!!11!!!!!"

dotnet tool install --global dotnet-ef

>&2 echo "!!!11!!!!!!11!!!!!!11!!!!!!11!!!!!!11!!!1!!!!!!11!!!!!"
>&2 echo "Running entrypoint.sh :: EF IS INSTALLED !!!!!!11!!!!!"
>&2 echo "!!!11!!!!!!11!!!!!!11!!!!!!11!!!!!!11!!!1!!!!!!11!!!!!"

ConnectionStringName="ConnectionStrings:DefaultConnection"
dotnet user-secrets init && dotnet user-secrets set "$ConnectionStringName" "$1" --project .

>&2 echo "!!!11!!!!!!11!!!!!!11!!!!!!11!!!!!!11!!!1!!!!!!11!!!!!"
>&2 echo "Running entrypoint.sh :: 0 SECRETS SET UP $0 !!!!!!!11!!!!!"
>&2 echo "Running entrypoint.sh :: 1 SECRETS SET UP $1 !!!!!!!11!!!!!"
>&2 echo "!!!11!!!!!!11!!!!!!11!!!!!!11!!!!!!11!!!1!!!!!!11!!!!!"

dotnet build

now_hourly=$(date +%Y-%d-%b-%H_%M) 
>&2 echo "dotnet ef migrations add" $now_hourly"ChangeDatabase"
dotnet ef migrations add $now_hourly"ChangeDb" --context StiebelEltronDashboard.Models.ApplicationDbContext --output-dir Data/Migrations;

>&2 echo "dotnet ef database update"
until dotnet ef database update; do
>&2 echo "SQL Server is starting up"
sleep 1
done

>&2 echo "!!!11!!!!!!11!!!!!!11!!!!!!1!"
>&2 echo "Migration done !!!!!!!11!!!!!"
>&2 echo "!!!11!!!!!!11!!!!!!11!!!!!!11"
