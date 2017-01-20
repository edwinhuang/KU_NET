using UnityEngine;
using System.Collections;

public class Example : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Execute ();
	}

	void Execute()
	{
		GameNotificationCenter.mIns.RegisterMonoBehaviour ();
		CheckAccountReq req = new CheckAccountReq ();
		req.name = "ss";
		req.password = "32";
		WebServer.mIns.LoadAccountDBCode<CheckAccountResp> ("account", "checkAccount", req, delegate(CheckAccountResp resp) {
			if (resp.ResqSucess())
			{
				SocketService.mIns.Create(resp.server.serverIp, resp.server.tcpPort, (value)=>{
					NormalLogin(resp);
				});
			}
		});
	}


	void NormalLogin(CheckAccountResp table)
	{
		LoginOrRegisterReq req = new LoginOrRegisterReq ();
		req.ServerID = WebServer.mIns.ServerID;
		req.token = table.userToken;
		req.os = SystemInfo.operatingSystem;

		SocketService.mIns.Send<LoginOrRegisterReq, LoginOrRegisterResp> (TcpMainCMD.LOGIN, TcpSubCMD.CS_LOGIN, TcpSubCMD.SC_LOGIN, req, (data, error) => {
			if (error)
				_LoadPlayerInfo(data);
		});
	}


	void _LoadPlayerInfo(LoginOrRegisterResp resp)
	{
		Debug.Log (resp.ToString());
		SocketService.mIns.CloseConnect ();
		Invoke ("Execute", 0.5f);
	}



	// Update is called once per frame
	void Update () {
	
	}
}
