using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PojisteniWebApp;
using PojisteniWebApp.Data;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = false;
    options.User.RequireUniqueEmail = true;
})
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllersWithViews();

var app = builder.Build();



// Vytvoøí role "admin" a "user"
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

    // Vytvoøení rolí, pokud neexistují
    if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
        await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
    if (!await roleManager.RoleExistsAsync(UserRoles.User))
        await roleManager.CreateAsync(new IdentityRole(UserRoles.User));


    
    // Tento uživatel se stane administrátorem.
    string adminEmail = "admin@admin.cz";
    var adminUser = await userManager.FindByEmailAsync(adminEmail);

    // Pøiøazení role admina, pokud uživatel existuje a ještì roli nemá
    if (adminUser != null && !await userManager.IsInRoleAsync(adminUser, UserRoles.Admin))
    {
        await userManager.AddToRoleAsync(adminUser, UserRoles.Admin);
    }
}



if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
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

app.Run();