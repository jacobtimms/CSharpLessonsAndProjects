using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace PhoneBook
{
    public class WriteToFile
    {
        internal static void writeTextFileToDictionary(Dictionary<int, long> Contacts)
        {
            lock (Contacts)
            {
                using (StreamWriter file = new StreamWriter("Contacts.txt"))
                {
                    Parallel.ForEach(Contacts, contact =>
                    {
                        file.WriteLine($"{contact.Key}, {contact.Value:D11}");
                    });
                }
            }
        }

        internal static void writeTextFileToDictionary(Dictionary<int, long> Contacts, string[] storedContacts)
        {
            Parallel.ForEach(storedContacts, row =>
            {
                string[] splitRow;
                int nameHash;
                long phoneNo;

                splitRow = row.Split(",");
                Int32.TryParse(splitRow[0], out nameHash);
                Int64.TryParse(splitRow[1], out phoneNo);

                lock (Contacts)
                {
                    Contacts.Add(nameHash, phoneNo);
                }
            });
        }
    }
}
