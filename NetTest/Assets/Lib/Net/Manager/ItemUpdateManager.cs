//ItemUpdateManager
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Kubility;
using System;



public class ItemUpdateManager : ICommonClass
{
	/*

    public override void DispatcherEvents(ValueType data, BaseEnum main, BaseEnum sub, object callback)
    {

        if (main == TcpMainCMD.COMMON)
        {
            if (sub == TcpSubCMD.SC_WAREHOUSE_ITEM_ADD)
            {
                S2cWareHouseItemAdd netdata = (S2cWareHouseItemAdd)System.Convert.ChangeType(data, typeof(S2cWareHouseItemAdd));

                if (netdata.item != null)
                    MMtool.GainItemStorage(netdata.item);

                if (PropController.Exist())
                {
                    PropController.mIns.Refresh();
                }

            }
            else if (sub == TcpSubCMD.SC_WAREHOUSE_ITEM_CHANGE)
            {
                S2cItemChange netdata = (S2cItemChange)System.Convert.ChangeType(data, typeof(S2cItemChange));
                //替换
                MMtool.ChangeItemCountStorage(netdata.uid.ToString(), netdata.Count);
                if (PropController.Exist())
                {
                    PropController.mIns.Refresh();
                }

            }
            else if (sub == TcpSubCMD.SC_WAREHOUSE_LIST)
            {
                S2cWareHouseList netdata = (S2cWareHouseList)System.Convert.ChangeType(data, typeof(S2cWareHouseList));

                MMtool.RefreshItemStorage(netdata);
                if (LocalValue.mIns.IsFirstGetItemStorage)
                {
                    LocalValue.mIns.IsFirstGetItemStorage = false;
                }
                if (PropController.Exist())
                {
                    PropController.mIns.Refresh();
                }

            }
            else if (sub == TcpSubCMD.SC_WAREHOUSE_REMOVE)
            {
                S2cWareHouseItemRemove netdata = (S2cWareHouseItemRemove)System.Convert.ChangeType(data, typeof(S2cWareHouseItemRemove));


                MMtool.DeleteItemStorage(netdata.itemuid.ToString());
                if (PropController.Exist())
                {
                    PropController.mIns.Refresh();
                }
            }
        }
    }

    public ItemUpdateManager(TcpSubCMD subdata)
    {
        this.data = subdata;
        CommonManager.mIns.Register(this);
    }

    public override void Destroy()
    {

    }

    public static void Wait_Event(Action<S2cItemChange> Callback)
    {
        CommonManager.mIns.Wait_Resp<S2cItemChange>(TcpMainCMD.COMMON, TcpSubCMD.SC_WAREHOUSE_ITEM_CHANGE, Callback);
    }

    public static void Wait_Event(Action<S2cWareHouseItemAdd> Callback)
    {
        CommonManager.mIns.Wait_Resp<S2cWareHouseItemAdd>(TcpMainCMD.COMMON, TcpSubCMD.SC_WAREHOUSE_ITEM_ADD, Callback);
    }

    public static void Wait_Event(Action<S2cWareHouseItemRemove> Callback)
    {
        CommonManager.mIns.Wait_Resp<S2cWareHouseItemRemove>(TcpMainCMD.COMMON, TcpSubCMD.SC_WAREHOUSE_REMOVE, Callback);
    }

    public static void Wait_Event(Action<S2cWareHouseList> Callback)
    {
        CommonManager.mIns.Wait_Resp<S2cWareHouseList>(TcpMainCMD.COMMON, TcpSubCMD.SC_WAREHOUSE_LIST, Callback);
    }


    public static void Wait_Event_AutoRemove(Action<S2cItemChange> Callback)
    {
        CommonManager.mIns.Wait_Resp<S2cItemChange>(TcpMainCMD.COMMON, TcpSubCMD.SC_WAREHOUSE_ITEM_CHANGE, Callback);
        MonoDelegate.mIns.Coroutine_Delay(delay, delegate ()
        {
            CommonManager.mIns.Remove_Resp(TcpMainCMD.COMMON, TcpSubCMD.SC_WAREHOUSE_ITEM_CHANGE, Callback);
        });
    }

    public static void Wait_Event_AutoRemove(Action<S2cWareHouseItemAdd> Callback)
    {
        CommonManager.mIns.Wait_Resp<S2cWareHouseItemAdd>(TcpMainCMD.COMMON, TcpSubCMD.SC_WAREHOUSE_ITEM_ADD, Callback);
        MonoDelegate.mIns.Coroutine_Delay(delay, delegate ()
        {
            CommonManager.mIns.Remove_Resp(TcpMainCMD.COMMON, TcpSubCMD.SC_WAREHOUSE_ITEM_ADD, Callback);
        });
    }

    public static void Wait_Event_AutoRemove(Action<S2cWareHouseItemRemove> Callback)
    {
        CommonManager.mIns.Wait_Resp<S2cWareHouseItemRemove>(TcpMainCMD.COMMON, TcpSubCMD.SC_WAREHOUSE_REMOVE, Callback);
        MonoDelegate.mIns.Coroutine_Delay(delay, delegate ()
        {
            CommonManager.mIns.Remove_Resp(TcpMainCMD.COMMON, TcpSubCMD.SC_WAREHOUSE_REMOVE, Callback);
        });
    }

    public static void Wait_Event_AutoRemove(Action<S2cWareHouseList> Callback)
    {
        CommonManager.mIns.Wait_Resp<S2cWareHouseList>(TcpMainCMD.COMMON, TcpSubCMD.SC_WAREHOUSE_LIST, Callback);
        MonoDelegate.mIns.Coroutine_Delay(delay, delegate ()
        {
            CommonManager.mIns.Remove_Resp(TcpMainCMD.COMMON, TcpSubCMD.SC_WAREHOUSE_LIST, Callback);
        });
    }
    //*/
}

