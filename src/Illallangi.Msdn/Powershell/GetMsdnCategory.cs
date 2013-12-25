using System.Collections.Generic;
using System.Management.Automation;
using Illallangi.Msdn.Client;

namespace Illallangi.Msdn.Powershell
{
    [Cmdlet(VerbsCommon.Get, MsdnNouns.MsdnCategory)]
    public sealed class GetMsdnCategory : MsdnCmdlet<IProductCategoryClient>
    {
        protected override IEnumerable<object> Process(IProductCategoryClient client)
        {
            return client.GetProductCategories();
        }
    }
}
