using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace WindowsForms.appCode
{
    public class PrintPreviewDialogEx : PrintPreviewDialog
    {
        public PrintPreviewDialogEx()
        {
            foreach (Control ctrl in base.Controls)
            {
                if (ctrl.GetType() == typeof(ToolStrip))
                {
                    ToolStrip tools = ctrl as ToolStrip;
                    tools.Items.RemoveAt(0);//删除打印预览原先的打印按钮
                    tools.Items.Insert(0, CreatePrintsetButton());//新增自创打印按钮功能
                }
            }
        }

        ToolStripButton CreatePrintsetButton()
        {
            ToolStripButton Stripbutton = new ToolStripButton();
            Stripbutton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            //Stripbutton.Image = global::UserCtrl.CtrlResource.property;
            Stripbutton.Image = Image.FromFile("..\\..\\Images\\print.gif");//Application.StartupPath + "\\print.gif"
            Stripbutton.ImageTransparentColor = System.Drawing.Color.Magenta;
            Stripbutton.Name = "printsetStripButton";
            Stripbutton.Size = new System.Drawing.Size(23, 22);
            Stripbutton.Text = "打印";
            Stripbutton.Click += new System.EventHandler(this.Stripbutton_Click);
            return Stripbutton;
        }

        private void Stripbutton_Click(object sender, EventArgs e)
        {
            using (PrintDialog diag = new PrintDialog())
            {
                diag.Document = base.Document;
                if (diag.ShowDialog() == DialogResult.OK)
                {
                    base.Document.Print();
                }
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // PrintPreviewDialogEx
            // 
            this.ClientSize = new System.Drawing.Size(652, 514);
            this.Name = "PrintPreviewDialogEx";
            this.ShowIcon = false;
            this.ResumeLayout(false);

        }


    }
}
