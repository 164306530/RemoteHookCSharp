using EasyHook;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;

namespace RemoteHook
{
    class Program
    {
        static String ChannelName = null;

        static void Main(string[] args)
        {
            int TargetPID;
            string targetExe = null;
            // Will contain the name of the IPC server channel
            string channelName = null;
            Process[] localByName = Process.GetProcessesByName("NotePad");
            TargetPID = localByName[0].Id;
            RemoteHooking.IpcCreateServer<TestLib.FileMonInterface>(ref ChannelName, WellKnownObjectMode.SingleCall);
            string injectionLibrary = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "TestLib.dll");

            RemoteHooking.Inject(
                        TargetPID,
                        injectionLibrary,
                        injectionLibrary,
                        ChannelName);
            Console.WriteLine("Injected to process {0}", TargetPID);
            Console.WriteLine("<Press any key to exit>");
            Console.ReadKey();
        }
    }
}
