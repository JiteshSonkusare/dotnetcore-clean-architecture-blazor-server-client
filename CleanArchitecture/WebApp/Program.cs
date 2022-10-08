using WebApp.Extenions;
using WebApp.Models.Authentication;

#region "Add services to the container"

var builder = WebApplication.CreateBuilder(args);
builder.Services.RegisterAzureAuthDependencies(builder.Configuration);
builder.Services.Configure<ApiClientConfig>(builder.Configuration.GetSection(nameof(ApiClientConfig)));
var app = builder.Build();

#endregion

#region "Configure the HTTP request pipeline"

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
app.MapControllers();
app.Run();

#endregion