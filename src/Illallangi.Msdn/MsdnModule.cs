using System.Configuration;
using System.Reflection;
using Illallangi.Msdn.Client;
using Illallangi.Msdn.Config;
using log4net.Config;
using Ninject;
using Ninject.Modules;
using RestSharp;

namespace Illallangi.Msdn
{
    public sealed class MsdnModule : NinjectModule
    {
        public override void Load()
        {
            XmlConfigurator.Configure(
                Assembly
                    .GetExecutingAssembly()
                    .GetManifestResourceStream(
                        string.Format(
                            "{0}.Log4Net.config", 
                            Assembly.GetExecutingAssembly().GetName().Name)));

            this.Bind<IProductCategoryClient>()
                .To<MsdnClient>()
                .InSingletonScope();
            
            this.Bind<IProductFamilyClient>()
                .To<MsdnClient>()
                .InSingletonScope();
            
            this.Bind<IFileSearchClient>()
                .To<MsdnClient>()
                .InSingletonScope();

            this.Bind<IRestClient>()
                .ToMethod(cx => new RestClient(cx.Kernel.Get<IConfig>().BaseUrl))
                .InSingletonScope();

            this.Bind<IConfig>()
                .ToMethod(
                    cx =>
                        (MsdnConfig)
                            ConfigurationManager.OpenExeConfiguration(
                                Assembly.GetExecutingAssembly().Location).GetSection("MsdnConfig"));
        }
    }
}