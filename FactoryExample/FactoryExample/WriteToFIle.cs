using System;
using System.Collections.Generic;
using System.IO;

namespace FactoryExample
{
    public class CsvExporter
    {
        static internal StreamWriter _streamwriter;
        static internal List<Contact> _contacts;

        internal CsvExporter(StreamWriter file, List<Contact> contacts)
        {
            _streamwriter = file;
            _contacts = contacts;
        }

        public void ExportContacts()
        {
            using (_streamwriter)
                foreach (var contact in _contacts)
                    _streamwriter.WriteLine($"{contact.Name}, {contact.Number:D11}");
        }
    }
}
