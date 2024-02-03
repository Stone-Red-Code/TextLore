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
    databaseContext.RoguelikeRooms.AddRange(
        new TextLore.Games.Roguelike.Models.RoomDefinition("Room 1"),
        new TextLore.Games.Roguelike.Models.RoomDefinition("Room 2"),
        new TextLore.Games.Roguelike.Models.RoomDefinition("Room 3"),
        new TextLore.Games.Roguelike.Models.RoomDefinition("Room 4"),
        new TextLore.Games.Roguelike.Models.RoomDefinition("Room 5"),
        new TextLore.Games.Roguelike.Models.RoomDefinition("Room 6"),
        new TextLore.Games.Roguelike.Models.RoomDefinition("Room 7"),
        new TextLore.Games.Roguelike.Models.RoomDefinition("Room 8"),
        new TextLore.Games.Roguelike.Models.RoomDefinition("Room 9"),
        new TextLore.Games.Roguelike.Models.RoomDefinition("Room 10")
    );

    _ = await databaseContext.SaveChangesAsync();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();