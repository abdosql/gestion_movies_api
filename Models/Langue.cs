namespace mastering_.NET_API.Models
{
    [Flags]
    public enum Langue
    {
        FR = 1 << 0, // 1 (2^0)
        EN = 1 << 1, // 2 (2^1)
        ES = 1 << 2, // 4 (2^2)
        AR = 1 << 3  // 8 (2^3)
    }

}
