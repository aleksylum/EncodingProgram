using System;

namespace EncodingProgram
{
    class PathParser
    {
        private EProgramMode _mode;

        public PathParser(EProgramMode mode)
        {
            _mode = mode;
        }

        public String Parse(String inputPath)
        {
            try
            {
                IsNullOrEmptyPath(inputPath);
                String resultPath = PathFixer.UnifyPath(PathFixer.DeleteWhiteSpaces(inputPath));
                Validate(resultPath);
                return resultPath;
            }
            catch (Exception ex)
            {
                ExceptionHandler.HandleException(ex);
                return null;
            }
        }

        private void IsNullOrEmptyPath(String path)
        {
            if (String.IsNullOrEmpty(path))
            {
                throw new ArgumentException("Empty file name.");
            }
        }

        private void Validate(String path)
        {
            if (_mode == EProgramMode.Decoder)
            {
                Validator.CheckFile(path);
            }
            else
            {
                Validator.CheckDirectory(GetPathWithoutFileName(path));
                Validator.CheckDestinationFileAvailable(path);
            }
        }


        private String GetPathWithoutFileName(String path)
        {
            Int32 lastBackSlash = path.LastIndexOf('\\');

            if (lastBackSlash != -1)
            {
                return path.Substring(0, lastBackSlash);
            }
            throw new ArgumentException($"Destination path {path} is invalid.");
        }
    }
}
