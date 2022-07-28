using Defender.UserManagement.Application;
using Defender.UserManagement.Infrastructure;
using Defender.UserManagement.WebUI_old;
using Hellang.Middleware.ProblemDetails;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
builder.Services.AddWebUI_oldServices(builder.Environment, builder.Configuration);
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddInfrastructureServices(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();

    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    });
}
else
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

//app.UseCors("AllowAll");

//app.UseAuthentication();
//app.UseAuthorization();

app.UseProblemDetails();

app.MapControllerRoute(
    name: "default",
    pattern: "api/{controller}/{action=Index}");

app.MapFallbackToFile("index.html");

app.Run();
