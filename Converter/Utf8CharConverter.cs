using System;
using System.Collections.Generic;

namespace EncodingProgram
{
    class Utf8Converter : IConverter
    {
        public static readonly Byte[] Bom = new Byte[] { 0xef, 0xbb, 0xbf };
        public static readonly EEncodingMode Mode = EEncodingMode.Utf8;

        public List<Char> ConvertToChars(byte[] arr)
        {
            List<Char> res = new List<char>(arr.Length / 2);
            for (Int32 i = 0; i < arr.Length; ++i)
            {
                //0<=arr[i]<128 (2^7)
                if ((arr[i] & 0x80) == 0)//0b10000000 или 1 << 7
                {
                    res.Add((Char)arr[i]);
                }
                else if ((arr[i] & 0xE0) == 0xC0)//128<=arr[i]<2048(2^11)
                {
                    //ставим в конец 2xбайтового char значимые биты 1го байта, сдвигаем влево на 6, записываем значимые биты 2го байта
                    res.Add((char)(((arr[i] & 0x1F) << 6) | (arr[i + 1] & 0x3F)));
                    ////0b00011111 или 0x1F (для 5 бит 1го) //0b00111111 или 0x3F (для 6 бит 2го)
                    ++i;
                }
                else { throw new NotSupportedException(); }

            }
            return res;
        }

        public Byte[] ConvertFromChars(List<Char> list)
        {
            List<Byte> res = new List<Byte>(list.Count);
            for (Int32 i = 0; i < list.Count; ++i)
            {
                if (list[i] < 128)
                {
                    res.Add((Byte)list[i]);
                }
                else if (list[i] < 2048)
                {
                    res.Add((Byte)(list[i] >> 6 | 0xC0));//0b11000000 или 3<<6 //первые 5 бит
                    res.Add((Byte)(list[i] & 0x3F | 0x80));//0b00111111 //последние 6 бит
                                                           //0b10000000
                }
                else { throw new NotSupportedException(); }
            }
            return res.ToArray();
        }

        public byte[] GetBom()
        {
            return Bom;
        }

        public EEncodingMode GetMode()
        {
            return Mode;
        }

    }
}
