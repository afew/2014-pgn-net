using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace golf_net
{
	public partial class FormLobby:Form
	{
		public FormLobby()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender,EventArgs e)
		{
			MessageBox.Show("Hello world");

			PGN.TcpCln pNet = Program.GetMainNet ();


			Program.GetMainForm().ChageForm(APC.PHASE_PLAY);
		}

		private void usrLst_SelectedIndexChanged(object sender,EventArgs e)
		{

		}
	}
}
