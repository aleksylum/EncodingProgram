using System;
using System.Collections.Generic;

namespace EncodingProgram
{
    static class UserDialog
    {

        public static EEncodingMode TakeEncodingModeFromUser()
        {
            Console.WriteLine(@"
Press 1 to encode to ASCII.
Press 2 to encode to Unicode Big Endian.
Press 3 to encode to Unicode Little Endian.
Press any key to encode default (UTF8).
 
");
            switch (Console.ReadKey(true).KeyChar)
            {
                case '1':
                    return EEncodingMode.Ascii;
                case '2':
                    return EEncodingMode.UnicodeBigEndian;
                case '3':
                    return EEncodingMode.UnicodeLittleEndian;
                default:
                    return EEncodingMode.Utf8;
            }

        }

        public static EProgramMode TakeProgramModeFromUser()
        {
            Console.WriteLine(
@"Press 1 to decode from file.
Press 2 to encode to file.
Press any key to exit.
");
            switch (Console.ReadKey(true).KeyChar)
            {
                case '1':
                    return EProgramMode.Decoder;
                case '2':
                    return EProgramMode.Encoder;

                default:
                    return 0;
            }
        }


        public static String TakePathFromUser()
        {
            Console.WriteLine(@"Input file path.");
            return Console.ReadLine();
            //return $@"{Console.ReadLine()}";
        }


        public static void ReportAboutSuccessReading(String path, EEncodingMode mode)
        {
            Console.WriteLine($"Success. File {path} was read from {mode.ToString()}.\n");
        }

        public static void ReportAboutSuccessWriting(String path, EEncodingMode mode)
        {
            Console.WriteLine($"Success. File {path} was written in {mode.ToString()}.\n");
        }

        public static void PrintResult(List<Char> res)
        {
            foreach (var ch in res)
            {
                Console.Write(ch);
            }
            Console.WriteLine();
        }

        public static Boolean AskUserAboutFileReplacement(String path)
        {
            Console.WriteLine($"\nFile {path} is already exist. Do you want replace it? Y/N");

            if (CheckUserAnswer())
            {
                Console.WriteLine("\nFile will be overwritten.");
                return true;
            }
            Console.WriteLine("\nFile will not be overwritten.");
            return false;
        }


        public static Boolean AskUserAboutTryingAgain()
        {
            Console.WriteLine("Press Y to try again or any key to return to main menu.");
            return CheckUserAnswer();
        }

        public static Boolean ReportAboutEmptyInitialData()
        {
            Console.WriteLine("Your initial data is empty. You really want to create empty file? Y/N");// Please, decode data from file first.
            return CheckUserAnswer();
        }


        private static Boolean CheckUserAnswer()
        {
            Char answer = Console.ReadKey(true).KeyChar;

            if (answer == 'y' || answer == 'Y' || answer == 'н' || answer == 'H')
            {
                return true;
            }
            return false;
        }

    }
}
