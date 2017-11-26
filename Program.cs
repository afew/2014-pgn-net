using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace golf_net
{
	public static class Program
	{
		public static FormAlpha		m_formAlpha = null;
		public static PGN.TcpCln	m_tcpCln    = null;

		/// <summary>
		/// 해당 응용 프로그램의 주 진입점입니다.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			m_tcpCln    = new PGN.TcpCln();
			m_formAlpha = new FormAlpha();


			Application.Run(m_formAlpha);
		}

		public static FormAlpha  GetMainForm() { return m_formAlpha; }
		public static PGN.TcpCln GetMainNet () { return m_tcpCln;    }
	}}
