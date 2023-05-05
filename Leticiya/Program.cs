using Npgsql;
using System;
using System.Windows.Forms;

namespace Leticiya
{
    static class Program
    {
        public static FormMain formMain;
        public static NpgsqlConnection connection;

        public static string ver = "Ver. Beta 2.0.0 L";
        public const string key = "asK/332dsas27dgagf2Wk9#";

        public static bool SQLStat = false;

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());
        }
    }
}
