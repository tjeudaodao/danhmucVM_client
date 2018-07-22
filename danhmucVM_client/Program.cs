using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace danhmucVM_client
{
    static class Program
    {
        public static bool moFrom { get; set; }
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            moFrom = false;
            Application.Run(new form_login());
            if (moFrom)
            {
                Application.Run(new Formchinh());
            }
        }
    }
}
