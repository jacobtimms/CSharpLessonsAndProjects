using System;
using System.Collections.Generic;
using System.IO;

namespace FactoryExample
{
    class Program
    {
        public class ExportContacts
        {
            public static CsvExporter CreateExporter()
            {
                var contacts = new List<Contact>() {
                    new Contact(){ Name="Bill", Number = 07583647183},
                    new Contact(){ Name="Steve", Number = 02746275842},
                    new Contact(){ Name="Ram", Number = 12846372904},
                    new Contact(){ Name="Abdul", Number = 73857361723}
                 };

                var streamWriter = new StreamWriter("export.txt");
                var csvExporter = new CsvExporter(streamWriter, contacts);
                return csvExporter;
            }
        }

        static void Main(string[] args)
        {
            CsvExporter exporter = ExportContacts.CreateExporter();
            exporter.ExportContacts();
        }
    }
}
