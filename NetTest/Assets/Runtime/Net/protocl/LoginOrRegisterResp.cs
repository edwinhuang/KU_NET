using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Kubility;

public struct LoginOrRegisterResp : NetDataRespInterface
{
    public void DeSerialize (byte[] data)
    {

        NetByteBuffer buffer = new NetByteBuffer (data); 
        this.userId = (long)buffer;
        this.diamond = (int)buffer;
        this.gold = (int)buffer;
        this.iron = (int)buffer;
        this.power = (int)buffer;
        this.farmers = (short)buffer;
        this.competitive = (int)buffer;
        this.totalCompetitive = (int)buffer;
        this.protectTime = (int)buffer;
//        this.mapArray = buffer.ReadRespList<BuildTo> ();
//        this.soils = buffer.ReadRespList<SoilTo> ();
        this.nickname = (string)buffer;
        this.friendApplys = (short)buffer;
        this.sociatyName = (string)buffer;
        this.sociatyid = (int)buffer;
        this.unreadMail = (short)buffer;
        this.resThiefTime = (int)buffer;
        this.diamondThiefTime = (int)buffer;
        this.achieveAward = (short)buffer;
//        this.playerScience = buffer.ReadRespList<PlayerScienceTo> ();
        this.invite = (string)buffer;
        this.imgId = (string)buffer;
        this.attOverTime = (int)buffer;
        this.mapIndex = (int)buffer;
//        this.monsterBases = buffer.ReadRespList<MonsterBaseDataTo> ();
//        this.heroRecords = buffer.ReadRespList<HeroRecordTo> ();
//        this.agents = buffer.ReadRespList<AgentTo> ();
		
        this.totalMaxCup = (int)buffer;
        this.pvpWinCount = (int)buffer;
        this.pvpStarCount = (int)buffer;
        this.vectorStep = (int)buffer;

        this.pvpCdTime = (int)buffer;
        this.sandbox=(Bool8)buffer;
    }

    

    public long userId;
    public int diamond;
    public int gold;
    public int iron;
    /** 魔泉 */
    public int power;
    /** 目前可用的农民数量 */
    public short farmers;
    /** 奖杯数 */
    public int competitive;
    /** 奖杯数 */
    public int totalCompetitive;
    /** 距离护盾结束剩余的秒数 */
    public int protectTime;
//    public List<BuildTo> mapArray;

//    public List<SoilTo> soils;
// 地块
    // 建筑
    public string nickname;
    /** 好友申请数 */
    public short friendApplys;
    /** 公会名，没有则为 "" */
    public string sociatyName;
    /** 公会id，没有则为null */
    public int sociatyid;
    /** 未读邮件的数量 */
    public short unreadMail;
    /** 资源小偷下次出现的剩余时间 */
    public int resThiefTime;
    /** 钻石小偷下次出现的剩余时间 */
    public int diamondThiefTime;
    /** 成就总数 */
    public short achieveAward;
//    public List<PlayerScienceTo> playerScience;
    public string invite;
    // 邀请唯一标识
    public string imgId;
    // 头像
    public int attOverTime;
    // 战斗剩余保护时间
    public int mapIndex;
    // 玩家当前使用的地图索引
//    public List<MonsterBaseDataTo> monsterBases;
    // 怪物基础数据
//    public List<HeroRecordTo> heroRecords;
    // 玩家拥有的英雄
//    public List<AgentTo> agents;
    // 玩家的药剂属性
    public int totalMaxCup;
// 历史最高奖杯数
    public int pvpWinCount;
// pvp战斗胜利次数
    public int pvpStarCount;
// pvp三星胜利次数
    public int vectorStep;
// 新手引导步骤
    /// <summary>
    /// PvpCD
    /// </summary>
    public int pvpCdTime;

    public Bool8 sandbox;
}
