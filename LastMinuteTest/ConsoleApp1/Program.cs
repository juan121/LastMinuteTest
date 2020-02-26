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
                var goods = ReadFile();
                if (goods.Any())
                {
                    Console.WriteLine("Input");
                    WriteGoods(goods);
                    var taxCalculator = new TaxCalculator();
                    taxCalculator.CalculateTotal(goods);
                    Console.WriteLine("Output");
                    WriteGoods(goods, taxCalculator.TotalTaxValue, taxCalculator.TotalPrice);
                }
                else
                {
                    Console.WriteLine("Files to read data are empty");
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static ICollection<Good> ReadFile()
        {
            try
            {
                var json = File.ReadAllText(filePath + "\\shoppingBasket3.json");
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
                Console.WriteLine($"{good.Quantity} {good.Category.ToString()} at {good.Price.ToString("F")} ");
            }
            Console.WriteLine();
        }

        private static void WriteGoods(ICollection<Good> goods, double totalTaxes, double totalPrice)
        {
            foreach (IGood good in goods)
            {
                Console.WriteLine($"{good.Quantity} {good.Category.ToString()} at {good.Price.ToString("F")}");
            }

            Console.WriteLine($"Sales Taxes {totalTaxes.ToString("F")}");
            Console.WriteLine($"Sales Taxes {totalPrice.ToString("F")}");
        }
    }
}
