using System;
using System.Windows.Forms;
namespace ThreeDViewCS {
    public class Program {
        static void Main(string[] args) {
            /*string[] inputArgs = new string[0];
            if (args.Length != 0) {
                inputArgs = args[0].Split('%');
                foreach (string ia in inputArgs)
                    Console.WriteLine(ia);
            }
            Console.WriteLine("-------------------------------------------------");
            string applicationPath = inputArgs.Length != 0 ? inputArgs[0] : "NULL";
            Console.WriteLine("applicationPath: " + applicationPath);
            Console.WriteLine("-------------------------------------------------");
            string entryObjects = inputArgs.Length > 1 ? inputArgs[1] : "NULL";
            Console.WriteLine("entryObjects: " + entryObjects);
            Console.WriteLine("-------------------------------------------------");
            string vaultGUID = inputArgs.Length > 2 ? inputArgs[2] : "NULL";
            Console.WriteLine("vaultGUID: " + vaultGUID);
            Console.WriteLine("-------------------------------------------------");*/

            Program program = new Program(Parser.InputArgs(args)/*applicationPath, entryObjects, vaultGUID*/);
            program.Run();
        }

        private ThreeDViewForm viewForm = null;

        public Program(string[] pInputArgs/*string pApplicationPath, string pEntryObjects, string pVaultGUID*/) {
            Parser.Initialize(pInputArgs[0], pInputArgs[1], pInputArgs[2]/*pApplicationPath, pEntryObjects, pVaultGUID*/);

            viewForm = new ThreeDViewForm();
            //viewForm.Show();
        }

        private void Run() {
            while (viewForm.Visible) {
                Application.DoEvents();
                viewForm.threeDView.Run();
            }
        }

    }
}
