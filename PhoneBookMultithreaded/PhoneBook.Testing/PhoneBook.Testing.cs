using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace PhoneBook.Testing
{
    [TestFixture]
    public class Test
    {
        //STORE FUNCTION TESTING
        [Test]
        public void TestStoreFunctionAddsContacts()
        {
            // arrange
            PhoneBook Phonebook1 = new PhoneBook();

            // act
            long result_1 = Phonebook1.Store("Dan", "07791306748");
            long result_2 = Phonebook1.Store("St3v3n", "12345678987");

            // assert
            Assert.That(result_1, Is.EqualTo(07791306748));
            Assert.That(result_2, Is.EqualTo(12345678987));
        }

        [Test]
        public void TestStoreEmptyName()
        {
            // arrange
            PhoneBook Phonebook1 = new PhoneBook();

            // assert
            Assert.Throws<ArgumentOutOfRangeException>(() => Phonebook1.Store("", "07791306748"));
            Assert.Throws<ArgumentOutOfRangeException>(() => Phonebook1.Store("", "12345678987"));
        }

        [Test]
        public void TestStoreFunctionNameIncludesSpaces()
        {
            // arrange
            PhoneBook Phonebook1 = new PhoneBook();

            // assert
            Assert.Throws<ArgumentOutOfRangeException>(() => Phonebook1.Store("Dan", "Dan"));
            Assert.Throws<ArgumentOutOfRangeException>(() => Phonebook1.Store("St3v3n", "Smithies"));
        }

        [Test]
        public void TestStorePhoneNumberInputLength()
        {
            // arrange
            PhoneBook Phonebook1 = new PhoneBook();

            // assert
            Assert.Throws<ArgumentOutOfRangeException> (() => Phonebook1.Store("Dan", "7"));
            Assert.Throws<ArgumentOutOfRangeException>(() => Phonebook1.Store("Steven", "12345678909784"));
        }

        [Test]
        public void TestStoreDoesNotCreateDuplicateNames()
        {
            // arrange
            PhoneBook Phonebook1 = new PhoneBook();

            // act
            Phonebook1.Store("Dan", "07791306748");
            Phonebook1.Store("St3v3n", "12345678987");

            // assert
            Assert.Throws<AggregateException>(() => Phonebook1.Store("Dan", "07791306748"));
            Assert.Throws<AggregateException>(() => Phonebook1.Store("Dan", "12345678987"));
            Assert.Throws<AggregateException>(() => Phonebook1.Store("St3v3n", "98765432123"));
        }

        //GET FUNCTION TESTING
        [Test]
        public void TestGetFunction()
        {
            // arrange
            PhoneBook Phonebook1 = new PhoneBook();

            // act
            Phonebook1.Store("Dan", "07791306748");
            long result_1 = Phonebook1.Get("Dan");

            Phonebook1.Store("Steven", "12345678987");
            long result_2 = Phonebook1.Get("Steven");

            Phonebook1.Store("Jen", "00000437643");
            long result_3 = Phonebook1.Get("Jen");

            // assert
            Assert.That(result_1, Is.EqualTo(07791306748));
            Assert.That(result_2, Is.EqualTo(12345678987));
            Assert.That(result_3, Is.EqualTo(00000437643));
        }

        [Test]
        public void TestGetFunctionInvalidContact()
        {
            // arrange
            PhoneBook Phonebook1 = new PhoneBook();

            // act
            Phonebook1.Store("Dan", "07791306748");

            // assert
            Assert.Throws<ArgumentException>(() => Phonebook1.Get("D4n"));
            Assert.Throws<ArgumentException>(() => Phonebook1.Get("Stacey"));
        }

        [Test]
        public void TestGetFunctionNameIsEmpty()
        {
            // arrange
            PhoneBook Phonebook1 = new PhoneBook();

            // assert
            Assert.Throws<ArgumentOutOfRangeException>(() => Phonebook1.Get(""));
        }

        //DEL FUNCTION TESTING
        [Test]
        public void TestDelNameFunction()
        {
            // arrange
            PhoneBook Phonebook1 = new PhoneBook();

            // act
            Phonebook1.Store("Dan", "07791306748");
            long result_1 = Phonebook1.Del("Dan");

            Phonebook1.Store("Steven", "12345678987");
            long result_2 = Phonebook1.Del("Steven");

            // assert
            Assert.That(result_1, Is.EqualTo(07791306748));
            Assert.That(result_2, Is.EqualTo(12345678987));

            Assert.Throws<ArgumentException>(() => Phonebook1.Get("Dan"));
            Assert.Throws<ArgumentException>(() => Phonebook1.Get("Steven"));
        }

        [Test]
        public void TestDelFunctionIsEmpty()
        {
            // arrange
            PhoneBook Phonebook1 = new PhoneBook();

            // assert
            Assert.Throws<ArgumentOutOfRangeException>(() => Phonebook1.Del(""));
        }

        [Test]
        public void TestDelNameFunctionInvalidContact()
        {
            // arrange
            PhoneBook Phonebook1 = new PhoneBook();

            // act
            Phonebook1.Store("Dan", "07791306748");
            Phonebook1.Store("Steven", "12345678987");

            // assert
            Assert.Throws<ArgumentException>(() => Phonebook1.Del("D4n"));
            Assert.Throws<ArgumentException>(() => Phonebook1.Del("Stacey"));
        }

        [Test]
        public void TestDelNumberFunction()
        {
            // arrange
            PhoneBook Phonebook1 = new PhoneBook();

            // act
            Phonebook1.Store("Dan", "07791306748");
            Phonebook1.Del(07791306748);

            Phonebook1.Store("Steven", "12345678987");
            Phonebook1.Del(12345678987);

            // assert
            Assert.Throws<ArgumentException>(() => Phonebook1.Get("Dan"));
            Assert.Throws<ArgumentException>(() => Phonebook1.Get("Steven"));
        }

        //UPDATE FUNCTION TESTING
        [Test]
        public void TestUpdateFunction()
        {
            // arrange
            PhoneBook Phonebook1 = new PhoneBook();

            // act
            Phonebook1.Store("Dan", "07791306748");
            long result_1 = Phonebook1.Update("Dan", "07876253695");

            Phonebook1.Store("Steven", "12345678987");
            long result_2 = Phonebook1.Update("Steven", "98765432123");

            // assert
            Assert.That(result_1, Is.EqualTo(07876253695));
            Assert.That(result_2, Is.EqualTo(98765432123));
        }

        [Test]
        public void TestUpdateFunctionPhoneNumberInputLength()
        {
            // arrange
            PhoneBook Phonebook1 = new PhoneBook();

            // act
            Phonebook1.Store("Dan", "07791306748");
            Phonebook1.Store("Steven", "12345678987");

            // assert
            Assert.Throws<ArgumentOutOfRangeException>(() => Phonebook1.Update("Dan", "7"));
            Assert.Throws<ArgumentOutOfRangeException>(() => Phonebook1.Update("Steven", "12345678909784"));
        }

        [Test]
        public void TestUpdateFunctionInvalidContact()
        {
            // arrange
            PhoneBook Phonebook1 = new PhoneBook();

            // act
            Phonebook1.Store("Dan", "07791306748");
            Phonebook1.Store("Steven", "12345678987");

            // assert
            Assert.Throws<ArgumentException>(() => Phonebook1.Update("D4n", "01857385673"));
            Assert.Throws<ArgumentException>(() => Phonebook1.Update("Stacey", "38491847391"));
            Assert.Throws<ArgumentException>(() => Phonebook1.Update("", "07791306748"));
        }

        [Test]
        public void TestUpdateFunctionContactNumberIsSame()
        {
            // arrange
            PhoneBook Phonebook1 = new PhoneBook();

            // act
            Phonebook1.Store("Dan", "07791306748");
            Phonebook1.Store("Steven", "12345678987");

            // assert
            Assert.Throws<ArgumentException>(() => Phonebook1.Update("Dan", "07791306748"));
            Assert.Throws<ArgumentException>(() => Phonebook1.Update("Steven", "12345678987"));
        }
    }
}
