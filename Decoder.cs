using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace EncodingProgram
{
    class Decoder
    {
        private String _path;
        private IConverter _converter;

        private Decoder(String path, IConverter converter)
        {
            _path = path;
            _converter = converter;
        }

        public static Decoder CreateDecoder(String path, out EEncodingMode mode)
        {
            IConverter converter = GetConverter(path);
            mode = converter.GetMode();
            return new Decoder(path, converter);
        }

        public List<Char> DecodeDataFromFile()
        {
            using (var fileStream = new FileStream(_path, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                Byte[] arr = ReadBytesFromStream(fileStream);
                return _converter.ConvertToChars(arr);
            }
        }

        public Byte[] ReadBytesFromStream(FileStream fileStream)
        {
            Int32 indent = _converter.GetBom().Length;
            fileStream.Position = indent;
            Int32 arrLength = checked((Int32)fileStream.Length - indent);
            Byte[] arr = new byte[arrLength];
            fileStream.Read(arr, 0, arrLength);
            return arr;
        }

        private static IConverter GetConverter(String path)
        {
            Byte[] byteOrderMark;
            using (var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                if (fileStream.Length > 2)
                {
                    byteOrderMark = new Byte[3];
                    fileStream.Read(byteOrderMark, 0, 3);
                    if (Enumerable.SequenceEqual(byteOrderMark, Utf8Converter.Bom)) return new Utf8Converter();
                }
                if (fileStream.Length > 1)
                {
                    fileStream.Position = 0;
                    byteOrderMark = new Byte[2];
                    fileStream.Read(byteOrderMark, 0, 2);
                    if (Enumerable.SequenceEqual(byteOrderMark, UnicodeLECharConverter.Bom)) return new UnicodeLECharConverter();
                    if (Enumerable.SequenceEqual(byteOrderMark, UnicodeBECharConverter.Bom)) return new UnicodeBECharConverter();
                }
                return new AsciiCharConverter();
            }
        }
    }
}
