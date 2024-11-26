using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
class Program{
    static void Main(string [] args){

        if(args.Length == 0){
            Console.WriteLine("\nPercorso del file mancante\n");
            Environment.Exit(0);
        }

        Console.WriteLine(args[0]);

        string filePath = args[0];

        if(File.Exists(filePath) == false){
            Console.WriteLine("\nFile inesistente\n");
            Environment.Exit(0);
        }

        if(new FileInfo(filePath).Length == 0){
            Console.WriteLine("\nFile vuoto\n");
            Environment.Exit(0);
        }
        
        List<Order> orders = null;

        try
        {
            orders = ReadCsv(filePath);
        }
        catch (System.Exception err)
        {
             Console.WriteLine($"Errore: {err.Message}");
        }
        

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
        //skips the first line then proceed to checks every value to put in the object attibutes an then in the array
        var lines = File.ReadAllLines(filePath).Skip(1); 
        var orders = new List<Order>();
        var counter = 0;
        foreach (var line in lines)
        {
            var parts = line.Split(',');
            //checks if values are all right
            if (int.Parse(parts[0].Replace("\"", "")) <= 0 || parts[0] == "")
            {
                Console.WriteLine("Id inferiore o uguale a 0 alla linea " + counter);
                return null;
            }
            else if (int.Parse(parts[2]) <= 0 || parts[2] == "")
            {
                Console.WriteLine("Quantity inferiore o uguale a 0 alla linea " + counter);
                return null;
            }
            else if (float.Parse(parts[3]) < 0 || parts[3] == "")
            {
                Console.WriteLine("UnitPrice inferiore a 0 alla linea " + counter);
                return null;
            }
            else if (float.Parse(parts[4]) < 0 || parts[4] == "")
            {
                Console.WriteLine("PercentageDiscount inferiore a 0 alla linea " + counter);
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