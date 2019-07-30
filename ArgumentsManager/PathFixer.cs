using System;

namespace EncodingProgram
{
    static class PathFixer
    {
        public static String DeleteWhiteSpaces(String path)
        {
            return path.Trim();
        }

        public static String UnifyPath(String path)
        {
            return path.Replace('/', '\\');
        }
    }
}
