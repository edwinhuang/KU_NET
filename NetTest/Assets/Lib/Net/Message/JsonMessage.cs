using UnityEngine;
using System.Collections;
using System;
using System.IO;


namespace Kubility
{
		public class JsonMessageHead : MessageHead
		{

		}

		public sealed class JsonMessage : BaseMessage
		{

				public string jsonData {
						get {
								return DataBody.m_FirstValue;
						}
				}

				/// <summary>
				/// create send json data
				/// </summary>
				/// <param name="data">Data.</param>
				/// <param name="jhead">Jhead.</param>
				/// <typeparam name="T">The 1st type parameter.</typeparam>
				public static JsonMessage Create<T> (T data, JsonMessageHead jhead)
				{

						if (MessageInfo.MessageTypeCheck (MessageDataType.Json)) {
								JsonMessage message = new JsonMessage ();
	
								message.DataBody.m_FirstValue = ParseUtils.Json_Serialize (data);
								if (jhead.LogNull()) {
										message.head = jhead;
										message.MessageType = jhead.MainCMD;
										message.SubMessageType = jhead.SubCMD;
								}

				
								return message;
						}
						return null;

				}

				/// <summary>
				/// receive json data
				/// </summary>
				/// <param name="ev">Ev.</param>
				/// <typeparam name="T">The 1st type parameter.</typeparam>
				public override void Wait_Deserialize<T> (Action<T> ev ,params int[] ReceveIDs) 
				{
						if (MessageInfo.MessageTypeCheck (MessageDataType.Json)) {

								for(int i=0; i < ReceveIDs.Length;++i)
								{
										MessageManager.mIns.PushToWaitQueue (this,ReceveIDs[i], delegate(string value ,int eventID) {
												T obj = default(T);
												if (!string.IsNullOrEmpty (value)) {
														obj = ParseUtils.Json_Deserialize<T> (value);
												}

												ev (obj);
										}, ev.GetHashCode ());
								}


						}

				}

				/// <summary>
				/// Serialize the send data
				/// </summary>

				public override byte[] Serialize (bool addHead = true)
				{
						if (MessageInfo.MessageTypeCheck (MessageDataType.Json)) {
								ByteBuffer buffer = null;
				
								var bys = System.Text.Encoding.UTF8.GetBytes (DataBody.m_FirstValue);
				
								if (head != null && addHead) {

										buffer = head.Serialize (bys);
								}
								else
								{
										buffer = new NetByteBuffer (64);
										buffer+= bys;
								}

								buffer += DataBody.m_FirstValue;
				
								return buffer.ConverToBytes ();
						}

						return null;

				}

		}
}


