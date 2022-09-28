namespace Util
{
    public static class FileHelper
    {
        public static string GetFilenameFromPath(string path)
        {
            var idx = path.LastIndexOf('/') + 1;
            return path.Substring(idx, path.Length - idx);
        }

        public static string GetNameFromFilename(string filename)
        {
            return filename.Substring(0, filename.LastIndexOf('.'));
        }
    }
}