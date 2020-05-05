using System;
using System.IO;
using System.Diagnostics;

namespace ThreeDViewCS {
    public class Processes {

        public static void Start(string pFilePath, bool pWriteable, EventHandler pEventHandler) {
            Process currentProcess = new Process();
            currentProcess.StartInfo.FileName = Path.GetFullPath(pFilePath);
            Console.WriteLine("Processes: Started as: " + (pWriteable ? "writeable" : "read-only"));
            if (pWriteable) {
                currentProcess.EnableRaisingEvents = true;
                currentProcess.Exited += new EventHandler(pEventHandler);
            }
            currentProcess.Start();
            currentProcess.Exited += new EventHandler(OnProcessExit);
        }

        public static void OnProcessExit(object sender, EventArgs pEvent) {
            Console.WriteLine("Processes: Process exited");
        }
    }
}
