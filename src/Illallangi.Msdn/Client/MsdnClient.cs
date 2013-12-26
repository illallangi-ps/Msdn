using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Illallangi.Msdn.Config;
using Illallangi.Msdn.Model;
using Ninject.Extensions.Logging;
using RestSharp;

namespace Illallangi.Msdn.Client
{
    public sealed class MsdnClient : IProductCategoryClient, IFileClient, IProductFamilyClient
    {
        private readonly IRestClient currentRestClient;
        private readonly IConfig currentConfig;
        private readonly ILogger currentLogger;
        private readonly IRestCache currentRestCache;

        public MsdnClient(IRestClient restClient, IRestCache restCache, IConfig config, ILogger logger)
        {
            this.currentRestClient = restClient;
            this.currentConfig = config;
            this.currentLogger = logger;
            this.currentRestCache = restCache;
        }

        public IEnumerable<ProductCategory> GetProductCategories(
            string brand = null,
            string localeCode = null,
            string resource = null)
        {
            string json;
            return this.GetProductCategories(out json, brand, localeCode, resource);
        }

        public IEnumerable<ProductCategory> GetProductCategories(
            out string json,
            string brand = null,
            string localeCode = null,
            string resource = null)
        {
            var finalBrand = brand ?? this.Config.Brand;
            var finalLocaleCode = localeCode ?? this.Config.LocaleCode;
            var finalResource = resource ?? this.Config.Uris.GetProductCategories;

            var call = string.Format(@"MsdnClient.GetProductCategories(""{0}"",""{1}"",""{2}"");",
                finalBrand,
                finalLocaleCode,
                finalResource);

            this.Logger.Debug(
                call);

            var restResult = this.RestCache.CacheCheck<ProductCategory[]>(
                call,
                () => this.RestClient.HttpGet<ProductCategory[]>(
                    finalResource,
                    new Dictionary<string, string>
                    {
                        {"brand", finalBrand},
                        {"localeCode", finalLocaleCode},
                    }));

            json = restResult.Raw;
            return restResult.Result.Where(productCategory => !this.Config.ExcludedCategoryIds.Contains(productCategory.ProductGroupId));
        }

        public IEnumerable<ProductFamily> GetProductFamiliesForCategory(
            int categoryId,
            string brand = null,
            string localeCode = null,
            string resource = null)
        {
            string json;
            return this.GetProductFamiliesForCategory(categoryId, out json, brand, localeCode, resource);
        }

        public IEnumerable<ProductFamily> GetProductFamiliesForCategory(
            int categoryId,
            out string json,
            string brand = null,
            string localeCode = null,
            string resource = null)
        {
            var finalCategoryId = categoryId.ToString(CultureInfo.InvariantCulture);
            var finalBrand = brand ?? this.Config.Brand;
            var finalLocaleCode = localeCode ?? this.Config.LocaleCode;
            var finalResource = resource ?? this.Config.Uris.GetProductFamiliesForCategory;

            var call = string.Format(
                @"MsdnClient.GetProductFamiliesForCategory(""{0}"",""{1}"",""{2}"",""{3}"");",
                finalCategoryId,
                finalBrand,
                finalLocaleCode,
                finalResource);

            this.Logger.Debug(call);

            var restResult = this.RestCache.CacheCheck<ProductFamily[]>(
                call,
                () => this.RestClient.HttpGet<ProductFamily[]>(
                    finalResource,
                    new Dictionary<string, string>
                    {
                        {"categoryId", finalCategoryId},
                        {"brand", finalBrand},
                        {"localeCode", finalLocaleCode},
                    }));

            json = restResult.Raw;
            return restResult.Result.Where(productFamily => !this.Config.ExcludedFamilyIds.Contains(productFamily.ProductFamilyId));
        }

        public IEnumerable<File> GetFileSearchResult(
            int productFamilyId,
            string brand = null,
            string languages = null,
            int? pageSize = null,
            string resource = null)
        {
            string json;
            return this.GetFileSearchResult(productFamilyId, out json, brand, languages, pageSize, resource);
        }

        public IEnumerable<File> GetFileSearchResult(
            int productFamilyId,
            out string json,
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

            var call = string.Format(
                @"MsdnClient.GetFileSearchResult(""{0}"",""{1}"",""{2}"",""{3}"",""{4}"");",
                finalProductFamilyId,
                finalBrand,
                finalLanguages,
                finalPageSize,
                finalResource);

            this.Logger.Debug(call);

            var restResult = this.RestCache.CacheCheck<FileSearchResult>(
                call,
                () => this.RestClient.HttpPost<FileSearchResult>(
                    finalResource,
                    new
                    {
                        ProductFamilyId = finalProductFamilyId,
                        Brand = finalBrand,
                        Languages = finalLanguages,
                        PageSize = finalPageSize,
                    }));

            json = restResult.Raw;
            return restResult.Result.Files.Where(file => !this.Config.ExcludedFileIds.Contains(file.FileId));
        }

        public File GetFileDetail(
            int fileId,
            string brand = null,
            string resource = null)
        {
            string json;
            return this.GetFileDetail(fileId, out json, brand, resource);
        }

        public File GetFileDetail(
            int fileId,
            out string json,
            string brand = null,
            string resource = null)
        {
            var finalFileId = fileId.ToString(CultureInfo.InvariantCulture);
            var finalBrand = brand ?? this.Config.Brand;
            var finalResource = resource ?? this.Config.Uris.GetFileDetail;

            var call = string.Format(
                @"MsdnClient.GetFileDetail(""{0}"",""{1}"",""{2}"");",
                finalFileId,
                finalBrand,
                finalResource);

            this.Logger.Debug(call);

            var restResult = this.RestCache.CacheCheck<File>(
                call,
                () => this.RestClient.HttpPost<File>(
                    finalResource,
                    new
                    {
                        fileId = finalFileId,
                        brand = finalBrand,
                    }));
            
            json = restResult.Raw;
            return restResult.Result;
        }

        private IRestClient RestClient
        {
            get { return this.currentRestClient; }
        }

        private IConfig Config
        {
            get { return this.currentConfig; }
        }

        private ILogger Logger
        {
            get { return this.currentLogger; }
        }

        private IRestCache RestCache
        {
            get { return this.currentRestCache; }
        }
    }
}