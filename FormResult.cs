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
	public partial class FormResult:Form
	{
		public FormResult()
		{
			InitializeComponent();
		}

		private void btnLobby_Click(object sender,EventArgs e)
		{
			Program.GetMainForm().ChageForm(APC.PHASE_LOBBY);
		}

		private void btnReplay_Click(object sender,EventArgs e)
		{
			Program.GetMainForm().ChageForm(APC.PHASE_PLAY);
		}

		private void btnQuit_Click(object sender,EventArgs e)
		{
			Program.GetMainForm().ChageForm(APC.PHASE_BEGIN);
		}
	}
}
