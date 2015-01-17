using System.Collections.Generic;
using System.Management.Automation;
using Illallangi.Msdn.Client;

namespace Illallangi.Msdn.Powershell
{
    [Cmdlet(VerbsCommon.Get, MsdnNouns.MsdnFamily)]
    public sealed class GetMsdnFamily : MsdnCmdlet<IProductFamilyClient>
    {
        [Alias("ProductGroupId")]
        [Parameter(Mandatory = true)]
        public int CategoryId { get; set; }

        protected override IEnumerable<object> Process(IProductFamilyClient client)
        {
            return client.GetProductFamiliesForCategory(this.CategoryId);
        }
    }
}
