#!/bin/bash

docker run -e 'ACCEPT_EULA=Y' --name="QROrganizer" -e 'SA_PASSWORD=Password1!' -p 1433:1433 -d mcr.microsoft.com/mssql/server:2019-latest