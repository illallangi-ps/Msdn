using System.Collections.Generic;
using Illallangi.Msdn.Model;

namespace Illallangi.Msdn.Client
{
    public interface IFileSearchClient
    {
        IEnumerable<File> GetFileSearchResult(
            int productFamilyId,
            string brand = null,
            string languages = null,
            int? pageSize = null,
            string resource = null);
    }
}