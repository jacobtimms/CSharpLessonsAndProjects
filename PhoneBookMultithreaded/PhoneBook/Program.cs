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
                Console.WriteLine("\nEnter command:");
                var input = Console.ReadLine();

                //EXIT COMMAND
                if (input.Equals("EXIT"))
                {
                    return;
                }

                //SPLIT USER INPUT STRING
                string[] userInputs = input.Split(' ');

                //CALL STORE COMMAND
                if (userInputs[0] == "STORE")
                {
                    if (userInputs.Length < 3)
                    {
                        Console.WriteLine("Name / Phone Number is empty");
                    }
                    else
                    {
                        try
                        {
                            Phonebook.Store(userInputs[1], userInputs[2]);
                            WriteToFile.writeTextFileToDictionary(Phonebook.Contacts);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }
                }

                //CALL GET COMMAND
                else if (userInputs[0] == "GET")
                {
                    if(userInputs.Length < 2)
                    {
                        Console.WriteLine("Name is empty");
                    }
                    else if (userInputs.Length > 2)
                    {
                        Console.WriteLine("Name cannot include spaces");
                    }
                    else
                    {
                        try
                        {
                            Phonebook.Get(userInputs[1]);
                            WriteToFile.writeTextFileToDictionary(Phonebook.Contacts);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }
                }

                //CALL DEL COMMAND
                else if (userInputs[0] == "DEL")
                {
                    long phoneNum;

                    if (userInputs.Length < 2)
                    {
                        Console.WriteLine("Name / Phone Number is empty");
                    }
                    else if (userInputs.Length > 2)
                    {
                        Console.WriteLine("Name / Phone Number cannot include spaces");
                    }
                    else if (Int64.TryParse(userInputs[1], out phoneNum))
                    {
                        if(userInputs[1].Length != 11)
                        {
                            Console.WriteLine("Phone Number must be 11 digits");
                        }
                        else
                        {
                            try
                            {
                                Phonebook.Del(phoneNum);
                                WriteToFile.writeTextFileToDictionary(Phonebook.Contacts);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e.Message);
                            }
                        }
                    }
                    else
                    {
                        try
                        {
                            Phonebook.Del(userInputs[1]);
                            WriteToFile.writeTextFileToDictionary(Phonebook.Contacts);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }
                }

                //CALL UPDATE COMMAND
                else if (userInputs[0] == "UPDATE")
                {
                    if (userInputs.Length < 3)
                    {
                        Console.WriteLine("Name / Phone Number is empty");
                    }
                    else if(userInputs.Length > 3)
                    {
                        Console.WriteLine("Name / Phone Number cannot include spaces");
                    }
                    else
                    {
                        try
                        {
                            Phonebook.Update(userInputs[1], userInputs[2]);
                            WriteToFile.writeTextFileToDictionary(Phonebook.Contacts);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }
                }
                else if (userInputs[0] == "HELP" && userInputs.Length == 1)
                {
                    Console.WriteLine("\nSTORE: \nTo store a phone number, type 'STORE' followed by a single name (do not include any spaces) & phone number: 'STORE Name 12345678987'");
                    Console.WriteLine("GET: \nTo get an existing phone number, type 'GET' followed by the contact's name: 'GET Name'");
                    Console.WriteLine("DEL: \nTo delete an existing contact, type 'DEL' followed by the contact's name or phone number: 'DEL Name' or 'DEL 12345678987'");
                    Console.WriteLine("UPDATE: \nTo update an existing contact's phone number, type 'UPDATE' followed by the contact's name and the new phone nmuber: 'UPDATE Name 12345678987'");
                    Console.WriteLine("EXIT: \nTo exit the PhoneBook application, type 'EXIT");
                }
                else
                {
                    Console.WriteLine("Please input a valid command, type 'HELP' to show examples");
                }
            }
        }
    }
}
