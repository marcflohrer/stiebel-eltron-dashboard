
#!/bin/bash

set -e

run_cmd="dotnet stiebel-eltron-apiserver.dll"

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

until dotnet tool install --global dotnet-ef; do
>&2 echo "EntityFramework is installing..."
sleep 1
done

>&2 echo "!!!11!!!!!!11!!!!!!11!!!!!!11!!!!!!11!!!1!!!!!!11!!!!!"
>&2 echo "Running entrypoint.sh :: EF IS INSTALLED !!!!!!11!!!!!"
>&2 echo "!!!11!!!!!!11!!!!!!11!!!!!!11!!!!!!11!!!1!!!!!!11!!!!!"

until dotnet user-secrets init && dotnet user-secrets set ConnectionStrings:DefaultConnection "Server=db;Database=master;User=sa;Password=$1;" --project .; do
>&2 echo "Setting up user secret: " $1
sleep 1
done

>&2 echo "!!!11!!!!!!11!!!!!!11!!!!!!11!!!!!!11!!!1!!!!!!11!!!!!"
>&2 echo "Running entrypoint.sh :: 0 SECRETS SET UP $0 !!!!!!!11!!!!!"
>&2 echo "Running entrypoint.sh :: 1 SECRETS SET UP $1 !!!!!!!11!!!!!"
>&2 echo "!!!11!!!!!!11!!!!!!11!!!!!!11!!!!!!11!!!1!!!!!!11!!!!!"

until dotnet ef database update; do
>&2 echo "SQL Server is starting up"
sleep 1
done

>&2 echo "!!!11!!!!!!11!!!!!!11!!!!!!11!!!!!!11!!!1!!!!!!11!!!!!"
>&2 echo "Running entrypoint.sh :: SQL IS RUNNING !!!!!!!11!!!!!"
>&2 echo "!!!11!!!!!!11!!!!!!11!!!!!!11!!!!!!11!!!1!!!!!!11!!!!!"

>&2 echo "SQL Server is up - starting stiebel-eltron-apiserver next..."
until dotnet stiebel-eltron-apiserver.dll; do
>&2 echo "Starting up stiebel-eltron-apiserver..."
sleep 1
done

>&2 echo "!!!11!!!!!!11!!!!!!11!!!!!!11!!!!!!11!!!1!!!!!!11!!!!!"
>&2 echo "Running entrypoint.sh :: APP RUNNING !!!!!!!!!!11!!!!!"
>&2 echo "!!!11!!!!!!11!!!!!!11!!!!!!11!!!!!!11!!!1!!!!!!11!!!!!"
