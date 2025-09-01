using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace FileMoveWinService
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
#if DEBUG
            var service = new FileMoverService.FileMoverService();
            service.StartServiceForDebug();
            using (var stopEvent = new System.Threading.ManualResetEvent(false))
            {
                Console.CancelKeyPress += (s, e) =>
                {
                    e.Cancel = true;  // prevent console from closing
                    stopEvent.Set();  // signal exit
                };
                stopEvent.WaitOne(); // blocks here until Ctrl+C
            }
            service.StopServiceForDebug();

#else
    ServiceBase.Run(new FileMoverService.FileMoverService());
#endif


        }
    }
}
