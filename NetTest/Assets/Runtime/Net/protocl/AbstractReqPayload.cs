using UnityEngine;
using System.Collections;

/// <summary>
/// Web 请求
/// </summary>
public abstract class AbstractReqPayload
{
		
    //用户唯一ID；
    public string token = null;


    public AbstractReqPayload()
    {
    }

    public string Serialization()
    {

		return ParseUtils.Json_Serialize(this);

    }
}
