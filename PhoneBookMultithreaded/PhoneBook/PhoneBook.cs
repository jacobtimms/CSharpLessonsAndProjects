using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace PhoneBook
{
    public class PhoneBook
    {
        public Dictionary<int, long> Contacts { get; set; }

        public PhoneBook()
        {
            Contacts = new Dictionary<int, long>();
        }

        public long Store(string name, string phoneNoInput)
        {
            long phoneNo;

            if (name.Length == 0)
            {
                throw new ArgumentOutOfRangeException("Name is empty");
            }
            if (Int64.TryParse(phoneNoInput, out _) == false)
            {
                throw new ArgumentOutOfRangeException("Name cannot include spaces");
            }
            if (Int64.TryParse(phoneNoInput, out phoneNo) && phoneNoInput.Length != 11)
            {
                throw new ArgumentOutOfRangeException("Phone number must be 11 digits");
            }

            int nameHash = name.GetStableHashCode();

            lock (Contacts)
            {
                Parallel.ForEach(Contacts, contact =>
                {
                    if (nameHash == contact.Key)
                    {
                        throw new ArgumentException("This name is already taken by another contact");
                    }
                });
                Contacts.Add(nameHash, phoneNo);
            }

            Console.WriteLine($"OK");
            return Contacts[nameHash];
        }

        public long Get(string name)
        {
            if (name.Length == 0)
            {
                throw new ArgumentOutOfRangeException("Name is empty");
            }

            int nameHash = name.GetStableHashCode();
            if (Contacts.ContainsKey(nameHash))
            {
                Console.WriteLine($"OK {Contacts[nameHash]:D11}");
                return Contacts[nameHash];
            }
            else
            {
                throw new ArgumentException("Contact does not exist");
            }
        }

        public long Del(string name)
        {
            if (name.Length == 0)
            {
                throw new ArgumentOutOfRangeException("Name / Phone Number is empty");
            }

            int nameHash = name.GetStableHashCode();
            if (Contacts.ContainsKey(nameHash))
            {
                Console.WriteLine($"OK {Contacts[nameHash]:D11}");

                long num = Contacts[nameHash];
                lock (Contacts)
                {
                    Contacts.Remove(nameHash);
                }
                return num;
            }
            else
            {
                throw new ArgumentException("Contact does not exist");
            }
        }
        public void Del(long PhoneNo)
        {
            Parallel.ForEach(Contacts, contact =>
            {
                if (contact.Value == PhoneNo)
                {
                    int nameHash = contact.Key;
                    lock (Contacts)
                    {
                        Contacts.Remove(nameHash);
                    }
                    Console.WriteLine("OK");
                    return;
                }
                else
                {
                    throw new ArgumentException("Contact does not exist");
                }
            });
        }

        public long Update(string name, string phoneNoInput)
        {
            if (phoneNoInput.Length != 11)
            {
                throw new ArgumentOutOfRangeException("Phone number must be 11 digits");
            }

            int nameHash = name.GetStableHashCode();
            long phoneNo = Convert.ToInt64(phoneNoInput);

            if (Contacts.ContainsKey(nameHash))
            {
                if(Contacts[nameHash] == phoneNo)
                {
                    throw new ArgumentException("New phone number is the same as the contacts current phone number");
                }
                Console.WriteLine($"OK last no was - {Contacts[nameHash]:D11}");

                lock (Contacts)
                {
                    Contacts[nameHash] = phoneNo;
                }
                return Contacts[nameHash];
            }
            else
            {
                throw new ArgumentException("Contact does not exist");
            }
        }
    }

    public static class StringExtensionMethods
    {
        public static int GetStableHashCode(this string str)
        {
            unchecked
            {
                int hash1 = 5381;
                int hash2 = hash1;

                for (int i = 0; i < str.Length && str[i] != '\0'; i += 2)
                {
                    hash1 = ((hash1 << 5) + hash1) ^ str[i];
                    if (i == str.Length - 1 || str[i + 1] == '\0')
                        break;
                    hash2 = ((hash2 << 5) + hash2) ^ str[i + 1];
                }

                return hash1 + (hash2 * 1566083941);
            }
        }
    }
}
