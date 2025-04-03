using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace LAB4
{
    class Message
    {
    public Contact Author { get; }
    public string Text { get; private set; }
    public DateTime Timestamp { get; } = DateTime.Now;
    public bool IsEdited { get; private set; }

    public Message(Contact author, string text)
    {
        Author = author ?? throw new ArgumentNullException(nameof(author));
        Text = !string.IsNullOrWhiteSpace(text) ? text : throw new ArgumentException("Message cannot be empty");
        IsEdited = false;
    }

    public void EditMessage(string newText)
    {
        if (!string.IsNullOrWhiteSpace(newText))
        {
            (Text, IsEdited) = (newText, true);
        }
        else
        {
            Console.WriteLine("Edited message cannot be empty");
        }
    }

    public void Deconstruct(out DateTime date, out TimeSpan time) => (date, time) = (Timestamp.Date, Timestamp.TimeOfDay);
}
}
