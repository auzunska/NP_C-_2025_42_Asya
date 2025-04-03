using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB4
{
    class Contact
    {
        private static Random random = new Random();
        private string name;
        public string Name
        {
            get => name;
            set => name = string.IsNullOrWhiteSpace(value) ? $"user{random.Next(10000, 99999)}" : value;
        }

        public Contact(string name) => Name = name;
    }
}
