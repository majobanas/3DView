using System;
using System.Windows.Forms;
namespace ThreeDViewCS {
    public class Program {
        static void Main(string[] args) {
            string applicationPath = args.Length != 0 ? args[0] : "NULL";
            string entryObjects = args.Length > 1 ? args[1] : "NULL";
            string vaultGUID = args.Length > 2 ? args[2] : "NULL";
            string connectionNames = args.Length > 3 ? args[3] : "NULL";


            Console.WriteLine("Connection names: " + connectionNames);

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
