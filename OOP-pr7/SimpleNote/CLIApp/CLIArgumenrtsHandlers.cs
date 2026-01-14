using SimpleNote.Models;

namespace SimpleNote.Apps
{
    public class CLIArgumenrtsHandlers
    {
        private readonly INoteManager noteManager;
        public CLIArgumenrtsHandlers(INoteManager noteManager)
        {
            this.noteManager = noteManager;
            CLIArgumentsParser.CreateNote += CreateNote;
            CLIArgumentsParser.GetNote += GetNote;
            CLIArgumentsParser.ListNotes += ListNotes;
            CLIArgumentsParser.UpdateNote += UpdateNote;
            CLIArgumentsParser.AddToNote += AddToNote;
            CLIArgumentsParser.DeleteNote += DeleteNote;
            CLIArgumentsParser.ShowNotesDirectory += ShowNotesDirectory;
            CLIArgumentsParser.Help += Help;
        }

        private void CreateNote(string title, string content)
        {
            try
            {
                var note = noteManager.CreateNewNote(title, content);
                DisplayNote(note, false);
                Console.WriteLine($"File saved to: {Path.Combine(noteManager.NotesDirectory, $"{note.Title}.md")}");
            }
            catch (FileAlreadyExistException)
            {
                Console.WriteLine($"File {title}.md already exist!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to create note: {ex.Message}");
            }
        }

        private void GetNote(string title)
        {
            try
            {
                var note = noteManager.GetNote(title);
                DisplayNote(note);
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"Note with title '{title}' not found.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private void ListNotes()
        {
            try
            {
                var notes = noteManager.GetAllNotes().ToList();
                
                if (notes.Count == 0)
                {
                    Console.WriteLine("No notes found.");
                    return;
                }

                Console.WriteLine($"Found {notes.Count} note(s):\n");
                
                foreach (var note in notes)
                {
                    Console.WriteLine($"• {note.Title}");
                    DisplayNote(note, preview: 50);
                    Console.WriteLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error listing notes: {ex.Message}");
            }
        }

        private void UpdateNote(string title, string newContent)
        {
            try
            {
                noteManager.GetNote(title);
                
                Console.Write($"All previous data will be lost! \nAre you sure you want to rewrite note '{title}'? [y/N]: ");
                var response = Console.ReadLine()?.Trim().ToLower();
                
                if (response == "y" || response == "yes")
                {
                    noteManager.UpdateNote(title, newContent);
                    Console.WriteLine($"Note '{title}' updated successfully.");
                }
                else
                {
                    Console.WriteLine("Update cancelled.");
                }
                
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"Note with title '{title}' not found.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating note: {ex.Message}");
            }
        }

        private void AddToNote(string title, string newContent)
        {
            try
            {
                noteManager.AddToNote(title, newContent);
                Console.WriteLine($"Note '{title}' updated successfully.");
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"Note with title '{title}' not found.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating note: {ex.Message}");
            }
        }

        private void DeleteNote(string title)
        {
            try
            {
                Console.Write($"Are you sure you want to delete note '{title}'? [y/N]: ");
                var response = Console.ReadLine()?.Trim().ToLower();
                
                if (response == "y" || response == "yes")
                {
                    noteManager.DeleteNote(title);
                    Console.WriteLine($"Note '{title}' deleted successfully.");
                }
                else
                {
                    Console.WriteLine("Deletion cancelled.");
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"Note with title '{title}' not found.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting note: {ex.Message}");
            }
        }

        private void ShowNotesDirectory()
        {
            var dir = noteManager.NotesDirectory;
            Console.WriteLine($"Notes directory: {dir}");
            Console.WriteLine($"Directory exists: {Directory.Exists(dir)}");
            
            if (Directory.Exists(dir))
            {
                var files = Directory.GetFiles(dir, "*.md");
                Console.WriteLine($"Number of note files: {files.Length}");
            }
        }

        private void Help()
        {
            Console.WriteLine($"""
            =====================================
            Simple Note CLI with Markdown notes
            =====================================

            Commands:
                create "<title>" "<content>"      -- Create a new note

                now "<content>"                   -- Create note with current date as name

                get "<title>"                     -- View a specific note

                list                              -- List all notes

                update "<title>" "<new content>"  -- Update note content

                add "<title>" "<new content>"     -- add content in the end of note

                delete "<title>"                  -- Delete a note

                dir                               -- Show notes directory info

                help                              -- Show this help message
            
            =====================================
            Notes are saved as .md files in: {noteManager.NotesDirectory}
            
            """);
        }

        private static void DisplayNote(INote note, bool verbal=true, int? preview=null)
        {
            Console.WriteLine("=== Note Details ===");
            Console.WriteLine($"Title: {note.Title}");
            Console.WriteLine($"UUID: {note.UUID}");
            if (verbal)
            {
                Console.WriteLine("--- Content ---");
                string content = note.Content;
                if (content == null || content == "") content = "[ Empty ]";
                if (preview != null && content.Length > preview)
                {
                    content = content.Substring(0, (int)preview) + "...";
                }
                Console.WriteLine(content);
                Console.WriteLine("  --- End ---");
            }            
        }
    }
}