
#!/bin/bash

set -e

mkdir -p postgres/data

until PATH="$PATH:/root/.dotnet/tools"; do
>&2 echo "Setting up env variables..."
sleep 1
done

dotnet tool install --global dotnet-ef

>&2 echo "!!!11!!!!!!11!!!!!!11!!!!!!11!!!!!!11!!!1!!!!!!11!!!!!"
>&2 echo "Running entrypoint.sh :: EF IS INSTALLED !!!!!!11!!!!!"
>&2 echo "!!!11!!!!!!11!!!!!!11!!!!!!11!!!!!!11!!!1!!!!!!11!!!!!"

ConnectionStringName="ConnectionStrings:DefaultConnection"
dotnet user-secrets init && dotnet user-secrets set "$ConnectionStringName" "$1" --project .

>&2 echo "!!!11!!!!!!11!!!!!!11!!!!!!11!!!!!!11!!!1!!!!!!11!!!!!"
>&2 echo "Running entrypoint.sh :: SECRETS ARE SET UP !!!!!!!11!"
>&2 echo "!!!11!!!!!!11!!!!!!11!!!!!!11!!!!!!11!!!1!!!!!!11!!!!!"

>&2 echo "Building the project ..."
until dotnet build; do
>&2 echo "The project build is running..."
sleep 1
done

>&2 echo "!!!11!!!!!!11!!!!!!11!!!!!!11!!!!!!11!!!1!!!!!!11!!!!!"
>&2 echo "Running entrypoint.sh :: BUILD DONE !!!!!!!!!!!!!!!!11"
>&2 echo "!!!11!!!!!!11!!!!!!11!!!!!!11!!!!!!11!!!1!!!!!!11!!!!!"

until dotnet ef database update; do
>&2 echo "SQL Server is starting up"
sleep 1
done


>&2 echo "!!!11!!!!!!11!!!!!!11!!!!!!11!!!!!!11!!!1!!!!!!11!!!!!"
>&2 echo "Running entrypoint.sh :: MS SQL DB RUNNING !!!!!!!!!11"
>&2 echo "!!!11!!!!!!11!!!!!!11!!!!!!11!!!!!!11!!!1!!!!!!11!!!!!"

>&2 echo "SQL Server is up - starting scaffolding ..."
dotnet user-secrets init && dotnet user-secrets set "${ConnectionStringName}" "Host=db;Database=postgres;Username=postgres;Password=$1;SslMode=Disable"
dotnet ef dbcontext scaffold Name="${ConnectionStringName}" Microsoft.EntityFrameworkCore.SqlServer -o DbModels -f --context ApplicationDbContext --context-dir DbModels --namespace StiebelEltronDashboard.Models --context-namespace StiebelEltronDashboard.Models
