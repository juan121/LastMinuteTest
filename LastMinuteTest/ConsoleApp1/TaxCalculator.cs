using Models.SalesTaxes;
using System.Collections.Generic;
using static SalesTaxes.Utilities.Utilities;

namespace SalesTaxes
{
    public class TaxCalculator : ITaxCalculator
    {
        public double TotalPrice { get; private set; }
        public double TotalTaxValue { get; private set; }
        /// <summary>
        /// Calculate the price and the tax of a single good within the list
        /// </summary>
        /// <param name="good">object specifications for the good</param>
        public void CalculatePriceWithTaxesGood(IGood good)
        {
            bool exempt = ExemptTaxesCategoryList().Contains(good.Category);
            int applicableTax = 0;
            if (!exempt || good.Imported)
            {
                if (!exempt)
                    applicableTax += 10;
                if (good.Imported)
                    applicableTax += 5;
            }

            if (applicableTax > 0)
            {
                var saleTax = RoundToNearest05(CalculateRelativePercentage(applicableTax, good.Price));
                good.Tax = saleTax;
                good.Price = RoundToTwoDecimals(good.Price + saleTax);
            }
        }
        /// <summary>
        /// Calculate the outcome of the program, including total price with taxes for every single good and total values
        /// for the collection
        /// </summary>
        /// <typeparam name="T">Class that implements IGood, in this case Good</typeparam>
        /// <param name="goods">collection of goods beased on the input</param>
        public void CalculateTotal<T>(ICollection<T> goods) where T:IGood
        {
            foreach (IGood good in goods)
            {
                CalculatePriceWithTaxesGood(good);
                TotalPrice += good.Quantity * good.Price;
                TotalTaxValue += good.Tax == null ? 0 : (double)good.Tax;
            }
        }
    }
}
