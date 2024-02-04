using Microsoft.EntityFrameworkCore;

using TextLore.Components;
using TextLore.Database;

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
        new TextLore.Games.Roguelike.Models.RoomDefinition(1, TextLore.Games.Roguelike.Models.RoomTag.Start, "rogelike") { Name = "Start Room" },
        new TextLore.Games.Roguelike.Models.RoomDefinition(1, TextLore.Games.Roguelike.Models.RoomTag.Unique, "rogelike") { Name = "Unique Room" },
        new TextLore.Games.Roguelike.Models.RoomDefinition(2, TextLore.Games.Roguelike.Models.RoomTag.Unique, "rogelike") { Name = "Normal Room 1" },
        new TextLore.Games.Roguelike.Models.RoomDefinition(3, TextLore.Games.Roguelike.Models.RoomTag.Unique, "rogelike") { Name = "Normal Room 2" },
        new TextLore.Games.Roguelike.Models.RoomDefinition(4, TextLore.Games.Roguelike.Models.RoomTag.Unique, "rogelike") { Name = "Normal Room 3" },
        new TextLore.Games.Roguelike.Models.RoomDefinition(5, TextLore.Games.Roguelike.Models.RoomTag.Unique, "rogelike") { Name = "Normal Room 4" },
        new TextLore.Games.Roguelike.Models.RoomDefinition(6, TextLore.Games.Roguelike.Models.RoomTag.Unique, "rogelike") { Name = "Normal Room 5" },
        new TextLore.Games.Roguelike.Models.RoomDefinition(1, TextLore.Games.Roguelike.Models.RoomTag.End, "rogelike") { Name = "End Room" }
    );

    _ = await databaseContext.SaveChangesAsync();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();