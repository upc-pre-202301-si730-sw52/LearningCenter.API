using LearningCenter.API.Analytics.Domain.Services;
using LearningCenter.API.Analytics.Services;
using LearningCenter.API.Learning.Domain.Repositories;
using LearningCenter.API.Learning.Domain.Services;
using LearningCenter.API.Learning.Interfaces.Internal;
using LearningCenter.API.Learning.Persistence.Repositories;
using LearningCenter.API.Learning.Services;
using LearningCenter.API.Shared.Domain.Repositories;
using LearningCenter.API.Shared.Persistence.Contexts;
using LearningCenter.API.Shared.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



// Add Database Connection

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(
    options => options.UseMySQL(connectionString)
        .LogTo(Console.WriteLine, LogLevel.Information)
        .EnableSensitiveDataLogging()
        .EnableDetailedErrors());

// Lowercase URLs configuration
builder.Services.AddRouting(options => options.LowercaseUrls = true);

// Dependency Injection Configuration

// Shared Bounded Contexts Injection Configuration

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Learning Bounded Context Injection Configuration

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ITutorialRepository, TutorialRepository>();
builder.Services.AddScoped<ITutorialService, TutorialService>();
builder.Services.AddScoped<ILearningContextFacade, LearningContextFacade>();

// Analytics Bounded Context Injection Configuration

builder.Services.AddScoped<ILearningAnalyticsService, LearningAnalyticsService>();


// AutoMapper Configuration

builder.Services.AddAutoMapper(
    typeof(LearningCenter.API.Learning.Mapping.ModelToResourceProfile),
    typeof(LearningCenter.API.Learning.Mapping.ResourceToModelProfile));

// Application build

var app = builder.Build();


// Validation for ensuring Database Objects are created

using (var scope = app.Services.CreateScope())
using (var context = scope.ServiceProvider.GetService<AppDbContext>())
{
    context.Database.EnsureCreated();
}
        

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
