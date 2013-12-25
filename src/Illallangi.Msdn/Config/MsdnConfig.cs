using System;
using System.Configuration;

namespace Illallangi.Msdn.Config
{
    public sealed class MsdnConfig : ConfigurationSection, IConfig
    {
        [ConfigurationProperty("Brand", DefaultValue = @"MSDN", IsRequired = false)]
        public string Brand
        {
            get { return (string)this["Brand"]; }
        }

        [ConfigurationProperty("BaseUrl", DefaultValue = @"http://msdn.microsoft.com/en-us/subscriptions/json/", IsRequired = false)]
        public string BaseUrl
        {
            get { return (string)this["BaseUrl"]; }
        }

        [ConfigurationProperty("LocaleCode", DefaultValue = @"en-us", IsRequired = false)]
        public string LocaleCode
        {
            get { return (string)this["LocaleCode"]; }
        }

        [ConfigurationProperty("Languages", DefaultValue = @"en", IsRequired = false)]
        public string Languages 
        {
            get { return (string)this["Languages"]; }
        }

        [ConfigurationProperty("PageSize", DefaultValue = @"100", IsRequired = false)]
        public int PageSize 
        {
            get { return Convert.ToInt32(this["PageSize"]); }
        }

        public IConfigUris Uris
        {
            get { return new MsdnConfigUris(); }
        }
    }
}