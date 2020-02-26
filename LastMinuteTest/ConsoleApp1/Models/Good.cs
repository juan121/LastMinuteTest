using static SalesTaxes.Utilities.Utilities;

namespace Models.SalesTaxes
{
    public class Good : IGood
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public bool Imported { get; set; }
        public double Price { get; set; }
        public Category Category { get; set; }
        public double? Tax { get; set; }

    }
}
