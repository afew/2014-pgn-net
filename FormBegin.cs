using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ydnet
{
	public partial class FormBegin:Form
	{
		public FormBegin()
		{
			InitializeComponent();
			this.txtUID.Text = "User Id";
			this.txtPWD.Text = "User Password";
		}

		private void btnLogin_Click(object sender,EventArgs e)
		{
			MessageBox.Show("Hello world");
			Program.GetMainForm().ChageForm(YDC.PHASE_LOBBY);
		}
	}

}
