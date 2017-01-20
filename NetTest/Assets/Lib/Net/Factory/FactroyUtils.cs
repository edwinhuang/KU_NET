using UnityEngine;
using System.Collections;
using Kubility;


public static class FactoryUtils
{
	public static void InitFactory()
	{

		new StructMessFactory(MessageDataType.Struct);

/*		new BattleDropItemManager(TcpSubCMD.SC_DROP_ITEM);
		new GameDataManager(TcpSubCMD.SC_BASE_DATA);
		new UpdateTimeManager(TcpSubCMD.SC_TIME_UPDATE);
		new ErrorManager(TcpSubCMD.SC_ERRO_MSG);
		new ItemUpdateManager(TcpSubCMD.SC_WAREHOUSE_ITEM_CHANGE);
		new ItemUpdateManager(TcpSubCMD.SC_WAREHOUSE_ITEM_ADD);
		new ItemUpdateManager(TcpSubCMD.SC_WAREHOUSE_REMOVE);
		new ItemUpdateManager(TcpSubCMD.SC_WAREHOUSE_LIST);
		new MailUpdateManager(TcpSubCMD.SC_NEW_MAIL_NOTICE);
		new ChatUpdateManager(TcpSubCMD.SC_SOCIATY_CHAT);
		new ChatUpdateManager(TcpSubCMD.SC_SOCIATY_KICKED_NOTIFY);

		new AchievementUpdateManager(TcpSubCMD.SC_ALL_ACHIEVEMENT);
		new AchievementUpdateManager(TcpSubCMD.SC_UPDATE_ACHIEVEMENT);
		new AchievementUpdateManager(TcpSubCMD.SC_COMPLETE_ACHIEVEMENT);
		new AchievementUpdateManager(TcpSubCMD.SC_GET_REWARD);
//*/
		//		new JsonMessFactory(MessageDataType.Json);
		//		new ProtobufMessFactory(MessageDataType.ProtoBuf);
	}

}
