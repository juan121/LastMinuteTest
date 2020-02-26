using Models.SalesTaxes;
using System.Collections.Generic;

namespace SalesTaxes
{
    public interface ITaxCalculator
    {
        public double TotalPrice {get;}
        public double TotalTaxValue {get;}
        public void CalculateTotal<T>(ICollection<T> goods) where T:IGood;
        public void CalculatePriceWithTaxesGood(IGood good);
    }
}
