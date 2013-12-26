using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using Illallangi.Msdn.Config;
using System.IO;
using log4net;
using Ninject.Activation.Caching;
using Ninject.Extensions.Logging;


namespace Illallangi.Msdn.Client
{
    public sealed class RestCache : IRestCache
    {
        private readonly IConfig currentConfig;
        private ILogger currentLogger;

        public RestCache(IConfig config, ILogger logger)
        {
            this.currentConfig = config;
            this.currentLogger = logger;
        }

        public RestResult<T> CacheCheck<T>(string call, Func<RestResult<T>> callFunc) where T : class
        {
            var cacheFile = Path.Combine(this.Config.CacheDir, string.Join(".", call.GetSha1Hash(), "json"));
            
            if (!File.Exists(cacheFile))
            {
                this.Logger.Debug("Cache miss on {0} ({1})", call, cacheFile);
                var result = callFunc();
                File.WriteAllText(cacheFile, result.Raw);
            }
            else
            {
                this.Logger.Debug("Cache hit on {0} ({1})", call, cacheFile);
            }
            
            return new RestResult<T>(File.ReadAllText(cacheFile));
        }

        private IConfig Config
        {
            get { return this.currentConfig; }
        }

        public ILogger Logger
        {
            get { return this.currentLogger; }
        }
    }

    public static class StringExtensions
    {
        public static string GetSha1Hash(this string data)
        {
            var sha1 = SHA1.Create();
            var hashData = sha1.ComputeHash(Encoding.Default.GetBytes(data));
            var returnValue = new StringBuilder();

            foreach (var t in hashData)
            {
                returnValue.Append(t.ToString(CultureInfo.InvariantCulture));
            }

            return returnValue.ToString();
        }
    }
}