using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Timers;


namespace Kubility
{
		public class MonoDelegate : MonoSingleTon<MonoDelegate>
		{
				public delegate object MethodNameDelegate<T> (ref T y);

				public void Coroutine_DelayNextFrame (VoidDelegate ev)
				{

						StartCoroutine (NextFrame (ev));
				}

				public void Coroutine_Delay (float time, VoidDelegate ev)
				{

						StartCoroutine (Delay (time, ev));
				}

				public void StopAllCors()
				{
						StopAllCoroutines();
				}

				//		public void Coroutine_Delay<T>(float time,ref T value, MethodNameDelegate<T> ev)
				//		{
				//
				//			var timer = new  System.Timers.Timer(time *1000);
				//			timer.Elapsed += delegate(object sender, ElapsedEventArgs e) {
				//
				//				ev(ref value);
				//			};
				//			timer.AutoReset =false;
				//		}

				public CTask Coroutine_DelayAsTask (float time, VoidDelegate ev)
				{
			
						var ts = new CTask (Delay (time, ev));
						return ts;
				}

				public void Coroutine_Delay<T> (float time, T t, Action<T> ev)
				{
			
						StartCoroutine (Delay (time, delegate {
								ev (t);
						}));
				}

				public CTask Coroutine_DelayAsTask<T> (float time, T t, Action<T> ev)
				{
			
						var ts = new CTask (Delay (time, delegate {
								ev (t);
						}));
						return ts;
				}

				public void Lerp (Action<int> callback, int start, int end, float time)
				{

						StartCoroutine (ValueLerp (callback, start, end, time));
				}


                public void Lerp(Action<Vector3> callback, Vector3 start, Vector3 end, float time,Action finishhandler)
                {

                    StartCoroutine(ValueLerp(callback, start, end, time, finishhandler));
                }

                IEnumerator ValueLerp(Action<Vector3> callback, Vector3 start, Vector3 end, float time, Action finishhandler)
                {

                    float curtime = 0f;
                    float val = Time.fixedDeltaTime;

                    while (curtime < time)
                    {
                        yield return new WaitForFixedUpdate();
                        curtime += val;
                        callback(start + (end - start) * (curtime / time));

                    }

                    yield return null;
                    finishhandler.TryCall();
                }
				IEnumerator ValueLerp (Action<int> callback, int start, int end, float time)
				{
			
						float curtime = 0f;
						float val = Time.fixedDeltaTime;

						while (curtime < time) {
								yield return new WaitForFixedUpdate ();
								curtime += val;
								callback (start + (int)((end - start) * (curtime / time)));

						}
			
						yield return null;
				}

				/// <summary>
				/// Lerp the specified callback, start, end and time. float
				/// </summary>
				/// <param name="callback">Callback.</param>
				/// <param name="start">Start.</param>
				/// <param name="end">End.</param>
				/// <param name="time">Time.</param>
				public void Lerp (Action<float> callback, float start, float end, float time)
				{
						StartCoroutine (ValueLerp (callback, start, end, time));
				}

				IEnumerator ValueLerp (Action<float> callback, float start, float end, float time)
				{
			
						float curtime = 0f;
						float val = Time.fixedDeltaTime;

						while (curtime < time) {
								yield return new WaitForFixedUpdate ();
								curtime += val;
								float result = start + (end - start) * (curtime / time);
								callback (result);
				
						}
			
						yield return null;
				}

				IEnumerator NextFrame ( VoidDelegate ev)
				{
						yield return new WaitForFixedUpdate();
						if (ev != null) {
								ev ();
						}

				}


				IEnumerator Delay (float time, VoidDelegate ev)
				{
						yield return new WaitForSeconds (time);
						if (ev != null) {
								ev ();
						}
             
				}

		}
}

