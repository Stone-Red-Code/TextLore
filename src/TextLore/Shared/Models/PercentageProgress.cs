namespace TextLore.Shared.Models;

public readonly struct PercentageProgress(int current, int total)
{
    public int Current => current;
    public int Total => total;
    public int Percentage => (int)((double)Current / Total * 100);
    public bool IsComplete => Current >= Total;
}