using HashidsNet;

namespace KeyvanSafe.Shared.Assistant.Helpers;

public static class HashId
{
    private const string HashSalt =
        "illegible-0o0o1faL2sha;wodvasfjag812fFkGh123aFR:ALFSDJsdf4#vgwsrKfjHdHbKhgpls601";

    private const int MinHashLength = 12;
    private const string HashAlphabets = "abcdefghklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ123456789";

    private static readonly Hashids Hasher = new(HashSalt, MinHashLength, HashAlphabets);

    public static string EncodeInt(this int id)
    {
        return Hasher.Encode(id);
    }

    public static string EncodeLong(this long id)
    {
        return Hasher.EncodeLong(id);
    }

    public static int DecodeInt(this string eid)
    {
        try
        {
            return Hasher.Decode(eid)[0];
        }
        catch
        {
            return -1;
        }
    }

    public static long DecodeLong(this string eid)
    {
        try
        {
            return Hasher.Decode(eid)[0];
        }
        catch
        {
            return -1;
        }
    }
}
