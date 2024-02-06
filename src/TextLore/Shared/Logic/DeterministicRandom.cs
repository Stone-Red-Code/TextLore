using Microsoft.IdentityModel.Tokens;

using System.Diagnostics.CodeAnalysis;

namespace TextLore.Shared.Logic;

public class DeterministicRandom(int seed, int min, int max) : IParsable<DeterministicRandom>
{
    private readonly Random random = new Random(seed);
    public int Seed => seed;
    public int Min => min;
    public int Max => max;

    public DeterministicRandom(int min, int max) : this(Guid.NewGuid().GetHashCode(), min, max)
    {
    }

    public static DeterministicRandom Parse(string s, IFormatProvider? provider)
    {
        byte[] bytes = Base64UrlEncoder.DecodeBytes(s);

        int lseed = BitConverter.ToInt32(bytes.Take(4).ToArray(), 0);
        int lmin = BitConverter.ToInt32(bytes.Skip(4).Take(4).ToArray(), 0);
        int lmax = BitConverter.ToInt32(bytes.Skip(8).Take(4).ToArray(), 0);

        return new DeterministicRandom(lseed, lmin, lmax);
    }

    public static DeterministicRandom Parse(string s)
    {
        return Parse(s, null);
    }

    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [NotNullWhen(true)] out DeterministicRandom? result)
    {
        if (s is null)
        {
            result = null;
            return false;
        }

        int lseed;
        int lmin;
        int lmax;

        try
        {
            byte[] bytes = Base64UrlEncoder.DecodeBytes(s);

            lseed = BitConverter.ToInt32(bytes.Take(4).ToArray(), 0);
            lmin = BitConverter.ToInt32(bytes.Skip(4).Take(4).ToArray(), 0);
            lmax = BitConverter.ToInt32(bytes.Skip(8).Take(4).ToArray(), 0);
        }
        catch (Exception)
        {
            result = null;
            return false;
        }

        if (lmin > lmax)
        {
            result = null;
            return false;
        }

        result = new DeterministicRandom(lseed, lmin, lmax);
        return true;
    }

    public static bool TryParse([NotNullWhen(true)] string? s, [NotNullWhen(true)] out DeterministicRandom? result)
    {
        return TryParse(s, null, out result);
    }

    public int Next()
    {
        return random.Next(min, max);
    }

    public int Next(int min, int max)
    {
        return random.Next(min, max);
    }

    public string ToBase64()
    {
        byte[] bytes = new byte[12];

        Array.Copy(BitConverter.GetBytes(seed), 0, bytes, 0, 4);
        Array.Copy(BitConverter.GetBytes(min), 0, bytes, 4, 4);
        Array.Copy(BitConverter.GetBytes(max), 0, bytes, 8, 4);

        return Base64UrlEncoder.Encode(bytes);
    }
}