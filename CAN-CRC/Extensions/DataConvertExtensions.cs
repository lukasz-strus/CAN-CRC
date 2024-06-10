namespace CAN_CRC.Extensions;

internal static class DataConvertExtensions
{
    public static int[] ToByteArray(this string input)
    {
        var byteList = new List<int>();
        for (var i = input.Length - 1; i >= 0; i -= 8)
        {
            var byteString = "";
            for (var j = 0; (i - j) >= 0 && j < 8; j++)
            {
                byteString = input[i - j] + byteString;
            }
            if (!string.IsNullOrEmpty(byteString))
            {
                byteList.Add(Convert.ToInt32(byteString, 2));
            }
        }
        byteList.Reverse();

        return byteList.ToArray();
    }

    public static string ToHex(this int byteValue) => 
        byteValue.ToString("X2");
}