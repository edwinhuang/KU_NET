using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public enum GameCMD {RECONNECT}
/// <summary>
/// 这个版本暂时取消自动移除
/// </summary>
public class GameNotificationCenter : SingleTon<GameNotificationCenter>
{
    public Action AutoClear;

    #region for kbsinglton
    Dictionary<string, BarQueue<Action>> MsgDic = new Dictionary<string, BarQueue<Action>>();

    Queue<Action> ForQueue = new Queue<Action>();
    #endregion


    Dictionary<int, List<int>> TargetDic = new Dictionary<int, List<int>>();

    Dictionary<int, object> AllList = new Dictionary<int, object>();

    #region net aysnc to syc
    Queue<int> SysQueue = new Queue<int>();
    #endregion

    private class Monitor : MonoBehaviour
    {
        public int listenerTarget;


        void OnDestroy()
        {
            //#if UNITY_EDITOR
            //LogMgr.Log("移除 = "+listenerTarget.ToString());
            //#endif

            if (GameNotificationCenter._mins != null && GameNotificationCenter._mins.TargetDic.ContainsKey(listenerTarget))
            {
                GameNotificationCenter._mins.TargetDic.Remove(listenerTarget);
                //#if UNITY_EDITOR
                //LogMgr.Log("移除成功 +"+listenerTarget);
                //#endif
            }

        }
    }

    public void RegisterMonoBehaviour()
    {
        GlobalHelper.mIns.RegisterUpdate(WebServer.mIns,DoUpdate);
    }

    void DoUpdate()
    {
        while (ForQueue.Count > 0)
        {
            Action ev = ForQueue.Dequeue();
            if (ev != null)
                ev();
        }

        while (SysQueue.Count > 0)
        {
            int key = SysQueue.Dequeue();
            if (AllList.ContainsKey(key))
            {
                PostNotice(key);
                AllList.Remove(key);
            }
        }



    }



    #region noticemanager

    #region Func

    //func------------

    public static void AddObserver<T>(MonoBehaviour target, GameCMD eventID, Func<T> callback)
    {
        AddObserver<T>(target, (int)eventID, callback);
    }

    public static void AddObserver<T, U>(MonoBehaviour target, GameCMD eventID, Func<T, U> callback)
    {
        AddObserver<T, U>(target, (int)eventID, callback);
    }

    public static void AddObserver<T, U, V>(MonoBehaviour target, GameCMD eventID, Func<T, U, V> callback)
    {
        AddObserver<T, U, V>(target, (int)eventID, callback);
    }

    //func
    public static void AddObserver<T>(MonoBehaviour target, int eventID, Func<T> callback)
    {
        int hashCode = target.GetHashCode();

        if (mIns.TargetDic.ContainsKey(hashCode))
        {

            mIns.TargetDic[hashCode].Add(eventID);
            mIns.AllList.TryAdd(eventID, callback);
        }
        else
        {
            mIns.TargetDic.Add(hashCode, new List<int>() { eventID });
            if (mIns.AllList.TryAdd(eventID, callback))
            {
                Monitor m = target.gameObject.AddComponent<Monitor>();
                m.listenerTarget = hashCode;
            }
        }
    }

    public static void AddObserver<T, U>(MonoBehaviour target, int eventID, Func<T, U> callback)
    {
        int hashCode = target.GetHashCode();
        if (mIns.TargetDic.ContainsKey(hashCode))
        {

            mIns.TargetDic[hashCode].Add(eventID);
            mIns.AllList.TryAdd(eventID, callback);
        }
        else
        {
            mIns.TargetDic.Add(hashCode, new List<int>() { eventID });
            if (mIns.AllList.TryAdd(eventID, callback))
            {
                Monitor m = target.gameObject.AddComponent<Monitor>();
                m.listenerTarget = hashCode;
            }
        }
    }

    public static void AddObserver<T, U, V>(MonoBehaviour target, int eventID, Func<T, U, V> callback)
    {
        int hashCode = target.GetHashCode();
        if (mIns.TargetDic.ContainsKey(hashCode))
        {

            mIns.TargetDic[hashCode].Add(eventID);
            mIns.AllList.TryAdd(eventID, callback);
        }
        else
        {
            mIns.TargetDic.Add(hashCode, new List<int>() { eventID });
            if (mIns.AllList.TryAdd(eventID, callback))
            {
                Monitor m = target.gameObject.AddComponent<Monitor>();
                m.listenerTarget = hashCode;
            }
        }
    }

    //--func
    public static T PostNoticeAsFunc<T>(int eventID)
    {
        if (mIns.AllList.ContainsKey(eventID))
        {
            Func<T> callback = mIns.AllList[eventID] as Func<T>;
            if (callback != null)
            {
                return callback();
                //								FuncDic.Remove(eventID);
            }
            else
                LogMgr.LogError("回调不匹配");
        }

        return default(T);
    }

    public static U PostNoticeAsFunc<T, U>(int eventID, T arg)
    {
        if (mIns.AllList.ContainsKey(eventID))
        {

            Func<T, U> callback = mIns.AllList[eventID] as Func<T, U>;

            if (callback != null)
            {
                //								FuncDic.Remove(eventID);
                return callback(arg);

            }
            else
                LogMgr.LogError("回调不匹配");
        }

        return default(U);
    }

    public static V PostNoticeAsFunc<T, U, V>(int eventID, T arg, U arg1)
    {
        if (mIns.AllList.ContainsKey(eventID))
        {
            Func<T, U, V> callback = mIns.AllList[eventID] as Func<T, U, V>;
            if (callback != null)
            {
                return callback(arg, arg1);
                //								FuncDic.Remove(eventID);
            }
            else
                LogMgr.LogError("回调不匹配");
        }

        return default(V);
    }

    //func----
    public static T PostNoticeAsFunc<T>(GameCMD ID)
    {
        int eventID = (int)ID;
        return PostNoticeAsFunc<T>(eventID);
    }

    public static U PostNoticeAsFunc<T, U>(GameCMD ID, T arg)
    {
        int eventID = (int)ID;
        return PostNoticeAsFunc<T, U>(eventID, arg);
    }

    public static V PostNoticeAsFunc<T, U, V>(GameCMD ID, T arg, U arg1)
    {
        int eventID = (int)ID;
        return PostNoticeAsFunc<T, U, V>(eventID, arg, arg1);
    }

    #endregion

    public static void AddObserver(MonoBehaviour target, GameCMD eventID, Action callback)
    {
        AddObserver(target, (int)eventID, callback);
    }

    public static void AddObserver<T>(MonoBehaviour target, GameCMD eventID, Action<T> callback)
    {
        AddObserver<T>(target, (int)eventID, callback);
    }

    public static void AddObserver<T, U>(MonoBehaviour target, GameCMD eventID, Action<T, U> callback)
    {
        AddObserver<T, U>(target, (int)eventID, callback);
    }

    public static void AddObserver<T, U, V>(MonoBehaviour target, GameCMD eventID, Action<T, U, V> callback)
    {
        AddObserver<T, U, V>(target, (int)eventID, callback);
    }


    public static void AddNetObserver(int eventID, Action callback)
    {
        if (mIns.AllList.ContainsKey(eventID))
        {
            LogMgr.Log("将会覆盖消息 " + eventID.ToString());
            mIns.AllList[eventID] = callback;
        }
        else
        {
            mIns.AllList.Add(eventID, callback);
        }

        mIns.SysQueue.Enqueue(eventID);
    }

    public static void AddObserver(MonoBehaviour target, int eventID, Action callback)
    {
        int hashCode = target.GetHashCode();

        if (mIns.TargetDic.ContainsKey(hashCode))
        {


            mIns.TargetDic[hashCode].Add(eventID);
            mIns.AllList.TryAdd(eventID, callback);
        }
        else
        {
            mIns.TargetDic.Add(hashCode, new List<int>() { eventID });
            if (mIns.AllList.TryAdd(eventID, callback))
            {
                Monitor m = target.gameObject.AddComponent<Monitor>();
                m.listenerTarget = hashCode;
            }
        }
    }

    public static void AddObserver<T>(MonoBehaviour target, int eventID, Action<T> callback)
    {
        int hashCode = target.GetHashCode();
        if (mIns.TargetDic.ContainsKey(hashCode))
        {

            mIns.TargetDic[hashCode].Add(eventID);
            mIns.AllList.TryAdd(eventID, callback);
        }
        else
        {
            mIns.TargetDic.Add(hashCode, new List<int>() { eventID });
            if (mIns.AllList.TryAdd(eventID, callback))
            {
                Monitor m = target.gameObject.AddComponent<Monitor>();
                m.listenerTarget = hashCode;
            }
        }
    }

    public static void AddObserver<T, U>(MonoBehaviour target, int eventID, Action<T, U> callback)
    {
        int hashCode = target.GetHashCode();

        if (mIns.TargetDic.ContainsKey(hashCode))
        {

            mIns.TargetDic[hashCode].Add(eventID);
            mIns.AllList.TryAdd(eventID, callback);
        }
        else
        {
            mIns.TargetDic.Add(hashCode, new List<int>() { eventID });
            if (mIns.AllList.TryAdd(eventID, callback))
            {
                Monitor m = target.gameObject.AddComponent<Monitor>();
                m.listenerTarget = hashCode;
            }
        }
    }

    public static void AddObserver<T, U, V>(MonoBehaviour target, int eventID, Action<T, U, V> callback)
    {
        int hashCode = target.GetHashCode();

        if (mIns.TargetDic.ContainsKey(hashCode))
        {

            mIns.TargetDic[hashCode].Add(eventID);
            mIns.AllList.TryAdd(eventID, callback);
        }
        else
        {
            mIns.TargetDic.Add(hashCode, new List<int>() { eventID });
            if (mIns.AllList.TryAdd(eventID, callback))
            {
                Monitor m = target.gameObject.AddComponent<Monitor>();
                m.listenerTarget = hashCode;
            }
        }
    }



    public static void PostNotice(int eventID)
    {
        if (mIns.AllList.ContainsKey(eventID))
        {

            object DicCallback = mIns.AllList[eventID];

            var callback = DicCallback as Action;
            if (callback != null)
            {
                callback.TryCall();
                //NoticeDic.Remove(eventID);
            }
            else
                LogMgr.LogError("回调不匹配");
            //NoticeDic.Remove(eventID);
        }
    }



    public static void PostNotice<T>(int eventID, T arg)
    {
        if (mIns.AllList.ContainsKey(eventID))
        {
            object DicCallback = mIns.AllList[eventID];

            var callback = DicCallback as Action<T>;
            if (callback != null)
            {
                callback(arg);
                //NoticeDic.Remove(eventID);
            }
            else
                LogMgr.LogError("回调不匹配");
        }
    }

    public static void PostNotice<T, U>(int eventID, T arg, U arg1)
    {
        if (mIns.AllList.ContainsKey(eventID))
        {

            object DicCallback = mIns.AllList[eventID];

            var callback = DicCallback as Action<T, U>;
            if (callback != null)
            {
                callback(arg, arg1);
                //NoticeDic.Remove(eventID);
            }
            else
                LogMgr.LogError("回调不匹配");
        }
    }

    public static void PostNotice<T, U, V>(int eventID, T arg, U arg1, V arg2)
    {
        if (mIns.AllList.ContainsKey(eventID))
        {
            object DicCallback = mIns.AllList[eventID];

            var callback = DicCallback as Action<T, U, V>;
            if (callback != null)
            {
                callback(arg, arg1, arg2);
                //NoticeDic.Remove(eventID);
            }
            else
                LogMgr.LogError("回调不匹配");
        }
    }

    public static void PostNotice(GameCMD ID)
    {
        int eventID = (int)ID;

        PostNotice(eventID);
    }

    public static void PostNotice<T>(GameCMD ID, T arg)
    {
        int eventID = (int)ID;
        PostNotice<T>(eventID, arg);
    }

    public static void PostNotice<T, U>(GameCMD ID, T arg, U arg1)
    {
        int eventID = (int)ID;
        PostNotice<T, U>(eventID, arg, arg1);
    }

    public static void PostNotice<T, U, V>(GameCMD ID, T arg, U arg1, V arg2)
    {
        int eventID = (int)ID;
        PostNotice<T, U, V>(eventID, arg, arg1, arg2);
    }



    #endregion


    public void RemoveTarget<T>(T key) where T : class
    {
        string keyname = typeof(T).Name;
        if (MsgDic.ContainsKey(keyname))
        {
            MsgDic.Remove(keyname);
        }
    }

    public void PushMsg<T>(T key, Action act) where T : class
    {
        string keyname = typeof(T).Name;
        if (MsgDic.ContainsKey(keyname))
        {
            var temp = MsgDic[keyname];
            if (temp.Open)
            {
                ForQueue.Enqueue(act);
            }
            else
                temp.Enqueue(act);
        }
        else
        {
            MsgDic.Add(keyname, new BarQueue<Action>());
            MsgDic[keyname].Enqueue(act);
        }
    }

    public void AllowPop<T>(T key) where T : class
    {
        string keyname = typeof(T).Name;
        if (MsgDic.ContainsKey(keyname))
        {
            var temp = MsgDic[keyname];
            temp.Open = true;
            while (temp.Count > 0)
            {
                ForQueue.Enqueue(temp.Dequeue());
            }
        }
        else
        {
            MsgDic.Add(keyname, new BarQueue<Action>());
            MsgDic[keyname].Open = true;
        }
    }

}

public class BarQueue<T>
{
    public bool Open = false;
    Queue<T> queue = new Queue<T>();

    public int Count
    {
        get
        {
            return queue.Count;
        }
    }

    public T Dequeue()
    {
        if (Open)
        {
            return queue.Dequeue();
        }
        LogMgr.LogError(" Dequeue  default");
        return default(T);
    }

    public void Enqueue(T data)
    {
        queue.Enqueue(data);
    }

    public bool Contains(T item)
    {
        return queue.Contains(item);
    }


}
