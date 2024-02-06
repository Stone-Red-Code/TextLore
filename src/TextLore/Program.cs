using Microsoft.EntityFrameworkCore;

using TextLore.Components;
using TextLore.Database;
using TextLore.Shared.Models.Level;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddDbContext<GamesDatabaseContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("GamesDatabase")));

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    _ = app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    _ = app.UseHsts();
}

using AsyncServiceScope serviceScope = app.Services.CreateAsyncScope();

GamesDatabaseContext databaseContext = serviceScope.ServiceProvider.GetRequiredService<GamesDatabaseContext>();

if (await databaseContext.Database.EnsureCreatedAsync())
{
    databaseContext.Rooms.AddRange(
        new RoomDefinition(1, RoomTag.Start, "roguelike") { Name = "Start Room" },
        new RoomDefinition(1, RoomTag.Unique, "roguelike") { Name = "Unique Room" },
        new RoomDefinition(2, RoomTag.None, "roguelike") { Name = "Normal Room 1" },
        new RoomDefinition(3, RoomTag.None, "roguelike") { Name = "Normal Room 2" },
        new RoomDefinition(4, RoomTag.None, "roguelike") { Name = "Normal Room 3" },
        new RoomDefinition(5, RoomTag.None, "roguelike") { Name = "Normal Room 4" },
        new RoomDefinition(6, RoomTag.None, "roguelike") { Name = "Normal Room 5" },
        new RoomDefinition(1, RoomTag.End, "roguelike") { Name = "End Room" }
    );

    _ = await databaseContext.SaveChangesAsync();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();