﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic.ApplicationServices;
using NLog;
using Squirrel;

namespace ZSpriteTools
{
    static class Program
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
#if !DEBUG
            SquirrelAwareApp.HandleEvents(onAppUpdate: UpdateForm.OnAppUpdate,
                                          onAppUninstall: UpdateForm.OnAppUninstall,
                                          onInitialInstall: UpdateForm.OnInitialInstall);
#endif

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var args = Environment.GetCommandLineArgs();
            SingleInstanceController controller = new SingleInstanceController();
            controller.Run(args);
        }

        public class SingleInstanceController : WindowsFormsApplicationBase
        {
            public SingleInstanceController()
            {
                IsSingleInstance = true;

                StartupNextInstance += StartupAlreadyRunningHandler;
            }

            void StartupAlreadyRunningHandler(object sender, StartupNextInstanceEventArgs e)
            {
                ZSpriteToolForm form = MainForm as ZSpriteToolForm;

                if (e.CommandLine.Count > 1)
                {
                    form.LoadFile(e.CommandLine[1]);
                }
            }

            protected override void OnCreateMainForm()
            {
                MainForm = new ZSpriteToolForm();
            }
        }
    }
}
