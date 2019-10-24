## Mangled Headers
```
dotnet run --project MangledHeaders
```
Navigate to http://localhost:5000 to observe the manglage or lack thereof.  
Uncomment `.Use(async (context, next) => await next.Invoke())` in Startup.cs and try again.  
Targets ASP.NET Core 3.0. To try 2.2 or 3.1, comment-out the target framework and header propagation package reference in MangledHeaders.csproj, and uncomment the corresponding other ones.
