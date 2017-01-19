//using UnityEngine;
//using System.Collections;
//using Kubility;
//
//public class ErrorMessFactory : DataInterface
//{
//
//	public ErrorMessFactory (MessageDataType type) : base (type)
//	{
//				
//	}
//
//
//	public override BaseMessage DynamicCreate (byte[] data, MessageHead head)
//	{
//		
//		ErrorMsg Resp = new ErrorMsg ();
//		Resp.DeSerialize (data);
//
//		var value = Errormessage.CreateResp (head, Resp);
//
//		ErrorManager.PushMsg (Resp);
//		return value;
//
//	}
//}
