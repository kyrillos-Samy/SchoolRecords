using Common.Logger.Contract;
using Common.Logger.Implementation;
using Common.ServiceConnector.Contract;
using Common.ServiceConnector.Implementation;
using SchoolRecordsWeb;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddTransient<ILoggerBase, FileLogger>();
builder.Services.AddTransient<IServiceConnector, ServiceConnector>();

ApplicationSettings ApplicationSettings = new ApplicationSettings();
builder.Configuration.Bind("ApplicationSettings", ApplicationSettings);
builder.Services.AddSingleton(ApplicationSettings);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
