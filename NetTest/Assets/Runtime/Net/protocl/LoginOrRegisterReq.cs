using UnityEngine;
using System.Collections;
using Kubility;

public struct LoginOrRegisterReq : NetDataReqInterface
{
		public string ServerID ;

		public string token;

		/** 用来区分设备的唯一id */
		public string deviceId;
		/** IOS设备的device token。如果是安卓设备，为null ,推送专用*/
		public string deviceToken;
	
		/**
	 * 系统版本号
	 */
		public string os;
		/**
	 * 分辨率
	 */
		public string resolution;
		/**
	 * 
	 * 网络环境
	 */
		public string netWork;
		/**
	 * 设备与型号
	 */
		public string deviceNo;


		public byte[] Serialize ()
		{
			
				NetByteBuffer buffer = new NetByteBuffer(64);
				buffer += ServerID;
				buffer += token;
				buffer += "";
				buffer += "";
				buffer += "";
				buffer += "";
				buffer += "";
				buffer += "";
				return buffer.ConverToBytes();
		}
}
