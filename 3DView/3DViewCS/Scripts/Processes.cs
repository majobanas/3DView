using System;
using System.IO;
using System.Diagnostics;

namespace ThreeDViewCS {
    public class Processes {

        public static void Start(string pFilePath, bool pWritable, EventHandler pWritableExitedCallback) {
            Process currentProcess = new Process();
            currentProcess.EnableRaisingEvents = true;
            currentProcess.StartInfo.FileName = Path.GetFullPath(pFilePath);
            Console.WriteLine("Processes: Started as: " + (pWritable ? "writeable" : "read-only"));
            if (pWritable) {
                currentProcess.Exited += pWritableExitedCallback; //new EventHandler(pWritableExitedCallback);
            }
            currentProcess.Start();
            // Notify that process has exited
            currentProcess.Exited += (s, e) => { Console.WriteLine("Processes: Process exited"); };//new EventHandler(OnProcessExit);
        }

        public static void OnProcessExit(object sender, EventArgs pEvent) {
            Console.WriteLine("Processes: Process exited");
        }
    }
}
