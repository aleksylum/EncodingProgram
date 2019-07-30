using System;
using System.Collections.Generic;

namespace EncodingProgram
{
    class EncodingProgram
    {
        public List<Char> readChars { get; private set; }

        public EncodingProgram()
        {
            readChars = new List<Char>();
        }

        public void Start()
        {
            Boolean toContinue = true;
            while (toContinue)
            {
                try
                {
                    toContinue = SelectProgramMode();
                }
                catch (Exception ex)
                {
                    ExceptionHandler.HandleException(ex);
                }
            }
        }

        private Boolean SelectProgramMode()
        {
            switch (UserDialog.TakeProgramModeFromUser())
            {
                case EProgramMode.Decoder:
                    UseDecodeMode();
                    return true;
                case EProgramMode.Encoder:
                    UseEncodeMode();
                    return true;
                default: return false;
            }
        }

        private void UseDecodeMode()
        {
            String path = TakePathFromUser(EProgramMode.Decoder);
            if (!String.IsNullOrEmpty(path))
            {
                Decoder decoder = Decoder.CreateDecoder(path, out EEncodingMode encodingMode);
                readChars = decoder.DecodeDataFromFile();
                UserDialog.ReportAboutSuccessReading(path, encodingMode);
                UserDialog.PrintResult(readChars);
            }
        }

        private void UseEncodeMode()
        {
            if (!CheckInitialData()) { return; }
            String path = TakePathFromUser(EProgramMode.Encoder);
            if (!String.IsNullOrEmpty(path))
            {
                EEncodingMode encodingMode = UserDialog.TakeEncodingModeFromUser();
                Encoder encoder = Encoder.CreateEncoder(path, encodingMode);
                encoder.WriteDataToFile(readChars);
                UserDialog.ReportAboutSuccessWriting(path, encodingMode);
            }
        }

        private String TakePathFromUser(EProgramMode programMode)
        {
            PathParser pathParser = new PathParser(programMode);
            String res;
            do
            {
                res = pathParser.Parse(UserDialog.TakePathFromUser());

            } while (res == null && UserDialog.AskUserAboutTryingAgain());
            return res;
        }

        private Boolean CheckInitialData()
        {
            if (readChars.Count == 0)
            {
                return UserDialog.ReportAboutEmptyInitialData();
            }
            return true;
        }

    }

}