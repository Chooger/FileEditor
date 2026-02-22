using Spectre.Console;

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
        Easy.Print("Please enter the path and filename you want to copy the file to. (ex: ~/destination/file.txt)");
        string newPath = Console.ReadLine();
        FileInfo fi2 = new FileInfo(newPath);

        try
            {
                using (FileStream fs = fi2.Create()) {}

                if (File.Exists(newPath))
                {
                    fi2.Delete();
                }

                fi.CopyTo(newPath);

                Easy.Print($"File copied to {newPath}");
                Console.ReadLine();
            } catch (IOException ioex)
            {
                Easy.Print(ioex.Message);
            }
    }

    public static void AddText(string path)
    {
         if (!File.Exists(path))
            {
                Console.Clear();
                Easy.Error("Invalid Path");
                Console.ReadLine();
                return;
            }

            Console.Clear();
            Easy.Print("Please enter the text you want to append to file");
            string? addText = Console.ReadLine();

            if (addText == null || addText == "") {return;}
            
            using (FileStream writeStream = new FileStream (
                path,
                FileMode.Append,
                FileAccess.Write,
                FileShare.None))

            using (StreamWriter writer = new StreamWriter(writeStream))
        {
            writer.WriteLine(addText);
            writer.Flush();
        }

        Console.Clear();
        Easy.Print("Text added, full contents:");

        using (FileStream readStream = new FileStream(
            path,
            FileMode.Open,
            FileAccess.Read,
            FileShare.None))
        using (StreamReader reader = new StreamReader(readStream))
        {
            string? line;
            while ((line = reader.ReadLine()) != null)
            {
                Easy.Print(line);
            }
        }

        Console.ReadLine();
    }

    public static void ReadText(string path)
    {
        if (new FileInfo(path).Length == 0)
        {Easy.Error("File empty, nothing to read"); Console.ReadLine();}

        else{
        Console.Clear();
        Easy.Print($"Reading {path}...\n");

        using (StreamReader sr = File.OpenText(path))
        {
            string s = "";

            while ((s = sr.ReadLine()) != null)
            {
                Easy.Print(s);
            }
            
        }
        Console.WriteLine("\n");
        Console.ReadLine();
        }
    }

    public static void Delete(string path)
    {
        Console.Clear();
        File.Delete(path);
        Easy.Warn($"File Deleted at {path}");
        Console.ReadLine();
    }

    public static void Rename (string path, string fullPath)
    {
        Console.Clear();
        Easy.Print("Please enter new name for file:");
        string input = Console.ReadLine();
        if (input == null || input == "") {return;}
        string fullPath2 = path + input;
        if (File.Exists(fullPath2))
        {
            Easy.Print("File exists, continue? Y/N");
            string result = Console.ReadLine().ToLower();

            if (result == "y")
            {
                File.Copy(fullPath, fullPath2);
                File.Delete(fullPath);
                Easy.Success("File renamed...");
                Console.ReadLine();
            } else {Easy.Error("Something went wrong or you selected no. Cancelling..."); Console.ReadLine();};
        }
        else
        {
            File.Copy(fullPath, fullPath2);
            File.Delete(fullPath);
            Easy.Success("File renamed...");
            Console.ReadLine();
        }
    }

    public static void Print(string input)
    {
        var defaultText = new Style(Color.FromHex("#9467DB"));
        var message = new Text($"{input}\n", defaultText);
        AnsiConsole.Write(message);
    }

    public static void Warn(string input)
    {
        var warn = new Style(Color.FromHex("#F79E23"), decoration: Decoration.Bold);
        var message = new Text($"{input}\n", warn);
        AnsiConsole.Write(message);
    }

    public static void Success(string input)
    {
        var success = new Style(Color.FromHex("#3AF035"), decoration: Decoration.Bold);
        var message = new Text($"{input}\n", success);
        AnsiConsole.Write(message);
    }

    public static void Error(string input)
    {
        var error = new Style(Color.FromHex("#ED1818"), decoration: Decoration.Bold);
        var message = new Text($"{input}\n", error);
        AnsiConsole.Write(message);
    }
}