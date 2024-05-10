
using Coffe_Shop_WebAPI.Interface;
using Coffe_Shop_WebAPI.Models;
using Coffe_Shop_WebAPI.Repository;
using Coffe_Shop_WebAPI.Services;
using Coffe_Shop_WebAPI.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Coffe_Shop_WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string stringCors = "_myAllowSpecificOrigins";
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddScoped<IRepository<Product>,GenericRepository<Product>>();
            builder.Services.AddScoped<IRepository<Order>, GenericRepository<Order>>();
            builder.Services.AddScoped<IRepository<Category>, GenericRepository<Category>>();
            builder.Services.AddScoped<IRepository<Product_Order>, GenericRepository<Product_Order>>();
            builder.Services.AddScoped<IRepository<Product_User>, GenericRepository<Product_User>>();
            builder.Services.AddScoped<IRepository<AppUser>, GenericRepository<AppUser>>();

            builder.Services.AddScoped<unitOfWork<Product>, unitOfWork<Product>>();
            builder.Services.AddScoped<unitOfWork<Order>, unitOfWork<Order>>();
            builder.Services.AddScoped<unitOfWork<Product_Order>, unitOfWork<Product_Order>>();
            builder.Services.AddScoped<unitOfWork<Product_User>, unitOfWork<Product_User>>();

            builder.Services.AddScoped<unitOfWork<Category>, unitOfWork<Category>>();
            builder.Services.AddScoped<CategoryServices, CategoryServices>();
            builder.Services.AddScoped<OrderServices, OrderServices>();
            builder.Services.AddScoped<CartServices, CartServices>();
            builder.Services.AddScoped<FavouriteServices, FavouriteServices>();
            builder.Services.AddScoped<ProductServices, ProductServices>();
            builder.Services.AddDbContext<CoffeeContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
           
            builder.Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<CoffeeContext>();

            builder.Services.AddAuthentication(option => option.DefaultAuthenticateScheme = "myscheme")
                .AddJwtBearer("myscheme",
                // To validate token
                op =>
                {
                    #region secret key
                    string key = "Hello from the other side Shrouq Gamal Ali Shaban";
                    var secertkey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));
                    #endregion
                    op.TokenValidationParameters = new TokenValidationParameters()
                    {
                        IssuerSigningKey = secertkey,
                        ValidateIssuer = false,
                        ValidateAudience = false

                    };
                });

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(stringCors,
                builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyMethod()
                    .AllowAnyHeader().SetIsOriginAllowedToAllowWildcardSubdomains();
                });
            });
            var app = builder.Build();
                // Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.UseCors(stringCors);

            app.MapControllers();

            app.Run();
        }
    }
}
