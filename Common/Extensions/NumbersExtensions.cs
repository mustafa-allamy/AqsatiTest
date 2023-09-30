namespace Common.Extensions
{
    public static class NumbersExtensions
    {
        public static double TowDecimalPoints(this double num)
        {
            return Math.Round(num, 2, MidpointRounding.ToZero);
        }
    }
}