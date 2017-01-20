using UnityEngine;
using System.Collections;

public class CheckAccountResp : LoginAbstractResp
{
	
	public string userToken;
	
	/** 推荐服务器 */
	public GameServerTo recommend;
	/** 当server为null时，表示第一次登录（即注册），接下来需要客户端根据serverTotalPage请求服务器列表（PageGetGameServerReq） */
	public GameServerTo server;
	
	/** 游戏服务器列表的总页数 */
	public int serverTotalPage;

	public string errorMsg;//第三方返回的错误消息  判断长度  长度大于0的时候 取这个错误消息显示 否则去错误消息码
	
}
