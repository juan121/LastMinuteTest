using Models.SalesTaxes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SalesTaxes
{
    public class Program
    {
        private const string filePath = "D:\\Projects\\repos\\LastMinuteTest\\LastMinuteTest\\ConsoleApp1\\Data\\";
        static void Main(string[] usargs)
        {
            try
            {
                int numFiles = Directory.GetFiles(filePath, "*", SearchOption.TopDirectoryOnly).Length;
                for(int i=1; i<=numFiles; i++)
                {
                    var goods = ReadFile(filePath + "\\shoppingBasket" + i +".json");
                    if (goods.Any())
                    {
                        Console.WriteLine("Input " + i);
                        WriteGoods(goods);
                        var taxCalculator = new TaxCalculator();
                        taxCalculator.CalculateTotal(goods);
                        Console.WriteLine("Output " + i);
                        WriteGoods(goods, taxCalculator.TotalTaxValue, taxCalculator.TotalPrice);
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.WriteLine("File to read data is empty");
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static ICollection<Good> ReadFile(string fullPath)
        {
            try
            {
                var json = File.ReadAllText(fullPath);
                return JsonConvert.DeserializeObject<ICollection<Good>>(json);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        private static void WriteGoods(ICollection<Good> goods)
        {
            foreach (IGood good in goods)
            {
                Console.WriteLine($"{good.Quantity} {good.Name} at {good.Price.ToString("F")} ");
            }
            Console.WriteLine();
        }

        private static void WriteGoods(ICollection<Good> goods, double totalTaxes, double totalPrice)
        {
            foreach (IGood good in goods)
            {
                Console.WriteLine($"{good.Quantity} {good.Name} at {good.Price.ToString("F")}");
            }

            Console.WriteLine($"Sales Taxes {totalTaxes.ToString("F")}");
            Console.WriteLine($"Sales Taxes {totalPrice.ToString("F")}");
        }
    }
}
