using System;
using System.Collections.Generic;
using System.IO;

namespace EncodingProgram
{
    class Encoder
    {
        private String _path;
        private IConverter _converter;

        private Encoder(String path, IConverter converter)
        {
            _path = path;
            _converter = converter;
        }

        public static Encoder CreateEncoder(String path, EEncodingMode mode)
        {
            IConverter decoder = GetConverter(mode);
            return new Encoder(path, decoder);
        }

        public void WriteDataToFile(List<Char> data)
        {
            using (var fileStream = new FileStream(_path, FileMode.Create, FileAccess.Write, FileShare.Read))
            {
                WriteBom(fileStream);
                WriteEncodingBytes(fileStream, data);
            }
        }
        private void WriteBom(FileStream fileStream)
        {
            Byte[] bom = _converter.GetBom();
            fileStream.Write(bom, 0, bom.Length);
        }
        private void WriteEncodingBytes(FileStream fileStream, List<Char> data)
        {
            Byte[] resBytes = _converter.ConvertFromChars(data);
            fileStream.Write(resBytes, 0, resBytes.Length);
        }
        private static IConverter GetConverter(EEncodingMode mode)
        {
            switch (mode)
            {
                case EEncodingMode.Ascii: return new AsciiCharConverter();
                case EEncodingMode.UnicodeBigEndian: return new UnicodeBECharConverter();
                case EEncodingMode.UnicodeLittleEndian: return new UnicodeLECharConverter();        
                case EEncodingMode.Utf8: return new Utf8Converter();
                default: throw new NotSupportedException();
            }
        }
    }
}
