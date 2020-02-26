using Microsoft.AspNetCore.Hosting.Internal;
using Models.SalesTaxes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using static SalesTaxes.Utilities.Utilities;
using System.Linq;

namespace SalesTaxes
{
    public class Program
    {
        private const string filePath = "D:\\Projects\\repos\\LastMinuteTest\\LastMinuteTest\\ConsoleApp1\\Data\\";
        private static ICollection<Good> _goods;
        private static double _totalPrice;
        private static double _totalTaxValue;
        static void Main(string[] usargs)
        {
            Console.WriteLine("Input 1");
            ReadFile();
            WriteGoods(_goods);
            //Write entry of file to show users the input
            CalculateTaxes(_goods);
            //Write output
            Console.WriteLine("Input 1");
            WriteGoods(_goods, _totalPrice);
        }

        private static void CalculateTaxes(ICollection<Good> goods) 
        {
            foreach(IGood good in _goods)
            {
                CalculatePriceWithTaxesGood(good);
                //visibility
                _totalPrice += good.Price;
                _totalTaxValue += good.Tax == null ? 0 : (double)good.Tax;
            }
        }

        private static void CalculatePriceWithTaxesGood(IGood good)
        {
            bool exempt = ExemptTaxesCategoryList().Contains(good.Category);
            int applicableTax = 0;
            //if(good.Category.GetHashCode() in Category)
            if (!exempt|| good.Imported)
            {
                if (!exempt)
                    applicableTax += 10;
                if (good.Imported)
                    applicableTax += 5;
            }

            if(applicableTax > 0)
            {
               var saleTax = CalculateTaxesPercentageRelativeToGoodPrice(good, applicableTax);
               good.Tax = saleTax;
               good.Price = RoundToTwoDecimals(good.Price + saleTax);
            }
            //TODO
            //Create a response to store Sales Taxes and calculate total of price for all elements

        }

        private static double CalculateTaxesPercentageRelativeToGoodPrice(IGood good, int applicableTax)
        {
            var relativePercentage = CalculateRelativePercentage(applicableTax, good.Price);
            var relativePErcentageWithTwoDecimals  = RoundToTwoDecimals(relativePercentage);
            return RoundToNearest05(relativePErcentageWithTwoDecimals);
        }

        private static void ReadFile()
        {
            //var filePath = Path.GetFullPath(@"../Data/shoppingBasket.json");
            //var filePath = "D:\\Projects\\repos\\LastMinuteTest\\LastMinuteTest\\ConsoleApp1\\Data\\shoppingBasket.json";
            //var filePath = new HostingEnvironment().ContentRootPath; //MapPath(@"~/Data/shoppingBasket.json");
            var json = System.IO.File.ReadAllText(filePath + "\\shoppingBasket1.json");
            
            _goods = JsonConvert.DeserializeObject<ICollection<Good>>(json);
        }

        private static void WriteGoods(ICollection<Good> goods)
        {
            foreach (IGood good in _goods)
            {
                //Console.WriteLine($"Hello, {name}! Today is {date.DayOfWeek}, it's {date:HH:mm} now.");
                Console.WriteLine($"{good.Quantity} {good.Category.ToString()} at {good.Price} ");
            }

            Console.WriteLine();
        }

        private static void WriteGoods(ICollection<Good> goods, double totalPrice)
        {
            foreach (IGood good in _goods)
            {
                //Console.WriteLine($"Hello, {name}! Today is {date.DayOfWeek}, it's {date:HH:mm} now.");
                Console.WriteLine($"{good.Quantity} {good.Category.ToString()} at {good.Price} ");
            }

            Console.WriteLine($"Sales Taxes {_totalTaxValue}");
            Console.WriteLine($"Sales Taxes {_totalPrice}");

        }
    }
}
