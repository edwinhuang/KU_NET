using UnityEngine;
using Kubility;
using System;


public struct EmptyResp : NetDataRespInterface,System.IEquatable<EmptyResp>
{
		public void DeSerialize (byte[] data)
		{
				
		}

		public bool Equals (EmptyResp other)
		{
				return true;
		}
	
}
