namespace CAN_CRC.Models;

// ReSharper disable InconsistentNaming
internal class CRC
{
    public int Calculate(int[] data)
    {
        short crc = 0;
        foreach (var current in data)
        {
            crc ^= (short)(current << 7);
            for (var i = 0; i < 8; i++)
            {
                crc <<= 1;
                if ((crc & 0x8000) != 0)
                {
                    crc ^= 0x4599;
                }
            }
            crc &= 0x7FFF;
        }
        return crc;
    }

}