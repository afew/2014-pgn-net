// Tcp Application
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


public class TuserInfo
{
	public uint   id   = 0;
	public string name = null;
	public byte   owner= 0;
	public byte   ready= 0;

	public TuserInfo() : this(0, null, 0, 0){}
	public TuserInfo(uint _id, string _name, byte _owner, byte _ready)
	{
		id   = _id   ; name = _name; owner= _owner; ready= _ready;
	}
}

public class TplayMap
{
	public uint   id  = 0;
	public string name= null;

	public TplayMap() : this(0, null){}
	public TplayMap(uint _id, string _name)
	{
		id   = _id  ; name = _name;
	}
}


// Initial network data
public partial class TcpApp
{
	//public static string	net_ip		= "192.168.0.7";
	//public static string	net_ip		= "192.168.0.74";
	public static string	net_ip		= "192.168.0.20";
	public static string	net_pt		= "20001";
	public static string	net_uid		= "AAAAA";
	public static string	net_pwd		= "BBBBB";
}



public partial class TcpApp
{
	// IO
	public static TcpCln	m_tcpCln    = new TcpCln();
	public static Packet	m_wrkPck	= new Packet();							// work packet
	public static byte[]	m_wrkBuf	= new byte[NTC.PCK_MAX];				// work buffer = Header + Payload
	public static TcpCln	GetMainNet () { return m_tcpCln;    }				// tcp client application

	// Game Application
	public static string			app_char_name = null;						// character name
	public static uint				app_user_id   = 0;							// user id
	public static List<TuserInfo>	app_user_lst  = new List<TuserInfo>();		// user list
	public static List<TplayMap>	app_pmap_lst  = new List<TplayMap>();		// Play map list


	public static void Clear()
	{
		app_char_name = null;
		app_user_id   = 0;
		app_user_lst.Clear();
	}
}

