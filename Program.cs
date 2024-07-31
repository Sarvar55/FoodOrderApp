using FoodOrderApp;
using FoodOrderApp.Data;
using FoodOrderApp.Utility;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var startup = new Startup(builder.Configuration);

builder.Services.AddControllersWithViews();
builder.Services.AddControllers()
    .AddNewtonsoftJson();

builder.Services.AddDbContext<FoodOrderContext>(optionsAction =>optionsAction.UseSqlServer(AppUtil.GetSqlConnectionString(builder.Configuration)));


startup.ConfigureServices(builder.Services);


var app = builder.Build();

startup.Configure(app, app.Environment);

app.Run();
