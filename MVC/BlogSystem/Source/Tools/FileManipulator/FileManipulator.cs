using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace FileManipulator
{
    public class FileManipulator
    {
        static void Main()
        {
            string imagePath = @"\Source\Web\Blog.Web\Content\Images\";

            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            string pathToAssembly = Path.GetDirectoryName(path);
            var index = pathToAssembly.LastIndexOf(@"\Source\");

            string[] filePaths = Directory.GetFiles(pathToAssembly.Substring(0, index) + imagePath);

            for (int i = 0; i < filePaths.Count(); i++)
            {
                Console.WriteLine(filePaths[i]);
            }
        }
    }
}
