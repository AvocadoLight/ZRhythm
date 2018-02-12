using UnityEngine;
using System.Collections;
using System;


namespace BurningxEmpires.ZRhythm.Tools{
	public static class AndroidTool{

		const string PACKAGE_ANDROIDTOOL = "com.burningxempires.tool.AndoirdTool";

		const string ReceiverName = "ReceiverName";
		const string MethodName = "MethodName";
		//https://developer.android.com/reference/android/R.style.html#Theme_Material
		public const int Theme_Light = 16973836;
		public const int Theme_Material = 16974372;
		public const int Theme_Holo_Light = 16973934;

		public static void MakeToast(string message){		
			using (AndroidJavaClass Tool  = new AndroidJavaClass (PACKAGE_ANDROIDTOOL)) {
				Tool.CallStatic("makeToast",message);
			}
		}

		[System.Obsolete("no this function")]
		public static void AlertDialogBox(string title,string message){
			using (AndroidJavaClass Tool  = new AndroidJavaClass (PACKAGE_ANDROIDTOOL)) {
				Tool.CallStatic("AlertDialogBox",title,message);
			}
		}

		[System.Obsolete("no this function")]
		public static void AlertDialogBox(string title,string message,string button,Action callback){
			using (AndroidJavaClass Tool  = new AndroidJavaClass (PACKAGE_ANDROIDTOOL)) {
				var handle = AndroidCallbackHandle.Creat();
				Tool.SetStatic<string>(ReceiverName,handle.name);
				Tool.SetStatic<string>(MethodName,AndroidCallbackHandle.CallbackName);
				Tool.CallStatic("AlertDialogBox",title,message,button);
			}
		}

		public static void SetTheme(){
			using (AndroidJavaClass Tool  = new AndroidJavaClass (PACKAGE_ANDROIDTOOL)) {
				Tool.CallStatic("setTheme");
			}
		}

		public static void SetTheme(int id){
			using (AndroidJavaClass Tool  = new AndroidJavaClass (PACKAGE_ANDROIDTOOL)) {
				Tool.CallStatic("setTheme",id);
			}
		}


		public static void printRes(){
			using (AndroidJavaClass Tool  = new AndroidJavaClass (PACKAGE_ANDROIDTOOL)) {
				Tool.CallStatic("printRes");
			}
		}
		private class AndroidCallbackHandle:MonoBehaviour{
			public Action callback;
			public const string CallbackName = "Callback";
			public static AndroidCallbackHandle Creat(){
				var go = new GameObject(typeof(AndroidCallbackHandle).ToString());	
				return go.AddComponent<AndroidCallbackHandle>();
			}		

			public void Callback(String message){
				if(callback != null){
					callback();
				}
				Destroy(this.gameObject);
			}

		}

	}

}