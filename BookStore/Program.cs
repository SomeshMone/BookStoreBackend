using Businesslayer.Interfaces;
using Businesslayer.Services;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);



//add services to the container.

//builder.Services.AddMassTransitHostedService();
//builder.Services.AddMassTransitHostedService();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IUserBusiness,UserBusiness>();
builder.Services.AddTransient<IUserRepository, UserRepository>();

builder.Services.AddTransient<IBookRepository, BookRepository>();
builder.Services.AddTransient<IBookBusiness, BookBusiness>();

builder.Services.AddTransient<IAddressBusiness, AddressBusiness>();
builder.Services.AddTransient<IAddressRepository, AddressRepository>();

builder.Services.AddTransient<IReviewBusiness, ReviewBusiness>();
builder.Services.AddTransient<IReviewRepository, ReviewRepository>();

builder.Services.AddTransient<IOrderBusiness,OrderBusiness>();
builder.Services.AddTransient<IOrderRepository, OrderRepository>();

builder.Services.AddTransient<IWishListBusiness, WishListBusiness>();
builder.Services.AddTransient<IWishListRepository, WishListRepository>();

builder.Services.AddTransient<ICartListBusiness, CartListBusiness>();
builder.Services.AddTransient<ICartRepository, CartRepository>();


builder.Services.AddSwaggerGen(
    option =>
    {
        option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
        option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "Please enter a valid token",
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            BearerFormat = "JWT",
            Scheme = "Bearer"
        });
        option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
    });
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    var Key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]);
    o.SaveToken = true;
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});


////builder.Services.AddMassTransit(x =>
////{
////    x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(config =>
////    {
////        config.UseHealthCheck(provider);
////        config.Host(new Uri("rabbitmq://localhost"), h =>
////        {
////            h.Username("guest");
////            h.Password("guest");
////        });
////    }));
////});
//builder.Services.AddMassTransitHostedService();
builder.Services.AddDistributedMemoryCache();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthentication();

app.UseHttpsRedirection();

app.UseAuthorization();


app.MapControllers();

app.Run();
