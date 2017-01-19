using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Kubility
{


		public interface ICommonInterface
		{

				int SelfType{ get; }

				int GenerateID (BaseEnum Main, BaseEnum Sub);

				void DispatcherEvents (ValueType data, BaseEnum main, BaseEnum sub, object callback) ;

				void Destroy ();
		}


}


