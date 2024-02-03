namespace TextLore.Utilities.Models;

public class PercentageProgress(int current, int total)
{
    public int Current { get; set; } = current;
    public int Total { get; set; } = total;
    public int Percentage => (int)((double)Current / Total * 100);
    public bool IsComplete => Current >= Total;
}