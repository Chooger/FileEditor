using System;
using System.IO; // For reading and writing files/directories.
using System.Collections;



// Declaring path, finding user
string? user = Environment.UserName;
string? path = $"/home/{user}/txt/";

//Global variables
bool exit = false;
string fullPath;
int counter = 1;
List<string> files = [];


Console.Clear();

// Checks if working directory exists
bool exists = Directory.Exists(path);
Console.WriteLine($"Directory, {path} exists: {exists}");

// if directory doesn't exist, creates one and lets user know.
if (!exists)
    {
        Directory.CreateDirectory(path);
        Console.WriteLine($"Directory needed to to function created at {path}, all files will be in there. Press enter to continue...");
        Console.ReadLine();
        Console.Clear();
    }


// Do While loop for menu selection
while (!exit)
{

    // Asks user for input, checks if null, then tryParses the string, and if user enters valid int 1 - 4, then goes into the options loop.
    Easy.Clear(files);
    Console.WriteLine("Please select an option: 1-5.\n1. List all Files\n2. Add new file\n3. Delete a file\n4. Select file (options)\n5. Exit");
    string? choice = Console.ReadLine();

    // Checking if null here, prints on the samme line if so
    if (choice == null) {Console.WriteLine("Please enter a value 1 - 5...\n1. List all files\n2. Add new files\n3. Delete a file\n4. Select file (options)\n5. Exit");}

    // Declares int, checks if it can be parsed, and if the number is 1 - 5
    int intParse;
    if (int.TryParse(choice, out intParse) && intParse >= 1 && intParse <=5)
    { 
        
        switch (intParse)
        {
            // Menu for 1; listing all files. Iterates through Enumerate.Files at $path, saves file name to string, prints string, repeat through iteration
            case 1:
                Console.Clear();
                files = [];
                Console.WriteLine("Listing all files...\n");
                SearchFiles.Search(files, path);
                Console.WriteLine("\nPress enter to continue...");
                Console.ReadLine();
                Console.Clear();
            break;

            // Menu option for 2; add new file.
            case 2:
                Console.Clear();
                Console.WriteLine("Please enter the name of the file: ");
                choice = Console.ReadLine();
                if (choice != "")
                {
                    fullPath = path + choice;
                    
                    if (File.Exists(fullPath))
                    {
                        Console.WriteLine($"{fullPath} already exists, continue? (Y/N)");
                        choice = Console.ReadLine(); choice = choice.Trim().ToLower();

                        if (choice == "y")
                        {
                            Console.Clear();
                            files = [];
                            SearchFiles.Search(files, path);
                            File.Create(fullPath);
                            if (File.Exists(fullPath))
                                {
                                    Console.WriteLine($"File created at:\n{fullPath}");
                                    Console.ReadLine();   
                                }
                            else if (File.Exists(fullPath) == false) {Console.WriteLine("File error, try again later");}
                        } else
                        {
                            Console.Clear();
                            Console.WriteLine("Cancelling...");
                            Console.ReadLine();
                        }
                    }
                    else if (File.Exists(fullPath) == false)
                    {
                        Console.Clear();
                        File.Create(fullPath);

                        if (File.Exists(fullPath))
                                {
                                    Console.WriteLine($"File created at:\n{fullPath}");
                                    Console.ReadLine();   
                                }
                            else if (File.Exists(fullPath) == false) {Console.WriteLine("File error, try again later");}
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Invalid filename, cancelling....");
                    Console.ReadLine();
                }
            break;

            // Menu option for 3, delete a File
            case 3:
                Console.Clear();
                files = [];
                SearchFiles.Search(files, path);
                Console.WriteLine("Please enter the number of the file you want to delete:");
                choice = Console.ReadLine();
                if (int.TryParse(choice, out intParse) && (intParse > 0 && intParse <= files.Count))
                 {
                    Console.Clear();
                    Console.WriteLine($"You selected to delete {intParse}: {files[intParse-1]}. Continue? Y/N");
                    string? input = Console.ReadLine(); input = input.Trim().ToLower();

                    if (input == "y")
                        {
                            fullPath = path + files[intParse-1];
                            Console.WriteLine($"{files[intParse-1]} has been deleted");
                            File.Delete(fullPath);
                            Console.ReadLine();
                        }
                    else if (input == "n"){Console.WriteLine("Cancelling.");}

                    else if (intParse <= 0 || intParse > files.Count)
                        {
                            Console.Clear(); Console.WriteLine("Enter a valid number next time...");Console.ReadLine();
                        }

                }
                break;

            // Menu option 4, select a file
            case 4:
                Console.Clear();
                files = [];
                SearchFiles.Search(files, path);
                Console.WriteLine("Select a file to edit:");
                choice = Console.ReadLine();

                    if (int.TryParse(choice, out intParse) && (intParse > 0 && intParse <= files.Count) && (choice != "" || choice != null))
                    {
                       
                        fullPath = path + files[intParse-1];
                        Console.Clear();
                        Console.WriteLine($"You selected file:\n{files[intParse-1]}\n\nPlease select an option:\n1. Copy file\n2. Append text to file\n3. Read text of file");
                        choice = Console.ReadLine();
                        
                        if (int.TryParse(choice, out intParse) && (intParse >= 1 || intParse <= files.Count))
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
                            }

                        }

                    }
                break;

            case 5:
            Console.Clear();Console.WriteLine("Exiting..."); exit = true;
            break;
        }            
    }
}
