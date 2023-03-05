using libreria_desktop.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace libreria_desktop
{
    public class AppManager : ApplicationContext
    {
        public static empleadoDto user = new empleadoDto();
        public AppManager()
        {

            Boolean loop = true;
            do
            {
                views.layouts.login l = new views.layouts.login();
                l.ShowDialog();
                if (l.access)
                {
                    loop = true;
                    views.layouts.template t = new views.layouts.template();
                    t.ShowDialog();
                }
                else loop = false;

            } while (loop);

        }
    }
}
