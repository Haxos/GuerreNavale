using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GuerreNavale
{
    static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            GameManager gm;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            gm = new GameManager();
            gm.GetInfo();
            if (gm.ValuesIsEntered)
            {
                gm.StartGame();
            }
            gm.Close();
            //Application.Run();
        }
    }
}
