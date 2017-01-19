using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Kubility;
using System;

public class TcpMainCMD :BaseEnum
{

		public TcpMainCMD (string name) : base (name)
		{

		}

		public TcpMainCMD (string name, int value) : base (name, value)
		{

		}

		/// <summary>
		/// //256
		/// </summary>
		public static TcpMainCMD ACHIEVE = new TcpMainCMD ("ACHIEVE", 0x100);


		/**
	 * 活动512
	 */
		public static TcpMainCMD ACTIVITY = new TcpMainCMD ("ACTIVITY", 0x200);

		/**
	 * 建筑68
	 */
		public static TcpMainCMD BUILD = new TcpMainCMD ("BUILD", 0x300);
		//7
		/*
	 * 装备1024
	 */
		public static TcpMainCMD EQUIP = new TcpMainCMD ("EQUIP", 0x400);

		/**
	 * 英雄1280
	 */
		public static TcpMainCMD HERO = new TcpMainCMD ("HERO", 0x500);

		/// <summary>
		/// 邀请1536
		/// </summary>
		public static TcpMainCMD INVITE = new TcpMainCMD ("INVITE", 0x600);

		/// <summary>
		/// 道具1792
		/// </summary>
		public static TcpMainCMD ITEM = new TcpMainCMD ("ITEM", 0x700);

		/**
	 * 登陆2
	 */
		public static TcpMainCMD LOGIN = new TcpMainCMD ("LOGIN", 2);

		/**
	 * 邮件2048
	 */
		public static TcpMainCMD MAIL = new TcpMainCMD ("MAIL", 0x800);

		/**
	 * 地图2304
	 */
		public static TcpMainCMD MAP = new TcpMainCMD ("MAP", 0x900);

		/**
	 * 怪物2560
	 */
		public static TcpMainCMD MONSTER = new TcpMainCMD ("MONSTER", 0xA00);

		/// <summary>
		/// 玩家 2816
		/// </summary>
		public static TcpMainCMD PLAYER = new TcpMainCMD ("PLAYER", 0xB00);

		/// <summary>
		/// 生产3072
		/// </summary>
		public static TcpMainCMD PRODUCE = new TcpMainCMD ("PRODUCE", 0xC00);

		/// <summary>
		/// 3328 研究
		/// </summary>
		public static TcpMainCMD RESEARCH = new TcpMainCMD ("RESEARCH", 0xD00);

		/**
	 * 商城3584
	 */
		public static TcpMainCMD SHOP = new TcpMainCMD ("SHOP", 0xE00);

		/**
	 * 路3840
	 */
		public static TcpMainCMD SOIL = new TcpMainCMD ("SOIL", 0xF00);

		/**
	 * 任务4096
	 */
		public static TcpMainCMD TASK = new TcpMainCMD ("TASK", 0x1000);

		/// <summary>
		/// 通用 4352
		/// </summary>
		public static TcpMainCMD COMMON = new TcpMainCMD ("COMMON", 0x1100);
		/// <summary>
		/// 心跳
		/// </summary>
		public static TcpMainCMD HEART_BEAT = new TcpMainCMD ("HEART_BEAT", 0x1200);
	
		/// <summary>
		/// 聊天
		/// </summary>
		public static TcpMainCMD CHAT_MSG = new TcpMainCMD ("CHAT_MSG", 0x1300);
	
		/// <summary>
		/// 排行榜
		/// </summary>
		public static TcpMainCMD RANK_MSG = new TcpMainCMD ("RANK_MSG", 0x1400);

		/// <summary>
		/// 战斗
		/// </summary>
		public static TcpMainCMD BATTLE = new TcpMainCMD ("BATTLE", 0x1500);
	
	/**
	 * 公会
	 */
	public static TcpMainCMD SOCIATY = new TcpMainCMD ("SOCIATY", 0x1600);

	/**
	 * 服务器之间公会消息
	 */
	public static TcpMainCMD S2S_SOCIATY = new TcpMainCMD ("S2S_SOCIATY", 0x5000);
}



public class TcpSubCMD :BaseEnum
{
		public TcpSubCMD (string name) : base (name)
		{

		}

		public TcpSubCMD (string name, int value) : base (name, value)
		{

		}
		/******************* GameAppMsg.LOGIN ****************/
		public static TcpSubCMD CS_LOGIN = new TcpSubCMD ("CS_LOGIN", TcpMainCMD.LOGIN | 0x0001);

		public static TcpSubCMD SC_LOGIN = new TcpSubCMD ("SC_LOGIN", TcpMainCMD.LOGIN | 0x0002);

		/*****************GameAppMsg.BUILD******************/
		/// <summary>
		/// //建筑升级（BuildUpgradeReq）
		/// </summary>
		public static TcpSubCMD CS_BUILD_UPGRADE = new TcpSubCMD ("CS_BUILD_UPGRADE", TcpMainCMD.BUILD | 0x0001);
		/// <summary>
		/// //建筑升级完成（BuildUpgradeFinishReq）
		/// </summary>
		public static TcpSubCMD CS_BUILD_FINISH = new TcpSubCMD ("CS_BUILD_FINISH", TcpMainCMD.BUILD | 0x0003);
		/// <summary>
		/// //查询建筑订单数据
		/// </summary>
		public static TcpSubCMD CS_BUILD_RECIPE = new TcpSubCMD ("CS_BUILD_RECIPE", TcpMainCMD.BUILD | 0x0005);
		/// <summary>
		/// //查询建筑怪物数据
		/// </summary>
		public static TcpSubCMD CS_BUILD_MONSTER = new TcpSubCMD ("CS_BUILD_FINISH", TcpMainCMD.BUILD | 0x0007);
		/// <summary>
		/// //查询所有建筑怪物订单数据
		/// </summary>
		public static TcpSubCMD CS_ALL_BUILD_INFO = new TcpSubCMD ("CS_ALL_BUILD_INFO", TcpMainCMD.BUILD | 0x0009);
		/// <summary>
		/// //资源收取
		/// </summary>
		public static TcpSubCMD CS_HARVEST_RESOURCE = new TcpSubCMD ("CS_HARVEST_RESOURCE", TcpMainCMD.BUILD | 0x000b);

		/// <summary>
		/// //建筑升级
		/// </summary>
		public static TcpSubCMD SC_BUILD_UPGRADE = new TcpSubCMD ("SC_BUILD_UPGRADE", TcpMainCMD.BUILD | 0x0002);
		/// <summary>
		/// //建筑升级
		/// </summary>
		public static TcpSubCMD SC_BUILD_FINISH = new TcpSubCMD ("SC_BUILD_FINISH", TcpMainCMD.BUILD | 0x0004);
		/// <summary>
		/// //查询建筑订单数据
		/// </summary>
		public static TcpSubCMD SC_BUILD_RECIPE = new TcpSubCMD ("SC_BUILD_RECIPE", TcpMainCMD.BUILD | 0x0006);
		/// <summary>
		/// //查询建筑怪物数据
		/// </summary>
		public static TcpSubCMD SC_BUILD_MONSTER = new TcpSubCMD ("SC_BUILD_MONSTER", TcpMainCMD.BUILD | 0x0008);
		/// <summary>
		/// //查询所有建筑怪物订单数据
		/// </summary>
		public static TcpSubCMD SC_ALL_BUILD_INFO = new TcpSubCMD ("SC_ALL_BUILD_INFO", TcpMainCMD.BUILD | 0x000a);
		/// <summary>
		/// //资源收取
		/// </summary>
		public static TcpSubCMD SC_HARVEST_RESOURCE = new TcpSubCMD ("SC_HARVEST_RESOURCE", TcpMainCMD.BUILD | 0x000c);

		public static TcpSubCMD CS_CANCEL_BUILD_UPLV = new TcpSubCMD ("CS_CANCEL_BUILD_UPLV", TcpMainCMD.BUILD | 0x000d);

		/// <summary>
		/// //药剂升级
		/// </summary>
                public static TcpSubCMD CS_AGENT_UPGRADE = new TcpSubCMD("CS_AGENT_UPGRADE", TcpMainCMD.BUILD | 0x000f);
		/// <summary>
		/// //药剂升级
		/// </summary>
                public static TcpSubCMD SC_AGENT_UPGRADE = new TcpSubCMD("SC_AGENT_UPGRADE ", TcpMainCMD.BUILD | 0x0010);

		/// <summary>
		/// //药剂升级结束
		/// </summary>
                public static TcpSubCMD CS_AGENT_UPGRADE_FINISH = new TcpSubCMD("CS_AGENT_UPGRADE_FINISH", TcpMainCMD.BUILD | 0x0011);
		/// <summary>
		/// //药剂升级结束
		/// </summary>
                public static TcpSubCMD SC_AGENT_UPGRADE_FINISH = new TcpSubCMD("SC_AGENT_UPGRADE_FINISH  ", TcpMainCMD.BUILD | 0x0012);

		/// <summary>
		/// //药剂升级快速结束
		/// </summary>
                public static TcpSubCMD CS_AGENT_UPGRADE_FAST_FINISH = new TcpSubCMD("CS_AGENT_UPGRADE_FAST_FINISH ", TcpMainCMD.BUILD | 0x0013);

		/// <summary>
		/// //药剂升级快速结束
		/// </summary>
                public static TcpSubCMD SC_AGENT_UPGRADE_FAST_FINISH = new TcpSubCMD("SC_AGENT_UPGRADE_FAST_FINISH  ", TcpMainCMD.BUILD | 0x0014);
		/***************** GameAppMsg.SHOP ******************/
		/// <summary>
		/// //购买商品
		/// </summary>
		public static TcpSubCMD CS_BUY = new TcpSubCMD ("CS_BUY", TcpMainCMD.SHOP | 0x0001);
		public static TcpSubCMD SC_BUY = new TcpSubCMD ("SC_BUY", TcpMainCMD.SHOP | 0x0002);
		/// <summary>
		/// //购买资源
		/// </summary>
		public static TcpSubCMD CS_BUY_RESOURCE = new TcpSubCMD ("CS_BUY_RESOURCE", TcpMainCMD.SHOP | 0x0003);
		public static TcpSubCMD SC_BUY_RESOURCE = new TcpSubCMD ("SC_BUY_RESOURCE", TcpMainCMD.SHOP | 0x0004);
		/// <summary>
		/// 进入商店
		/// </summary>
		public static TcpSubCMD CS_ENTER_SHOP = new TcpSubCMD("CS_ENTER_SHOP", TcpMainCMD.SHOP | 0x0005);
		public static TcpSubCMD SC_ENTER_SHOP = new TcpSubCMD("SC_ENTER_SHOP", TcpMainCMD.SHOP | 0x0006);
		/// <summary>
		/// //黑市刷新
		/// </summary>
		public static TcpSubCMD CS_REFRESH_SHOP = new TcpSubCMD ("CS_REFRESH_SHOP", TcpMainCMD.SHOP | 0x0007);
		public static TcpSubCMD SC_REFRESH_SHOP = new TcpSubCMD ("SC_REFRESH_SHOP", TcpMainCMD.SHOP | 0x0008);
		/// <summary>
		/// //进入护盾商店
		/// </summary>
		public static TcpSubCMD SC_ENTER_SHIELD = new TcpSubCMD ("SC_ENTER_SHIELD", TcpMainCMD.SHOP | 0x000a);
        /// <summary>
        /// 商店购买道具（建筑，怪物，陷阱）
        /// </summary>
        public static TcpSubCMD CS_BUY_ITEM = new TcpSubCMD("CS_BUY_ITEM", TcpMainCMD.SHOP | 0x000b);
        public static TcpSubCMD SC_BUY_ITEM = new TcpSubCMD("SC_BUY_ITEM", TcpMainCMD.SHOP | 0x000c);
        /// <summary>
        /// // 作弊(DebugAddResourceJob)
        /// </summary>
        public static TcpSubCMD CS_DEBUG = new TcpSubCMD ("CS_DEBUG", TcpMainCMD.SHOP | 0x0099);
	
		/*****************GameAppMsg.SOIL******************/
		/// <summary>
		/// //挖路(DigSoilReq)
		/// </summary>
		public static TcpSubCMD CS_DIG_SOIL = new TcpSubCMD ("CS_DIG_SOIL", TcpMainCMD.SOIL | 0x0001);
		public static TcpSubCMD SC_DIG_SOIL = new TcpSubCMD ("SC_DIG_SOIL", TcpMainCMD.SOIL | 0x0002);
// 挖路(DigSoilReq)


		/// <summary>
		/// //挖路完成(DigSoilFinishReq)
		/// </summary>
		public static TcpSubCMD CS_SOIL_FINISH = new TcpSubCMD ("CS_SOIL_FINISH", TcpMainCMD.SOIL | 0x0003);

		public static TcpSubCMD CS_CANCEL_DIGSOIL = new TcpSubCMD ("CS_CANCEL_DIGSOIL", TcpMainCMD.SOIL | 0x0005);

		public static TcpSubCMD SC_SOIL_FINISH = new TcpSubCMD ("SC_SOIL_FINISH", TcpMainCMD.SOIL | 0x0004);


		/**********************GameAppMsg.Comm****************/
		/// <summary>
		/// //仓库列表请求包
		/// </summary>
		public static TcpSubCMD CS_WAREHOUSE_LIST = new TcpSubCMD ("CS_WAREHOUSE_LIST", TcpMainCMD.COMMON | 0x0001);
		/// <summary>
		/// // 仓库道具列表
		/// </summary>
		public static TcpSubCMD SC_WAREHOUSE_LIST = new TcpSubCMD ("SC_WAREHOUSE_LIST", TcpMainCMD.COMMON | 0x0002);

		/// <summary>
		/// //仓库道具删除包
		/// </summary>
		public static TcpSubCMD CS_WAREHOUSE_REMOVE = new TcpSubCMD ("CS_WAREHOUSE_REMOVE", TcpMainCMD.COMMON | 0x0003);
		/// <summary>
		/// //仓库移除
		/// </summary>
		public static TcpSubCMD SC_WAREHOUSE_REMOVE = new TcpSubCMD ("SC_WAREHOUSE_REMOVE", TcpMainCMD.COMMON | 0x0004);






		public static TcpSubCMD SC_TIME_UPDATE = new TcpSubCMD ("SC_TIME_UPDATE", TcpMainCMD.COMMON | 0x0006);
		/// <summary>
		/// //错误消息提示包
		/// </summary>
		public static TcpSubCMD SC_ERRO_MSG = new TcpSubCMD ("SC_ERRO_MSG", TcpMainCMD.COMMON | 0x0008);

		/// <summary>
		/// //仓库改变
		/// </summary>
		public static TcpSubCMD SC_WAREHOUSE_ITEM_CHANGE = new TcpSubCMD ("SC_WAREHOUSE_ITEM_CHANGE", TcpMainCMD.COMMON | 0x000a);

		/// <summary>
		/// //仓库增加
		/// </summary>
		public static TcpSubCMD SC_WAREHOUSE_ITEM_ADD = new TcpSubCMD ("SC_WAREHOUSE_ITEM_ADD", TcpMainCMD.COMMON | 0x000c);

		/// <summary>
		/// //基础数据更新包（货币）
		/// </summary>
		public static TcpSubCMD SC_BASE_DATA = new TcpSubCMD ("SC_BASE_DATA", TcpMainCMD.COMMON | 0x000e);
        /// <summary>
        /// //新手引导
        /// </summary>
        public static TcpSubCMD CS_VECTOR_STEP = new TcpSubCMD("CS_VECTOR_STEP", TcpMainCMD.COMMON | 0x000f);
        /// <summary>
        /// //新手引导
        /// </summary>
        public static TcpSubCMD SC_VECTOR_STEP = new TcpSubCMD("SC_VECTOR_STEP", TcpMainCMD.COMMON | 0x0010);
        /// <summary>
        /// //用户昵称
        /// </summary>
        public static TcpSubCMD CS_SET_NAME = new TcpSubCMD("CS_SET_NAME", TcpMainCMD.COMMON | 0x0011);
        /// <summary>
        /// //用户昵称
        /// </summary>
        public static TcpSubCMD SC_SET_NAME = new TcpSubCMD("SC_SET_NAME", TcpMainCMD.COMMON | 0x0012);
        /// <summary>
        /// //新手引导标记
        /// </summary>
        public static TcpSubCMD CS_VECTOR_MARK = new TcpSubCMD("CS_VECTOR_MARK", TcpMainCMD.COMMON | 0x0017);
        /// <summary>
        /// //新手引导标记
        /// </summary>
        public static TcpSubCMD SC_VECTOR_MARK = new TcpSubCMD("SC_VECTOR_MARK", TcpMainCMD.COMMON | 0x0018);
        /// <summary>
        /// //新手引导标记
        /// </summary>
        public static TcpSubCMD CS_VECTOR_MARK_LIST = new TcpSubCMD("CS_VECTOR_MARK_LIST", TcpMainCMD.COMMON | 0x0019);
        /// <summary>
        /// //新手引导标记
        /// </summary>
        public static TcpSubCMD SC_VECTOR_MARK_LIST = new TcpSubCMD("SC_VECTOR_MARK_LIST", TcpMainCMD.COMMON | 0x001a);


	/// <summary>
	/// //查看个人资料
	/// </summary>
	public static TcpSubCMD CS_VIEW_OTHER_ROLE = new TcpSubCMD("CS_VIEW_OTHER_ROLE", TcpMainCMD.COMMON | 0x0013);
	public static TcpSubCMD SC_VIEW_OTHER_ROLE = new TcpSubCMD("SC_VIEW_OTHER_ROLE", TcpMainCMD.COMMON | 0x0014);
	/// <summary>
	/// //修改用户头像
	/// </summary>
	public static TcpSubCMD CS_SET_ROLE_ICON = new TcpSubCMD("CS_SET_ROLE_ICON", TcpMainCMD.COMMON | 0x0015);
	public static TcpSubCMD SC_SET_ROLE_ICON = new TcpSubCMD("SC_SET_ROLE_ICON", TcpMainCMD.COMMON | 0x0016);

		/*****************GameAppMsg.MONSTER******************/
		/// <summary>
		/// //怪物升级
		/// </summary>
		public static TcpSubCMD CS_MONSTER_UPGRADE = new TcpSubCMD ("CS_MONSTER_UPGRADE", TcpMainCMD.MONSTER | 0x0001);
		/// <summary>
		/// //怪物进阶
		/// </summary>
		public static TcpSubCMD CS_MONSTER_QUALITY = new TcpSubCMD ("CS_MONSTER_QUALITY", TcpMainCMD.MONSTER | 0x0003);
		/// <summary>
		/// //怪物删除
		/// </summary>
		public static TcpSubCMD CS_MONSTER_DELETE = new TcpSubCMD ("CS_MONSTER_DELETE", TcpMainCMD.MONSTER | 0x0005);
		/// <summary>
		/// //怪物复活
		/// </summary>
		public static TcpSubCMD CS_MONSTER_RELIVE = new TcpSubCMD ("CS_MONSTER_RELIVE", TcpMainCMD.MONSTER | 0x0007);

		/// <summary>
		/// //怪物升级
		/// </summary>
		public static TcpSubCMD SC_MONSTER_UPGRADE = new TcpSubCMD ("SC_MONSTER_UPGRADE", TcpMainCMD.MONSTER | 0x0002);
		/// <summary>
		/// //怪物进阶
		/// </summary>
		public static TcpSubCMD SC_MONSTER_QUALITY = new TcpSubCMD ("SC_MONSTER_QUALITY", TcpMainCMD.MONSTER | 0x0004);
		/// <summary>
		/// //怪物删除
		/// </summary>
		public static TcpSubCMD SC_MONSTER_DELETE = new TcpSubCMD ("SC_MONSTER_DELETE", TcpMainCMD.MONSTER | 0x0006);
		/// <summary>
		/// //怪物复活
		/// </summary>
		public static TcpSubCMD SC_MONSTER_RELIVE = new TcpSubCMD ("SC_MONSTER_RELIVE", TcpMainCMD.MONSTER | 0x0008);




		/*****************GameAppMsg.PRODUCE******************/
		/// <summary>
		/// //生产怪物
		/// </summary>
		public static TcpSubCMD CS_PRODUCE_MONSTER = new TcpSubCMD ("CS_PRODUCE_MONSTER", TcpMainCMD.PRODUCE | 0x0001);
		/// <summary>
		/// //生产怪物完成一个
		/// </summary>
		public static TcpSubCMD CS_PRODUCE_MONSTER_FINISH_ONE = new TcpSubCMD ("CS_PRODUCE_MONSTER_FINISH_ONE", TcpMainCMD.PRODUCE | 0x0003);
		/// <summary>
		/// //生产怪物快速完成
		/// </summary>
		public static TcpSubCMD CS_PRODUCE_MONSTER_FAST_FINISH = new TcpSubCMD ("CS_PRODUCE_MONSTER_FAST_FINISH", TcpMainCMD.PRODUCE | 0x0005);
		/// <summary>
		/// //生产怪物取消
		/// </summary>
		public static TcpSubCMD CS_PRODUCE_MONSTER_CANCEL = new TcpSubCMD ("CS_PRODUCE_MONSTER_CANCEL", TcpMainCMD.PRODUCE | 0x0007);

		/// <summary>
		/// //生产订单
		/// </summary>
		public static TcpSubCMD CS_PRODUCE_RECIPE = new TcpSubCMD ("CS_PRODUCE_RECIPE", TcpMainCMD.PRODUCE | 0x0009);
		/// <summary>
		/// //生产订单完成一个
		/// </summary>
		public static TcpSubCMD CS_PRODUCE_RECIPE_FINISH_ONE = new TcpSubCMD ("CS_PRODUCE_RECIPE_FINISH_ONE", TcpMainCMD.PRODUCE | 0x000b);
		/// <summary>
		/// //生产订单取消
		/// </summary>
		public static TcpSubCMD CS_PRODUCE_RECIPE_CANCEL = new TcpSubCMD ("CS_PRODUCE_RECIPE_CANCEL", TcpMainCMD.PRODUCE | 0x000d);
		/// <summary>
		/// //生产订单快速完成
		/// </summary>
		public static TcpSubCMD CS_PRODUCE_RECIPE_FAST_FINISH = new TcpSubCMD ("CS_PRODUCE_RECIPE_FAST_FINISH", TcpMainCMD.PRODUCE | 0x000f);
		/// <summary>
		/// //生产订单收取
		/// </summary>
		public static TcpSubCMD CS_PRODUCE_RECIPE_TAKE = new TcpSubCMD ("CS_PRODUCE_RECIPE_TAKE", TcpMainCMD.PRODUCE | 0x0011);

		/// <summary>
		/// //生产怪物
		/// </summary>
		public static TcpSubCMD SC_PRODUCE_MONSTER = new TcpSubCMD ("SC_PRODUCE_MONSTER", TcpMainCMD.PRODUCE | 0x0002);

		/// <summary>
		/// //生产怪物完成一个
		/// </summary>
		public static TcpSubCMD SC_PRODUCE_MONSTER_FINISH_ONE = new TcpSubCMD ("SC_PRODUCE_MONSTER", TcpMainCMD.PRODUCE | 0x0004);

		/// <summary>
		/// //生产怪物快速完成
		/// </summary>
		public static TcpSubCMD SC_PRODUCE_MONSTER_FAST_FINISH = new TcpSubCMD ("SC_PRODUCE_MONSTER", TcpMainCMD.PRODUCE | 0x0006);

		/// <summary>
		/// //生产怪物取消
		/// </summary>
		public static TcpSubCMD SC_PRODUCE_MONSTER_CANCEL = new TcpSubCMD ("SC_PRODUCE_MONSTER", TcpMainCMD.PRODUCE | 0x0008);

		/// <summary>
		/// //生产订单
		/// </summary>
		public static TcpSubCMD SC_PRODUCE_RECIPE = new TcpSubCMD ("SC_PRODUCE_RECIPE", TcpMainCMD.PRODUCE | 0x000a);

		/// <summary>
		/// //生产订单完成一个
		/// </summary>
		public static TcpSubCMD SC_PRODUCE_RECIPE_FINISH_ONE = new TcpSubCMD ("SC_PRODUCE_RECIPE_FINISH_ONE", TcpMainCMD.PRODUCE | 0x000c);

		/// <summary>
		/// //生产订单取消
		/// </summary>
		public static TcpSubCMD SC_PRODUCE_RECIPE_CANCEL = new TcpSubCMD ("SC_PRODUCE_RECIPE_CANCEL", TcpMainCMD.PRODUCE | 0x000e);

		/// <summary>
		/// //生产订单快速完成
		/// </summary>
		public static TcpSubCMD SC_PRODUCE_RECIPE_FAST_FINISH = new TcpSubCMD ("SC_PRODUCE_RECIPE_FAST_FINISH", TcpMainCMD.PRODUCE | 0x0010);

		/// <summary>
		/// //生产订单收取
		/// </summary>
		public static TcpSubCMD SC_PRODUCE_RECIPE_TAKE = new TcpSubCMD ("SC_PRODUCE_RECIPE_TAKE", TcpMainCMD.PRODUCE | 0x0012);

		/*****************GameAppMsg.HERO******************/
		/// <summary>
		/// //英雄升级
		/// </summary>
		public static TcpSubCMD CS_HERO_UPGRADE = new TcpSubCMD ("CS_HERO_UPGRADE", TcpMainCMD.HERO | 0x0001);
		/// <summary>
		/// //英雄进阶
		/// </summary>
		public static TcpSubCMD CS_HERO_QUALITY = new TcpSubCMD ("CS_HERO_QUALITY", TcpMainCMD.HERO | 0x0003);
		/// <summary>
		/// //英雄技能升级
		/// </summary>
		public static TcpSubCMD CS_HERO_SKILL_UPGRADE = new TcpSubCMD ("CS_HERO_SKILL_UPGRADE", TcpMainCMD.HERO | 0x0005);
        /// <summary>
        /// //英雄升星
        /// </summary>
        public static TcpSubCMD CS_HERO_STAR_LEVEL = new TcpSubCMD("CS_HERO_STAR_LEVEL", TcpMainCMD.HERO | 0x0009);
		/// <summary>
		/// //英雄升级
		/// </summary>
		public static TcpSubCMD SC_HERO_UPGRADE = new TcpSubCMD ("SC_HERO_UPGRADE", TcpMainCMD.HERO | 0x0002);
		/// <summary>
		/// //英雄进阶
		/// </summary>
		public static TcpSubCMD SC_HERO_QUALITY = new TcpSubCMD ("SC_HERO_QUALITY", TcpMainCMD.HERO | 0x0004);
		/// <summary>
		/// //英雄技能升级
		/// </summary>
		public static TcpSubCMD SC_HERO_SKILL_UPGRADE = new TcpSubCMD ("SC_HERO_SKILL_UPGRADE ", TcpMainCMD.HERO | 0x0006);
        /// <summary>
        /// //英雄升星
        /// </summary>
        public static TcpSubCMD SC_HERO_STAR_LEVEL = new TcpSubCMD("SC_HERO_STAR_LEVEL ", TcpMainCMD.HERO | 0x000a);

    public static TcpSubCMD  CS_HERO_SET_TEAM = new TcpSubCMD("CS_HERO_SET_TEAM ", TcpMainCMD.HERO | 0x0007); // 设置英雄占位
    public static TcpSubCMD  SC_HERO_SET_TEAM = new TcpSubCMD("SC_HERO_SET_TEAM ", TcpMainCMD.HERO | 0x0008); // 设置英雄占位

    /***************** GameAppMsg.EQUIP ******************/
    public static TcpSubCMD CS_EQUIP_GRADE = new TcpSubCMD ("CS_EQUIP_GRADE", TcpMainCMD.EQUIP | 0x0001);
		//升级装备
		public static TcpSubCMD SC_EQUIP_GRADE = new TcpSubCMD ("SC_EQUIP_GRADE", TcpMainCMD.EQUIP | 0x0002);
		//升级装备


		/***************** GameAppMsg.HEART_BEAT ******************/
		/// <summary>
		/// 心跳
		/// </summary>
		public static TcpSubCMD CS_HEART_BEAT = new TcpSubCMD ("CS_HEART_BEAT", TcpMainCMD.HEART_BEAT | 0x0001);
//
		/// <summary>
		/// 心跳
		/// </summary>
		public static TcpSubCMD SC_HEART_BEAT = new TcpSubCMD ("CS_HEART_BEAT", TcpMainCMD.HEART_BEAT | 0x0002);
//
		/// <summary>
		/// //玩家退出
		/// </summary>
		public static TcpSubCMD CS_PLYAER_EXIT = new TcpSubCMD ("CS_PLYAER_EXIT", TcpMainCMD.HEART_BEAT | 0x0003);

		/***************** GameAppMsg.ACTIVITY ******************/
		/// <summary>
		/// 抓小偷
		/// </summary>
		public static TcpSubCMD CS_CAPTURE_THIEF = new TcpSubCMD ("CS_CAPTURE_THIEF", TcpMainCMD.ACTIVITY | 0x0001);
		public static TcpSubCMD SC_CAPTURE_THIEF = new TcpSubCMD ("SC_CAPTURE_THIEF", TcpMainCMD.ACTIVITY | 0x0002);

        public static TcpSubCMD CS_ALL_ACHIEVEMENT = new TcpSubCMD("CS_ALL_ACHIEVEMENT", TcpMainCMD.ACTIVITY | 0x0003);// 请求所有成就
        public static TcpSubCMD SC_ALL_ACHIEVEMENT = new TcpSubCMD("SC_ALL_ACHIEVEMENT", TcpMainCMD.ACTIVITY | 0x0004);

	    public static TcpSubCMD SC_UPDATE_ACHIEVEMENT = new TcpSubCMD("SC_UPDATE_ACHIEVEMENT", TcpMainCMD.ACTIVITY | 0x0006);// 更新成就
	    public static TcpSubCMD SC_COMPLETE_ACHIEVEMENT =new TcpSubCMD("SC_COMPLETE_ACHIEVEMENT", TcpMainCMD.ACTIVITY | 0x0008);// 完成成就

	    public static TcpSubCMD CS_GET_REWARD = new TcpSubCMD("CS_GET_REWARD", TcpMainCMD.ACTIVITY | 0x0009);// 请求领取成就奖励
        public static TcpSubCMD SC_GET_REWARD = new TcpSubCMD("SC_GET_REWARD", TcpMainCMD.ACTIVITY | 0x000a);
	
		/****************************CHAT_MSG******************************/
		/// <summary>
		/// 聊天
		/// </summary>
		public static TcpSubCMD CS_CHAT_MESSAGE = new TcpSubCMD ("CS_CHAT_MESSAGE", TcpMainCMD.CHAT_MSG | 0x0001);
		public static TcpSubCMD SC_CHAT_MESSAGE = new TcpSubCMD ("SC_CHAT_MESSAGE", TcpMainCMD.CHAT_MSG | 0x0002);
	
		/****************************RANK_MSG******************************/
		/// <summary>
		/// 排行榜
		/// </summary>
		public static TcpSubCMD CS_GET_RANK = new TcpSubCMD ("CS_GET_RANK", TcpMainCMD.RANK_MSG | 0x0001);
		public static TcpSubCMD SC_GET_RANK = new TcpSubCMD ("SC_GET_RANK", TcpMainCMD.RANK_MSG | 0x0002);


		/***************** GameAppMsg.MAP ******************/
		public static TcpSubCMD CS_GET_MAP_DATA = new TcpSubCMD ("CS_GET_MAP_DATA", TcpMainCMD.MAP | 0x0001);
// 获取地图数据
		public static TcpSubCMD SC_GET_MAP_DATA = new TcpSubCMD ("SC_GET_MAP_DATA", TcpMainCMD.MAP | 0x0002);
// 获取地图数据

		public static TcpSubCMD CS_SAVE_EDIT_MAP_DATA = new TcpSubCMD ("CS_SAVE_EDIT_MAP_DATA", TcpMainCMD.MAP | 0x0003);
// 保存编辑后的地图数据
		public static TcpSubCMD SC_SAVE_EDIT_MAP_DATA = new TcpSubCMD ("SC_SAVE_EDIT_MAP_DATA", TcpMainCMD.MAP | 0x0004);
// 保存编辑后的地图数据

		public static TcpSubCMD CS_USE_EDIT_MAP_DATA = new TcpSubCMD ("CS_USE_EDIT_MAP_DATA", TcpMainCMD.MAP | 0x0005);
// 使用编辑好的地图数据
		public static TcpSubCMD SC_USE_EDIT_MAP_DATA = new TcpSubCMD ("SC_USE_EDIT_MAP_DATA", TcpMainCMD.MAP | 0x0006);
// 使用编辑好的地图数据

		public static TcpSubCMD CS_COPY_MAP_DATA = new TcpSubCMD ("CS_COPY_MAP_DATA", TcpMainCMD.MAP | 0x0007);
// 复制地图编辑
		public static TcpSubCMD SC_COPY_MAP_DATA = new TcpSubCMD ("SC_COPY_MAP_DATA", TcpMainCMD.MAP | 0x0008);
// 复制地图编辑

		/************************* GameAppMsg.BATTLE **********************/
		public static TcpSubCMD CS_GET_PVE_MAPS = new TcpSubCMD("CS_GET_PVE_MAPS", TcpMainCMD.BATTLE | 0x0001);//获取地图
		public static TcpSubCMD SC_GET_PVE_MAPS =  new TcpSubCMD("SC_GET_PVE_MAPS", TcpMainCMD.BATTLE | 0x0002);//获取地图

		public static TcpSubCMD CS_ENTER_FIGHT =  new TcpSubCMD("CS_ENTER_FIGHT", TcpMainCMD.BATTLE | 0x0003);//进入战斗
		public static TcpSubCMD SC_ENTER_FIGHT =  new TcpSubCMD("SC_ENTER_FIGHT", TcpMainCMD.BATTLE | 0x0004);//进入战斗

		public static TcpSubCMD CS_FIGHT_VAILD =  new TcpSubCMD("CS_FIGHT_VAILD", TcpMainCMD.BATTLE | 0x0005);//验证战斗
		public static TcpSubCMD SC_DROP_ITEM=  new TcpSubCMD("SC_DROP_ITEM", TcpMainCMD.BATTLE | 0x0006);//战斗中掉落

		public static TcpSubCMD CS_FIGHT_RESULT = new TcpSubCMD("CS_FIGHT_RESULT", TcpMainCMD.BATTLE | 0x0007);//战斗结算
		public static TcpSubCMD SC_FIGHT_RESULT =  new TcpSubCMD("SC_FIGHT_RESULT", TcpMainCMD.BATTLE | 0x0008);//战斗结算

    public static TcpSubCMD CS_USE_MEDICINE = new TcpSubCMD("CS_USE_MEDICINE", TcpMainCMD.BATTLE | 0x00009);//使用药剂

    public static TcpSubCMD CS_ENTER_LAIR = new TcpSubCMD("CS_ENTER_LAIR", TcpMainCMD.BATTLE | 0x000b);//进入龙穴

    public static TcpSubCMD CS_MEDICINE_VALID = new TcpSubCMD("CS_MEDICINE_VALID", TcpMainCMD.BATTLE | 0x000d);//战斗药剂验证


    public static TcpSubCMD CS_PVP_ENTER_FIGHT = new TcpSubCMD("CS_PVP_ENTER_FIGHT", TcpMainCMD.BATTLE | 0x000f);//pvp进入战斗
    public static TcpSubCMD SC_PVP_ENTER_FIGHT = new TcpSubCMD("SC_PVP_ENTER_FIGHT", TcpMainCMD.BATTLE | 0x0010);//pvp进入战斗

    public static TcpSubCMD CS_PVP_SEARCH = new TcpSubCMD("CS_PVP_SEARCH", TcpMainCMD.BATTLE | 0x0011);//pvp搜索
    public static TcpSubCMD SC_PVP_SEARCH = new TcpSubCMD("SC_PVP_SEARCH", TcpMainCMD.BATTLE | 0x0012);//pvp搜索

    public static TcpSubCMD SC_PVP_FIGHT_RESULT = new TcpSubCMD("SC_PVP_FIGHT_RESULT", TcpMainCMD.BATTLE | 0x00014);// 战斗结算

    public static TcpSubCMD CS_PVP_CD_SPEED = new TcpSubCMD("CS_PVP_CD_SPEED", TcpMainCMD.BATTLE | 0x00015);// 战斗CD加速
    public static TcpSubCMD SC_PVP_CD_SPEED = new TcpSubCMD("SC_PVP_CD_SPEED", TcpMainCMD.BATTLE | 0x00016);// 战斗CD加速

    /************************* GameAppMsg.MAIL **********************/
    public static TcpSubCMD CS_MAIL_READ_MAIL = new TcpSubCMD("CS_MAIL_READ_MAIL", TcpMainCMD.MAIL | 0x0001);//读邮件
	public static TcpSubCMD SC_MAIL_READ_MAIL =  new TcpSubCMD("SC_MAIL_READ_MAIL", TcpMainCMD.MAIL | 0x0002);//读邮件
	
	public static TcpSubCMD CS_PAGE_GET_MAILS = new TcpSubCMD("CS_PAGE_GET_MAILS", TcpMainCMD.MAIL | 0x0003);//分页拉取邮件
	public static TcpSubCMD SC_PAGE_GET_MAILS =  new TcpSubCMD("SC_PAGE_GET_MAILS", TcpMainCMD.MAIL | 0x0004);//分页拉取邮件
	
	public static TcpSubCMD CS_RECV_MAIL_ATTACH = new TcpSubCMD("CS_RECV_MAIL_ATTACH", TcpMainCMD.MAIL | 0x0005);//收取邮件奖励
	public static TcpSubCMD SC_RECV_MAIL_ATTACH =  new TcpSubCMD("SC_RECV_MAIL_ATTACH", TcpMainCMD.MAIL | 0x0006);//收取邮件奖励
	
	public static TcpSubCMD CS_QUERY_UNREAD_MAIL_NUM = new TcpSubCMD("CS_QUERY_UNREAD_MAIL_NUM", TcpMainCMD.MAIL | 0x0007);//获取未读邮件数量
	public static TcpSubCMD SC_QUERY_UNREAD_MAIL_NUM =  new TcpSubCMD("SC_QUERY_UNREAD_MAIL_NUM", TcpMainCMD.MAIL | 0x0008);//获取未读邮件数量
	
	public static TcpSubCMD SC_NEW_MAIL_NOTICE = new TcpSubCMD ("SC_NEW_MAIL_NOTICE", TcpMainCMD.MAIL | 0x000A);    //新邮件通知
	
	public static TcpSubCMD CS_PAGE_GET_FIGHT_RECORD = new TcpSubCMD("CS_PAGE_GET_FIGHT_RECORD", TcpMainCMD.MAIL | 0x000B);//分页拉取战报
	public static TcpSubCMD SC_PAGE_GET_FIGHT_RECORD =  new TcpSubCMD("SC_PAGE_GET_FIGHT_RECORD", TcpMainCMD.MAIL | 0x000C);//分页拉取战报
	
	public static TcpSubCMD CS_GET_APPLY_MAILS = new TcpSubCMD("CS_GET_APPLY_MAILS", TcpMainCMD.MAIL | 0x000D);//获取认证邮件
	public static TcpSubCMD SC_GET_APPLY_MAILS =  new TcpSubCMD("SC_GET_APPLY_MAILS", TcpMainCMD.MAIL | 0x000E);//获取认证邮件
	
	/************************* GameAppMsg.SOCIATY **********************/
	public static TcpSubCMD CS_SOCIATY_CREATE = new TcpSubCMD("CS_SOCIATY_CREATE", TcpMainCMD.SOCIATY | 0x0001);//创建公会
	public static TcpSubCMD SC_SOCIATY_CREATE =  new TcpSubCMD("SC_SOCIATY_CREATE", TcpMainCMD.SOCIATY | 0x0002);//创建公会
	
	public static TcpSubCMD CS_SOCIATY_DELETE = new TcpSubCMD("CS_SOCIATY_DELETE", TcpMainCMD.SOCIATY | 0x0003);//解散公会
	public static TcpSubCMD SC_SOCIATY_DELETE =  new TcpSubCMD("SC_SOCIATY_DELETE", TcpMainCMD.SOCIATY | 0x0004);//解散公会
	
	public static TcpSubCMD CS_SOCIATY_EXIT = new TcpSubCMD("CS_SOCIATY_EXIT", TcpMainCMD.SOCIATY | 0x0005);//退出公会
	public static TcpSubCMD SC_SOCIATY_EXIT =  new TcpSubCMD("SC_SOCIATY_EXIT", TcpMainCMD.SOCIATY | 0x0006);//退出公会
	
	public static TcpSubCMD CS_SOCIATY_MY_INFO = new TcpSubCMD("CS_SOCIATY_MY_INFO", TcpMainCMD.SOCIATY | 0x0007);//查看自己的公会
	public static TcpSubCMD SC_SOCIATY_MY_INFO =  new TcpSubCMD("SC_SOCIATY_MY_INFO", TcpMainCMD.SOCIATY | 0x0008);//查看自己的公会
	
	public static TcpSubCMD CS_SOCIATY_SEARCH_ID = new TcpSubCMD("CS_SOCIATY_SEARCH_ID", TcpMainCMD.SOCIATY | 0x0009);//按公会ID搜索公会
	public static TcpSubCMD SC_SOCIATY_SEARCH_ID =  new TcpSubCMD("SC_SOCIATY_SEARCH_ID", TcpMainCMD.SOCIATY | 0x000A);//按公会ID搜索公会
	
	public static TcpSubCMD CS_SOCIATY_KICK_MEMBER = new TcpSubCMD("CS_SOCIATY_KICK_MEMBER", TcpMainCMD.SOCIATY | 0x000B);//公会踢人
	public static TcpSubCMD SC_SOCIATY_KICK_MEMBER =  new TcpSubCMD("SC_SOCIATY_KICK_MEMBER", TcpMainCMD.SOCIATY | 0x000C);//公会踢人
	
	public static TcpSubCMD CS_SOCIATY_APPLY_JOIN = new TcpSubCMD("CS_SOCIATY_APPLY_JOIN", TcpMainCMD.SOCIATY | 0x000D);//申请加入公会
	public static TcpSubCMD SC_SOCIATY_APPLY_JOIN =  new TcpSubCMD("SC_SOCIATY_APPLY_JOIN", TcpMainCMD.SOCIATY | 0x000E);//申请加入公会
	
	public static TcpSubCMD CS_SOCIATY_TRANSFER_BOSS = new TcpSubCMD("CS_SOCIATY_TRANSFER_BOSS", TcpMainCMD.SOCIATY | 0x000F);//转让会长
	public static TcpSubCMD SC_SOCIATY_TRANSFER_BOSS =  new TcpSubCMD("SC_SOCIATY_TRANSFER_BOSS", TcpMainCMD.SOCIATY | 0x0010);//转让会长
	
	public static TcpSubCMD CS_SOCIATY_DEAL_APPLY = new TcpSubCMD("CS_SOCIATY_DEAL_APPLY", TcpMainCMD.SOCIATY | 0x0011);//公会处理申请
	public static TcpSubCMD SC_SOCIATY_DEAL_APPLY =  new TcpSubCMD("SC_SOCIATY_DEAL_APPLY", TcpMainCMD.SOCIATY | 0x0012);//公会处理申请
	
	public static TcpSubCMD CS_SOCIATY_INVITE_JOIN = new TcpSubCMD("CS_SOCIATY_INVITE_JOIN", TcpMainCMD.SOCIATY | 0x0013);//邀请加入公会
	public static TcpSubCMD SC_SOCIATY_INVITE_JOIN =  new TcpSubCMD("SC_SOCIATY_INVITE_JOIN", TcpMainCMD.SOCIATY | 0x0014);//邀请加入公会
	
	public static TcpSubCMD CS_SOCIATY_DEAL_INVITE = new TcpSubCMD("CS_SOCIATY_DEAL_INVITE", TcpMainCMD.SOCIATY | 0x0015);//玩家处理邀请
	public static TcpSubCMD SC_SOCIATY_DEAL_INVITE =  new TcpSubCMD("SC_SOCIATY_DEAL_INVITE", TcpMainCMD.SOCIATY | 0x0016);//玩家处理邀请
	
	public static TcpSubCMD CS_SOCIATY_ALL_MATCH_CONDITION = new TcpSubCMD("CS_SOCIATY_ALL_MATCH_CONDITION", TcpMainCMD.SOCIATY | 0x0017);//根据条件查找所有公会
	public static TcpSubCMD SC_SOCIATY_ALL_MATCH_CONDITION =  new TcpSubCMD("SC_SOCIATY_ALL_MATCH_CONDITION", TcpMainCMD.SOCIATY | 0x0018);//根据条件查找所有公会
	
	public static TcpSubCMD CS_SOCIATY_RANKS = new TcpSubCMD("CS_SOCIATY_RANKS", TcpMainCMD.SOCIATY | 0x0019);//获取公会排名
	public static TcpSubCMD SC_SOCIATY_RANKS =  new TcpSubCMD("SC_SOCIATY_RANKS", TcpMainCMD.SOCIATY | 0x001A);//获取公会排名
	
	public static TcpSubCMD CS_SOCIATY_MODIFY_JOIN_CONDITION  = new TcpSubCMD("CS_SOCIATY_MODIFY_JOIN_CONDITION ", TcpMainCMD.SOCIATY | 0x001B);//修改公会加入条件
	public static TcpSubCMD SC_SOCIATY_MODIFY_JOIN_CONDITION  =  new TcpSubCMD("SC_SOCIATY_MODIFY_JOIN_CONDITION", TcpMainCMD.SOCIATY | 0x001C);//修改公会加入条件
	
	public static TcpSubCMD CS_SOCIATY_MODIFY_NOTICE = new TcpSubCMD("CS_SOCIATY_MODIFY_NOTICE", TcpMainCMD.SOCIATY | 0x001D);//修改公会公告
	public static TcpSubCMD SC_SOCIATY_MODIFY_NOTICE =  new TcpSubCMD("SC_SOCIATY_MODIFY_NOTICE", TcpMainCMD.SOCIATY | 0x001E);//修改公会公告
	
	public static TcpSubCMD CS_SOCIATY_CHAT = new TcpSubCMD("CS_SOCIATY_CHAT", TcpMainCMD.SOCIATY | 0x001F);//公会聊天
	public static TcpSubCMD SC_SOCIATY_CHAT =  new TcpSubCMD("SC_SOCIATY_CHAT", TcpMainCMD.SOCIATY | 0x0020);//公会聊天

  public static TcpSubCMD CS_PREPARE_AGENT = new TcpSubCMD("CS_PREPARE_AGENT", TcpMainCMD.ITEM | 0x0021);//准备战斗界面的药剂通知

	
	public static TcpSubCMD SC_SOCIATY_KICKED_NOTIFY = new TcpSubCMD("SC_SOCIATY_KICKED_NOTIFY", TcpMainCMD.SOCIATY | 0x0022);//主动推送玩家被T
}

