using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Package_Launcher
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                ActivateUIByProtocol();
            }
            else
            {
                DoSomethingElse(args[0]);
            }
        }

        static void ActivateUIByProtocol()
        {
            //Here the goal is to launch the uwp project, which in turn will establish a connection with the .net framework fulltrust project.
            // If you set a breakpoint in the constructor App() of app.xaml.cs file (uwp project,
            //You will see that it is being called twice.
            Task t = Task.Run(async () =>
               await Windows.System.Launcher.LaunchUriAsync(new Uri(@"troubleshootappserviceconnection:"))
                  );
            t.Wait();
            //If Visual studio complains about Launcher existing in two assemblies,
            //remove the 'windows' reference of 
            //C:\Program Files (x86)\Windows Kits\10\UnionMetadata\10.0.17763.0\Facade\windows.winmd
            //and put it again

            //Other required references are listed in the git readme file
        
    }
        static void DoSomethingElse(string arg)
        {

        }
    }
}
