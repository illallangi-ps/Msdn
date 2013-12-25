using System.Collections.Generic;
using Illallangi.Msdn.Model;

namespace Illallangi.Msdn.Client
{
    public interface IProductCategoryClient
    {
        IEnumerable<ProductCategory> GetProductCategories(
            string brand = null,
            string localeCode = null,
            string resource = null);
    }
}