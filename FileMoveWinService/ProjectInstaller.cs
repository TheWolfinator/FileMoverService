using System.ComponentModel;
using System.ServiceProcess;
using System.Configuration.Install;

namespace FileMoveWinService
{
    [RunInstaller(true)]
    public class ProjectInstaller : Installer
    {
        private ServiceProcessInstaller processInstaller;
        private ServiceInstaller serviceInstaller;

        public ProjectInstaller()
        {
            // Service will run under LocalSystem account
            processInstaller = new ServiceProcessInstaller
            {
                Account = ServiceAccount.LocalSystem
            };

            // Service info
            serviceInstaller = new ServiceInstaller
            {
                ServiceName = "FileMoverService",
                DisplayName = "File Mover Service",
                Description = "Moves files from C:\\Folder1 to C:\\Folder2 and logs events",
                StartType = ServiceStartMode.Automatic
            };

            Installers.Add(processInstaller);
            Installers.Add(serviceInstaller);
        }
    }
}