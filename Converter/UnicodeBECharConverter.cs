using System;
using System.Collections.Generic;


namespace EncodingProgram
{
    class UnicodeBECharConverter : IConverter
    {
        public static readonly Byte[] Bom = new Byte[] { 0xfe, 0xff };
        public static readonly EEncodingMode Mode = EEncodingMode.UnicodeBigEndian;


        public List<char> ConvertToChars(byte[] arr)
        {
            List<Char> res = new List<char>(arr.Length / 2);

            for (Int32 i = 0; i < arr.Length; i += 2)
            {
                res.Add((char)((arr[i] << 8) | arr[i + 1]));

            }
            return res;
        }

        public byte[] ConvertFromChars(List<char> list)
        {
            Byte[] res = new Byte[checked(list.Count * 2)];
            for (Int32 i = 0; i < list.Count; ++i)
            {
                res[i * 2] = (Byte)(list[i] >> 8);
                res[i * 2 + 1] = (Byte)(list[i]);
            }
            return res;
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
