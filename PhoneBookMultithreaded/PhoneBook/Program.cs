using System;
using System.IO;

namespace PhoneBook
{
    class Program
    {
        static void Main(string[] args)
        {
            PhoneBook Phonebook = new PhoneBook();

            if (File.Exists("/Users/JacobTimms/REPOS/C# Lessons/PhoneBookMultithreaded/Phonebook/bin/Debug/netcoreapp3.1/Contacts.txt"))
            {
                string[] storedContacts = System.IO.File.ReadAllLines("/Users/JacobTimms/REPOS/C# Lessons/PhoneBookMultithreaded/Phonebook/bin/Debug/netcoreapp3.1/Contacts.txt");

                if (storedContacts.Length != 0)
                {
                    WriteToFile.writeTextFileToDictionary(Phonebook.Contacts, storedContacts);
                }
            }

            while (true)
            {
                try
                {
                    Console.WriteLine("\nEnter command:");
                    var input = Console.ReadLine();
                    string[] userInputs = input.Split(' ');

                    switch (userInputs[0])
                    {
                        case "EXIT":
                            return;

                        case "STORE":
                            if (userInputs.Length < 3)
                            {
                                throw new EmptyFieldException();
                            }

                            Phonebook.Store(userInputs[1], userInputs[2]);
                            WriteToFile.writeTextFileToDictionary(Phonebook.Contacts);
                            break;

                        case "GET":
                            if (userInputs.Length < 2)
                            {
                                throw new EmptyFieldException("Name is empty");
                            }
                            if (userInputs.Length > 2)
                            {
                                throw new InvalidInputException("Name cannot include spaces");
                            }

                            Phonebook.Get(userInputs[1]);
                            WriteToFile.writeTextFileToDictionary(Phonebook.Contacts);
                            break;

                        case "DEL":
                            long phoneNum;
                            if (Int64.TryParse(userInputs[1], out phoneNum))
                            {
                                if (userInputs.Length < 2)
                                {
                                    throw new EmptyFieldException();
                                }
                                if (userInputs.Length > 2)
                                {
                                    throw new InvalidInputException();
                                }
                                if (userInputs[1].Length != 11)
                                {
                                    throw new ArgumentOutOfRangeException("Phone Number must be 11 digits");
                                }

                                Phonebook.Del(phoneNum);
                                WriteToFile.writeTextFileToDictionary(Phonebook.Contacts);
                            }
                            else
                            {
                                Phonebook.Del(userInputs[1]);
                                WriteToFile.writeTextFileToDictionary(Phonebook.Contacts);
                            }
                            break;

                        case "UPDATE":
                            if (userInputs.Length < 3)
                            {
                                throw new EmptyFieldException();
                            }
                            if (userInputs.Length > 3)
                            {
                                throw new InvalidInputException();
                            }

                            Phonebook.Update(userInputs[1], userInputs[2]);
                            WriteToFile.writeTextFileToDictionary(Phonebook.Contacts);
                            break;

                        case "HELP":
                            Console.WriteLine("\nSTORE: \nTo store a phone number, type 'STORE' followed by a single name (do not include any spaces) & phone number: 'STORE Name 12345678987'");
                            Console.WriteLine("GET: \nTo get an existing phone number, type 'GET' followed by the contact's name: 'GET Name'");
                            Console.WriteLine("DEL: \nTo delete an existing contact, type 'DEL' followed by the contact's name or phone number: 'DEL Name' or 'DEL 12345678987'");
                            Console.WriteLine("UPDATE: \nTo update an existing contact's phone number, type 'UPDATE' followed by the contact's name and the new phone nmuber: 'UPDATE Name 12345678987'");
                            Console.WriteLine("EXIT: \nTo exit the PhoneBook application, type 'EXIT");
                            break;

                        default:
                            throw new ArgumentOutOfRangeException("Please input a valid command, type 'HELP' to show examples");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }

    public class EmptyFieldException : Exception
    {
        public EmptyFieldException()
            : base("Name / Phone Number is empty")
        {
        }

        public EmptyFieldException(string message)
            : base(message)
        {
        }

        public EmptyFieldException(string message, Exception inner)
    : base(message, inner)
        {
        }
    }

    public class InvalidInputException : Exception
    {
        public InvalidInputException()
            : base("Name / Phone Number cannot include spaces")
        {
        }

        public InvalidInputException(string message)
            : base(message)
        {
        }

        public InvalidInputException(string message, Exception inner)
    : base(message, inner)
        {
        }
    }
}
