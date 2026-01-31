---
title: Dev DB Notes
updated: 2026-01-30
---

# Dev DB Notes

The project uses a local SQL Server hosted in a docker container, using the latest image retrieved using the command below. More information can be found [here](https://learn.microsoft.com/en-us/sql/linux/sql-server-linux-docker-container-deployment?view=sql-server-ver17&pivots=cs1-bash) in the SQL Server docs. I noticed two issues running on my Mac. The first is a warning concerning the hardware, which I am ignoring for now. Most of the articles I found about this are using the 2022 version of the image or older. The 2025 version seems to run fine.

```bash
docker pull mcr.microsoft.com/mssql/server:2025-latest
```

## Initial Creation

Run the following to create the container. Docker will use a container volume to persist the data even if the image is deleted. I tested this to confirm, but found that the first time I ran `dotnet run` that there was an unhandled exception. It might take time for the fresh SQL Server to recognize the data in the volume. The container volume is created using the `-v` option.

```bash
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=YourStrongPassw0rd" \
   -p 1433:1433 --name sql1 --hostname sql1 \
   -v sqlvolume:/var/opt/mssql \
   -d \
   mcr.microsoft.com/mssql/server:2025-latest
```

There are a few ways to verify that this was successful:

1. Check Docker Desktop
1. Run `docker ps`
1. Verify that the server is listening for connections by running `docker logs sql1 | grep connection`. You should see an entry reading "SQL Server is now ready for client connections".

Then run the following to start an interactive shell in the container.

```bash
docker exec -it sql1 "bash"
```

Connect to the SQL Server using this:

```bash
/opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P "YourStrongPassw0rd" -C
```

Create the database using this:

```SQL
CREATE DATABASE ExDataDB;
SELECT name FROM sys.databases;
GO
```

You should see the database in the list of database names. I think this could be automated somehow on the initial create of the container, but I haven't figured that out yet.

## Connecting the App to the DB

Add the following to `appsettings.Development.json`:

```json
"ConnectionStrings": {
  "Default": "Server=localhost,1433;Database=ExDataDB;User Id=sa;Password=YourStrongPassw0rd;TrustServerCertificate=True;"
}
```

Make sure that the password matches. This process uses the `sa` account, which should be disabled in production. I think it's probably fine for a local development database.
