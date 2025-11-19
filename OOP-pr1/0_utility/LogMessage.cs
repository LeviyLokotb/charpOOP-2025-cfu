using GLib;
using Gtk;
public partial class LogMessage
{
    // Здесь реализуем замыкание
    // Метод принимает TextView, запоминает его (лексическое окружение)
    // и возвращает метод LogMessage для этого TextView
    public static Action<string> CreateLogMessage(TextView? textBlock)
    {
        if (textBlock == null) return null!;

        TextView logTextView = textBlock;
        void LogMessage(string message)
        {
            TextBuffer? buffer = logTextView.Buffer;
            if (buffer is null) return;

            message = message.Replace("\n", "\n    ");
            string LogLine = $" :: {message}\n\n";
            string CurrentText = buffer.Text!;
            buffer.Text = CurrentText + LogLine;

            //logTextView.ScrollToIter(new TextIter(), 0, false, 0, 0);
            var adjustment = logTextView.Vadjustment;
            if (adjustment != null)
            {
                adjustment.Value = adjustment.Upper;
            }
        }
        return LogMessage;
    }
}