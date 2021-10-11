using System;
using System.Collections.Generic;
using System.IO;

namespace PhoneBook
{
    public class WriteToFile
    {
        internal static void writeTextFileToDictionary(Dictionary<int, long> Contacts)
        {
            using (StreamWriter file = new StreamWriter("Contacts.txt"))
                foreach (var contact in Contacts)
                    file.WriteLine($"{contact.Key}, {contact.Value:D11}");
        }

        internal static void writeTextFileToDictionary(Dictionary<int, long> Contacts, string[] storedContacts)
        {
            foreach (var row in storedContacts)
            {
                string[] splitRow;
                int nameHash;
                long phoneNo;

                splitRow = row.Split(",");
                Int32.TryParse(splitRow[0], out nameHash);
                Int64.TryParse(splitRow[1], out phoneNo);

                Contacts.Add(nameHash, phoneNo);
            }
        }
    }
}
