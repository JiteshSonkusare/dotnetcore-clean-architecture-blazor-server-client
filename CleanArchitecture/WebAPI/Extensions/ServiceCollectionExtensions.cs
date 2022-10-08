using WebAPI.GraphQL.Queries;
using Infrastructure.Context;
using WebAPI.GraphQL.Mutation;
using Microsoft.OpenApi.Models;
using HotChocolate.Data.Filters;
using WebAPI.GraphQL.CustomFilters;
using Microsoft.EntityFrameworkCore;
using HotChocolate.Data.Filters.Expressions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;

namespace WebAPI.Extensions
{
    public static class ServiceCollectionExtensions
    {
        internal static void AddAuthDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                        .AddMicrosoftIdentityWebApi(configuration.GetSection("AzureAd"));
        }
        internal static void RegisterGrapQLDependencies(this IServiceCollection services)
        {
            services.AddRouting()
                    .AddGraphQLServer()
                    .AddProjections()
                    .AddFiltering()
                    .AddSorting()
                    .AddQueryType<UserQuery>()
                    .AddMutationType<UserMutation>()
                    .AddConvention<IFilterConvention>(new FilterConventionExtension(
                                                    x => x.AddProviderExtension(
                                                        new QueryableFilterProviderExtension(
                                                            y => y.AddFieldHandler<QueryableStringInvariantEqualsHandler>()))));
        }

        internal static IServiceCollection AddDatabase(
            this IServiceCollection services,
            IConfiguration configuration)
            => services
                .AddDbContext<CleanArchitectureDBContext>(options => options
                    .UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        internal static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo Auth API", Version = "v1" });
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
                                Type = ReferenceType.SecurityScheme,
                                Id   = "Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
            });
        }

        internal static void MigrateDatabase(this IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var dataContext = scope.ServiceProvider.GetRequiredService<CleanArchitectureDBContext>();
                dataContext.Database.Migrate();
            }
        }
    }
}
