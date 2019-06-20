using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.UserSkins;
using DevExpress.Skins;
using App.Views;
using NewServices;
using DevExpress.XtraEditors;
using NewServices.Services;

namespace App
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //WindowsFormsSettings.DefaultFont = new System.Drawing.Font("Comic Sans MS", 9);
            //WindowsFormsSettings.DefaultFont = new System.Drawing.Font("Microsoft YaHei", 9);
            WindowsFormsSettings.DefaultFont = new System.Drawing.Font("SimHei", 10);
            Application.SetCompatibleTextRenderingDefault(false);

            BonusSkins.Register();
            SkinManager.EnableFormSkins();

            // Register DependencyInjector 
            DependencyInjectorRegister.Init();
            ManagementService managementService = DependencyInjector.Retrieve<ManagementService>();
            var ctx = new DatabaseConfig(managementService);
            Main main = DependencyInjector.Retrieve<Main>();

            Application.Run(main);
        }
    }
}
