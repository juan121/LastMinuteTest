
using static SalesTaxes.Utilities.Utilities;

namespace SalesTaxes
{
    public interface IGood
    {
        int Id { get; set; }
        string Name { get; set; }
        int Quantity { get; set; }
        bool Imported { get; set; }
        float Price { get; set; }
        Category CategoryId { get; set; }
    }
}
