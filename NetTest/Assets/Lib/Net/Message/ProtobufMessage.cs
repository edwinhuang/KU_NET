//#define protobuf
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

#if protobuf
using ProtoBuf;
#endif

namespace Kubility
{
		public class ProtobufMessageHead :MessageHead
		{

		}

		public class ProtobufMessageData
		{
		
		}

		public class ProtobufMessage : BaseMessage
		{
				public byte[] ProtobufData {
						get {
								return this.DataBody.m_SecondValue;
						}
				}

				public static ProtobufMessage Create<T> (ProtobufMessageHead head, T data) where  T :ProtobufMessageData
				{
						if (MessageInfo.MessageTypeCheck (MessageDataType.ProtoBuf)) {
								ProtobufMessage message = new ProtobufMessage ();
								message.head = head;
								message.DataBody.m_SecondValue = ParseUtils.ProtoBuf_SerializeAsBytes<T> (data);
								return message;
						}
						return null;
				}

				public override byte[] Serialize (bool addHead)
				{
						if (MessageInfo.MessageTypeCheck (MessageDataType.ProtoBuf)) {
								
								ByteBuffer buffer = null;

								byte[] bys = DataBody.m_SecondValue;
								if (head != null && addHead) {

										buffer = head.Serialize (bys);

								} else if (addHead && head == null) {
		
										buffer = new NetByteBuffer (64);

								}

								buffer += bys;
								return buffer.ConverToBytes ();

						}
						return null;
				}

				public override void Wait_Deserialize<T> (Action<T> ev ,params int[] ReceveIDs) 
				{
						if (MessageInfo.MessageTypeCheck (MessageDataType.ProtoBuf)) {


								for(int i=0; i < ReceveIDs.Length;++i)
								{
										MessageManager.mIns.PushToWaitQueue (this,ReceveIDs[i], delegate(byte[] value,int eventID) {
												T data = ParseUtils.ProtoBuf_DeserializeWithBytes<T> (value);

												ev (data);
										}, ev.GetHashCode ());
								}


						}

				}
		}

}


