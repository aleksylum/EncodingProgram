using System;
using System.Collections.Generic;


namespace EncodingProgram
{
    interface IConverter
    {
        List<Char> ConvertToChars(Byte[] arr);
        Byte[] ConvertFromChars(List<Char> list);
        Byte[] GetBom();
        EEncodingMode GetMode();
    }
}
