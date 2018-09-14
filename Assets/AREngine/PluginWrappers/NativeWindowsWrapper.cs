using System.Runtime.InteropServices;
using UnityEngine;

public class NativeWindowsWrapper : MonoBehaviour {
	[DllImport("NativeWindowsPlugin")]
	public static extern int Addify(int a, int b);
	
	private void Start() {
		var add = Addify(2, 4);
		Debug.Log(add);
	}
}
