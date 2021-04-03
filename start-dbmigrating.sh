. .env
cd Data/Generated/ && find . ! -name . -prune ! -name ../Models -exec rm {} \; && cd ../..
dotnet user-secrets set ConnectionStrings:DefaultConnection "${DatabaseConnectionString};" --project .
dotnet user-secrets set ServiceWeltPassword "${ServiceWeltPassword}" --project .
docker-compose -f docker-compose-dbmigrate.yml up --build --remove-orphans 
dotnet user-secrets clear
