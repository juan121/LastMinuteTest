using System;
using System.Collections.Generic;

namespace SalesTaxes.Utilities
{
    public static class Utilities
    {
        public enum Category
        {
            Books = 1,
            MusicCDs = 2,
            Food = 3,
            Perfume = 4,
            Medicine = 5
        }

        public static List<Category> ExemptTaxesCategoryList()
        {
            return new List<Category>
            {
                Category.Books,
                Category.Food,
                Category.Medicine
            };
        }

        public static double CalculateRelativePercentage(int percentage, double amount)
        {
            return RoundToTwoDecimals(amount * percentage / 100);
        }

        public static double RoundToTwoDecimals(double amount)
        {
            return Math.Round(amount,2, MidpointRounding.AwayFromZero);
        }

        public static double RoundToNearest05(double amount)
        {
            return Math.Round(amount * 20) / 20;
        }
    }
}
