using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Kubility;


public struct ErrorMsgResp : NetDataReqInterface, NetDataRespInterface, System.IEquatable<ErrorMsgResp>
{
    public byte[] Serialize()
    {
        NetByteBuffer buffer = new NetByteBuffer(64);
        buffer += ErrorType;
        buffer += MsgID;
        buffer += Sub;
        return buffer.ConverToBytes();
    }

    public void DeSerialize(byte[] data)
    {
        NetByteBuffer buffer = new NetByteBuffer(data);
        this.ErrorType = (short)buffer;
        this.MsgID = (int)buffer;

        this.Sub = (short)buffer;
    }

    public static implicit operator ErrorMsgResp(int id)
    {
        var msg = new ErrorMsgResp();
        msg.ErrorType = 1;
        msg.MsgID = id;

        return msg;
    }

    public short ErrorType;

    public int MsgID;

    public short Sub;


    public bool Equals(ErrorMsgResp other)
    {

        if (ErrorType != other.ErrorType) return false;
        else if (MsgID != other.MsgID) return false;
        else if (Sub != other.Sub) return false;

        return true;
    }
}

