﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace golf_net
{
	public partial class FormPlay:Form
	{
		public FormPlay()
		{
			InitializeComponent();
		}

		private void Shot_Click(object sender,EventArgs e)
		{
			Program.ChageForm(APC.PHASE_RESULT);
		}
	}
}
