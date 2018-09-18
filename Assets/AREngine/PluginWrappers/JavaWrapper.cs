using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JavaWrapper : MonoBehaviour {

	// Use this for initialization
	void Start () {
		#if UNITY_ANDROID && !UNITY_EDITOR
		    var javaClass = new AndroidJavaObject("com.casia.androidlibrary.Addition");
			int add = javaClass.Call<int>("Addify", 2, 2);
			Debug.Log(add);
		#endif
	}
}
