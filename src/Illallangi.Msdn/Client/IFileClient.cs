using System.Collections.Generic;
using Illallangi.Msdn.Model;

namespace Illallangi.Msdn.Client
{
    public interface IFileClient
    {
        IEnumerable<File> GetFileSearchResult(
            int productFamilyId,
            string brand = null,
            string languages = null,
            int? pageSize = null,
            string resource = null);

        IEnumerable<File> GetFileSearchResult(
            int productFamilyId,
            out string json,
            string brand = null,
            string languages = null,
            int? pageSize = null,
            string resource = null);

        File GetFileDetail(
            int fileId,
            string brand = null,
            string resource = null);

        File GetFileDetail(
            int fileId,
            out string json,
            string brand = null,
            string resource = null);
    }
}