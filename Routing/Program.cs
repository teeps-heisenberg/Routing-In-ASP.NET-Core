var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseRouting();

app.Use(async (HttpContext httpContext,RequestDelegate next) =>
{
    Endpoint endpoint = httpContext.GetEndpoint();
    if (endpoint != null)
        await httpContext.Response.WriteAsync(endpoint.DisplayName+ "\n\n");
    await next(httpContext);
});

app.UseEndpoints(endpoints =>
{
    //endpoints.Map("/Home", async(HttpContext httpContext) => { 
    //    await httpContext.Response.WriteAsync("I am in Home Page");    
    //});

    //endpoints.MapGet("/Product", async (HttpContext httpContext) => {
    //    await httpContext.Response.WriteAsync("I am in Product Page");
    //});

    //endpoints.MapPost("/Product", async (HttpContext httpContext) => {
    //    await httpContext.Response.WriteAsync("New product created");
    //});
    endpoints.MapGet("/Product/{id:int}", async (HttpContext httpContext) => {
        var id = httpContext.Request.RouteValues["id"];
        if(id != null)
        {
            id = Convert.ToInt32(id);
            await httpContext.Response.WriteAsync("Product with id: " + id);
        }
        else
            await httpContext.Response.WriteAsync("On Product Page");
    });


    endpoints.Map("/Book/{authorName}/{bookId}", async (HttpContext httpContext) => {
        var bookId = Convert.ToInt32(httpContext.Request.RouteValues["bookId"]);
        var authorName = Convert.ToString(httpContext.Request.RouteValues["authorName"]);
        await httpContext.Response.WriteAsync($"Book with id {bookId} and author {authorName}");
    });

});


app.Run(async (HttpContext httpContext) => {
    await httpContext.Response.WriteAsync("Default Route");
});


app.Run();
