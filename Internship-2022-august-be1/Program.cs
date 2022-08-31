using Internship_2022.Application.Interfaces;
using Internship_2022.Application.JwtUtils;
using Internship_2022.Application.Mapper;
using Internship_2022.Application.Models.MailDto;
using Internship_2022.Application.Services;
using Internship_2022.Infrastructure.Contexts;
using Internship_2022.Infrastructure.Interfaces;
using Internship_2022.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text;
using Quartz;
using Ninject;
using Quartz.Spi;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddCors(options =>
{

    options.AddPolicy("PolicyUser",
        policy =>
        {
            policy
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});


builder.Services.AddMvc();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<EFContext>(
      options => options.UseSqlServer(builder.Configuration.GetConnectionString("DBString")),
      ServiceLifetime.Transient,
      ServiceLifetime.Transient);





builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IListingRepository, ListingRepository>();
builder.Services.AddScoped<IListingService, ListingService>();

builder.Services.AddScoped<IFavoriteRepository, FavoriteRepository>();
builder.Services.AddScoped<IFavoriteService, FavoriteService>();

builder.Services.AddScoped<IMessageRepository, MessageRepository>();
builder.Services.AddScoped<IMessageService, MessageService>();

builder.Services.AddScoped<IAzureStorageService, AzureStorageService>();


builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
builder.Services.AddSingleton<IMailService, MailService>();

builder.Services.AddScoped<IJobFactory, SendReminderEmailJobFactory>();


builder.Services.AddScoped<SendReminderEmailJob>();

builder.Services.AddQuartz(q =>
{
    q.UseMicrosoftDependencyInjectionJobFactory();

    var jobKey = new JobKey("SendReminderEmailJob");

    q.AddJob<SendReminderEmailJob>(opts => opts.WithIdentity(jobKey));

    q.AddTrigger(opts => opts
        .ForJob(jobKey) 
        .WithIdentity("SendReminderEmailJobb-trigger") 
        .WithCronSchedule("0/5 * * * * ?")); 

});
builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

builder.Services.AddScoped<IListingRepository, ListingRepository>();
builder.Services.AddScoped<IListingService, ListingService>();

builder.Services.AddScoped<IFavoriteRepository, FavoriteRepository>();
builder.Services.AddScoped<IFavoriteService, FavoriteService>();

builder.Services.AddScoped<IJwtUtils, JwtUtils>();

builder.Services.AddScoped<IAzureStorageService, AzureStorageService>();

builder.Services.AddAutoMapper(typeof(UserProfile));
builder.Services.AddAutoMapper(typeof(ListingProfile));
builder.Services.AddControllersWithViews();

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(builder.Configuration.GetSection("AppSettings:Secret").Value)),
            ValidateIssuer = false,
            ValidateAudience = false
        };

    });


builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy =>
    policy.RequireAssertion(context => context.User.Claims.First(x=>x.Type=="Role").Value=="Admin" || 
    context.User.Claims.First(x=>x.Type=="Role").Value=="User"));

    options.AddPolicy("User", policy =>
    policy.RequireAssertion(context => context.User.Claims.First(x => x.Type == "Role").Value == "User"));
});

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.OAuthClientId("clientname");
    options.OAuthRealm("your-real");
});

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();
app.UseMiddleware<JwtMiddleware>();



app.Run();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});