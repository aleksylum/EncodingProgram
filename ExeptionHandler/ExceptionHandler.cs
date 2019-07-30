using System;

namespace EncodingProgram
{
    static class ExceptionHandler
    {
        public static void HandleException(Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }
}
