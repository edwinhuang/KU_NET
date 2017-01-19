using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Kubility;
using System;

public class UpdateTimeManager : ICommonClass
{

	/*

		public override void DispatcherEvents (ValueType data, BaseEnum main, BaseEnum sub, object callback)
		{
				S2CTimeUpdateResp Gamedata = (S2CTimeUpdateResp)System.Convert.ChangeType (data, typeof(S2CTimeUpdateResp));

				#if UNITY_EDITOR
				LogMgr.Log ("发现游戏时间更新包");
				#endif

				if (callback != null) {

						if (callback is Action<S2CTimeUpdateResp>) {
								var realCall = callback as Action<S2CTimeUpdateResp>;

								realCall.TryCall (Gamedata);
						} else {
								LogMgr.LogError ("类型不匹配");
						}
				} else {
						AutoUpdateData (Gamedata);
				}

		}

		void AutoUpdateData (S2CTimeUpdateResp netdata)
		{
				int bid = netdata.ID;

				if (netdata.type == (byte)TimeFromType.BUILD_LV_TIME) {
						LogUtils.LogError ("建筑时间校正 ", netdata.Value.ToString ());

						BuildComponent build = BuildComponent.BuildComponentList.Find (p => p.mBuildingInfo.buildID == bid);

						if (build != null) {

								if (MMtool.IsDragonEgg (build.mBuildingInfo)) {
										if (CompDetailGridContoller.mIns != null && CompDetailGridContoller.mIns.hatchController != null) {
												CompDetailGridContoller.mIns.hatchController.LocalEgg.BaseBuildUpdateData.RemainUpgradeTime = netdata.Value;
										}
								} else {
										if (build.mBuildingInfo != null) {
								
												build.mBuildingInfo.BaseBuildUpdateData.RemainUpgradeTime = netdata.Value;

												if (build.componentBuildTimer != null) {
														build.componentBuildTimer.Countdown = netdata.Value;
                                                        if (build.componentBuildTimer.mBuildingInfo == null)
                                                            build.componentBuildTimer.mBuildingInfo = build.mBuildingInfo;
                                                        BuildComponent buildComponent = MMtool.GetBuildComponentContainDragon(build.mBuildingInfo.buildID);

                                                        if (buildComponent.IsUpgrading) //建筑正在升级中，客户端与服务器时间不一致
                                                        {
                                                            build.mBuildingInfo.BaseBuildUpdateData.RemainUpgradeTime = netdata.Value;
                                                            build.mBuildingInfo.BaseBuildUpdateData.recodUpgradeTime = (int)Time.realtimeSinceStartup;
                                                            build.ComponentUpgrading(netdata.Value);
                                                        }
                                                        else //建筑未在升级中，客户端升级请求回包丢失时
                                                        {
                                                            //build.componentBuildTimer.BuildEndTimer();

                                                            buildComponent.IsUpgrading = true;
                                                            build.mBuildingInfo.BaseBuildUpdateData.RemainUpgradeTime = netdata.Value;
                                                            build.mBuildingInfo.BaseBuildUpdateData.recodUpgradeTime = (int)Time.realtimeSinceStartup;
                                                            build.ComponentUpgrading(netdata.Value);
                                                            build.UpdateCompDetailGridContoller();
                                                        }
                                                           
												}
							
										}
								}
						}
				} else if (netdata.type == (byte)TimeFromType.MONSTER_TIME) {
						LogUtils.LogError ("怪物时间校正 ", netdata.Value.ToString ());
						BuildComponent build = BuildComponent.BuildComponentList.Find (p => p.mBuildingInfo.buildID == bid);
			   			if (build != null) {
							build.TimeUpdateData(netdata);
						}

                }
                else if (netdata.type == (byte)TimeFromType.RECIPE_TIME)
                {
						LogUtils.LogError ("配方时间校正 ", netdata.Value.ToString ());


                }
                else if (netdata.type == (byte)TimeFromType.SOIL_TIME)
                {
						LogUtils.LogError ("挖土时间校正 ", netdata.Value.ToString ());

						if (LocalValue.mIns.gameState == GameState.Normal) {

								for (int i = 0; i < MapController.mIns.maps.GetLength (0); ++i) {
										for (int j = 0; j < MapController.mIns.maps.GetLength (1); ++j) {
												Map map = MapController.mIns.maps [i, j];
												if (map != null && map.buildingInfo != null && map.buildingInfo.buildID == bid) {
														map.nextTime = netdata.Value;
												}
										}
								}
						}
                }
                else if (netdata.type == (byte)TimeFromType.PVPCD_TIME)
                {
                    LogUtils.LogError("PVP时间校正 ", netdata.Value.ToString());

                    LocalValue.mIns.pvpCdTime = netdata.Value;
                    LocalValue.mIns.pvpRecordeTime = (int)Time.realtimeSinceStartup;
                    TeamController.mIns.ButtonBackClick();

                }
		}

		public UpdateTimeManager (TcpSubCMD subdata)
		{
				this.data = subdata;
				CommonManager.mIns.Register (this);
		}

		public static void Wait_Event (Action<S2CTimeUpdateResp> Callback)
		{
				CommonManager.mIns.Wait_Resp<S2CTimeUpdateResp> (TcpMainCMD.COMMON, TcpSubCMD.SC_TIME_UPDATE, Callback);
		}

		public static void Wait_Event_AutoRomove (Action<S2CTimeUpdateResp> Callback)
		{
				Wait_Event (Callback);
				MonoDelegate.mIns.Coroutine_Delay (delay, delegate() {

						CommonManager.mIns.Remove_Resp<S2CTimeUpdateResp> (TcpMainCMD.COMMON, TcpSubCMD.SC_TIME_UPDATE, Callback);
				});

		}

		public override void Destroy ()
		{
		
		}
//*/
}
