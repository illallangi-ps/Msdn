using System;
using Illallangi.Msdn.Model;

namespace Illallangi.Msdn.Client
{
    public interface IRestCache
    {
        RestResult<T> CacheCheck<T>(string call, Func<RestResult<T>> callFunc) where T : class;
    }
}