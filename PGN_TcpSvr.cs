﻿// Tcp Server
//
//+++++++1+++++++++2+++++++++3+++++++++4+++++++++5+++++++++6+++++++++7+++++++++8

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net;
using System.Net.Sockets;
using System.Threading;


namespace PGN
{
	class TcpSvr : PGN.TcpBase
	{
		protected List<PGN.TcpCln>	m_vCln	= new List<PGN.TcpCln>();			// client list
		protected Thread			m_thAcp = null;								// accept thread


		override public void Destroy()
		{
			CloseSocket();
		}

		override public int Query(string msg, object v)
		{
			if("Remove Client" == msg)
			{
				TcpBase	p = (TcpBase)v;
				RemoveClient(p);
			}

			return NTC.EFAIL;
		}


		public int Create(string ip, int pt)
		{
			IPAddress	ipAdd		= null;


			m_sIp = ip;
			m_sPt = pt;

			if(null == ip)
			{
				IPAddress[]	vAdd
					= Dns.GetHostEntry(Environment.MachineName)
						 .AddressList;

				ipAdd	= vAdd[vAdd.Length - 1];
			}
			else
			{
				ipAdd	= IPAddress.Parse(m_sIp);
			}


			m_sdH		= new IPEndPoint(ipAdd, m_sPt);
			m_scH		= new Socket( AddressFamily.InterNetwork
									, SocketType.Stream
									, ProtocolType.Tcp);

			m_scH.Bind(m_sdH);										// Binding
			m_scH.Listen(1);										// Listen

			m_thAcp = new Thread(new ThreadStart(WorkAcp));			// create accept thread
			m_thAcp.Start();

			return NTC.OK;
		}


		public void CloseSocket()
		{
			if(null == m_scH)
				return;

			lock (m_oLock)
			{
				m_thAcp.Abort();
				m_thAcp = null;

				//m_scH.Shutdown(SocketShutdown.Both);
				m_scH.Close();
				m_scH = null;
				m_sIp = "";
				m_sPt = 0;
			}
		}


		protected int AddNewClient(Socket scH)
		{
			if(NTC.MAX_CONNECT <= m_vCln.Count)
				return NTC.EFAIL;

			EndPoint	sdH   = scH.RemoteEndPoint;
			uint		netId = PGN.Packet.GetSocketId(ref scH);
			TcpCln		pCln  = new TcpCln(netId);

			pCln.Create(this, scH, sdH);

			//string guid = Guid.NewGuid().ToString().ToUpper();

			string crpKey = netId.ToString() + " PGN_ENCRYPTION_KEY_BYTE_STRING";

			PGLog.LOGI("AddNewClient::" + crpKey);

			pCln.Send(crpKey, NTC.GP_DEFAULT);

			m_vCln.Add(pCln);
			pCln.Recv();

			return NTC.OK;
		}

		protected void RemoveClient(TcpBase v)
		{
			int n = m_vCln.FindIndex(_cln => _cln.GetSocket() == v.GetSocket());
			if(0 > n)
				return;


			uint key = m_vCln[n].NetId;

			m_vCln[n].Destroy();
			m_vCln.RemoveAt(n);

			PGLog.LOGI("RemoveClient::[" + n +"]: "
						+ key + ", Remain Client :" + m_vCln.Count);
		}


		////////////////////////////////////////////////////////////////////////
		// Inner Process...

		protected void WorkAcp()
		{
			int		hr = NTC.OK;

			try
			{
				while( true)
				{
					Socket scH = null;

					scH = m_scH.Accept();

					PGLog.LOGI("WorkAcp::New Client::" + scH);

					lock (m_oLock)
					{
						hr = AddNewClient(scH);
						if (0 > hr)
							PGLog.LOGI("WorkAcp::Client List is Full");
					}
				}
			}

			catch(SocketException e0)
			{
				PGLog.LOGW("WorkAcp::SocketException::" + e0.ToString() );
				hr = NTC.EFAIL_SOCK;
				IoEvent(NTC.EV_ACCEPT, hr, this.m_scH, 0, this);
			}
			catch(Exception e1)
			{
				PGLog.LOGW("WorkAcp::Exception::" + e1.ToString() );
				hr = NTC.EFAIL;
				IoEvent(NTC.EV_ACCEPT, hr, this.m_scH, 0, this);
			}
		}
	}

}// namespace PGN

