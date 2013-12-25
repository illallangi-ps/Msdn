namespace Illallangi.Msdn.Config
{
    public interface IConfig
    {
        string Brand { get; }
        string BaseUrl { get; }
        string LocaleCode { get; }
        IConfigUris Uris { get; }
        string Languages { get; }
        int PageSize { get; }
    }
}