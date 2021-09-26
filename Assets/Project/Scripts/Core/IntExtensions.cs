namespace Project.Scripts.Core
{
    public static class IntExtensions
    {
        public static string ToPaddedString(this int value, int numChars = 11, char paddingChar = '0')
        {
            return value.ToString().PadLeft(numChars, paddingChar);
        }
    }
}