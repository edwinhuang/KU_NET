using UnityEngine;
using Kubility;
using System;


public struct EmptyReq : NetDataReqInterface, System.IEquatable<EmptyReq>
{
    public byte[] Serialize()
    {

        return null;
    }

    public bool Equals(EmptyReq other)
    {
        return true;
    }

}
