## Mangled Headers
```
dotnet run --project MangledHeaders
```
Navigate to http://localhost:5000 to observe the manglage or lack thereof.  
Uncomment `.Use(async (context, next) => await next.Invoke())` in Startup.cs and try again.
