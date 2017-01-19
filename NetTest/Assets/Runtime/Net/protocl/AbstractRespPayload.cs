using UnityEngine;
using System.Collections;

/// <summary>
/// 请求 回复;
/// </summary>
public abstract class AbstractRespPayload
{
    public int msgid;
    public virtual bool ResqSucess()
    {
        if (msgid != 0)
        {
            return false;
        }
        else return true;
    }

    public static T Deserialization<T>(string jsonString)
    {
        try
        {

			return ParseUtils.Json_Deserialize<T>(jsonString);
        }
        catch (System.Exception ex)
        {
            LogMgr.Log(ex);
            return default(T);
        }
    }
}
