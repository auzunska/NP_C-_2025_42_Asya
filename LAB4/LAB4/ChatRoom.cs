using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB4
{
    class ChatRoom
    {
        public string Name { get; }
        public List<Contact> Contacts { get; } = new List<Contact>();
        public List<Message> Messages { get; } = new List<Message>();

        public ChatRoom(string name) => Name = name ?? "Invalid ChatRoom";

        public void AddContact(Contact contact)
        {
            if (contact != null && !Contacts.Any(c => c.Name == contact.Name))
            {
                Contacts.Add(contact);
            }
            else
            {
                Console.WriteLine("Contact already exists or name is invalid!");
            }
        }

        public void AddMessage(Message message)
        {
            if (message != null)
            {
                Messages.Add(message);
            }
            else
            {
                Console.WriteLine("Message cannot be empty");
            }
        }

        public (string MostActiveUser, int MessageCount, string ShortestMessage) GetStatistics()
        {
            var groupedMessages = Messages.GroupBy(m => m.Author.Name)
                                          .OrderByDescending(g => g.Count())
                                          .FirstOrDefault();

            return groupedMessages == null ? ("No Users", 0, "No Messages") :
                   (groupedMessages.Key, groupedMessages.Count(), groupedMessages.OrderBy(m => m.Text.Length).First().Text);
        }
    }
}
