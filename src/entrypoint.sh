
#!/bin/bash
set -ex

mkdir -p mssql/data
mkdir -p mssql/log
mkdir -p mssql/secrets

>&2 echo "!!!11!!!!!!11!!!!!!11!!!!!!11!!!!!!11!!!"
>&2 echo "Running entrypoint.sh !!!11!!!!!!11!!!!!"
>&2 echo "!!!11!!!!!!11!!!!!!11!!!!!!11!!!!!!11!!!"

dotnet dev-certs https

until dotnet user-secrets init && dotnet user-secrets set ConnectionStrings:DefaultConnection "$1" --project .; do
>&2 echo "Setting up user secret #1: " $1
sleep 1
done

until dotnet user-secrets init && dotnet user-secrets set ServiceWeltUrl "$2" --project .; do
>&2 echo "Setting up user secret #2: " $2
sleep 1
done

until dotnet user-secrets init && dotnet user-secrets set ServiceWeltUser "$3" --project .; do
>&2 echo "Setting up user secret #3: " $3
sleep 1
done

until dotnet user-secrets init && dotnet user-secrets set ServiceWeltPassword "$4" --project .; do
>&2 echo "Setting up user secret #4: " $4
sleep 1
done

dotnet stiebel-eltron-dashboard.dll

>&2 echo "!!!11!!!!!!11!!!!!!11!!!!!!11!!!!!!11!!!1!!!!!!11!!!!!"
>&2 echo "Running entrypoint.sh :: APP RUNNING !!!!!!!!!!11!!!!!"
>&2 echo "!!!11!!!!!!11!!!!!!11!!!!!!11!!!!!!11!!!1!!!!!!11!!!!!"
