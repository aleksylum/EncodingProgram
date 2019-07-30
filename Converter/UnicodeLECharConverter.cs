using System;
using System.Collections.Generic;

namespace EncodingProgram
{
    class UnicodeLECharConverter : IConverter
    {
        public static readonly Byte[] Bom = new Byte[] { 0xff, 0xfe };
        public static readonly EEncodingMode Mode = EEncodingMode.UnicodeLittleEndian;


        public List<Char> ConvertToChars(Byte[] arr)
        {
            List<Char> res = new List<Char>(arr.Length / 2);

            for (Int32 i = 0; i < arr.Length; i += 2)
            {
                res.Add((Char)((arr[i + 1] << 8) | arr[i]));
            }
            return res;
        }

        public Byte[] ConvertFromChars(List<Char> list)
        {
            Byte[] res = new Byte[checked(list.Count * 2)];
            for (Int32 i = 0; i < list.Count; ++i)
            {
                res[i*2] = (Byte)(list[i]);
                res[i*2 + 1] =  (Byte)(list[i] >> 8);
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
