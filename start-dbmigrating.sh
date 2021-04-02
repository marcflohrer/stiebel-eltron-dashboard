. .env
cd Data/Generated/ && find . ! -name . -prune ! -name ../Models -exec rm {} \; && cd ../..
dotnet user-secrets set ConnectionStrings:DefaultConnection "Server=db;Database=master;User=sa;Password=${DatabasePassword};" --project .
dotnet user-secrets set DatabasePassword "${DatabasePassword}" --project .
docker-compose -f docker-compose-dbmigrate.yml up --build --remove-orphans 
dotnet user-secrets remove "DatabasePassword"
dotnet user-secrets remove "ConnectionStrings:DefaultConnection"
