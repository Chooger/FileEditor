public static class Easy
{
    public static void Clear(List<string> filePath)
    {
        Console.Clear();
        filePath = [];
    }

    public static bool Parse(string choice, int number)
    {
        bool isParsed = int.TryParse(choice, out number);
        return isParsed;
    }

    public static void Copy(string path)
    {
        FileInfo fi = new FileInfo(path);
        Console.Clear();
        Console.WriteLine("Please enter the path and filename you want to copy the file to. (ex: ~/destination/file.txt)");
        string newPath = Console.ReadLine();
        FileInfo fi2 = new FileInfo(newPath);

        try
            {
                using (FileStream fs = fi.Create()) {}

                if (File.Exists(newPath))
                {
                    fi2.Delete();
                }

                fi.CopyTo(newPath);

                Console.WriteLine($"File copied to {newPath}");
                Console.ReadLine();
            } catch (IOException ioex)
            {
                Console.WriteLine(ioex.Message);
            }
    }

    public static void AddText(string path)
    {
         if (!File.Exists(path))
            {
                Console.Clear();
                Console.WriteLine("Invalid Path");
                Console.ReadLine();
            }

            Console.Clear();
            Console.WriteLine("Please enter the text you want to append to file");
            string addText = Console.ReadLine();

            //using Stream Writer to append text gotten from user in last output, then promps for full contents
            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine(addText);
                Console.Clear();
                Console.WriteLine("Text added, full contents:\n\n");

            }

            //after the last using block, text actually gets saved so we can go to the next using Stream Read to open file, iterate through characters and listing each one
            using (StreamReader sr = File.OpenText(path))
            {
                string s = "";

                while ((s = sr.ReadLine()) != null)
                {
                    Console.WriteLine(s);
                }
            }
            Console.ReadLine();
    }

    public static void ReadText(string path)
    {
        Console.Clear();
        Console.WriteLine($"Reading {path}...\n\n");

        using (StreamReader sr = File.OpenText(path))
        {
            string s = "";

            while ((s = sr.ReadLine()) != null)
            {
                Console.WriteLine(s);
            }
            
        }
        Console.WriteLine("\n");
        Console.ReadLine();
    }
}