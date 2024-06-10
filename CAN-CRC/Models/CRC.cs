namespace CAN_CRC.Models;

// ReSharper disable InconsistentNaming
internal class CRC
{
    internal int Calculate(int[] data)
    {
        var crc = 0;
        foreach (var current in data)
        {
            crc ^= current << 7;
            for (var i = 0; i < 8; i++)
            {
                crc = (crc << 1) ^ ((crc & 0x8000) != 0 ? 0x4599 : 0);
            }
            crc &= 0x7FFF;
        }
        return crc;
    }

}