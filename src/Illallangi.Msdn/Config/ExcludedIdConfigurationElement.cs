using System;
using System.Configuration;

namespace Illallangi.Msdn.Config
{
    public sealed class ExcludedIdConfigurationElement : ConfigurationElement
    {
        [ConfigurationProperty("Id", IsRequired = true)]
        public int CategoryId
        {
            get { return Convert.ToInt32(this["Id"]); }
        }
    }
}