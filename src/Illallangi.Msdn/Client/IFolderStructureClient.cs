using System.Collections.Generic;

namespace Illallangi.Msdn.Client
{
    public interface IFolderStructureClient
    {
        IEnumerable<object> NewFolderStructure();
    }
}