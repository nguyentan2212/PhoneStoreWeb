# PhoneStoreWeb
## Install .NET 5.0
https://dotnet.microsoft.com/download/dotnet/5.0

## SqlServer database
PhoneStoreWeb/PhoneStoreWeb.Data/Contexts/PhoneStoreDbContext.cs
**thay** 
    optionsBuilder.UseInMemoryDatabase("data-context");
**bằng** 
    optionsBuilder.UseSqlServer(AppConstant.DbConnectionString); 

PhoneStoreWeb/PhoneStoreWeb.AdminApp/Startup.cs
**thay**
    options.UseInMemoryDatabase("data-context");
**bằng**
    options.UseSqlServer(Configuration.GetConnectionString("PhoneStoreContext"));

### Install EF tool
```bash
dotnet tool install --global dotnet-ef
```
### Migration database
```bash
cd PhoneStoreWeb\PhoneStoreWeb.Data
dotnet ef database update
```
## InMemoryDatabase (database ảo)
Làm ngược lại trên
## Run code
```bash
cd PhoneStoreWeb
dotnet restore
cd PhoneStoreWeb.AdminApp
dotnet run
```
## Docker
```bash
docker build -t phonestore .
docker run -d -p 8080:80 --name abc phonestore
```
link: http://localhost:8080/
## Admin account
**user name:** admin
**pass:** 12345