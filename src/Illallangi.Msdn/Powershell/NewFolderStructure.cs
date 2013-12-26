using System.Collections.Generic;
using System.Management.Automation;
using Illallangi.Msdn.Client;

namespace Illallangi.Msdn.Powershell
{
    [Cmdlet(VerbsCommon.New, "FolderStructure")]
    public sealed class NewFolderStructure : MsdnCmdlet<IFolderStructureClient>
    {
        protected override IEnumerable<object> Process(IFolderStructureClient client)
        {
            return client.NewFolderStructure();
        }
    }
}