using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Kubility;

public class GlobalHelper : MonoBehaviour
{

    public static GlobalHelper mIns;
    private GameObject _TargetObject;

    public GameObject TargetObject
    {
        get
        {
            if (_TargetObject == null)
            {
                GameObject go = GameObject.FindGameObjectWithTag("Kubility");
                if (go == null)
                {
                    _TargetObject = new GameObject("Global");
                    _TargetObject.tag = "Kubility";
                }
                else
                    _TargetObject = go;

                DontDestroyOnLoad(_TargetObject);
            }
            return _TargetObject;
        }
    }

    private int frameCount = 0;
    public delegate void updateDelegate();
	public delegate void FixedDelegate();

    private Dictionary<ComponentObject, updateDelegate> updateEvent = new Dictionary<ComponentObject, updateDelegate>();
    private Dictionary<ComponentObject, FixedDelegate> fixedEvent = new Dictionary<ComponentObject, FixedDelegate>();

    private bool isUpdating;
    private bool isFixUpdate;

    private LinkedList<KeyValuePair<Type, int>> ComponentQueue = new LinkedList<KeyValuePair<Type, int>>();

    public class ComponentObject
    {
        public Component com;
        public Type type;
        public GameObject gobj;

        public static implicit operator ComponentObject(Component comp)
        {
            ComponentObject p = new ComponentObject();
            p.com = comp;
            p.type = comp.GetType();
            p.gobj = comp.gameObject;
            return p;
        }
    }

    public enum TargetPlatform
    {
        None = 0,
        IOS = 8,
        ANDROID = 11,
    }


    void Awake()
    {
        Application.targetFrameRate = 60;
        DontDestroyOnLoad(gameObject);
        mIns = this;

    }

    public void RegisterUpdate(Component target, updateDelegate ev, TargetPlatform platform = TargetPlatform.None)
    {
        if (platform != TargetPlatform.None)
        {
            if (Application.platform == RuntimePlatform.IPhonePlayer && platform != TargetPlatform.IOS)
            {

                return;
            }
            else if (Application.platform == RuntimePlatform.Android && platform != TargetPlatform.ANDROID)
            {

                return;
            }
        }

        bool found = false;

        List<ComponentObject> removeList = new List<ComponentObject>();
        ComponentObject comp = null;
        foreach (var sub in updateEvent)
        {
            comp = sub.Key;
            if (comp.com == null)
            {
                removeList.Add(comp);
            }
            else if (comp.com == target)
            {
                comp.com = target;
                found = true;
                break;
            }
        }

        for (int i = 0; i < removeList.Count; ++i)
        {
            updateEvent.Remove(removeList[i]);
        }

        if (!found)
        {
            updateEvent.Add(target, ev);
        }
        else
        {
            updateEvent[comp] += ev;
        }

    }

    public void UnRegister(Component target, FixedDelegate ev)
    {


        foreach (var sub in fixedEvent)
        {
            ComponentObject comp = sub.Key;
            if (comp.com != null && comp.com == target)
            {
                fixedEvent.Remove(comp);
                break;
            }
        }

        foreach (var sub in updateEvent)
        {
            ComponentObject comp = sub.Key;
            if (comp.com != null && comp.com == target)
            {
                updateEvent.Remove(comp);
                break;
            }
        }


    }

    public void RegisterFixedUpdate(Component target, FixedDelegate ev, TargetPlatform platform = TargetPlatform.None)
    {


        if (platform != TargetPlatform.None)
        {
            if (Application.platform == RuntimePlatform.IPhonePlayer && platform != TargetPlatform.IOS)
            {

                return;
            }
            else if (Application.platform == RuntimePlatform.Android && platform != TargetPlatform.ANDROID)
            {

                return;
            }
        }

        bool found = false;

        List<ComponentObject> removeList = new List<ComponentObject>();
        ComponentObject comp = null;
        foreach (var sub in fixedEvent)
        {
            comp = sub.Key;
            if (comp.com == null)
            {
                removeList.Add(comp);
            }
            else if (comp.com == target)
            {
                found = true;
                break;
            }
        }

        for (int i = 0; i < removeList.Count; ++i)
        {
            fixedEvent.Remove(removeList[i]);
        }

        if (!found)
        {
            fixedEvent.Add(target, ev);
        }
        else
        {
            fixedEvent[comp] += ev;
        }

    }

    void Update()
    {

        if (Application.platform == RuntimePlatform.Android && Input.GetKeyDown(KeyCode.Escape))
        {
           // DialogController.mIns.DialogControllerShow(106007, Quit);
        }

        ++frameCount;
        if (updateEvent.Count == 0)
            return;

        List<ComponentObject> keylist = new List<ComponentObject>(updateEvent.Keys);
        for (int i = 0; i < keylist.Count; ++i)
        {
            ComponentObject comp = keylist[i];
            if (updateEvent.ContainsKey(comp))
            {
                updateDelegate up = updateEvent[comp];
                if (comp.com != null && comp.gobj.activeSelf && up != null)
                {
                    up();
                }
            }
        }


    }


    void FixedUpdate()
    {


        if (fixedEvent.Count == 0)
            return;

        List<ComponentObject> keylist = new List<ComponentObject>(fixedEvent.Keys);
        for (int i = 0; i < keylist.Count; ++i)
        {
            ComponentObject comp = keylist[i];

            if (fixedEvent.ContainsKey(comp))
            {
                FixedDelegate up = fixedEvent[comp];
                if (comp.com != null && ((comp.gobj != null && comp.gobj.activeSelf) || comp.gobj == null) && up != null)
                {
                    up();
                }
            }
        }
    }
    void OnApplicationQuit()
    {
//        KApplication.isPlaying = false;
//        PlatformIns.mIns.CallPlatformBack(UnityCallBack.GAME_QUIT);

        SocketService.mIns.Close();

        KThread.CloseAll();

        if (MessageManager.mIns != null)
        {
            MessageManager.mIns.Close();
        }
       
        

//        KAssetBundleManger.CloseApk();

    //    KAssetBundleManger.mIns.UnLoadAll();

        fixedEvent.Clear();
        updateEvent.Clear();

        
   //     IOSGameCenterManager.mIns.SavaLocal();

        

 //       KAssetDispather.mIns.Close();

//        KApplication.DestoryAllIns();

#if UNITY_EDITOR
        LogMgr.Log("此次游戏中 调用了 " + AsyncSocket.ConnectedCount + " 次的链接检查");
#endif



    }

    void Quit(Hashtable ht)
    {

        try
        {
            Application.Quit();
        }
        catch (Exception ex)
        {
            LogMgr.LogError(ex);
        }
        finally
        {
            Application.Quit();
        }



    }

    #region tools function

    public T AddComponentImmediate<T>() where T : Component
    {

        return TargetObject.AddComponent<T>();
    }

    ///<summary>
    ///根据优先级初始化
    ///</summary>
    public void AddComponent<T>(int level = 0) where T : Component
    {
        if (level == 0 || ComponentQueue.Count == 0)
        {
            ComponentQueue.AddLast(new KeyValuePair<Type, int>(typeof(T), level));
        }
        else
        {
            LinkedListNode<KeyValuePair<Type, int>> firstNode = ComponentQueue.First;
            if (firstNode.Value.Value > level)
            {
                ComponentQueue.AddFirst(new KeyValuePair<Type, int>(typeof(T), level));
            }
            else
            {
                ComponentQueue.AddAfter(firstNode, new KeyValuePair<Type, int>(typeof(T), level));
            }

        }

    }

    #endregion
}