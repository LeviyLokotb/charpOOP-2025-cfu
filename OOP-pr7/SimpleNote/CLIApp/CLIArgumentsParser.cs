namespace SimpleNote.Apps
{
    public static class CLIArgumentsParser
    {
        public static event Action<string, string>? CreateNote;
        public static event Action<string>? GetNote;
        public static event Action? ListNotes;
        public static event Action<string, string>? UpdateNote;
        public static event Action<string, string>? AddToNote;
        public static event Action<string>? DeleteNote;
        public static event Action? Help;
        public static event Action? ShowNotesDirectory;

        public static void ProcessCommand(string[] args_)
        {
            if (args_.Count() == 0)
            {
                Help?.Invoke();
                return;
            }
            var command = args_[0].ToLower();

            var timeNow =  $"{DateTime.UtcNow:MMddyyyy}";
            List<string> args = [];
            foreach (string arg in args_)
                if(arg == "-now" || arg=="--now") 
                    args.Add(timeNow);
                else
                    args.Add(arg);

            switch (command)
            {
                case "create":
                    if (args.Count < 3)
                    {
                        Console.WriteLine("Usage: create \"<title>\" \"<content>\"");
                        return;
                    }
                    CreateNote?.Invoke(args[1], args[2]);
                    break;
                case "now":
                    if (args.Count < 2)
                    {
                        Console.WriteLine("Usage: now \"<content>\"");
                        return;
                    }
                    CreateNote?.Invoke(timeNow, args[1]);
                    break;
                case "get":
                    if (args.Count < 2)
                    {
                        Console.WriteLine("Usage: get \"<title>\"");
                        return;
                    }
                    GetNote?.Invoke(args[1]);
                    break;

                case "list":
                    ListNotes?.Invoke();
                    break;

                case "update":
                    if (args.Count < 3)
                    {
                        Console.WriteLine("Usage: update \"<title>\" \"<new content>\"");
                        return;
                    }
                    UpdateNote?.Invoke(args[1], args[2]);
                    break;
                
                case "add":
                    if (args.Count < 3)
                    {
                        Console.WriteLine("Usage: update \"<title>\" \"<new content>\"");
                        return;
                    }
                    AddToNote?.Invoke(args[1], args[2]);
                    break;

                case "delete":
                    if (args.Count < 2)
                    {
                        Console.WriteLine("Usage: delete \"<title>\"");
                        return;
                    }
                    DeleteNote?.Invoke(args[1]);
                    break;

                case "search":
                    if (args.Count < 2)
                    {
                        Console.WriteLine("Usage: search \"<search term>\"");
                        return;
                    }
                    //SearchNotes(args[1]);
                    throw new NotImplementedException("ComingSoon");
                    //break;

                case "help":
                case "--help":
                case "-h":
                    Help?.Invoke();
                    break;

                case "dir":
                    ShowNotesDirectory?.Invoke();
                    break;

                default:
                    Console.WriteLine($"Unknown command: {command}");
                    Help?.Invoke();
                    break;
            }
        }


    }
}