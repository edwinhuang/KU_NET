using UnityEngine;
using System.Collections;

namespace Kubility
{
		public interface NetDataReqInterface
		{

				byte[] Serialize ();

		}

		public interface NetDataSubReqInterface
		{

				void Serialize (NetByteBuffer buffer);

		}

		public interface NetDataRespInterface
		{

				void DeSerialize (byte[] data);
		}

		public interface NetDataSubRespInterface
		{

				void DeSerialize (NetByteBuffer buffer);
		}
}


