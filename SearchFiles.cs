
public static class SearchFiles
{
    
    public static void Search(List<string> fileSearch, string path)
    {
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
            Console.WriteLine($"{counter}: {fileSearch[i]}");
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