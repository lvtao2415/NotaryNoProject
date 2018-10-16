using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;

namespace NotaryNoProject.GloBal
{
    public class MainWindow
    {
        public static Form frmActive;//焦点窗口

        public static string _newNo;
        public static void show(GroupBox grp1, GroupBox grp2)
        {
            grp1.Visible = true;
            grp2.Visible = true;
        }
    }
}
