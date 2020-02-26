using Microsoft.Extensions.DependencyInjection;
using Models.SalesTaxes;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using static SalesTaxes.Utilities.Utilities;

namespace SalesTaxes.Tests
{
    [TestFixture]
    public class TaxCalculatorTests
    {
        private ICollection<IGood> _goods;
        private ITaxCalculator _taxCalculator;

        public TaxCalculatorTests()
        {
            _taxCalculator = ServiceProviderSingleton.Instance.ServiceProvider.GetService<ITaxCalculator>();
        }

        [SetUp]
        public void Setup()
        {
            _goods = new List<IGood>() {
                new Good() { Id=1, Name="box of chocolates" , Imported = true, Price = 11.25f, Quantity=1, Category = Category.Food },
                new Good() { Id=2, Name="bottle of perfume" , Imported = false, Price = 18.99f, Quantity=1, Category = Category.Perfume },
                new Good() { Id=3, Name="packet of headache pills" , Imported = false, Price = 9.75f, Quantity=1, Category = Category.Medicine },
                new Good() { Id=4, Name="bottle of perfume" , Imported = true, Price = 27.99f, Quantity=1, Category = Category.Perfume }
            };
        }

        [Test,Category("Files")]
        public void ShouldNotShoppingBasketBeEmpty()
        {
            Assert.IsTrue(_goods.Count > 0);
        }
        
        [Test,Category("CalculatePriceWithTaxesGood")]
        public void ShouldNotAddImportedTaxesNorSalesTaxes()
        {
            var good = _goods.Where(g => g.Id == 3).FirstOrDefault();
            Assert.IsFalse(good.Imported);
            var priceBeforeTaxes = good.Price;

            _taxCalculator.CalculatePriceWithTaxesGood(good);
            Assert.IsTrue(good.Price == priceBeforeTaxes);
        }

        [Test,Category("CalculatePriceWithTaxesGood")]
        public void ShouldAddSaleTaxes()
        {
            var good = _goods.Where(g => g.Id == 2).FirstOrDefault();
            var priceBeforeTaxes = RoundToTwoDecimals(good.Price);
            var tax = 1.90; //CalculateRelativePercentage(10, priceBeforeTaxes);
            var shouldFinalPrice = RoundToTwoDecimals(priceBeforeTaxes + tax);
            _taxCalculator.CalculatePriceWithTaxesGood(good);
            Assert.IsTrue(good.Price > priceBeforeTaxes);
            Assert.IsTrue(good.Price == shouldFinalPrice);
        }

        [Test,Category("CalculatePriceWithTaxesGood")]
        public void ShouldAddImportedTaxes()
        {
            var good = _goods.Where(g => g.Id == 1).FirstOrDefault();
            var priceBeforeTaxes = RoundToTwoDecimals(good.Price);
            var tax = 0.55;//CalculateRelativePercentage(5, priceBeforeTaxes);
            var shouldFinalPrice = RoundToTwoDecimals(priceBeforeTaxes + tax);
            _taxCalculator.CalculatePriceWithTaxesGood(good);
            Assert.IsTrue(good.Price > priceBeforeTaxes);
            Assert.IsTrue(good.Price == shouldFinalPrice);
        }

        [Test,Category("CalculatePriceWithTaxesGood")]
        public void ShouldAddImportedAndSalesTaxes()
        {
            var good = _goods.Where(g => g.Id == 4).FirstOrDefault();
            var priceBeforeTaxes = RoundToTwoDecimals(good.Price);
            var tax = 4.20; //CalculateRelativePercentage(15, priceBeforeTaxes);
            var shouldFinalPrice = RoundToTwoDecimals(priceBeforeTaxes + tax);
            _taxCalculator.CalculatePriceWithTaxesGood(good);
            Assert.IsTrue(good.Price > priceBeforeTaxes);
            Assert.IsTrue(good.Price == shouldFinalPrice);
        }

        //ToDo
        [Test, Category("CalculateTotal")]
        public void ShouldTotalPriceIncludeTaxes()
        {
            _taxCalculator.CalculateTotal(_goods);

            var totalPrice = 74.63;
            var totalTaxes = 6.65;

            Assert.AreEqual(totalPrice, _taxCalculator.TotalPrice);
            Assert.AreEqual(totalTaxes, _taxCalculator.TotalTaxValue);
        }

        [Test, Category("CalculateTotal")]
        public void ShouldTotalPriceIncludeTaxesAndQuantities()
        {
            _taxCalculator = new TaxCalculator();
            var newGoods = new List<IGood>() { new Good() { Id = 5, Name = "bread", Imported = false, Price = 1.42f, Quantity = 2, Category = Category.Food } };
            _taxCalculator.CalculateTotal(newGoods);

            var totalPrice = 2.84;
            var totalTaxes = 0;

            Assert.AreEqual(totalPrice, RoundToTwoDecimals(_taxCalculator.TotalPrice));
            Assert.AreEqual(totalTaxes, RoundToTwoDecimals(_taxCalculator.TotalTaxValue));
        }

        [TearDown]
        public void AfterEachTest()
        {
            _goods = null;
        }

    }
}