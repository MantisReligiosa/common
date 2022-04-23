using System;

namespace SmartTechnologiesM.Base.Extensions
{
    public static class ByteExtensions
    {
        [Obsolete("Использует обратный порядок байтов. Лучше использовать Bits()[]")]
        public static bool GetBit(this byte b, byte position)
        {
            if (position > 7)
                throw new ArgumentException("Значение должно быть в пределах от 0 до 7",
                    nameof(position));
            return (b & (1 << (7 - position))) != 0;
        }

        /// <summary>
        /// Извлечение значений битов в байте. Нулевой бит самый младший
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool[] Bits(this byte b)
        {
            return new bool[]
            {
                (b & 0b00000001) > 0,
                (b & 0b00000010) > 0,
                (b & 0b00000100) > 0,
                (b & 0b00001000) > 0,
                (b & 0b00010000) > 0,
                (b & 0b00100000) > 0,
                (b & 0b01000000) > 0,
                (b & 0b10000000) > 0,
            };
        }
    }
}
