using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LoginAbstractResp : AbstractRespPayload {

	public string errorMsg;

	public override bool ResqSucess ()
	{
		if(string.IsNullOrEmpty(errorMsg) && msgid == 0)
		{
			return true;
		}
		else if(string.IsNullOrEmpty(errorMsg) && msgid != 0)
		{
/*			if (msgid == 4)
				DialogController.mIns.DialogControllerShow(msgid, GameController.ReLogin);
			else if (msgid == 5 || msgid == 2012 || msgid == 16005) { }
			else
				DialogController.mIns.DialogControllerShow(msgid);
//*/			return false;
		}
		else
		{
//			DialogController.mIns.DialogControllerShow(errorMsg,Color.red);
			return false;
		}
	}

}
