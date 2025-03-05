using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text.Json;
using System.Xml.Linq;
using System.Globalization;

namespace LAB2
{
    struct Coordinate
    {
        public float lat { get; set; }
        public float lng { get; set; }
    }

    class Program
    {
        static void Main()
        {
            ProcessCoordinates("D:\\TU_sem_8\\C#\\LAB2\\LAB2\\bin\\Debug\\input-01.txt", "D:\\TU_sem_8\\C#\\LAB2\\LAB2\\output1.json");
            ProcessContacts("D:\\TU_sem_8\\C#\\LAB2\\LAB2\\bin\\Debug\\input-02.txt", "D:\\TU_sem_8\\C#\\LAB2\\LAB2\\contacts.xml");
            Process3DScene("D:\\TU_sem_8\\C#\\LAB2\\LAB2\\bin\\Debug\\input-03.dae", "D:\\TU_sem_8\\C#\\LAB2\\LAB2\\scene.json");
            UpdateHtmlFile("D:\\TU_sem_8\\C#\\LAB2\\LAB2\\bin\\Debug\\google-maps.html", "D:\\TU_sem_8\\C#\\LAB2\\LAB2\\output1.json");
        }

        static void ProcessCoordinates(string inputFile, string outputFile)
        {
            var coordinates = File.ReadAllText(inputFile)
                .Split(';')
                .Select(pair => pair.Split(','))
                .Where(parts => parts.Length == 2)
                .Select(parts => new Coordinate
                {
                    lat = float.Parse(parts[0], CultureInfo.InvariantCulture),
                    lng = float.Parse(parts[1], CultureInfo.InvariantCulture)
                })
                .ToList();

            string json = JsonSerializer.Serialize(coordinates, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(outputFile, json);
        }

        static void ProcessContacts(string inputFile, string outputFile)
        {
            var contacts = new List<(string Name, string ID, string Phone)>();
            string patternName = @"([\p{IsCyrillic}]+)";
            string patternID = @"([0-9]{6})";
            string patternPhone = @"(\+395\s?[0-9]{3}\s?[0-9]{2}\s?[0-9]{2})";

            var content = File.ReadAllText(inputFile);

            if (string.IsNullOrEmpty(content))
            {
                Console.WriteLine("Входният файл е празен.");
                return;
            }

            var names = Regex.Matches(content, patternName).Cast<Match>().Select(m => m.Value.Trim()).ToList();
            var ids = Regex.Matches(content, patternID).Cast<Match>().Select(m => m.Value.Trim()).ToList();
            var phones = Regex.Matches(content, patternPhone).Cast<Match>().Select(m => m.Value.Trim()).ToList();

            Console.WriteLine($"Намерени имена: {names.Count}, ID: {ids.Count}, Телефони: {phones.Count}");

            int contactIndex = 0;

            while (contactIndex < names.Count || contactIndex < ids.Count || contactIndex < phones.Count)
            {
                string name = (contactIndex < names.Count) ? names[contactIndex] : null;
                string id = (contactIndex < ids.Count) ? ids[contactIndex] : null;
                string phone = (contactIndex < phones.Count) ? phones[contactIndex] : null;

                if ((name != null && id != null) || (name != null && phone != null) || (id != null && phone != null))
                {
                    contacts.Add((name, id, phone));
                }

                // Увеличаваме индекса
                contactIndex++;
            }

            if (contacts.Count == 0)
            {
                return;
            }

            var xml = new XElement("Contacts",
                contacts.Select(c => new XElement("Contact",
                    new XElement("Name", c.Name ?? ""),
                    new XElement("ID", c.ID ?? ""),
                    new XElement("Phone", c.Phone ?? ""))));

            xml.Save(outputFile);
            Console.WriteLine("XML файлът е записан успешно.");
        }

        static void Process3DScene(string inputFile, string outputFile)
        {
            var doc = XDocument.Load(inputFile);
            var elements = doc.Descendants()
                              .Where(el => el.Attribute("id") != null)  // Проверка дали има атрибут "id"
                              .ToDictionary(el => (string)el.Attribute("id"), el => el);

            var result = new List<object>();

            foreach (var el in doc.Descendants())
            {
                var refAttr = el.Attributes().FirstOrDefault(a => a.Value.StartsWith("#"));

                var connection = refAttr != null && elements.ContainsKey(refAttr.Value.TrimStart('#'))
                    ? elements[refAttr.Value.TrimStart('#')]?.Name.LocalName
                    : null;

                result.Add(new
                {
                    Tag = el.Name.LocalName ?? "Unknown", 
                    Connection = connection ?? "No connection"  
                });
            }

            File.WriteAllText(outputFile, JsonSerializer.Serialize(result));
        }



        static void UpdateHtmlFile(string htmlFile, string jsonFile)
        {
            string json = File.ReadAllText(jsonFile).Trim();
            string htmlContent = File.ReadAllText(htmlFile);
            string pattern = @"var locations = \[.*?\];";
            string replacement = $"var locations = [{json}];";
            string updatedHtml = Regex.Replace(htmlContent, pattern, replacement, RegexOptions.Singleline);
            File.WriteAllText(htmlFile, updatedHtml);
        }
    }
}




