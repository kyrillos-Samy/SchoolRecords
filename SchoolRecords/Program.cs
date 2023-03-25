using Business.Helper;
using Business.Services.Contract;
using Business.Services.Implementation;
using Common.Logger.Contract;
using Common.Logger.Implementation;
using DTO;
using Entities;
using Microsoft.EntityFrameworkCore;
using Persistence;
using Persistence.UnitOfWork.Contract;
using Persistence.UnitOfWork.Implementation;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetSection("ConnectionStrings")
                        .GetSection("DefaultConnection").Value;

builder.Services.AddDbContext<SchoolRecordsContext>(options =>
{
    options.UseSqlServer(connectionString).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

#region Repositories
builder.Services.AddScoped<IUnitOfWork<Class>, UnitOfWork<Class>>();
builder.Services.AddScoped<IUnitOfWork<Course>, UnitOfWork<Course>>();
builder.Services.AddScoped<IUnitOfWork<StudentClass>, UnitOfWork<StudentClass>>();
builder.Services.AddScoped<IUnitOfWork<StudentData>, UnitOfWork<StudentData>>();
builder.Services.AddScoped<IUnitOfWork<StudentDataConfiguration>, UnitOfWork<StudentDataConfiguration>>();
builder.Services.AddScoped<IUnitOfWork<Student>, UnitOfWork<Student>>();
#endregion Repositories

#region Services
builder.Services.AddTransient<IBaseService<StudentDataConfigurationDTO>, BaseService<StudentDataConfiguration, StudentDataConfigurationDTO>>();
builder.Services.AddTransient<IClassService, ClassService>();
builder.Services.AddTransient<ICourseService, CourseService>();
builder.Services.AddTransient<IStudentService, StudentService>();
#endregion Services

builder.Services.AddTransient<ILoggerBase, FileLogger>();

builder.Services.AddAutoMapper(typeof(AutoMapping));

builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
