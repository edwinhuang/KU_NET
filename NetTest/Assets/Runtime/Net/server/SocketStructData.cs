using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Kubility;

public class SocketStructData<U> where U:struct,NetDataReqInterface
{
		public StructMessageHead Head;
		public U Content;

		public SocketStructData()
		{
				this.Head = new StructMessageHead();
		}

		public SocketStructData(StructMessageHead head,U data)
		{
				this.Head = head;
				this.Content = data;
		}

		public virtual StructMessage Init ()
		{
				return StructMessage.CreateReq  (Head, Content);
		}
}


