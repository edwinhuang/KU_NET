using UnityEngine;
using System.Collections;
using Kubility;
using System;

public class JsonMessFactory : DataInterface
{

		public JsonMessFactory (MessageDataType type) : base (type)
		{
		
		}

		public override BaseMessage DynamicCreate (byte[] data, MessageHead head)
		{
				if (MessageInfo.MessageTypeCheck (MessageDataType.Json)) {
						JsonMessage message = new JsonMessage ();
						message.MessageType = 0;
						if (BitConverter.IsLittleEndian)
								Array.Reverse (data);
			
						message.DataBody.m_FirstValue = System.Text.Encoding.UTF8.GetString (data);
						if (head != null) {
								message.DataHead = head;
								message.MessageType = head.MainCMD;
						}
			
						return message;
				}
				return null;
		}

}
