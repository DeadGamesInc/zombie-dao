# Zombie DAO (WIP)
This project contains the main features for the Zombie DAO application.
## How to run locally?

### Setup Environment Variables
- ZOMBIE_DAO_DBCONN : Postgres connection string in the following format:
`Host=dbhost;Port=5432;Database=dbname;Username=dbusername;Password=dbpass;`

### Install frontend dependencies
- Inside the `UI/` directory
- `yarn install`

### Build and run .NET app
- Inside the `ZombieDAO/` directory
- `dotnet build`
- `dotnet run`
