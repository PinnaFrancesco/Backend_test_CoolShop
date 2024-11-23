using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
class Program{
    static void Main(string [] args){
        string filePath = @"C:\Users\pinna\GitHub\Backend_test_CoolShop\orders.csv";

        Console.WriteLine(ReadCsv(filePath));

    }
    
    static List<Order> ReadCsv(string filePath)
    {
        var lines = File.ReadAllLines(filePath).Skip(1); // Salta l'intestazione
        var orders = new List<Order>();

        foreach (var line in lines)
        {
            var parts = line.Split(',');
            orders.Add(new Order
            {
                Id = int.Parse(parts[0]),
                ArticleName = parts[1],
                Quantity = int.Parse(parts[2]),
                UnitPrice = decimal.Parse(parts[3]),
                PercentageDiscount = decimal.Parse(parts[4]),
                Buyer = parts[5],
            });
        }

        return orders;
    }
}

class Order{
    public int Id { get; set; }
    public string ArticleName { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal PercentageDiscount { get; set; }
    public string Buyer { get; set; }

    public decimal TotalWithoutDiscount => Quantity * UnitPrice;
    public decimal TotalWithDiscount => TotalWithoutDiscount * (1 - PercentageDiscount / 100);
    public decimal DiscountDifference => TotalWithoutDiscount - TotalWithDiscount;

    public override string ToString()
    {
        return base.ToString();
    }
}