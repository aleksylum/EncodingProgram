using System;
using System.Collections.Generic;
using System.Linq;

namespace EncodingProgram
{
    class AsciiCharConverter : IConverter
    {
        public static readonly Byte[] Bom = new Byte[0];
        public static readonly EEncodingMode Mode = EEncodingMode.Ascii;

        Dictionary<Byte, Char> _ascii1251ToChar;
        Dictionary<Char, Byte> _charToAscii1251;

        public AsciiCharConverter()
        {
            _ascii1251ToChar = new Dictionary<byte, char>() {
                {128, '\u0402'},
                {129, '\u0403'},
                {130, '\u201A'},
                {131, '\u0453'},
                {132, '\u201E'},
                {133, '\u2026'},
                {134, '\u2020'},
                {135, '\u2021'},
                {136, '\u20AC'},
                {137, '\u2030'},
                {138, '\u0409'},
                {139, '\u2039'},
                {140, '\u040A'},
                {141, '\u040C'},
                {142, '\u040B'},
                {143, '\u040F'},
                {144, '\u0452'},
                {145, '\u2018'},
                {146, '\u2019'},
                {147, '\u201C'},
                {148, '\u201D'},
                {149, '\u2022'},
                {150, '\u2013'},
                {151, '\u2014'},
                //{152, },
                {153, '\u2122'},
                {154, '\u0459'},
                {155, '\u203A'},
                {156, '\u045A'},
                {157, '\u045C'},
                {158, '\u045B'},
                {159, '\u045F'},
                {160, '\u00A0'},
                {161, '\u040E'},
                {162, '\u045E'},
                {163, '\u0408'},
                {164, '\u00A4'},
                {165, '\u0490'},
                {166, '\u00A6'},
                {167, '\u00A7'},
                {168, '\u0401'},
                {169, '\u00A9'},
                {170, '\u0404'},
                {171, '\u00AB'},
                {172, '\u00AC'},
                {173, '\u00AD'},
                {174, '\u00AE'},
                {175, '\u0407'},
                {176, '\u00B0'},
                {177, '\u00B1'},
                {178, '\u0406'},
                {179, '\u0456'},
                {180, '\u0491'},
                {181, '\u00B5'},
                {182, '\u00B6'},
                {183, '\u00B7'},
                {184, '\u0451'},
                {185, '\u2116'},
                {186, '\u0454'},
                {187, '\u00BB'},
                {188, '\u0458'},
                {189, '\u0405'},
                {190, '\u0455'},
                {191, '\u0457'},
                {192, '\u0410'},
                {193, '\u0411'},
                {194, '\u0412'},
                {195, '\u0413'},
                {196, '\u0414'},
                {197, '\u0415'},
                {198, '\u0416'},
                {199, '\u0417'},
                {200, '\u0418'},
                {201, '\u0419'},
                {202, '\u041A'},
                {203, '\u041B'},
                {204, '\u041C'},
                {205, '\u041D'},
                {206, '\u041E'},
                {207, '\u041F'},
                {208, '\u0420'},
                {209, '\u0421'},
                {210, '\u0422'},
                {211, '\u0423'},
                {212, '\u0424'},
                {213, '\u0425'},
                {214, '\u0426'},
                {215, '\u0427'},
                {216, '\u0428'},
                {217, '\u0429'},
                {218, '\u042A'},
                {219, '\u042B'},
                {220, '\u042C'},
                {221, '\u042D'},
                {222, '\u042E'},
                {223, '\u042F'},
                {224, '\u0430'},
                {225, '\u0431'},
                {226, '\u0432'},
                {227, '\u0433'},
                {228, '\u0434'},
                {229, '\u0435'},
                {230, '\u0436'},
                {231, '\u0437'},
                {232, '\u0438'},
                {233, '\u0439'},
                {234, '\u043A'},
                {235, '\u043B'},
                {236, '\u043C'},
                {237, '\u043D'},
                {238, '\u043E'},
                {239, '\u043F'},
                {240, '\u0440'},
                {241, '\u0441'},
                {242, '\u0442'},
                {243, '\u0443'},
                {244, '\u0444'},
                {245, '\u0445'},
                {246, '\u0446'},
                {247, '\u0447'},
                {248, '\u0448'},
                {249, '\u0449'},
                {250, '\u044A'},
                {251, '\u044B'},
                {252, '\u044C'},
                {253, '\u044D'},
                {254, '\u044E'},
                {255, '\u044F'},
            };
            _charToAscii1251 = _ascii1251ToChar.ToDictionary(kv => kv.Value, kv => kv.Key);
        }

        public List<char> ConvertToChars(Byte[] arr)
        {

            List<Char> res = new List<char>(arr.Length);

            foreach (var v in arr)
            {
                if ((v & 0x80) == 0)
                {
                    res.Add((Char)v);
                }
                else
                {
                    res.Add(_ascii1251ToChar[v]);
                }
            }
            return res;
        }

        public Byte[] ConvertFromChars(List<char> list)
        {
            Byte[] res = new Byte[list.Count];

            for (Int32 i = 0; i < list.Count; ++i)
            {
                if ((list[i] >> 8 & 0xff) == 0)
                {
                    res[i] = ((Byte)list[i]);
                }
                else
                {
                    res[i] = _charToAscii1251[list[i]];
                }

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
