using Microsoft.EntityFrameworkCore;

using System.ComponentModel.DataAnnotations.Schema;

namespace TextLore.Shared.Models.Level;

[PrimaryKey(nameof(Id), nameof(Tag), nameof(Game))]
public class RoomDefinition(int id, RoomTag tag, string game)
{
    [Column(Order = 0)]
    public int Id { get; } = id;

    [Column(Order = 1)]
    public RoomTag Tag { get; } = tag;

    [Column(Order = 2)]
    public string Game { get; } = game;

    public required string Name { get; set; }

    public required Size Size { get; set; }

    public string Script { get; set; } = string.Empty;
}