using SimpleNote.Models;
using SimpleNote.Utils;
using SimpleNote.Services;

namespace SimpleNote.Apps
{
    class Program
    {
        /// <summary>
        /// Главная абстракция взаимодействия с заметками
        /// </summary>
        private static INoteManager? noteManager;

        private static CLIArgumenrtsHandlers? _CLIArgumenrtsHandlers;
        // Рабочая директория (не директория приложения)
        private static string notesDirectory = Path.Combine(Directory.GetCurrentDirectory(), "notes");

        static void Main(string[] args)
        {
            InitServices();

            try
            {
                CLIArgumentsParser.ProcessCommand(args);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private static void InitServices()
        {
            var storage = new Sandglass(notesDirectory);
            noteManager = new NoteManager(storage);

            _CLIArgumenrtsHandlers = new CLIArgumenrtsHandlers(noteManager);
        }
    }
}