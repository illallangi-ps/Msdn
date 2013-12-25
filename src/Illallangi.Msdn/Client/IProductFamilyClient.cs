using System.Collections.Generic;
using Illallangi.Msdn.Model;

namespace Illallangi.Msdn.Client
{
    public interface IProductFamilyClient
    {
        IEnumerable<ProductFamily> GetProductFamiliesForCategory(
            int categoryId,
            string brand = null,
            string localeCode = null,
            string resource = null);
    }
}