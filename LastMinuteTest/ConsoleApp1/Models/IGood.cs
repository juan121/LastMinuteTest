
using static SalesTaxes.Utilities.Utilities;

namespace Models.SalesTaxes
{
    public interface IGood
    {
        int Id { get; set; }
        string Name { get; set; }
        int Quantity { get; set; }
        bool Imported { get; set; }
        double Price { get; set; }
        Category Category { get; set; }

        double? Tax { get; set; }
    }
}
