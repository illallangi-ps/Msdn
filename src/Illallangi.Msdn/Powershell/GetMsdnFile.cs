using System;
using System.Collections.Generic;
using System.Management.Automation;
using Illallangi.Msdn.Client;

namespace Illallangi.Msdn.Powershell
{
    [Cmdlet(VerbsCommon.Get, MsdnNouns.MsdnFile, DefaultParameterSetName = "ProductFamilyId")]
    public sealed class GetMsdnFile : MsdnCmdlet<IFileClient>
    {
        [Parameter(Mandatory = true, ParameterSetName = "ProductFamilyId")]
        public int ProductFamilyId { get; set; }

        [Parameter(Mandatory = true, ParameterSetName = "FileId")]
        public int FileId { get; set; }

        protected override IEnumerable<object> Process(IFileClient client)
        {
            switch (this.ParameterSetName)
            {
                case "ProductFamilyId":
                    return client.GetFileSearchResult(this.ProductFamilyId);
                case "FileId":
                    return new[] {client.GetFileDetail(this.FileId)};
                default:
                    throw new NotImplementedException(this.ParameterSetName);
            }
            
        }
    }
}
