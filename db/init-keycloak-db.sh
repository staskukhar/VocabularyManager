#!/bin/bash
set -e

# Create keycloak database if it does not exist (runs only on first init when volume is empty)
if [ -n "${KEYCLOAK_DB}" ]; then
    psql -v ON_ERROR_STOP=1 --username "$POSTGRES_USER" --dbname "$POSTGRES_DB" <<EOSQL
SELECT 'CREATE DATABASE "${KEYCLOAK_DB}"'
WHERE NOT EXISTS (SELECT FROM pg_database WHERE datname = '${KEYCLOAK_DB}')\gexec
EOSQL
fi
