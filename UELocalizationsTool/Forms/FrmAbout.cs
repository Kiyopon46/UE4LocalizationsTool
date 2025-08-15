using System;
using System.Drawing;
using System.Windows.Forms;
using UELocalizationsTool.Controls;

namespace UELocalizationsTool
{
    public partial class FrmAbout : NForm
    {
        public FrmAbout(Form form)
        {
            InitializeComponent();
            this.Location = new Point(form.Location.X + (form.Width - this.Width) / 2, form.Location.Y + (form.Height - this.Height) / 2);
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
