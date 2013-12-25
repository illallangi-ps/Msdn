using System.Collections.Generic;
using System.Globalization;
using Illallangi.Msdn.Config;
using Illallangi.Msdn.Model;
using Ninject.Extensions.Logging;
using RestSharp;

namespace Illallangi.Msdn.Client
{
    public sealed class MsdnClient : IProductCategoryClient, IFileSearchClient, IProductFamilyClient
    {
        private readonly IRestClient currentRestClient;
        private readonly IConfig currentConfig;
        private readonly ILogger currentLogger;

        public MsdnClient(IRestClient restClient, IConfig config, ILogger logger)
        {
            this.currentRestClient = restClient;
            this.currentConfig = config;
            this.currentLogger = logger;
        }

        public IEnumerable<ProductCategory> GetProductCategories(
            string brand = null,
            string localeCode = null,
            string resource = null)
        {
            var finalBrand = brand ?? this.Config.Brand;
            var finalLocaleCode = localeCode ?? this.Config.LocaleCode;
            var finalResource = resource ?? this.Config.Uris.GetProductCategories;

            this.Logger.Debug(
                @"MsdnClient.GetProductCategories(""{0}"",""{1}"",""{2}"");",
                finalBrand,
                finalLocaleCode,
                finalResource);

            return this.RestClient.HttpGet<ProductCategory[]>(
                finalResource,
                new Dictionary<string, string>
                {
                    { "brand", finalBrand },
                    { "localeCode", finalLocaleCode },
                });
        }

        public IEnumerable<ProductFamily> GetProductFamiliesForCategory(
            int categoryId,
            string brand = null,
            string localeCode = null,
            string resource = null)
        {
            var finalCategoryId = categoryId.ToString(CultureInfo.InvariantCulture);
            var finalBrand = brand ?? this.Config.Brand;
            var finalLocaleCode = localeCode ?? this.Config.LocaleCode;
            var finalResource = resource ?? this.Config.Uris.GetProductFamiliesForCategory;

            this.Logger.Debug(
                @"MsdnClient.GetProductFamiliesForCategory(""{0}"",""{1}"",""{2}"",""{3}"");",
                finalCategoryId,
                finalBrand,
                finalLocaleCode,
                finalResource);

            return this.RestClient.HttpGet<ProductFamily[]>(
                finalResource,
                new Dictionary<string, string>
                {
                    { "categoryId", finalCategoryId },
                    { "brand", finalBrand },
                    { "localeCode", finalLocaleCode },
                });
        }

        public IEnumerable<File> GetFileSearchResult(
            int productFamilyId,
            string brand = null,
            string languages = null,
            int? pageSize = null,
            string resource = null)
        {
            var finalProductFamilyId = productFamilyId.ToString(CultureInfo.InvariantCulture);
            var finalBrand = brand ?? this.Config.Brand;
            var finalLanguages = languages ?? this.Config.Languages;
            var finalPageSize = pageSize.HasValue ? pageSize.Value : this.Config.PageSize;
            var finalResource = resource ?? this.Config.Uris.GetFileSearchResult;
            
            this.Logger.Debug(
                @"MsdnClient.GetFileSearchResult(""{0}"",""{1}"",""{2}"",""{3}"",""{4}"");",
                finalProductFamilyId,
                finalBrand,
                finalLanguages,
                finalPageSize,
                finalResource);

            return this.RestClient.HttpPost<FileSearchResult>(
                finalResource,
                new
                {
                    ProductFamilyId = finalProductFamilyId,
                    Brand = finalBrand,
                    Languages = finalLanguages,
                    PageSize = finalPageSize,
                }).Files;
        }

        public IRestClient RestClient
        {
            get { return this.currentRestClient; }
        }

        public IConfig Config
        {
            get { return this.currentConfig; }
        }

        public ILogger Logger
        {
            get { return currentLogger; }
        }
    }
}