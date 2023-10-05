using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;
using UOAmarking.Data;
using UOAmarking.Handler;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;



var builder = WebApplication.CreateBuilder(args);
var provider = builder.Services.BuildServiceProvider();
var configation = provider.GetRequiredService<IConfiguration>();
// Add services to the container.

/*builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    
});*/

builder.Services.AddControllers().AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
    });


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


/*builder.Services
    .AddAuthentication(options => { 
        options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
    }).AddCookie().AddGoogle(GoogleDefaults.AuthenticationScheme,options =>
    {
        options.ClientId = configation["GoogleAuth:ClientId"];
        options.ClientSecret = configation["GoogleAuth:ClientSecret"];

    }).AddScheme<AuthenticationSchemeOptions, AuthHandler>("Authentication", null);*/

builder.Services.AddDbContext<WebAPIDBContext>(options =>
    options.UseSqlServer(configation.GetConnectionString("myrdstest")));

//builder.Services.AddDbContext<WebAPIDBContext>(options => builder.Services.AddDbContext<WebAPIDBContext>(options => options.UseSqlServer(configation.GetConnectionString("myrdstest"))));
//builder.Services.AddDbContext<WebAPIDBContext>(options => options.UseSqlite(builder.Configuration["WebAPIConnection"]));

//builder.Services.AddControllers();
builder.Services.AddScoped<IWebAPIRepo, DBWebAPIRepo>();
/*builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireClaim("admin"));
    options.AddPolicy("AuthOnly", policy => {
        policy.RequireAssertion(context =>
            context.User.HasClaim(c =>
            (c.Type == "normalUser" || c.Type == "admin")));
    });
});*/


builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}
app.UseCors();
app.Use(async (context, next) =>
{
    context.Response.Headers.Add("Cross-Origin-Opener-Policy", "same-origin");
    await next();
});

app.UseSwagger();
app.UseSwaggerUI();


//app.UseHttpsRedirection();
//app.UseAuthentication();

//app.UseAuthorization();
/*app.Use(async(context, next) =>
{
    context.Response.Headers.Add("Cross-Origin-Opener-Policy", "same-origin");
    await next();
});*/




app.MapControllers();

app.Run();
