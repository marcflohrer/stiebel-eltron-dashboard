. .env

mkdir -p mssql/data

cd Data/Generated/ && find . ! -name . -prune ! -wholename ../Models -exec rm {} \; && cd ../..
dotnet build
dotnet user-secrets set ConnectionStrings:DefaultConnection "${DatabaseConnectionString};" --project .
dotnet user-secrets set ServiceWeltUser "${ServiceWeltUser}" --project .
dotnet user-secrets set ServiceWeltPassword "${ServiceWeltPassword}" --project .
dotnet user-secrets set ServiceWeltUrl "${ServiceWeltUrl}" --project .
docker compose -f docker-compose-dbmigrate.yml up --build --remove-orphans 
dotnet user-secrets clear
