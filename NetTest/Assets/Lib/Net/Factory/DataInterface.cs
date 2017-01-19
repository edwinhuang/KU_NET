using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Threading;


namespace Kubility
{

		public abstract class DataInterface
		{
				static Dictionary<int,DataInterface> dic = new Dictionary<int, DataInterface> ();


				public static  DataInterface TryGet (MessageHead head)
				{

						return dic [(int)MessageInfo.MessageType];

				}


				public abstract BaseMessage DynamicCreate (byte[] data, MessageHead head);


				public void Clear ()
				{
						dic.Clear ();
				}

				public DataInterface (MessageDataType type)
				{
						int intvalue = (int)type;
						if (!dic.ContainsKey (intvalue)) {
								dic.Add (intvalue, this);
						}


				}
		}
	
}

