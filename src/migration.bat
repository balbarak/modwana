@Echo off
cd "Modwana.Web"
SET /P NAME=Enter Migration Name: %=%

@ECHO on

dotnet ef migrations add %NAME% --project "../Modwana.Persistance" --context SqlDbContext --output-dir "../Modwana.Persistance/Migrations/Sql"

dotnet ef migrations add %NAME% --project "../Modwana.Persistance" --context SqliteDbContext --output-dir "../Modwana.Persistance/Migrations/Sqlite"

dotnet ef migrations add %NAME% --project "../Modwana.Persistance" --context PostgreSqlDbContext --output-dir "../Modwana.Persistance/Migrations/Postgresql"

dotnet ef migrations add %NAME% --project "../Modwana.Persistance" --context MySqlDbContext --output-dir "../Modwana.Persistance/Migrations/MySql"




