ApiPlayground project is R&D for the following things:

1. Serilog logging system
	* Configurate your settings which contains: enrichers, writeTos (sinks)
	* Create the logger configuration
	* On the IHostBulder add UseSerilog();

2. Seq logging dashboard (running on docker port 8081)
	* Add Nuget for seq
	* Run docker container to have a seq server.
	* Add sink and specify the seq server url.


3. Create custom Middlewares in .NET
	* Basically create a class that takes a RequestDelegate (next) and have an Invoke or InvokeAsync method.
	* in Startup.Cs/Configure use app.UseMiddleware<YourMiddleware>();

4. .NET Core In-Memory caching.
	

5. Using API Key authentication
6. Using JWT Bearer authentication
7. Hmac Sha hashing
8. 
