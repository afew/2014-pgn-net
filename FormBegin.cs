using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using PGN;

namespace golf_net
{
	public partial class FormBegin:Form
	{
		public FormBegin()
		{
			InitializeComponent();
			this.txtIp.Text = TcpApp.net_ip;
			this.txtPt.Text = TcpApp.net_pt;

			this.txtUID.Text = TcpApp.net_uid;
			this.txtPWD.Text = TcpApp.net_pwd;
		}
		
		
		private void btnConnect_Click(object sender,EventArgs e)
		{
			TcpApp.SendConnect(this.txtIp.Text, this.txtPt.Text);
		}

		private void btnLogin_Click(object sender,EventArgs e)
		{
			TcpApp.SendLogin(this.txtUID.Text, this.txtPWD.Text );
		}
	}

}
