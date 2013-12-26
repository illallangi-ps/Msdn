using System.Configuration;

namespace Illallangi.Msdn.Config
{
    public sealed class ExcludedIdConfigurationElementCollection : ConfigurationElementCollection
    {
        public ExcludedIdConfigurationElement this[int index]
        {
            get
            {
                return base.BaseGet(index) as ExcludedIdConfigurationElement;
            }
            set
            {
                if (base.BaseGet(index) != null)
                {
                    base.BaseRemoveAt(index);
                }
                this.BaseAdd(index, value);
            }
        }
        protected override ConfigurationElement CreateNewElement()
        {
            return new ExcludedIdConfigurationElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ExcludedIdConfigurationElement)element).CategoryId;
        }
    }
}