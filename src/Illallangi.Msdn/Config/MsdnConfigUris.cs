namespace Illallangi.Msdn.Config
{
    public sealed class MsdnConfigUris : IConfigUris
    {
        public string GetProductCategories
        {
            get { return @"GetProductCategories"; }
        }

        public string GetProductFamiliesForCategory
        {
            get { return @"GetProductFamiliesForCategory"; }
        }

        public string GetFileSearchResult
        {
            get { return @"GetFileSearchResult"; }
        }

        public string GetFileDetail
        {
            get { return @"GetFileDetail"; }
        }
    }
}