using static SalesTaxes.Utilities.Utilities;

namespace SalesTaxes
{
    public class Good : IGood
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public bool Imported { get; set; }
        public float Price { get; set; }
        public Category CategoryId { get; set; }

    }
}
