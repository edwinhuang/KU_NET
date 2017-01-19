//#define KDEBUG
using UnityEngine;
using System.Collections;
using Kubility;
using System.Text;

public class StructMessFactory : DataInterface
{
		public StructMessFactory (MessageDataType type)
				: base (type)
		{

		}

		public override BaseMessage DynamicCreate (byte[] data, MessageHead head)
		{
				StructMessage value = null;
#if UNITY_EDITOR
				BaseEnum e1 = (BaseEnum)head.MainCMD;
				BaseEnum e2 = (BaseEnum)head.SubCMD;
				if (e1 != null && e2 != null) {
						LogMgr.Log ("消息头 MAIN =" + e1.ToString () + "   sub =" + e2.ToString ());
				} else {
						LogMgr.Log ("消息头 MAIN =" + head.MainCMD.ToString () + "   sub =" + head.SubCMD.ToString ());
				}



#endif

				#if KDEBUG
				LogMgr.Log ("DynamicCreate  " + data.Length);


				StringBuilder sb = new StringBuilder ();
				for (int i = 0; i < data.Length; ++i) {
						sb.Append (data [i].ToString () + " -> ");
				}
				sb.Append (" End");

				LogMgr.Log ("Info = " + sb.ToString ());
				#endif

				if (MessageInfo.MessageTypeCheck (MessageDataType.Struct)) {
						if (head.MainCMD == TcpMainCMD.LOGIN) {//Login
								if (head.SubCMD == TcpSubCMD.SC_LOGIN) {
										LoginOrRegisterResp Resp = new LoginOrRegisterResp ();
										Resp.DeSerialize (data);
										value = StructMessage.CreateResp (head, Resp);
								}
						}
            #region build
            else if (head.MainCMD == TcpMainCMD.BUILD) {
								if (head.SubCMD == TcpSubCMD.SC_BUILD_FINISH) {
										BuildUpgradeFinishResp Resp = new BuildUpgradeFinishResp ();
										Resp.DeSerialize (data);
										value = StructMessage.CreateResp (head, Resp);
								} else if (head.SubCMD == TcpSubCMD.SC_BUILD_UPGRADE) {

										BuildUpgradeResp Resp = new BuildUpgradeResp ();
										Resp.DeSerialize (data);
										value = StructMessage.CreateResp (head, Resp);
								} else if (head.SubCMD == TcpSubCMD.SC_BUILD_RECIPE) {

								} else if (head.SubCMD == TcpSubCMD.SC_BUILD_MONSTER) {

								} else if (head.SubCMD == TcpSubCMD.SC_ALL_BUILD_INFO) {
										BuildInfoResp Resp = new BuildInfoResp ();
										Resp.DeSerialize (data);
										value = StructMessage.CreateResp (head, Resp);
								} else if (head.SubCMD == TcpSubCMD.SC_HARVEST_RESOURCE) {
										HarvestResourceResp Resp = new HarvestResourceResp ();
										Resp.DeSerialize (data);
										value = StructMessage.CreateResp (head, Resp);
								} else if (head.SubCMD == TcpSubCMD.SC_AGENT_UPGRADE) {
										PotionUpgradeResp msg = new PotionUpgradeResp ();
										msg.DeSerialize (data);
										value = StructMessage.CreateResp (head, msg);
								} else if (head.SubCMD == TcpSubCMD.SC_AGENT_UPGRADE_FINISH) {
										PotionUpgradeFinishResp msg = new PotionUpgradeFinishResp ();
										msg.DeSerialize (data);
										value = StructMessage.CreateResp (head, msg);
								} else if (head.SubCMD == TcpSubCMD.SC_AGENT_UPGRADE_FAST_FINISH) {
										PotionUpgradeFastFinishResp msg = new PotionUpgradeFastFinishResp ();
										msg.DeSerialize (data);
										value = StructMessage.CreateResp (head, msg);
								}
						}
            #endregion
            #region soil
            else if (head.MainCMD == TcpMainCMD.SOIL) {
								if (head.SubCMD == TcpSubCMD.SC_SOIL_FINISH) {
										DigSoilFinishResp Resp = new DigSoilFinishResp ();
										Resp.DeSerialize (data);
										value = StructMessage.CreateResp (head, Resp);
								} else if (head.SubCMD == TcpSubCMD.SC_DIG_SOIL) {
										DigSoilResp Resp = new DigSoilResp ();
										Resp.DeSerialize (data);
										value = StructMessage.CreateResp (head, Resp);


								}
						}
            #endregion
            #region hero
            else if (head.MainCMD == TcpMainCMD.HERO) {
								if (head.SubCMD == TcpSubCMD.SC_HERO_QUALITY) {

										HeroQualityResp Resp = new HeroQualityResp ();
										Resp.DeSerialize (data);
										value = StructMessage.CreateResp (head, Resp);
								} else if (head.SubCMD == TcpSubCMD.SC_HERO_UPGRADE) {
										HeroUpgradeResp Resp = new HeroUpgradeResp ();
										Resp.DeSerialize (data);
										value = StructMessage.CreateResp (head, Resp);
								} else if (head.SubCMD == TcpSubCMD.SC_HERO_SKILL_UPGRADE) {
										HeroSkillUpgradeResp Resp = new HeroSkillUpgradeResp ();
										Resp.DeSerialize (data);
										value = StructMessage.CreateResp (head, Resp);
								} else if (head.SubCMD == TcpSubCMD.SC_HERO_SET_TEAM) {
										SetTeamResp resp = new SetTeamResp ();
										resp.DeSerialize (data);
										value = StructMessage.CreateResp (head, resp);
								} else if (head.SubCMD == TcpSubCMD.SC_HERO_STAR_LEVEL) {
										HeroStarResp resp = new HeroStarResp ();
										resp.DeSerialize (data);
										value = StructMessage.CreateResp (head, resp);
								}
						}
            #endregion
            else if (head.MainCMD == TcpMainCMD.EQUIP) {
								if (head.SubCMD == TcpSubCMD.SC_EQUIP_GRADE) {
										EquipUpgradeResp Resp = new EquipUpgradeResp ();
										Resp.DeSerialize (data);
										value = StructMessage.CreateResp (head, Resp);
								}
						}

            #region produce
            else if (head.MainCMD == TcpMainCMD.PRODUCE) {
								if (head.SubCMD == TcpSubCMD.SC_PRODUCE_MONSTER) {
										ProduceMonsterResp Resp = new ProduceMonsterResp ();
										Resp.DeSerialize (data);
										value = StructMessage.CreateResp (head, Resp);
								} else if (head.SubCMD == TcpSubCMD.SC_PRODUCE_MONSTER_FINISH_ONE) {
										ProduceMonsterFinishOneResp Resp = new ProduceMonsterFinishOneResp ();
										Resp.DeSerialize (data);
										value = StructMessage.CreateResp (head, Resp);
								} else if (head.SubCMD == TcpSubCMD.SC_PRODUCE_MONSTER_FAST_FINISH) {
										ProduceMonsterFastFinishResp Resp = new ProduceMonsterFastFinishResp ();
										value = StructMessage.CreateResp (head, Resp);
								} else if (head.SubCMD == TcpSubCMD.SC_PRODUCE_MONSTER_CANCEL) {
										ProduceMonsterCancelResp Resp = new ProduceMonsterCancelResp ();
										Resp.DeSerialize (data);
										value = StructMessage.CreateResp (head, Resp);
								} else if (head.SubCMD == TcpSubCMD.SC_PRODUCE_RECIPE) {
										ProduceRecipeResp Resp = new ProduceRecipeResp ();
										Resp.DeSerialize (data);
										value = StructMessage.CreateResp (head, Resp);
								} else if (head.SubCMD == TcpSubCMD.SC_PRODUCE_RECIPE_FAST_FINISH) {
										ProduceRecipeFastFinishResp Resp = new ProduceRecipeFastFinishResp ();
										value = StructMessage.CreateResp (head, Resp);
								} else if (head.SubCMD == TcpSubCMD.SC_PRODUCE_RECIPE_TAKE) {
										ProduceTakeRecipeResp Resp = new ProduceTakeRecipeResp ();
										Resp.DeSerialize (data);
										value = StructMessage.CreateResp (head, Resp);
								} else if (head.SubCMD == TcpSubCMD.SC_PRODUCE_RECIPE_CANCEL) {
										ProduceRecipeCancelResp Resp = new ProduceRecipeCancelResp ();
										Resp.DeSerialize (data);
										value = StructMessage.CreateResp (head, Resp);
								}
						}
            #endregion
            #region monster
            else if (head.MainCMD == TcpMainCMD.MONSTER) {

								if (head.SubCMD == TcpSubCMD.SC_MONSTER_UPGRADE) {
										MonsterUpgradeResp Resp = new MonsterUpgradeResp ();
										Resp.DeSerialize (data);
										value = StructMessage.CreateResp (head, Resp);
								} else if (head.SubCMD == TcpSubCMD.SC_MONSTER_QUALITY) {
										MonsterQualityResp Resp = new MonsterQualityResp ();
										Resp.DeSerialize (data);
										value = StructMessage.CreateResp (head, Resp);
								} else if (head.SubCMD == TcpSubCMD.SC_MONSTER_DELETE) {
										MonsterDeleteResp Resp = new MonsterDeleteResp ();
										value = StructMessage.CreateResp (head, Resp);
								} else if (head.SubCMD == TcpSubCMD.SC_MONSTER_RELIVE) {
										MonsterReliveResp Resp = new MonsterReliveResp ();
										value = StructMessage.CreateResp (head, Resp);
								}
						}
            #endregion
            #region shop
            else if (head.MainCMD == TcpMainCMD.SHOP) {
								if (head.SubCMD == TcpSubCMD.SC_BUY) {
										BuyResp Resp = new BuyResp ();
										Resp.DeSerialize (data);
										value = StructMessage.CreateResp (head, Resp);
								} else if (head.SubCMD == TcpSubCMD.SC_BUY_RESOURCE) {
										QuickBuyResourceResp Resp = new QuickBuyResourceResp ();
										//										Resp.DeSerialize (data);
										value = StructMessage.CreateResp (head, Resp);
								} else if (head.SubCMD == TcpSubCMD.SC_REFRESH_SHOP) {
										ShopInfoResp Resp = new ShopInfoResp ();
										Resp.DeSerialize (data);
										value = StructMessage.CreateResp (head, Resp);
								} else if (head.SubCMD == TcpSubCMD.SC_ENTER_SHOP) {
										ShopInfoResp Resp = new ShopInfoResp ();
										Resp.DeSerialize (data);
										value = StructMessage.CreateResp (head, Resp);
								} else if (head.SubCMD == TcpSubCMD.SC_ENTER_SHIELD) {
										ProtectTimeResp Resp = new ProtectTimeResp ();
										Resp.DeSerialize (data);
										value = StructMessage.CreateResp (head, Resp);
								}
						}
            #endregion
            #region Common
            else if (head.MainCMD == TcpMainCMD.COMMON) {
								if (head.SubCMD == TcpSubCMD.SC_ERRO_MSG) {
										ErrorMsgResp msg = new ErrorMsgResp ();
										msg.DeSerialize (data);
										value = StructMessage.CreateResp (head, msg);
								} else if (head.SubCMD == TcpSubCMD.SC_TIME_UPDATE) {
										S2CTimeUpdateResp msg = new S2CTimeUpdateResp ();
										msg.DeSerialize (data);
										value = StructMessage.CreateResp (head, msg);
								} else if (head.SubCMD == TcpSubCMD.SC_WAREHOUSE_ITEM_ADD) {
										S2cWareHouseItemAdd msg = new S2cWareHouseItemAdd ();
										msg.DeSerialize (data);
										value = StructMessage.CreateResp (head, msg);
								} else if (head.SubCMD == TcpSubCMD.SC_WAREHOUSE_ITEM_CHANGE) {
										S2cItemChange msg = new S2cItemChange ();
										msg.DeSerialize (data);
										value = StructMessage.CreateResp (head, msg);
								} else if (head.SubCMD == TcpSubCMD.SC_WAREHOUSE_LIST) {
										S2cWareHouseList msg = new S2cWareHouseList ();
										msg.DeSerialize (data);
										value = StructMessage.CreateResp (head, msg);
								} else if (head.SubCMD == TcpSubCMD.SC_WAREHOUSE_REMOVE) {
										S2cWareHouseItemRemove msg = new S2cWareHouseItemRemove ();
										msg.DeSerialize (data);
										value = StructMessage.CreateResp (head, msg);

								} else if (head.SubCMD == TcpSubCMD.SC_BASE_DATA) {
										S2cGameDataResp msg = new S2cGameDataResp ();
										msg.DeSerialize (data);
										value = StructMessage.CreateResp (head, msg);
								} else if (head.SubCMD == TcpSubCMD.SC_WAREHOUSE_LIST) {
										S2cWareHouseList msg = new S2cWareHouseList ();
										msg.DeSerialize (data);
										value = StructMessage.CreateResp (head, msg);
								} else if (head.SubCMD == TcpSubCMD.SC_VECTOR_STEP) {
										TutorialStepResp msg = new TutorialStepResp ();
										msg.DeSerialize (data);
										value = StructMessage.CreateResp (head, msg);
								} else if (head.SubCMD == TcpSubCMD.SC_SET_NAME) {
										NickNameResp msg = new NickNameResp ();                    
										value = StructMessage.CreateResp (head, msg);
								} else if (head.SubCMD == TcpSubCMD.SC_VIEW_OTHER_ROLE) {
										ViewOtherRoleResp msg = new ViewOtherRoleResp ();
										msg.DeSerialize (data);
										value = StructMessage.CreateResp (head, msg);
								} else if (head.SubCMD == TcpSubCMD.SC_SET_ROLE_ICON) {
										UpdateRoleImgResp msg = new UpdateRoleImgResp ();
//					msg.DeSerialize(data);
										value = StructMessage.CreateResp (head, msg);
								} else if (head.SubCMD == TcpSubCMD.SC_VECTOR_MARK_LIST) {
										TutorialInfoResp msg = new TutorialInfoResp ();
										msg.DeSerialize (data);
										value = StructMessage.CreateResp (head, msg);
								} else if (head.SubCMD == TcpSubCMD.SC_VECTOR_MARK) {
										TutorialStepResp msg = new TutorialStepResp ();
										msg.DeSerialize (data);
										value = StructMessage.CreateResp (head, msg);
								}
						}
            #endregion
            else if (head.MainCMD == TcpMainCMD.HEART_BEAT) {
								if (head.SubCMD == TcpSubCMD.SC_HEART_BEAT) {
										EmptyResp msg = new EmptyResp ();
										value = StructMessage.CreateResp (head, msg);
								}

						}
            #region activity
            else if (head.MainCMD == TcpMainCMD.ACTIVITY) {
								if (head.SubCMD == TcpSubCMD.SC_CAPTURE_THIEF) {
										CaptureThiefResp msg = new CaptureThiefResp ();
										msg.DeSerialize (data);
										value = StructMessage.CreateResp (head, msg);
								} else if (head.SubCMD == TcpSubCMD.SC_ALL_ACHIEVEMENT) {
										AllAchievementResp msg = new AllAchievementResp ();
										msg.DeSerialize (data);
										value = StructMessage.CreateResp (head, msg);
								} else if (head.SubCMD == TcpSubCMD.SC_UPDATE_ACHIEVEMENT) {
										UpdateAchievingResp msg = new UpdateAchievingResp ();
										msg.DeSerialize (data);
										value = StructMessage.CreateResp (head, msg);
								} else if (head.SubCMD == TcpSubCMD.SC_COMPLETE_ACHIEVEMENT) {
										UpdateAchievedResp msg = new UpdateAchievedResp ();
										msg.DeSerialize (data);
										value = StructMessage.CreateResp (head, msg);
								} else if (head.SubCMD == TcpSubCMD.SC_GET_REWARD) {
										RecvAchieveAwardResp msg = new RecvAchieveAwardResp ();
										msg.DeSerialize (data);
										value = StructMessage.CreateResp (head, msg);
								}

						}
            #endregion
            #region map
            else if (head.MainCMD == TcpMainCMD.MAP) {
								if (head.SubCMD == TcpSubCMD.SC_GET_MAP_DATA) {
										GetEditableMapResp msg = new GetEditableMapResp ();
										msg.DeSerialize (data);
										value = StructMessage.CreateResp (head, msg);
								} else if (head.SubCMD == TcpSubCMD.SC_SAVE_EDIT_MAP_DATA) {
										SaveEditableMapResp msg = new SaveEditableMapResp ();
										msg.DeSerialize (data);
										value = StructMessage.CreateResp (head, msg);
								} else if (head.SubCMD == TcpSubCMD.SC_USE_EDIT_MAP_DATA) {
										ApplyEditableMapResp msg = new ApplyEditableMapResp ();
										msg.DeSerialize (data);
										value = StructMessage.CreateResp (head, msg);
								} else if (head.SubCMD == TcpSubCMD.SC_COPY_MAP_DATA) {
										CopyEditableMapResp msg = new CopyEditableMapResp ();
										msg.DeSerialize (data);
										value = StructMessage.CreateResp (head, msg);
								}
						} else if (head.MainCMD == TcpMainCMD.RANK_MSG) {
								if (head.SubCMD == TcpSubCMD.SC_GET_RANK) {
										RankResp msg = new RankResp ();
										msg.DeSerialize (data);
										value = StructMessage.CreateResp (head, msg);
								}

						}
            #endregion
            #region battle
            else if (head.MainCMD == TcpMainCMD.BATTLE) {
								if (head.SubCMD == TcpSubCMD.SC_GET_PVE_MAPS) {
										PveGetAllMapsResp msg = new PveGetAllMapsResp ();
										msg.DeSerialize (data);
										value = StructMessage.CreateResp (head, msg);
								} else if (head.SubCMD == TcpSubCMD.SC_DROP_ITEM) {
										BattleDropItemResp msg = new BattleDropItemResp ();
										msg.DeSerialize (data);
										value = StructMessage.CreateResp (head, msg);
								} else if (head.SubCMD == TcpSubCMD.SC_FIGHT_RESULT) {
										BattleResultResp msg = new BattleResultResp ();
										msg.DeSerialize (data);
										value = StructMessage.CreateResp (head, msg);
								} else if (head.SubCMD == TcpSubCMD.SC_ENTER_FIGHT) {
										EmptyResp msg = new EmptyResp ();
										value = StructMessage.CreateResp (head, msg);
								} else if (head.SubCMD == TcpSubCMD.SC_PVP_ENTER_FIGHT) {
										EmptyResp msg = new EmptyResp ();
										value = StructMessage.CreateResp (head, msg);
								} else if (head.SubCMD == TcpSubCMD.SC_PVP_SEARCH) {
										PvpSearchResp msg = new PvpSearchResp ();
										msg.DeSerialize (data);
										value = StructMessage.CreateResp (head, msg);
								} else if (head.SubCMD == TcpSubCMD.SC_PVP_FIGHT_RESULT) {
										PVPResultResp msg = new PVPResultResp ();
										msg.DeSerialize (data);
										value = StructMessage.CreateResp (head, msg);
								} else if (head.SubCMD == TcpSubCMD.SC_PVP_CD_SPEED) {
										PvpCdSpeedResp msg = new PvpCdSpeedResp ();
										msg.DeSerialize (data);
										value = StructMessage.CreateResp (head, msg);
								}
						}
            #endregion
            #region mail
            else if (head.MainCMD == TcpMainCMD.MAIL) {
								if (head.SubCMD == TcpSubCMD.SC_MAIL_READ_MAIL) {
										ReadMailResp msg = new ReadMailResp ();
										msg.DeSerialize (data);
										value = StructMessage.CreateResp (head, msg);
								} else if (head.SubCMD == TcpSubCMD.SC_PAGE_GET_MAILS) {
										PageGetMailsResp msg = new PageGetMailsResp ();
										msg.DeSerialize (data);
										value = StructMessage.CreateResp (head, msg);
								} else if (head.SubCMD == TcpSubCMD.SC_RECV_MAIL_ATTACH) {
										RecvMailAttachResp msg = new RecvMailAttachResp ();
										msg.DeSerialize (data);
										value = StructMessage.CreateResp (head, msg);
								} else if (head.SubCMD == TcpSubCMD.SC_QUERY_UNREAD_MAIL_NUM) {
										QueryUnreadMailNumResp msg = new QueryUnreadMailNumResp ();
										msg.DeSerialize (data);
										value = StructMessage.CreateResp (head, msg);
								} else if (head.SubCMD == TcpSubCMD.SC_NEW_MAIL_NOTICE) {
										NewMailNoticeResp msg = new NewMailNoticeResp ();
										msg.DeSerialize (data);
										value = StructMessage.CreateResp (head, msg);
								} else if (head.SubCMD == TcpSubCMD.SC_PAGE_GET_FIGHT_RECORD) {
										PageGetFightRecordsResp msg = new PageGetFightRecordsResp ();
										msg.DeSerialize (data);
										value = StructMessage.CreateResp (head, msg);
								} else if (head.SubCMD == TcpSubCMD.SC_GET_APPLY_MAILS) {
										GetApplyMailsResp msg = new GetApplyMailsResp ();
										msg.DeSerialize (data);
										value = StructMessage.CreateResp (head, msg);
								}	
						}
            #endregion
			
			#region society
			else if (head.MainCMD == TcpMainCMD.SOCIATY) {
								if (head.SubCMD == TcpSubCMD.SC_SOCIATY_CREATE) { //创建公会
										SociatyCreateResp msg = new SociatyCreateResp ();
										msg.DeSerialize (data);
										value = StructMessage.CreateResp (head, msg);
								} else if (head.SubCMD == TcpSubCMD.SC_SOCIATY_DELETE) {    //解散公会
										SociatyDeleteResp msg = new SociatyDeleteResp ();
//					msg.DeSerialize(data);
										value = StructMessage.CreateResp (head, msg);
								} else if (head.SubCMD == TcpSubCMD.SC_SOCIATY_EXIT) {      //退出公会
										SociatyExitResp msg = new SociatyExitResp ();
//					msg.DeSerialize(data);
										value = StructMessage.CreateResp (head, msg);
								} else if (head.SubCMD == TcpSubCMD.SC_SOCIATY_MY_INFO) {   //查看自己的公会
										SociatyMyInfoResp msg = new SociatyMyInfoResp ();
										msg.DeSerialize (data);
										value = StructMessage.CreateResp (head, msg);
								} else if (head.SubCMD == TcpSubCMD.SC_SOCIATY_SEARCH_ID) { //按公会ID搜索公会
										SociatySearchIdResp msg = new SociatySearchIdResp ();
										msg.DeSerialize (data);
										value = StructMessage.CreateResp (head, msg);
								} else if (head.SubCMD == TcpSubCMD.SC_SOCIATY_KICK_MEMBER) {       //公会踢人
										SociatyKickMemberResp msg = new SociatyKickMemberResp ();
//					msg.DeSerialize(data);
										value = StructMessage.CreateResp (head, msg);
								} else if (head.SubCMD == TcpSubCMD.SC_SOCIATY_APPLY_JOIN) {        //申请加入公会
										SociatyApplyJoinResp msg = new SociatyApplyJoinResp ();
										msg.DeSerialize (data);
										value = StructMessage.CreateResp (head, msg);
								} else if (head.SubCMD == TcpSubCMD.SC_SOCIATY_TRANSFER_BOSS) {     //转让会长
										SociatyTransferBossResp msg = new SociatyTransferBossResp ();
//					msg.DeSerialize(data);
										value = StructMessage.CreateResp (head, msg);
								} else if (head.SubCMD == TcpSubCMD.SC_SOCIATY_DEAL_APPLY) {        //公会处理申请
										SociatyDealApplyResp msg = new SociatyDealApplyResp ();
//					msg.DeSerialize(data);
										value = StructMessage.CreateResp (head, msg);
								} else if (head.SubCMD == TcpSubCMD.SC_SOCIATY_INVITE_JOIN) {       //邀请加入公会
										SociatyInviteJoinResp msg = new SociatyInviteJoinResp ();
//					msg.DeSerialize(data);
										value = StructMessage.CreateResp (head, msg);
								} else if (head.SubCMD == TcpSubCMD.SC_SOCIATY_DEAL_INVITE) {       //玩家处理邀请
										SociatyDealInviteResp msg = new SociatyDealInviteResp ();
//					msg.DeSerialize(data);
										value = StructMessage.CreateResp (head, msg);
								} else if (head.SubCMD == TcpSubCMD.SC_SOCIATY_ALL_MATCH_CONDITION) {       //根据条件查找所有公会
										SociatyAllMatchResp msg = new SociatyAllMatchResp ();
										msg.DeSerialize (data);
										value = StructMessage.CreateResp (head, msg);
								} else if (head.SubCMD == TcpSubCMD.SC_SOCIATY_RANKS) {     //获取公会排名
										SociatyGetRankResp msg = new SociatyGetRankResp ();
										msg.DeSerialize (data);
										value = StructMessage.CreateResp (head, msg);
								} else if (head.SubCMD == TcpSubCMD.SC_SOCIATY_MODIFY_JOIN_CONDITION) {     //修改公会加入条件
										SociatyModifyJoinConditionResp msg = new SociatyModifyJoinConditionResp ();
//					msg.DeSerialize(data);
										value = StructMessage.CreateResp (head, msg);
								} else if (head.SubCMD == TcpSubCMD.SC_SOCIATY_MODIFY_NOTICE) {     //修改公会公告
										SociatyModifyNoticeResp msg = new SociatyModifyNoticeResp ();
//					msg.DeSerialize(data);
										value = StructMessage.CreateResp (head, msg);
								} else if (head.SubCMD == TcpSubCMD.SC_SOCIATY_CHAT) {     //公会聊天
										S2SSociatyChatRsp msg = new S2SSociatyChatRsp ();
										msg.DeSerialize (data);
										value = StructMessage.CreateResp (head, msg);
								} else if (head.SubCMD == TcpSubCMD.SC_SOCIATY_KICKED_NOTIFY) {     //主动推送玩家被T
										S2SSociatyKickedOutRsp msg = new S2SSociatyKickedOutRsp ();
//					msg.DeSerialize(data);
										value = StructMessage.CreateResp (head, msg);
								}
						}
						#endregion
				}

				return value;
		}


		private BaseMessage Create ()
		{
				return null;
		}
}
