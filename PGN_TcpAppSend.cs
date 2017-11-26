// Tcp Application:: sending process
//
//+++++++1+++++++++2+++++++++3+++++++++4+++++++++5+++++++++6+++++++++7+++++++++8

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net;
using System.Net.Sockets;
using System.Threading;

using golf_net;
using PGN;


public partial class TcpApp
{
	public static int SendConnect(string _ip, string _pt)						// Connection
	{
		TcpApp.Clear();

		TcpCln	pNet = TcpApp.GetMainNet();
		int		hr = 0;

		int     pt = Convert.ToInt32(_pt);
	
		pNet.SetIoEvent(OnIoEvent);

		pNet.CloseSocket();
		hr = pNet.Create(null, _ip, pt);
		hr = pNet.Connect();
		return hr;
	}


	public static int SendLogin(string _id, string _pwd)						// Login
	{
		TcpCln	pNet = TcpApp.GetMainNet();
		int		hr = 0;

		ushort opp = NTC.CS_REQ_LOGIN;
		byte[] src = Encoding.Unicode.GetBytes(_id);							// string -> byte, string str = Encoding.Default.GetString(src);	// byte -> string

		Array.Clear(m_wrkBuf, 0, m_wrkBuf.Length);								// clear the work buffer
		Array.Copy (src, m_wrkBuf, src.Length);									// copy the source data to work buffer

		m_wrkPck.Reset();														// work packet reset
		m_wrkPck.AddData(m_wrkBuf, 40);											// add the buffer
		m_wrkPck.EnCode(opp);													// finally, makeup the op code and complete the packet

		hr = pNet.Send(m_wrkPck);
		return hr;
	}

	//
	// GAME PLAY PACKET: gpp + send id + {dest id} + data
	//

	public static int SendRqInvite(uint destId, uint idx_map)
	{
		TcpCln	pNet = TcpApp.GetMainNet();
		int		hr = 0;

		ushort opp = NTC.CS_REQ_BROADCAST;										// op protocol
		ushort gpp = NTC.GP_RQ_INVITE;											// game play protocol

		m_wrkPck.Reset();
		m_wrkPck.AddData(gpp);
		m_wrkPck.AddData(TcpApp.app_user_id);
		m_wrkPck.AddData(destId);
		m_wrkPck.AddData(idx_map);
		m_wrkPck.EnCode(opp);

		hr = pNet.Send(m_wrkPck);
		return hr;
	}

	public static int SendRsInvite(uint destId, byte rq)
	{
		TcpCln	pNet = TcpApp.GetMainNet();
		int		hr = 0;

		ushort opp = NTC.CS_REQ_BROADCAST;										// op protocol
		ushort gpp = NTC.GP_RS_INVITE;											// game play protocol

		m_wrkPck.Reset();
		m_wrkPck.AddData(gpp);
		m_wrkPck.AddData(TcpApp.app_user_id);
		m_wrkPck.AddData(destId);
		m_wrkPck.AddData(rq);
		m_wrkPck.EnCode(opp);

		hr = pNet.Send(m_wrkPck);
		return hr;
	}

	public static int SendGpShot( float posX, float posY, float posZ
								, float  dir
								, float ctpX, float ctpY
								, uint  club, float power, float best)
	{
		TcpCln	pNet = TcpApp.GetMainNet();
		int		hr = 0;

		ushort opp = NTC.CS_REQ_BROADCAST;										// op protocol
		ushort gpp = NTC.GP_SHOT;												// game play protocol

		m_wrkPck.Reset();
		m_wrkPck.AddData(gpp);
		m_wrkPck.AddData(TcpApp.app_user_id);
		m_wrkPck.AddData(posX);
		m_wrkPck.AddData(posY);
		m_wrkPck.AddData(posZ);
		m_wrkPck.AddData(dir);
		m_wrkPck.AddData(ctpX);
		m_wrkPck.AddData(ctpY);
		m_wrkPck.AddData(club);
		m_wrkPck.AddData(power);
		m_wrkPck.AddData(best);
		m_wrkPck.EnCode(opp);

		hr = pNet.Send(m_wrkPck);
		return hr;
	}

	public static int SendGpPutt( float posX, float posY, float posZ
								, float  dir
								, float ctpY
								, uint  club, float pow)
	{
		TcpCln	pNet = TcpApp.GetMainNet();
		int		hr = 0;

		ushort opp = NTC.CS_REQ_BROADCAST;										// op protocol
		ushort gpp = NTC.GP_PUTT;												// game play protocol

		m_wrkPck.Reset();
		m_wrkPck.AddData(gpp);
		m_wrkPck.AddData(TcpApp.app_user_id);
		m_wrkPck.AddData(posX);
		m_wrkPck.AddData(posY);
		m_wrkPck.AddData(posZ);
		m_wrkPck.AddData(dir);
		m_wrkPck.AddData(ctpY);
		m_wrkPck.AddData(club);
		m_wrkPck.AddData(pow);
		m_wrkPck.EnCode(opp);

		hr = pNet.Send(m_wrkPck);
		return hr;
	}

	public static int SendGpMoveStop( float posX, float posY, float posZ)
	{
		TcpCln	pNet = TcpApp.GetMainNet();
		int		hr = 0;

		ushort opp = NTC.CS_REQ_BROADCAST;										// op protocol
		ushort gpp = NTC.GP_MOVESTOP;											// game play protocol

		m_wrkPck.Reset();
		m_wrkPck.AddData(gpp);
		m_wrkPck.AddData(TcpApp.app_user_id);
		m_wrkPck.AddData(posX);
		m_wrkPck.AddData(posY);
		m_wrkPck.AddData(posZ);
		m_wrkPck.EnCode(opp);

		hr = pNet.Send(m_wrkPck);
		return hr;
	}


	public static int SendGpEnd()
	{
		TcpCln	pNet = TcpApp.GetMainNet();
		int		hr = 0;

		ushort opp = NTC.CS_REQ_BROADCAST;										// op protocol
		ushort gpp = NTC.GP_END;												// game play protocol

		m_wrkPck.Reset();
		m_wrkPck.AddData(gpp);
		m_wrkPck.AddData(TcpApp.app_user_id);
		m_wrkPck.EnCode(opp);

		hr = pNet.Send(m_wrkPck);
		return hr;
	}

	public static int SendGpResult()
	{
		TcpCln	pNet = TcpApp.GetMainNet();
		int		hr = 0;

		ushort opp = NTC.CS_REQ_BROADCAST;										// op protocol
		ushort gpp = NTC.GP_RESULT;												// game play protocol

		m_wrkPck.Reset();
		m_wrkPck.AddData(gpp);
		m_wrkPck.AddData(TcpApp.app_user_id);
		m_wrkPck.EnCode(opp);

		hr = pNet.Send(m_wrkPck);
		return hr;
	}


	public static int SendReady(byte bReady)
	{
		TcpCln	pNet = TcpApp.GetMainNet();
		int		hr = 0;

		ushort opp = NTC.CS_REQ_READY;

		m_wrkPck.Reset();
		m_wrkPck.AddData(bReady);
		m_wrkPck.EnCode(opp);

		hr = pNet.Send(m_wrkPck);
		return hr;
	}

	public static int SendGo()
	{
		TcpCln	pNet = TcpApp.GetMainNet();
		int		hr = 0;

		ushort opp = NTC.CS_REQ_GO;

		m_wrkPck.Reset();
		m_wrkPck.EnCode(opp);

		hr = pNet.Send(m_wrkPck);
		return hr;
	}

	public static int SendStop()
	{
		TcpCln	pNet = TcpApp.GetMainNet();
		int		hr = 0;

		ushort opp = NTC.CS_REQ_STOP;

		m_wrkPck.Reset();
		m_wrkPck.EnCode(opp);

		hr = pNet.Send(m_wrkPck);
		return hr;
	}
}

