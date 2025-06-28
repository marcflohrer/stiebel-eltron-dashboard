#!/bin/bash

set -e

# Versionen setzen
OLD_VERSION="16"
NEW_VERSION="17"
OLD_DATA="/var/lib/postgresql/$OLD_VERSION/data"
NEW_DATA="/var/lib/postgresql/$NEW_VERSION/data"
PG_USER="postgres"

echo "Stoppe alle PostgreSQL-Prozesse..."
apt-get update && apt-get install -y procps 
pgrep -u postgres -x postgres && pkill -9 -u postgres -x postgres || echo "Keine aktiven PostgreSQL-Prozesse gefunden"

echo "Prüfe, ob PostgreSQL läuft..."
if [ -f "$NEW_DATA/postmaster.pid" ]; then
    echo "PostgreSQL läuft noch, stoppe es jetzt..."
    su - postgres -c "/usr/lib/postgresql/$NEW_VERSION/bin/pg_ctl stop -D $NEW_DATA"
    echo "Warte 5 Sekunden auf vollständigen Shutdown..."
    sleep 5
else
    echo "PostgreSQL ist bereits gestoppt."
fi


echo "Überprüfe, ob PostgreSQL 17-Datenverzeichnis existiert..."
if [ -d "$NEW_DATA" ]; then
    echo "Setze Berechtigungen für das PostgreSQL 17-Datenverzeichnis..."
    apt-get update && apt-get install -y sudo
    chown -R postgres:postgres "$NEW_DATA"
    chmod -R u+w "$NEW_DATA"
    ls -la /var/lib/postgresql/
    ls -la /var/lib/postgresql/17/data
    
    echo "Überprüfe laufende PostgreSQL-Instanzen..."
    su - postgres -c "pg_lsclusters"

    echo "Lösche das alte PostgreSQL 17-Datenverzeichnis..."
    # su - root -c "rm -rf $NEW_DATA"
fi

echo "Erstelle neuen Datenbankordner für PostgreSQL $NEW_VERSION..."
mkdir -p "$NEW_DATA"
chown postgres:postgres "$NEW_DATA"

echo "Initialisiere neue PostgreSQL 17-Datenbank..."
su - postgres -c "/usr/lib/postgresql/$NEW_VERSION/bin/initdb -D $NEW_DATA"

echo "Führe pg_upgrade durch..."
su - postgres -c "/usr/lib/postgresql/$NEW_VERSION/bin/pg_upgrade -d $OLD_DATA -D $NEW_DATA -b /usr/lib/postgresql/$OLD_VERSION/bin -B /usr/lib/postgresql/$NEW_VERSION/bin -U $PG_USER"

echo "Backup der alten Datenbankstruktur..."
mv "$OLD_DATA" "${OLD_DATA}_backup"

echo "Ersetze alte Datenbank durch neue Version..."
mv "$NEW_DATA" "$OLD_DATA"

echo "Upgrade abgeschlossen! Starte den PostgreSQL 17-Dienst manuell."
