using Microsoft.VisualStudio.TestTools.UnitTesting;
using LedgerConsolid.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace LedgerConsolid.Models.Tests
{
    [TestClass()]
    public class LedgerBookTests
    {
        private static int BookNamingIndex = -1;
        private LedgerBook getSampleBook()
        {
            Console.WriteLine("Creating Sample Book");
            LedgerBook book = new LedgerBook("SampleBook" + ++BookNamingIndex);

            Console.WriteLine("Adding Items to book, current count: " + book.Count);
            book.Add(LedgerItem.Create(new DateTime(1997, 3, 1), "Sales Profit", LedgerItemCreateMode.Credit, 327.8));


            Console.WriteLine("Adding Items to book, current count: " + book.Count);
            book.Add(LedgerItem.Create(new DateTime(1997, 3, 3), "Sales Profit", LedgerItemCreateMode.Credit, 668.23));

            Console.WriteLine("Adding Items to book, current count: " + book.Count);
            book.Add(LedgerItem.Create(new DateTime(1997, 3, 4), "Boiler Maintenance", LedgerItemCreateMode.Debit, 122.6));

            Console.WriteLine("Adding Items to book, current count: " + book.Count);
            book.Add(LedgerItem.Create(new DateTime(1997, 3, 8), "Sales Profit", LedgerItemCreateMode.Credit, 1024.3));

            Console.WriteLine("Adding Items to book, current count: " + book.Count);
            book.Add(LedgerItem.Create(new DateTime(1997, 3, 9), "Sales Profit", LedgerItemCreateMode.Credit, 422));

            Console.WriteLine("Adding Items to book, current count: " + book.Count);
            book.Add(LedgerItem.Create(new DateTime(1997, 3, 12), "Sales Profit", LedgerItemCreateMode.Credit, 23));

            Console.WriteLine("Added Items to book complete, current count: " + book.Count);
            return book;
        }
        [TestMethod()]
        public void AddTest()
        {
            foreach (LedgerItem item in getSampleBook())
            {
                Console.WriteLine(item);
            }
        }

        [TestMethod()]
        public void ClearTest()
        {
            LedgerBook book = getSampleBook();
            book.Clear();
            Console.WriteLine("Cleared book, current count is:" + book.Count);
            Console.WriteLine("Trying to enumerate:");
            foreach (LedgerItem item in book)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("Enumerating Complete.");
        }

        [TestMethod()]
        public void ContainsTest()
        {
            LedgerBook book = getSampleBook();
            Console.WriteLine("Generating Item contained in the collection");
            LedgerItem containedItem = LedgerItem.Create(new DateTime(1997, 3, 12), "Sales Profit", LedgerItemCreateMode.Credit, 23);
            containedItem.Parent = book;

            Console.WriteLine("containedItem:" + containedItem);

            Console.WriteLine("Is the item contained in book? " + book.Contains(containedItem));

            Console.WriteLine("Generating Item not contained in the collection");

            LedgerItem notContainedItem = LedgerItem.Create(new DateTime(2091, 6, 27), "Budget to create rocket", LedgerItemCreateMode.Credit, 664773.68);

            Console.WriteLine("notContainedItem:" + notContainedItem);

            Console.WriteLine("Is the item not contained in book? " + book.Contains(notContainedItem));
        }

        [TestMethod()]
        public void CopyToTest()
        {
            LedgerBook book = getSampleBook();
            LedgerItem[] targetArray = new LedgerItem[book.Count + 5];
            book.CopyTo(targetArray, 1);
            for (int i = 0; i < targetArray.Length; i++)
            {
                Console.WriteLine(targetArray[i]);
            }
        }

        [TestMethod()]
        public void GetEnumeratorTest()
        {
            LedgerBook book = getSampleBook();
            IEnumerator<LedgerItem> rator = book.GetEnumerator();
            Console.WriteLine("Testing for normal move next enumerator methods");
            while (rator.MoveNext())
            {
                Console.WriteLine(rator.Current);
            }
            rator.Dispose();
            Console.WriteLine("Testing LINQ,");
            Console.WriteLine("from i in book where i.Credit < 350 select i");

            var query = from i in book where i.Credit < 350 select i;
            foreach (var item in query)
            {
                Console.WriteLine(item);
            }

        }

        [TestMethod()]
        public void IndexOfTest()
        {
            LedgerBook book = getSampleBook();
            Console.WriteLine("Getting item at index 3");
            LedgerItem Index3 = book[3];

            Console.WriteLine($"The Index Of {Index3} is: {book.IndexOf(Index3)}");

            Console.WriteLine("Creating an item not in index");
            LedgerItem notContainedItem = LedgerItem.Create(new DateTime(2091, 6, 27), "Budget to create rocket", LedgerItemCreateMode.Credit, 664773.68);


            Console.WriteLine($"The Index Of {notContainedItem} is: {book.IndexOf(notContainedItem)}");

        }

        [TestMethod()]
        public void InsertTest()
        {
            LedgerBook book = getSampleBook();
            Console.WriteLine("The Items Before Insert:");
            foreach (LedgerItem item in book)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("Inserting new item at index 0");
            book.Insert(0
                , LedgerItem.Create(new DateTime(2020, 1, 19), "New Installation of Boiler", LedgerItemCreateMode.Debit, 1024.22));
            foreach (LedgerItem item in book)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("Inserting new item at index 3");
            book.Insert(3
                , LedgerItem.Create(new DateTime(2019, 12, 2), "New Installation of Boiler", LedgerItemCreateMode.Debit, 884.8));
            foreach (LedgerItem item in book)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("Inserting new item at index Count");
            book.Insert(book.Count
                , LedgerItem.Create(new DateTime(2077, 5, 6), "New Installation of Boiler", LedgerItemCreateMode.Debit, 2884.8));
            foreach (LedgerItem item in book)
            {
                Console.WriteLine(item);
            }
        }

        [TestMethod()]
        public void RemoveTest()
        {
            LedgerBook book = getSampleBook();
            Console.WriteLine("The Items Before Remove:");
            foreach (LedgerItem item in book)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("Removing new item at index 0");
            book.Remove(book[0]);
            foreach (LedgerItem item in book)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("Removing new item at index 3");
            book.Remove(book[3]);
            foreach (LedgerItem item in book)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("Removing new item at index Count");
            book.Remove(book[book.Count() - 1]);
            foreach (LedgerItem item in book)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("Removing item not included");
            book.Remove(LedgerItem.Create(new DateTime(2020, 1, 19), "New Installation of Boiler", LedgerItemCreateMode.Debit, 1024.22));
            foreach (LedgerItem item in book)
            {
                Console.WriteLine(item);
            }
        }

        [TestMethod()]
        public void RemoveAtTest()
        {
            LedgerBook book = getSampleBook();
            Console.WriteLine("The Items Before RemoveAt:");
            foreach (LedgerItem item in book)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("Removing new item at index 0");
            book.RemoveAt(0);
            foreach (LedgerItem item in book)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("Removing new item at index 3");
            book.RemoveAt(3);
            foreach (LedgerItem item in book)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("Removing new item at index Count");
            book.RemoveAt(book.Count() - 1);
            foreach (LedgerItem item in book)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("Removing item not included");
            try
            {
                book.RemoveAt(17);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            foreach (LedgerItem item in book)
            {
                Console.WriteLine(item);
            }
        }
    }
}