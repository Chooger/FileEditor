using Spectre.Console;
public static class SearchFiles
{
    
    public static void Search(List<string> fileSearch, string path)
    {
        var message = new Text("");
        var defaultText = new Style(Color.FromHex("#9467DB"));
        int counter = 1;
        string? user = Environment.UserName;

        //Creating a list of files in the directory
        foreach (string file in Directory.EnumerateFiles(path)) 
            {
                fileSearch.Add(Path.GetFileName(file));
            }

        fileSearch = [.. fileSearch.OrderBy(File.GetCreationTime).Select(f => Path.GetFileName(f))];

        for (int i = 0; i < fileSearch.Count; i++)
        {
            message = new Text($"{counter}: {fileSearch[i]}\n", defaultText);
            AnsiConsole.Write(message);
            counter++;
        } 
        counter = 1;
        return;
    }



    public static bool IsParsed(string input)
    {    
        return int.TryParse(input, out int inter);
    }
}