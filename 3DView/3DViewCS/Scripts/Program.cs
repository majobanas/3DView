using System;
using System.Windows.Forms;
namespace ThreeDViewCS {
    public class Program {
        static void Main(string[] args) {
            string[] inputArgs = new string[0];
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
            Console.WriteLine("-------------------------------------------------");
            string connectionNames = inputArgs.Length > 3 ? inputArgs[3] : "NULL";
            Console.WriteLine("connectionNames: " + connectionNames);

            Program program = new Program(applicationPath, entryObjects, vaultGUID);
            program.Run();
        }

        private ThreeDViewForm viewForm = null;

        public Program(string pApplicationPath, string pEntryObjects, string pVaultGUID) {
            Parser.Initialize(pApplicationPath, pEntryObjects, pVaultGUID);

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
