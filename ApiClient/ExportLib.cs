using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueMoon.ClientApp
{
    public static class ExportLib
    {
        public static void Run()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new Main());
        }
    }
}
