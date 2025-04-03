namespace LAB4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ChatRoom chat = new ChatRoom("General");
            while (true)
            {
                Console.WriteLine("Choose an option:");
                Console.WriteLine("1. Add new contact");
                Console.WriteLine("2. Send a message");
                Console.WriteLine("3. View messages");
                Console.WriteLine("4. View message statistics");
                Console.WriteLine("5. Decompose messages");
                Console.WriteLine("0. Exit");
                Console.Write("Choose an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Write("Enter contact name: ");
                        string contactName = Console.ReadLine();
                        Contact newContact = new Contact(contactName);
                        chat.AddContact(newContact);
                        Console.WriteLine($"New contact added: {newContact.Name}");
                        break;
                    case "2":
                        Console.Write("Enter sender name: ");
                        string senderName = Console.ReadLine();
                        Contact sender = chat.Contacts.FirstOrDefault(c => c.Name == senderName);
                        if (sender == null)
                        {
                            Console.WriteLine("Sender not found");
                            break;
                        }
                        Console.Write("Enter a messagе: ");
                        string messageText = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(messageText))
                        {
                            Console.WriteLine("Message cannot be empty");
                            break;
                        }
                        chat.AddMessage(new Message(sender, messageText));
                        Console.WriteLine("Message sent successfully");
                        break;
                    case "3":
                        Console.WriteLine("Messages in the chatroom:");
                        if (!chat.Messages.Any())
                        {
                            Console.WriteLine("No messages available");
                        }
                        else
                        {
                            foreach (var msg in chat.Messages)
                            {
                                Console.WriteLine($"[{msg.Timestamp}] {msg.Author.Name}: {msg.Text}");
                            }
                        }
                        break;
                    case "4":
                        var stats = chat.GetStatistics();
                        Console.WriteLine($"Most active user: {stats.MostActiveUser}, Number of messages: {stats.MessageCount}, Shortest message: \"{stats.ShortestMessage}\"");
                        break;
                    case "5":
                        if (chat.Messages.Any())
                        {
                            chat.Messages.First().Deconstruct(out DateTime date, out TimeSpan time);
                            Console.WriteLine($"First message sent on: {date} в {time}");
                        }
                        else
                        {
                            Console.WriteLine("No avaliable messages");
                        }
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Invalid choice! Please, try again");
                        break;
                }
            }
        }
    }
}
