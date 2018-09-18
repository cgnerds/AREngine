using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ObjectiveCWrapper : MonoBehaviour {
    #if UNITY_IOS && !UNITYEDITOR
	[DllImport("__Internal")]
	public static extern int Addition(int a, int b);
	#else
	[DllImport("ObjectiveCPlugin")]
	public static extern int Addition(int a, int b);
	#endif

	// Use this for initialization
	void Start () {
		var add = Addition(2, 3);
		Debug.Log(add);
	}	
}
