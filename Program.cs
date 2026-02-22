using Spectre.Console;

// Declaring path and finding user
string? user = Environment.UserName;
string? path = $"/home/{user}/fileApp/";

// Global Variables needed by a lot
bool exit = false;
string fullPath = "";
int counter = 1;
List<string> files = [];
var message = new Text("Bottom Text");



// Start of the program;
Console.Clear();

// Checking if directory exists, if not creates one and lets user know

if (!Directory.Exists(path))
{
    Directory.CreateDirectory(path);
    Easy.Print($"Directory created at {path}");
    Console.ReadLine();
}

// While loop for program
while (!exit)
{
    // Asks for user input
    Easy.Clear(files);
    Easy.Print("Please select an option: 1-5.\n1. List all Files\n2. Add new file\n3. Select file (options)\n4. Exit");
    string? choice = Console.ReadLine();

    // After user selects menu option
    if ((choice != null) && int.TryParse(choice, out int intParse) && intParse >= 1 && intParse <= 4)
    {
        
        // Switch case handeling user selection
        switch(intParse)
        {
            // Menu for option 1; list all files
            case 1:
            Console.Clear();
            files = [];
            Easy.Print("Listing all files...");
            SearchFiles.Search(files, path);
            Console.ReadLine();
            break;

            // Menu option for 2; add new file
            case 2:
            Console.Clear();
            files = [];
            SearchFiles.Search(files, path);
            Easy.Print("Please enter filename...");
            fullPath = path + Console.ReadLine();

            if (File.Exists(fullPath))
                {
                    Easy.Warn($"File already exists at {fullPath}, continue? Y/N");
                    choice = Console.ReadLine().ToLower().Trim();

                    switch(choice)
                    {
                        case "y":
                            Console.Clear();
                            File.Delete(fullPath);
                            using (File.Create(fullPath)) {};
                            Easy.Success($"File saved at {fullPath}");
                            Console.ReadLine();
                        break;
                        
                        case "n":
                            Console.Clear();
                            Easy.Warn("Cancelling...");
                            Console.ReadLine();
                        break;
                    }
                }
                else
                {
                    Console.Clear();
                    using (File.Create(fullPath)) {};
                    Easy.Success($"File Created at {fullPath}");
                    Console.ReadLine();
                }
            break;

            // Menu option for 3; file options
            case 3:
            Console.Clear();
            files = [];
            SearchFiles.Search(files, path);
            Easy.Print("Select a file to edit:");
            choice = Console.ReadLine();

            if (int.TryParse(choice, out intParse) && (intParse > 0 && intParse <= files.Count) && (choice != "" && choice != null))
                {
                    fullPath = path + files[intParse-1];
                    Console.Clear();
                    Easy.Print($"You selected file: {files[intParse-1]}\n\nPlease select an option:\n1. Copy file\n2. Append text to file\n3. Read text of file\n4. Delete File\n5. Rename file");
                    choice = Console.ReadLine();

                    if (int.TryParse(choice, out intParse) && (intParse >= 1 || intParse <= 4))
                    {
                        switch (intParse)
                            {
                                // Case for option one in settings menu, copy file to given directory
                                case 1:
                                    Easy.Copy(fullPath);
                                break;

                                // Case for option 2 in settings menu, adding text
                                case 2:
                                    Easy.AddText(fullPath);
                                break;

                                // Case for option 3 of settings menu, read text from file
                                case 3:
                                    Easy.ReadText(fullPath);
                                break;

                                // Case for option 4 of settings menu, delete file
                                case 4:
                                    Easy.Delete(fullPath);
                                break;

                                // Case for option 5 of settings menu, rename a file
                                case 5:
                                Easy.Rename(path, fullPath);
                                break;
                            }
                    }
                }
            break;

            case 4:
            Console.Clear();
            Easy.Error("Exiting...");
            exit = true;
            break;
        }
    }
}