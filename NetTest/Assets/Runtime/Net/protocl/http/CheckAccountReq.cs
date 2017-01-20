using UnityEngine;
using System.Collections;

public class CheckAccountReq : AbstractReqPayload 
{
	/** 用户名 */
	public string name;
	/** 密码 */
	public string password;
}
