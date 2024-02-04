using Microsoft.EntityFrameworkCore;

using TextLore.Games.Roguelike.Models;
using TextLore.Utilities.Logic;
using TextLore.Utilities.Models;

namespace TextLore.Games.Roguelike.Logic;

public class LevelGenerator(DeterministicRandom seed, IQueryable<RoomDefinition> rooms)
{
    public async Task<Level> GenerateLevel(IProgress<PercentageProgress> progress)
    {
        // Random size between 10x10 and 100x100.
        Size size = new Size(seed.Next(10, 100), seed.Next(10, 100));

        // Fill 20-60% of the level with rooms.
        int roomCount = size.Width * size.Height / 100 * seed.Next(20, 60);

        Level level = new Level(size, seed);
        Queue<Room> generationQueue = new Queue<Room>();

        RoomDefinition startRoomDefinition = await GetRandomRoomDefinition(level) ?? throw new InvalidOperationException("No rooms found in the database.");
        Position startRoomPosition = new Position(seed.Next(0, size.Width), seed.Next(0, size.Height));

        Room startRoom = new Room(startRoomPosition, startRoomDefinition);

        _ = level.AddRoom(startRoom);
        generationQueue.Enqueue(startRoom);

        while (generationQueue.Count > 0)
        {
            Room currentRoom = generationQueue.Dequeue();

            foreach (Direction direction in currentRoom.DoorDirections.ToArray())
            {
                Position newPosition = currentRoom.Position + direction;

                // Randomly skip a direction to create a maze-like structure.
                if (seed.Next(0, 100) < 50)
                {
                    continue;
                }

                if (level.IsPositionInBounds(newPosition) && level.GetRoom(newPosition) is null)
                {
                    RoomDefinition? newRoomDefinition = await GetRandomRoomDefinition(level);

                    Room newRoom = new Room(newPosition, newRoomDefinition ?? throw new InvalidOperationException("No rooms found in the database."));

                    _ = level.AddRoom(newRoom);
                    generationQueue.Enqueue(newRoom);
                }
            }

            await Task.Delay(1);
            progress.Report(new(level.RoomCount, roomCount));

            if (level.RoomCount >= roomCount)
            {
                break;
            }

            if (generationQueue.Count == 0)
            {
                Room? nextRoom = level.GetRooms().Values.ElementAt(seed.Next(0, level.RoomCount));

                if (nextRoom is not null)
                {
                    generationQueue.Enqueue(nextRoom);
                }
            }
        }

        return level;
    }

    private async Task<RoomDefinition?> GetRandomRoomDefinition(Level level)
    {
        RoomDefinition? room = null;
        int randomRoomIndex = seed.Next();

        // This is a fallback in case the room doesn't exist in the database to keep the order of the future rooms the same.
        DeterministicRandom fallbackRandom = new DeterministicRandom(randomRoomIndex, seed.Min, seed.Max);

        // Try to find a room 100 times, if it doesn't exist, then return null.
        for (int i = 0; i < 100; i++)
        {
            room = await rooms.FirstOrDefaultAsync(x => x.Id == randomRoomIndex);

            if (room?.Tag == RoomTag.Unique && level.RoomExists(room))
            {
                room = null;
            }

            if (room is not null)
            {
                break;
            }

            randomRoomIndex = fallbackRandom.Next();
        }

        return room;
    }
}