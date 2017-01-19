//#define IN_KUNET
using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using Kubility;


public class LimitList
{
		private List<ClsTuple<short,Action,Action> > queue;

		public int Count {
				get {
						return queue.Count;
				}
		}

		public Action FailedEvent;

		public readonly int Limit;

		public LimitList (int limitcount, Action PFailedEvent)
		{
				Limit = limitcount;
				queue = new List<ClsTuple<short, Action, Action>> ();
				FailedEvent = PFailedEvent;
		}

		public bool CanPop (Action SuccessEv)
		{
				var cls = queue.Find (p => p.field1 == SuccessEv);

				if (cls != null && cls.field0 < Limit) {
						return true;
				} else
						return false;
		}

		public void Remove (Action SuccessEv)
		{
				var cls = queue.Find (p => p.field1 == SuccessEv);
				if (cls != null) {
						queue.Remove (cls);
				}
		}

		public void PushCall (Action SuccessEv)
		{
				var cls = queue.Find (p => p.field1 == SuccessEv);

				if (cls != null && cls.field0 < Limit) {
						cls.field0++;
						cls.field1.Invoke ();
				} else if (cls == null) {
						cls = new ClsTuple<short, Action, Action> ();
						cls.field0 = 0;
						cls.field1 = SuccessEv;
						cls.field2 = FailedEvent;
						queue.Add (cls);

						SuccessEv.Invoke ();
				} else {
						//failed
						cls.field2.Invoke ();
						queue.Remove (cls);
				}

		}


}

public class WebServer : MonoBehaviour
{
	public static WebServer mIns;
	public string ResourceIP, ServerID, IP, AbDownLoadSite;
	public HttpClient httpClient {
			get {
					if (client == null) {
							client = new HttpClient ();
							GlobalHelper.mIns.RegisterFixedUpdate (this, client.DofixedUpdate);

					}
					return client;

			}
	}

	public string GSAddressDBUrl = "http://127.0.0.1/API";

    public string Port = "7878";

	HttpClient client;

	LimitList requsetQueue;

	List<string> requesting = new List<string> ();


	void Awake ()
	{
		mIns = this;
		requsetQueue = new LimitList (3, delegate() {
				requesting.Clear ();
		});

		GSAddressDBUrl = "http://" + IP + ":" + Port + "/MonsterMaster";

	}



	public void Clear ()
	{
		requesting.Clear ();
	}

	public void OnApplicationQuit ()
	{
			if (client != null) {
				client.Close ();
				client = null;
			}

	}

	public void LoadAccountDBCode<T> (string page, string method, AbstractReqPayload abstractReqPayload, Action<T> result, string unique = null)
    where T : AbstractRespPayload
	{
			string identification = page + method + unique;
			if (!requesting.Exists (p => p == identification)) {
					requesting.Add (identification);
					string url = "http://" + IP + ":" + Port.ToString () + (page != "" ? "/" + page : "") + (method != "" ? "/" + method : "");
					LogMgr.Log ("客户端请求:" + url + "    参数为:" + abstractReqPayload.Serialization ());

					string res = abstractReqPayload.Serialization ();

					Action requestEv = null;
					requestEv = delegate() {
							WWWForm form = new WWWForm ();
							form.AddField ("params", res);
							StartCoroutine (PostWWWForm (url, requestEv, form, identification, delegate(WWW w) {

									LogMgr.Log ("服务器返回:" + url + ";web:" + w.text);
									requesting.RemoveAll (p => p == identification);

									T t = ParseUtils.Json_Deserialize<T> (w.text);

									if (t != null) {
											result (t);
									} else {
											LogMgr.LogError ("反序列化失败");
									}

							}));
					};

					requsetQueue.PushCall (requestEv);


			} else {
					LogMgr.Log ("狂点了:" + identification);
			}
	}

	public void LoadGameDBCode<T> (string page, string method, AbstractReqPayload abstractReqPayload, Action<T> result, string unique = null)
    where T : AbstractRespPayload
	{

		string serverIp = IP;
		string identification = page + method + unique;
		if (!requesting.Exists (p => p == identification)) {
				requesting.Add (identification);
				string url = "http://" + serverIp + ":" + "" + (page != "" ? "/" + page : "") + (method != "" ? "/" + method : "");

				LogMgr.Log ("客户端请求:" + url + "    参数为:" + abstractReqPayload.Serialization ());

				return;
				string res = abstractReqPayload.Serialization ();

				WWWForm form = new WWWForm ();
				form.AddField ("params", res);

				Action requestEv = null;
				requestEv = delegate() {
						StartCoroutine (PostWWWForm (url, requestEv, form, identification, delegate(WWW w) {

								LogMgr.Log ("服务器返回:" + url + ";web:" + w.text);
								requesting.RemoveAll (p => p == identification);

								T t = ParseUtils.Json_Deserialize<T> (w.text);

								if (t != null) {
										result (t);
								} else {
										LogMgr.LogError ("反序列化失败");
								}

						}));
				};

				requsetQueue.PushCall (requestEv);

		} else {
				LogMgr.Log ("狂点了:" + identification);
		}
	}

	public void LoadHttpGet (string url, WWWForm form, Action<string> result)
	{

			if (mIns != null) {
			
					StartCoroutine (PostWWWForm (url, null, form, null, delegate(WWW w) {
				
							LogMgr.Log ("服务器返回:" + url + ";web:" + w.text);

							result (w.text);
				
					}));
			}


	}


	private IEnumerator PostWWWForm (string url, Action request, WWWForm form, string identification, Action<WWW> result)
	{
			WWW post;
			if (form != null)
					post = new WWW (url, form);
			else
					post = new WWW (url);

			yield return post;

			if (string.IsNullOrEmpty (post.error)) {
					result (post);
					if (request != null)
							requsetQueue.Remove (request);

			} else {
					if (request != null)
							requsetQueue.PushCall (request);
					Debug.LogWarning ("---------post.error" + post.error + "/" + result + "url=" + url);
			}
			post.Dispose ();

	}


}
