using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;

namespace Illallangi.Msdn.Config
{
    public sealed class MsdnConfig : ConfigurationSection, IConfig
    {
        private string currentCacheDir;

        [ConfigurationProperty("Brand", DefaultValue = @"MSDN", IsRequired = false)]
        public string Brand
        {
            get { return (string) this["Brand"]; }
        }

        [ConfigurationProperty("BaseUrl", DefaultValue = @"http://msdn.microsoft.com/en-us/subscriptions/json/",
            IsRequired = false)]
        public string BaseUrl
        {
            get { return (string) this["BaseUrl"]; }
        }

        [ConfigurationProperty("LocaleCode", DefaultValue = @"en-us", IsRequired = false)]
        public string LocaleCode
        {
            get { return (string) this["LocaleCode"]; }
        }

        [ConfigurationProperty("Languages", DefaultValue = @"en", IsRequired = false)]
        public string Languages
        {
            get { return (string) this["Languages"]; }
        }

        [ConfigurationProperty("PageSize", DefaultValue = @"100", IsRequired = false)]
        public int PageSize
        {
            get { return Convert.ToInt32(this["PageSize"]); }
        }

        
        [ConfigurationProperty("CacheDir", DefaultValue = @"%localappdata%\Illallangi Enterprises\MSDN REST Cache", IsRequired = false)]
        public string CacheDir
        {
            get
            {
                if (null == this.currentCacheDir)
                {
                    this.currentCacheDir = Environment.ExpandEnvironmentVariables((string)this["CacheDir"]);
                    if (!Directory.Exists(this.currentCacheDir))
                    {
                        Directory.CreateDirectory(this.currentCacheDir);
                    }
                }
                return this.currentCacheDir;
            }
        }

        public IEnumerable<int> ExcludedCategoryIds
        {
            get
            {
                return (from object excludedId in this.ExcludedCategoryIdCollection
                    select ((ExcludedIdConfigurationElement) excludedId).CategoryId);
            }
        }

        public IEnumerable<int> ExcludedFamilyIds
        {
            get
            {
                return (from object excludedId in this.ExcludedFamilyIdCollection
                        select ((ExcludedIdConfigurationElement)excludedId).CategoryId);
            }
        }

        public IEnumerable<int> ExcludedFileIds
        {
            get
            {
                return (from object excludedId in this.ExcludedFileIdCollection
                        select ((ExcludedIdConfigurationElement)excludedId).CategoryId);
            }
        }
        
        [ConfigurationProperty("ExcludedCategoryIds")]
        public ExcludedIdConfigurationElementCollection ExcludedCategoryIdCollection
        {
            get
            {
                return (ExcludedIdConfigurationElementCollection)this["ExcludedCategoryIds"] ??
                       new ExcludedIdConfigurationElementCollection();
            }
        }

        [ConfigurationProperty("ExcludedFamilyIds")]
        public ExcludedIdConfigurationElementCollection ExcludedFamilyIdCollection
        {
            get
            {
                return (ExcludedIdConfigurationElementCollection)this["ExcludedFamilyIds"] ??
                       new ExcludedIdConfigurationElementCollection();
            }
        }

        [ConfigurationProperty("ExcludedFileIds")]
        public ExcludedIdConfigurationElementCollection ExcludedFileIdCollection
        {
            get
            {
                return (ExcludedIdConfigurationElementCollection)this["ExcludedFileIds"] ??
                       new ExcludedIdConfigurationElementCollection();
            }
        }

        public IConfigUris Uris
        {
            get { return new MsdnConfigUris(); }
        }
    }
}