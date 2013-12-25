using System;
using System.Collections.Generic;
using System.Management.Automation;
using Illallangi.Msdn.Config;
using Illallangi.Msdn.Powershell;
using Ninject;
using Ninject.Extensions.Logging.Log4net;

namespace Illallangi.Msdn
{
    [Cmdlet(VerbsCommon.Get, MsdnNouns.Abstract)]
    public abstract class MsdnCmdlet<T> : PSCmdlet where T: class
    {
        private StandardKernel currentKernel;
        private MsdnModule currentModule;

        private MsdnModule Module
        {
            get
            {
                return this.currentModule ?? (this.currentModule = new MsdnModule());
            }
        }

        private StandardKernel Kernel
        {
            get
            {
                return this.currentKernel ?? (this.currentKernel = new StandardKernel(this.Module, new Log4NetModule()));
            }
        }

        protected override void ProcessRecord()
        {
            try
            {
                this.WriteObject(this.Process(this.Kernel.Get<T>()), true);
            }
            catch (Exception failure)
            {
                this.WriteError(new ErrorRecord(
                    failure,
                    failure.Message,
                    ErrorCategory.InvalidResult,
                    this.Kernel.Get<IConfig>()));
            }
        }

        protected abstract IEnumerable<Object> Process(T client);
    }
}