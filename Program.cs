using System;

namespace EncodingProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                EncodingProgram encodingProgram = new EncodingProgram();
                encodingProgram.Start();
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
            }
        }    

    }
}
