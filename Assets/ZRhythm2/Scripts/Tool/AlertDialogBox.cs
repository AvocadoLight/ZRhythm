using UnityEngine;
using System.Collections;
using System;

namespace BurningxEmpires.ZRhythm.Tools{
	public class AlertDialogBox{

		public AndroidJavaObject javaObject;

		private JavaCallBack javaCallBack;

		public AlertDialogBox(bool useTheme = false){
			javaCallBack = new JavaCallBack();
			javaObject = 
				new AndroidJavaObject(
					"com.burningxempires.tool.AlertDialogBox",
					javaCallBack,useTheme
				);

		}

		public AlertDialogBox(Action<int> onCallback,bool useTheme = false){		
			javaCallBack = new JavaCallBack(onCallback);
			javaObject = 
				new AndroidJavaObject(
					"com.burningxempires.tool.AlertDialogBox",
					javaCallBack,useTheme
				);

		}

		public AlertDialogBox(string title,string text,Action<int> onCallback,bool useTheme = false){
			javaCallBack = new JavaCallBack(onCallback);
			javaObject = 
				new AndroidJavaObject(
					"com.burningxempires.tool.AlertDialogBox",
					title,
					text,
					javaCallBack,
					useTheme
				);

		}

		public void setCallback(Action<int> callback){
			javaCallBack.callBack = callback;
		}

		public void setTitle(string title){
			javaObject.Call("setTitle",title);
		}

		public void setMessage(string message){
			javaObject.Call("setMessage",message);
		}

		public void setButton(BUTTON button,string text){
			setButton((int)button,text);
		}

		public void setButton(int button,string text){
			javaObject.Call("setButton",button,text);
		}

		public void BreakLine(){
			javaObject.Call("BreakLine");
		}

		public void MakeItScroll(){
			javaObject.Call("makeItScroll");
		}

		public void MakeItList(){
			javaObject.Call("makeItList");
		}

		public AndroidJavaObject getLayout( ){
			return javaObject.Call<AndroidJavaObject>("getLayout");
		}

		public void setLayoutPadding(int left,int right,int top,int bottom){
			javaObject.Call("setLayoutPadding",left, right, top, bottom);
		}

		public AndroidJavaObject addInputText(string text){
			return javaObject.Call<AndroidJavaObject>("addInputText",text);
		}

		public AndroidJavaObject addToggleButton(string text,bool check){
			return javaObject.Call<AndroidJavaObject>("addToggleButton",text,check);
		}

		public AndroidJavaObject addCheckBox(string text,bool check){
			return javaObject.Call<AndroidJavaObject>("addCheckBox",text,check);
		}

		public AndroidJavaObject addTextView(string text){
			/*AndroidJavaObject javaObject = 
			new AndroidJavaObject("android.widget.TextView",
				new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity")	
			);
		javaObject.Call("setText",text);
		getLayout().Call("addView",javaObject);
		return javaObject;*/
			return javaObject.Call<AndroidJavaObject>("addTextView",text);
		}

		public AndroidJavaObject setInputText(string text){
			return javaObject.Call<AndroidJavaObject>("setInputText",text);
		}

		public AndroidJavaObject setToggleButton(string text,bool enable){
			return javaObject.Call<AndroidJavaObject>("setToggleButton",text,enable);
		}

		public string getInputText(int index){
			return javaObject.Call<string>("getInputText",index);
		}

		public bool getToggleEnable(int index){
			return javaObject.Call<bool>("getToggleEnable",index);
		}

		public int getButtonNeutral(){
			return javaObject.Call<int>("getButtonNeutral");
		}

		public int getButtonPositive(){
			return javaObject.Call<int>("getButtonPositive");
		}

		public int getButtonNegative(){
			return javaObject.Call<int>("getButtonNegative");
		}

		public void Cancel(){
			javaObject.Call("Cancel");
		}

		public void Show(){
			javaObject.Call("Show");
		}

		public enum BUTTON:int{
			/// <summary>
			/// The NEUTRA. 中間
			/// </summary>
			NEUTRAL  = -3,
			/// <summary>
			/// The POSITIV. 左邊
			/// </summary>
			POSITIVE = -1,
			/// <summary>
			/// The NEGATIV. 右邊
			/// </summary>
			NEGATIVE = -2	
		}

	}

	public class JavaCallBack:AndroidJavaProxy{
		public Action<int> callBack;
		public JavaCallBack(Action<int> callback):base("com.burningxempires.tool.ICallBack"){		
			callBack = callback;
		}

		public JavaCallBack():base("com.burningxempires.tool.ICallBack"){		
		}

		public void onCallBack(int which){
			if(callBack!=null){
				callBack(which);
			}
			//ZRAndroidTool.MakeToast("按下的按鈕是"+which);
		}
	}

}