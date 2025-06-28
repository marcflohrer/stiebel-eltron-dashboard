
#!/bin/bash

set -e

# Clean up possible casing conflict BEFORE adding migration
rm -rf Data

mkdir -p postgres/data

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

# Clean up possible casing conflict BEFORE adding migration
rm -rfv /app/Data
ls -la /app
[ -d /app/Data ] && echo "⚠️  [WARN] Ordner /app/Data existiert noch"
[ -d /app/data ] && echo "✅ [INFO] Ordner /app/data existiert"
rm -rfv /app/data/Migrations
rm -rfv /app/Data/Migrations

now_hourly=$(date +%Y-%d-%b-%H_%M) 
>&2 echo "dotnet ef migrations add" $now_hourly"ChangeDatabase"
dotnet ef migrations add $now_hourly"ChangeDb" --context StiebelEltronDashboard.Models.ApplicationDbContext --output-dir data/Migrations;

>&2 echo "dotnet ef database update"
until dotnet ef database update \
  --project ./stiebel-eltron-dashboard.csproj \
  --startup-project ./stiebel-eltron-dashboard.csproj \
  --context StiebelEltronDashboard.Models.ApplicationDbContext \
  -v; do
  echo "[!] ❌ EF Update failed. Showing tree:"
  tree -d -L 2 /app > /app/tree-out.txt 2>&1
  echo "[i] Tree saved to /app/tree-out.txt (showing top 30 lines)"
  head -n 30 /app/tree-out.txt
  >&2 echo "SQL Server is starting up"
  sleep 10
done

echo "[DEBUG] Nach Migration: Verzeichnisstruktur"
tree -d -L 3 /app | grep -i migrations

>&2 echo "!!!11!!!!!!11!!!!!!11!!!!!!1!"
>&2 echo "Migration done !!!!!!!11!!!!!"
>&2 echo "!!!11!!!!!!11!!!!!!11!!!!!!11"
