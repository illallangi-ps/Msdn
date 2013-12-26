namespace Illallangi.Msdn.Config
{
    public interface IConfigUris
    {
        string GetProductCategories { get; }
        string GetProductFamiliesForCategory { get; }
        string GetFileSearchResult { get; }
        string GetFileDetail { get; }
    }
}