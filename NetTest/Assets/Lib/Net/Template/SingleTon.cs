using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Kubility;

#if UNITY_EDITOR
using UnityEditor;
#endif

/// <summary>
/// Single ton.
/// </summary>
public class SingleTon<T> where T : class, new()
{

		protected static readonly object m_lock = new object ();
		protected static T _mins;

		public static T mIns {
				get {
						if (_mins == null) {
								lock (m_lock) {
										if (_mins == null) {
												_mins = new T ();

										}

								}
						}
						return _mins;

				}
		}

		public static bool Exist()
		{
				return _mins != null;
		}


		public static void Destroy () 
		{
				if(SingleTon<T>._mins != null)
				{
						SingleTon<T>._mins = null;
				}
		}

}



/// <summary>
/// Mono awake single ton.
/// </summary>
public class MonoEventsBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{

		protected virtual void Awake ()
		{

		}

		protected virtual void Start ()
		{

		}

		protected virtual void OnDestroy ()
		{


		}

		protected virtual void OnEnble ()
		{

		}

		protected virtual void OnDisable ()
		{

		}

}

/*

public class MonoBattleBinder : MonoBehaviour 
{
		
		public bool Get<U,V>(ref V v) where U:struct, IConvertible where V :KVBinder<U>
		{
				string name = typeof(V).Name;
				if(KBinder.HasBind<MonoBattleBinder,U>(this,name))
				{
						v= (V)System.Convert.ChangeType(KBinder.Get<MonoBattleBinder,U>(this,name),typeof(V));
						return true;
				}

				return false;

		}

		public U GetValue<U>(string name) where U:struct, IConvertible
		{
				return Kubility.KBinder.GetValue<MonoBattleBinder,U>(this,name);
		}

		protected virtual void Awake ()
		{

		}

		protected virtual void Start ()
		{

		}

		protected virtual void OnDestroy ()
		{
				Kubility.KBinder.UnBind(this);
		}

		protected virtual void OnEnble ()
		{

		}

		protected virtual void OnDisable ()
		{

		}

}

//*/

/// <summary>
/// Mono single ton.
/// </summary>
public class MonoSingleTon<T> : MonoBehaviour where T : MonoBehaviour
{
		private static readonly object m_lock = new object ();
		private static T _mins;

		public static T mIns {
				get {
						if (_mins == null) {
								lock (m_lock) {

										if (_mins == null) {

												GameObject go = GameObject.FindGameObjectWithTag ("Kubility");
												if (go == null) {
														go = GameObject.Find ("Global");
														if (go == null) {
																go = new GameObject ("Global");
																DontDestroyOnLoad (go);

														} else {
																_mins = go.GetComponent<T> ();
														}

														go.tag = "Kubility";
												} else {
														_mins = go.GetComponent<T> ();
												}

												if (_mins == null) {
														_mins = go.AddComponent<T> ();
												}


										}

								}
						}

						return _mins;

				}
		}

		public static bool Exist()
		{
				return _mins != null;
		}

		protected virtual void Awake ()
		{
				T value = System.Convert.ChangeType(this,typeof(T)) as T;
				if(value != null)
						_mins =value;


		}

		protected virtual void OnDestroy()
		{
				_mins = null;
		}
}


