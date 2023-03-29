using Npgsql;
using System;
using System.Windows.Forms;

namespace Leticiya
{
    static class Program
    {
        public static FormMain formMain;
        public static NpgsqlConnection connection;

        public static string ver = "Ver. Alpha 0.2.0 L";
        public const string key = "asK/332dsas27dgagf2Wk9#";

        public static bool SQLStat = false;
        public static bool flagUpdateAdmin = false;
        public static bool flagSelectUser = false;

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());
        }
    }
}
