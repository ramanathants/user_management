# User Management

1. Update the connection string in the file \user_management\IdentityWebAPIAuthentication\appsettings.json
2. Delete the Migrations folder under WebAPI Project
3. Run the following Migrations Scripts from the WebAPI Project (https://learn.microsoft.com/en-us/ef/core/managing-schemas/migrations/?tabs=dotnet-core-cli)
   
      `dotnet ef migrations add InitialCreate`
   
      `dotnet ef database update`
   
   ### ** Note that you may need to install the tool dotnet-ef https://www.nuget.org/packages/dotnet-ef ** 
5. Ensure the MySQL DB is created with the required tables
   
   ![image](https://github.com/user-attachments/assets/52eab861-0d3f-458b-862a-9f6abf7c5123)

6. Run the WebAPI Project initially
7. Run the WebApp Project and register an Admin User
8. Create the admin Role using the WebAPI Project
9. Assign the admin Role to the Admin User using the WebAPI Project
   
