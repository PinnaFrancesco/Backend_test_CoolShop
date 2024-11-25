using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
class Program{
    static void Main(string [] args){

        if(args.Length == 0){
            Console.WriteLine("\nMissing file path\n");
            Environment.Exit(0);
        }

        Console.WriteLine(args[0]);

        string filePath = args[0];

        if(File.Exists(filePath) == false){
            Console.WriteLine("\nFile does't exists\n");
            Environment.Exit(0);
        }

        if(new FileInfo(filePath).Length == 0){
            Console.WriteLine("\nFile vuoto\n");
            Environment.Exit(0);
        }
        
        List<Order> orders = ReadCsv(filePath);

        if(orders != null){
            var maxTotal = orders.OrderByDescending(o => o.TotalWithDiscount).First();
            var maxQuantity = orders.OrderByDescending(o => o.Quantity).First();
            var maxDiscountDiff = orders.OrderByDescending(o => o.DiscountDifference).First();

            // Stampa i risultati
            Console.WriteLine("\nRecord con importo totale più alto:");
            Console.WriteLine(maxTotal);

            Console.WriteLine("\nRecord con quantità più alta:");
            Console.WriteLine(maxQuantity);

            Console.WriteLine("\nRecord con maggior differenza tra totale senza sconto e totale con sconto:");
            Console.WriteLine(maxDiscountDiff);
        }

    }
    
    /// <summary>
    /// Method that recives a file path and create an array of Order objects
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns> List<Order> orders </returns>
    static List<Order> ReadCsv(string filePath)
    {
        var lines = File.ReadAllLines(filePath).Skip(1); // skip the frist line
        var orders = new List<Order>();
        var counter = 0;
        foreach (var line in lines)
        {
            var parts = line.Split(',');
            //checks if values are all right
            if (int.Parse(parts[0].Replace("\"", "")) <= 0 || int.Parse(parts[0].Replace("\"", "")) == null)
            {
                Console.WriteLine("Id is less or equal to 0 at line " + counter);
                return null;
            }
            else if (int.Parse(parts[2]) <= 0 || int.Parse(parts[2]) == null)
            {
                Console.WriteLine("Quantity is less or equal to 0 at line " + counter);
                return null;
            }
            else if (float.Parse(parts[3]) < 0 || float.Parse(parts[3]) == null)
            {
                Console.WriteLine("UnitPrice is less than 0 at line " + counter);
                return null;
            }
            else if (float.Parse(parts[4]) < 0 || float.Parse(parts[4]) == null)
            {
                Console.WriteLine("PercentageDiscount is less than 0 at line " + counter);
                return null;
            }
            counter++;
            orders.Add(new Order
            {
                Id = int.Parse(parts[0].Replace("\"", "")),
                ArticleName = parts[1],
                Quantity = int.Parse(parts[2]),
                UnitPrice = float.Parse(parts[3]),
                PercentageDiscount = float.Parse(parts[4]),
                Buyer = parts[5].Replace("\"", ""),
            });
        }

        return orders;
    }

    
}
/// <summary>
/// Order class 
/// </summary>
class Order{
    public int Id { get; set; }
    public string ArticleName { get; set; }
    public int Quantity { get; set; }
    public float UnitPrice { get; set; }
    public float PercentageDiscount { get; set; }
    public string Buyer { get; set; }

    public float TotalWithoutDiscount => Quantity * UnitPrice;
    public float TotalWithDiscount => TotalWithoutDiscount - ((PercentageDiscount * TotalWithoutDiscount) / 100);
    public float DiscountDifference => TotalWithoutDiscount - TotalWithDiscount;

    public override string ToString()
    {
        return $"Id: {Id}, Article: {ArticleName}, Quantity: {Quantity}, Unit Price: {UnitPrice:C}, " +
               $"Discount: {PercentageDiscount}%, Buyer: {Buyer}, " +
               $"Total: {TotalWithDiscount:C}, Discount Diff: {DiscountDifference:C}";
    }
}