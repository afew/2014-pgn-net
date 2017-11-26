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
	public partial class FormBegin:Form
	{
		public FormBegin()
		{
			InitializeComponent();
			this.txtUID.Text = "User Id";
			this.txtPWD.Text = "User Password";
		}
		
		
		private void btnConnect_Click(object sender,EventArgs e)
		{
			PGN.TcpCln pNet = Program.GetMainNet ();

			pNet.OnConnect = OnConnect;
			pNet.OnRecv    = OnRecv;
			pNet.Create(null, "192.168.0.7", 20001);
			pNet.Connect();
		}

		private void btnLogin_Click(object sender,EventArgs e)
		{
			//MessageBox.Show("Hello world");
			Program.GetMainForm().ChageForm(APC.PHASE_LOBBY);

			PGN.TcpCln pNet = Program.GetMainNet ();

			pNet.SendLogin("AAAAAA", "BBBBB");
		}


		protected void OnConnect()
		{
			PGN.TcpCln pNet = Program.GetMainNet ();
			PGLog.LOGI("On Connect");
		}

		protected void OnRecv()
		{
			PGLog.LOGI("On Recv");
			//// game proc....
			//byte[] b = new byte[20]{;

			//PGN.Packet pck = new PGN.Packet();
			//pck.Reset();

			//pck.AddData();
			//NTC.CS_REQ_LOGIN;
		}

	}

}
