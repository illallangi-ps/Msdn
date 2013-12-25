using System.Collections.Generic;
using System.Management.Automation;
using Illallangi.Msdn.Client;

namespace Illallangi.Msdn.Powershell
{
    [Cmdlet(VerbsCommon.Get, MsdnNouns.MsdnFile)]
    public sealed class GetMsdnFile : MsdnCmdlet<IFileSearchClient>
    {
        [Parameter(Mandatory = true)]
        public int ProductFamilyId { get; set; }

        protected override IEnumerable<object> Process(IFileSearchClient client)
        {
            return client.GetFileSearchResult(this.ProductFamilyId);
        }
    }
}
