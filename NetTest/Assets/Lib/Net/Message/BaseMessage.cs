//#define Protobuf
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;
using System.IO;
using System.Threading;

#if Protobuf
using ProtoBuf;
#endif


namespace Kubility
{


		public enum MessageDataType
		{
				Struct,
				Json,
				ProtoBuf,
				Error,
		}

		public class MessageHead
		{

				protected NetByteBuffer _buffer;

				protected MiniTuple<ushort, int> _SubCMD;

				protected MiniTuple<ushort, int> _MainCMD;

				protected MiniTuple<int, int> _bodyLen;

				protected MiniTuple<int, int> _CheckedValue;

				public ushort MainCMD {
						get {
								return _MainCMD.field0;
						}

						set {
								_MainCMD.field0 = value;
						}
				}

				public int bodyLen {
						get {
								return _bodyLen.field0;
						}

						protected set {
								_bodyLen.field0 = value;
						}
				}


				public ushort SubCMD {
						get {
								return _SubCMD.field0;
						}

						set {
								_SubCMD.field0 = value;
						}
				}

				public int CheckedValue {
						get {
								return _CheckedValue.field0;
						}

						protected set {
								_CheckedValue.field0 = value;
						}
				}

				public NetByteBuffer buffer {
						get {
								return _buffer;
						}

						set {
								_buffer = value;
						}
				}

				public MessageHead ()
				{

				}

				public virtual void Reset ()
				{
						this._bodyLen = new MiniTuple<int, int> ();

						this._SubCMD = new MiniTuple<ushort, int> ();

						this._CheckedValue = new MiniTuple<int, int> ();
						this._MainCMD = new MiniTuple<ushort, int> ();

						this.buffer.Clear ();
				}

				public void Read (Stream buffer)
				{
						this._bodyLen.field1 = ByteStream.readInt32(buffer, out this._bodyLen.field0);

//						this._CheckedValue.field1 = ByteStream.readShort16(buffer, out this._CheckedValue.field0);
						this._MainCMD.field1 = ByteStream.readUShort16(buffer, out this._MainCMD.field0);
						this._SubCMD.field1 = ByteStream.readUShort16(buffer, out this._SubCMD.field0);

				}

				public void Read (byte[] bytes)
				{
						buffer = new NetByteBuffer (bytes);
						bodyLen = (int)buffer;
//						CheckedValue = (int)buffer;
						MainCMD = (ushort)buffer;
						SubCMD = (ushort)buffer;

				}

				protected int calculateCheckSum(byte[] b) {
						int val1 = 0x66;
						int i = 0; // 从数据byte[] 第0位开始
						int len =b.Length;
						while (i < len) {

								val1 += b[i++] & 0xff; // byte[] 数据流
						}
						return (val1) & 0x8F8F;
				}

				public virtual ByteBuffer Serialize (byte[] content)
				{
						this.bodyLen = MessageInfo.HeadLen +content.Length;

						NetByteBuffer by = new NetByteBuffer (MessageInfo.HeadLen);
						by += SubCMD;
						by += content;

						byte[] needCheck =by.ConverToBytes ();

						this.CheckedValue = calculateCheckSum(needCheck);

						by.Clear();

						by += bodyLen;// 4+
						by += CheckedValue;//4
						by += MainCMD;//2
						by += SubCMD;//2

						return by;
				}




		}


		public abstract class BaseMessage
		{
				/// <summary>
				/// The type of the message.
				/// </summary>
				protected int messageType;

				public int MessageType {
						get {
								return messageType;
						}
						set {
								messageType = value;
						}
				}


				protected int _SubMessageType;

				public int SubMessageType {
						get {
								return _SubMessageType;
						}
						set {
								_SubMessageType = value;
						}
				}



				/// <summary>
				/// content
				/// </summary>
				protected Union<string, byte[]> m_DataBody;

				public Union<string, byte[]> DataBody {
						get {
								return m_DataBody;
						}

						set {
								m_DataBody = value;
						}
				}


				/// <summary>
				/// flag
				/// </summary>
				protected MessageHead head;

				public MessageHead DataHead {
						get {
								return head;
						}
						set {
								head = value;
						}
				}


				public BaseMessage ()
				{

						this.messageType = 0;
						this.head = new MessageHead ();
						this.DataBody = new Union<string, byte[]> ();
				}


				public static MessageHead ReadHead (byte[] bytes)
				{
						MessageHead head = new MessageHead ();
						head.Read (bytes);
						return head;
				}


				public int GetPriority ()
				{
						return (int)messageType;
				}

				public abstract byte[] Serialize (bool addHead = true);


				public abstract void Wait_Deserialize<T> (Action<T> ev,params int[] ReceveIDs) where T :NetDataRespInterface;


		}
}