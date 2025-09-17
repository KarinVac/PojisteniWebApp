using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PojisteniWebApp;
using PojisteniWebApp.Data;
using PojisteniWebApp.Interfaces;
using PojisteniWebApp.Managers;
using PojisteniWebApp.Models;
using PojisteniWebApp.Repositories;

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
    options.SignIn.RequireConfirmedAccount = false;
})
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IClientManager, ClientManager>();
builder.Services.AddScoped<IInsuranceRepository, InsuranceRepository>();
builder.Services.AddScoped<IInsuranceManager, InsuranceManager>();

var app = builder.Build();

await SeedDatabaseAsync(app);


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
app.MapRazorPages();

app.Run();


static async Task SeedDatabaseAsync(IHost host)
{
    using (var scope = host.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        var logger = services.GetRequiredService<ILogger<Program>>();
        try
        {
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
            var dbContext = services.GetRequiredService<ApplicationDbContext>();

            await dbContext.Database.EnsureCreatedAsync();

            if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            if (!await roleManager.RoleExistsAsync(UserRoles.User))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

            
            string adminEmail = "admin@admin.com";
            string adminPassword = "Heslo.123";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
                adminUser = new IdentityUser { UserName = adminEmail, Email = adminEmail, EmailConfirmed = true };
                await userManager.CreateAsync(adminUser, adminPassword);
                logger.LogInformation("Admin účet vytvořen.");
            }
           
            else
            {
                if (!await userManager.CheckPasswordAsync(adminUser, adminPassword))
                {
                    var token = await userManager.GeneratePasswordResetTokenAsync(adminUser);
                    var result = await userManager.ResetPasswordAsync(adminUser, token, adminPassword);
                    if (result.Succeeded)
                    {
                        logger.LogWarning("Heslo pro admina bylo resetováno na výchozí hodnotu.");
                    }
                }
            }

            if (!await userManager.IsInRoleAsync(adminUser, UserRoles.Admin))
            {
                await userManager.AddToRoleAsync(adminUser, UserRoles.Admin);
                logger.LogInformation("Adminovi byla přidělena/potvrzena role 'Admin'.");
            }

            if (!await dbContext.Client.AnyAsync())
            {
                var demoUsersData = new List<(IdentityUser User, string Password, Client ClientInfo)>
                {
                    (new IdentityUser { UserName = "harry@potter.cz", Email = "harry@potter.cz", EmailConfirmed = true }, "Password.123", new Client { FirstName = "Harry", LastName = "Potter", Email = "harry@potter.cz", PhoneNumber = "123456789", Street = "Zobí 4", City = "Kvikálkov", Zip = "12345" }),
                    (new IdentityUser { UserName = "ron@weasley.cz", Email = "ron@weasley.cz", EmailConfirmed = true }, "Password.123", new Client { FirstName = "Ron", LastName = "Weasley", Email = "ron@weasley.cz", PhoneNumber = "987654321", Street = "Doupě 1", City = "Vydrník", Zip = "54321" }),
                    (new IdentityUser { UserName = "hermiona@granger.cz", Email = "hermiona@granger.cz", EmailConfirmed = true }, "Password.123", new Client { FirstName = "Hermiona", LastName = "Granger", Email = "hermiona@granger.cz", PhoneNumber = "555666777", Street = "Hampstead 8", City = "Londýn", Zip = "67890" }),                   
                };

                var clientsToSave = new List<Client>();
                foreach (var (user, password, clientInfo) in demoUsersData)
                {
                    if (await userManager.FindByEmailAsync(user.Email) == null)
                    {
                        await userManager.CreateAsync(user, password);
                        await userManager.AddToRoleAsync(user, UserRoles.User);
                        clientsToSave.Add(clientInfo);
                    }
                }

                await dbContext.Client.AddRangeAsync(clientsToSave);
                await dbContext.SaveChangesAsync();

                var insurances = new List<Insurance>
                {
                    new() { Type = "Pojištění koštěte", Amount = 10000, StartDate = DateTime.Now, EndDate = DateTime.Now.AddYears(1), ClientId = clientsToSave[0].Id },
                    new() { Type = "Pojištění auta", Amount = 20000, StartDate = DateTime.Now, EndDate = DateTime.Now.AddYears(6), ClientId = clientsToSave[0].Id },
                    new() { Type = "Pojištění auta", Amount = 50000, StartDate = DateTime.Now, EndDate = DateTime.Now.AddYears(5), ClientId = clientsToSave[1].Id },
                    new() { Type = "Životní pojištění", Amount = 250000, StartDate = DateTime.Now, EndDate = DateTime.Now.AddYears(10), ClientId = clientsToSave[2].Id },
                    
                };
                await dbContext.Insurance.AddRangeAsync(insurances);
                await dbContext.SaveChangesAsync();

                logger.LogInformation("Demo data byla úspěšně vytvořena.");
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Nastala chyba při vytváření úvodních dat.");
        }
    }
}
