using System;
namespace Avg_Inventory
{
    // define here the possible events that can happen in the stocks
    enum Events
    {
        Buy,
        Sell,
        Return
    };
    class Program
    {
        // declare the function as a dictionary to have access to Id's and values
        public static Dictionary<int, int> AvgInventoryBetweenDates(DateTime startDate, DateTime endDate, List<Product> inventories)

        {
            if (startDate > endDate || endDate < startDate) throw new Exception("Check the dates again!");
            if (inventories.Count == 0) throw new Exception("The list is empty!");
            // first get all the id's
            Dictionary<int, int> IdQantityPairs = new Dictionary<int, int>();
            foreach (var ids in inventories)
            {
                if (!IdQantityPairs.ContainsKey(ids.id))
                {
                    IdQantityPairs.Add(ids.id, 0);
                }
            }
            // for each id, calculate the total quantity 
            foreach (var keys in IdQantityPairs)
            {
                int eventCount = 0;
                int totalQuantity = 0;
                foreach (var itm in inventories)
                {
                    if (startDate <= itm.eventDate && endDate >= itm.eventDate && keys.Key == itm.id)
                    {
                        if (itm.productEvent == Events.Buy)
                        {
                            eventCount++;
                            totalQuantity += itm.quantity;

                        }
                        else if (itm.productEvent == Events.Sell)
                        {
                            eventCount++;
                            totalQuantity -= itm.quantity;

                        }
                        else if (itm.productEvent == Events.Return)
                        {
                            eventCount++;
                            totalQuantity += itm.quantity;
                        }
                    }
                }
                // check if there are events between dates so we dont get divide by 0
                // if there are events, set the avg as value for the id(key)
                if (eventCount != 0) IdQantityPairs[keys.Key] = totalQuantity / eventCount;
            }
            return IdQantityPairs;
        }
        static void Main(string[] args)
        {
            // create a bunch of items in inventory
            var InventoryList = new List<Product>
            {
                new Product(1,Events.Buy,new DateTime(2023,2,1),100),
                new Product(1,Events.Sell,new DateTime(2023,2,1),50),
                new Product(1,Events.Buy,new DateTime(2023,2,5),120),
                new Product(1,Events.Return,new DateTime(2023,2,7),20),
                new Product(1,Events.Sell,new DateTime(2023,2,17),40),
                new Product(1,Events.Buy,new DateTime(2023,2,25),100),
                new Product(1,Events.Sell,new DateTime(2023,3,1),50),
                new Product(1,Events.Buy,new DateTime(2023,3,5),120),
                new Product(1,Events.Return,new DateTime(2023,3,23),20),
                new Product(1,Events.Sell,new DateTime(2023,3,30),40),
                new Product(2,Events.Buy,new DateTime(2023,2,1),150),
                new Product(2,Events.Sell,new DateTime(2023,2,16),10),
                new Product(2,Events.Buy,new DateTime(2023,2,25),120),
                new Product(2,Events.Return,new DateTime(2023,3,10),30),
                new Product(2,Events.Sell,new DateTime(2023,3,25),100),
                new Product(2,Events.Sell,new DateTime(2023,4,5),150),
                new Product(2,Events.Sell,new DateTime(2023,4,8),10),
                new Product(2,Events.Buy,new DateTime(2023,4,25),220),
                new Product(2,Events.Return,new DateTime(2023,5,3),30),
                new Product(2,Events.Sell,new DateTime(2023,5,20),100),

            };
            Dictionary<int, int> ans1 = AvgInventoryBetweenDates(new DateTime(2023, 4, 16), new DateTime(2023, 5, 21), InventoryList);
            Dictionary<int, int> ans2 = AvgInventoryBetweenDates(new DateTime(2023, 1, 1), new DateTime(2023, 6, 20), InventoryList);
            Dictionary<int, int> ans3 = AvgInventoryBetweenDates(new DateTime(2023, 1, 1), new DateTime(2023, 4, 20), InventoryList);
            Dictionary<int, int> ans4 = AvgInventoryBetweenDates(new DateTime(2022, 1, 1), new DateTime(2022, 6, 20), InventoryList);

            foreach (var (key, value) in ans1)
            {
                // check if there are products that were not affected in the respective data
                if (value == 0) Console.WriteLine($"Product with id:{key} didn't have any changes in stocks");
                else Console.WriteLine($"Product with id:{key} have an average of {value} between {new DateTime(2023, 4, 16):d} and {new DateTime(2023, 5, 21):d} ");
            }
            Console.WriteLine();
            foreach (var (key, value) in ans2)
            {
                // check if there are products that were not affected in the respective data
                if (value == 0) Console.WriteLine($"Product with id:{key} didn't have any changes in stocks");
                else Console.WriteLine($"Product with id:{key} have an average of {value} between {new DateTime(2023, 1, 1):d} and {new DateTime(2023, 6, 20):d} ");
            }
            Console.WriteLine();
            foreach (var (key, value) in ans3)
            {
                // check if there are products that were not affected in the respective data
                if (value == 0) Console.WriteLine($"Product with id:{key} didn't have any changes in stocks");
                else Console.WriteLine($"Product with id:{key} have an average of {value} between {new DateTime(2023, 1, 1):d} and {new DateTime(2023, 4, 20):d} ");
            }
            Console.WriteLine();
            foreach (var (key, value) in ans4)
            {
                // check if there are products that were not affected in the respective data
                if (value == 0) Console.WriteLine($"Product with id:{key} didn't have any changes in stocks");
                else Console.WriteLine($"Product with id:{key} have an average of {value} between {new DateTime(2022, 1, 1):d} and {new DateTime(2022, 6, 20):d} ");
            }
        }
        // create an product class with product ID, product Event, the date and quantity
        public class Product
        {
            public int id;
            public Events productEvent;
            public DateTime eventDate;
            public int quantity;
            public Product(int Id, Events ProductEvents, DateTime EventDate, int Quantity)
            {
                this.id = Id;
                this.productEvent = ProductEvents;
                this.eventDate = EventDate;
                this.quantity = Quantity;
            }
        }
    }
}
