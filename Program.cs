using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace enumerateyolo
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Count() == 0)
            {
                Console.WriteLine("You have to pass an argument with the path to the data.");
                return;
            }
            
            var path = args[0];
            if (!Directory.Exists(path))
            {
                Console.WriteLine("Write a valid folder path to your data.");
            }
            List<String> validfiles = new List<String>();
            // Getting all the files here
            var files = Directory.GetFiles(path);
            foreach(var file in files)
            {
                // Finding jpg file in data
                var filename = file.Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
                if (filename[1] == "jpg")
                {
                    if (File.Exists(filename[0] + ".xml"))
                    {
                        validfiles.Add(file);
                    }
                }
                // Then check if there is an xml file with the same nameB which is related to the given image
            }
            String itemspath;
            try
            {


                itemspath = args[1];


            }
            catch (IndexOutOfRangeException)
            {
                itemspath = Directory.GetCurrentDirectory() + "\\" + "1.txt";
                Console.WriteLine("The output file is going to be stored in this path " + itemspath);
            }
            try
            {
                using (FileStream fs = new FileStream(itemspath, FileMode.OpenOrCreate))
                {
                    using (TextWriter tw = new StreamWriter(fs))
                    {
                        foreach(var v in validfiles)
                        {
                            tw.WriteLine(PathToLinux(v));
                        }
                        
                    }
                }
            }
            catch(FileNotFoundException fnfe)
            {
                Console.WriteLine(fnfe.ToString());
            }
        }
        /// <summary>
        /// Makes linux path out of windows path
        /// </summary>
        /// <param name="windowspath">Windows path you want to change to the linux one.</param>
        /// <returns></returns>
        static String PathToLinux(String windowspath)
        {
            var split = windowspath.Split(new[] { '\\' }, StringSplitOptions.RemoveEmptyEntries);
            StringBuilder builder = new StringBuilder();
            foreach(var v in split)
            {
                //v.Replace(":", String.Empty);
                builder.Append(@"/");
                builder.Append(v);
            }
            var result = builder.ToString();
            result = result.Replace(":", String.Empty);
            return result;
        }
    }
}
