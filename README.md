# ZRhythm

這是一個免費開源的音樂遊戲＜/br＞
This project is free to use＜/br＞
你可以在這裡下載並且在你的手機裡面測試＜/br＞
you can download here＜/br＞
https://play.google.com/store/apps/details?id=com.BurningxEmpires.ZRhythm2&hl=zh_HK

需要額外下載 NGUI 插件＜/br＞
 NGUI require＜/br＞
https://assetstore.unity.com/packages/tools/gui/ngui-next-gen-ui-2413

![alt text](https://lh3.googleusercontent.com/Nr1mn3itf1Ah5m_Sav4wlyqeV7O8PFzuq1vgjrSSdhk0bxOWN8Lv2kuOWfeCp3pHsjw=h900-rw)

裡面包含了在手機抓取圖片或是音訊檔案、還有瀏覽資料夾的功能，你可以在你的專案裡面無償使用這些功能

```
AndroidFilePicker.PickFolder(callback);

AndroidFilePicker.PickFile(callback);

AndroidFilePicker.PickAudio(callback);

AndroidFilePicker.PickImage(callback);
```
裡面還包含了呼叫安卓平台一些常見的原生功能:

```
	public class AlertDialogBox{
		public AlertDialogBox(bool useTheme = false){}

		public AlertDialogBox(Action<int> onCallback,bool useTheme = false){}

		public AlertDialogBox(string title,string text,Action<int> onCallback,bool useTheme = false){}

		public void setCallback(Action<int> callback){}

		public void setTitle(string title){}

		public void setMessage(string message){}

		public void setButton(BUTTON button,string text){}

		public void setButton(int button,string text){}

		public void BreakLine(){}

		public void MakeItScroll(){}

		public void MakeItList(){}

		public AndroidJavaObject getLayout( ){}

		public void setLayoutPadding(int left,int right,int top,int bottom){}

		public AndroidJavaObject addInputText(string text){}

		public AndroidJavaObject addToggleButton(string text,bool check){}

		public AndroidJavaObject addCheckBox(string text,bool check){}

		public AndroidJavaObject addTextView(string text){}

		public AndroidJavaObject setInputText(string text){}

		public AndroidJavaObject setToggleButton(string text,bool enable){}
		
		public bool getToggleEnable(int index){}

		public int getButtonNeutral(){}

		public int getButtonPositive(){}

		public void Cancel(){}

		public void Show(){}
	}
```

```
	public static class AndroidTool{
		//https://developer.android.com/reference/android/R.style.html#Theme_Material
		public const int Theme_Light = 16973836;
		public const int Theme_Material = 16974372;
		public const int Theme_Holo_Light = 16973934;

		public static void MakeToast(string message){}

		public static void SetTheme(){}

		public static void SetTheme(int id){}
	}
```
技術提供:

https://stackoverflow.com/questions/33295300/how-to-get-absolute-path-in-android-for-file
http://givemepass.blogspot.tw/2016/09/imagepickercreatechooser.html
https://stackoverflow.com/questions/29713587/howto-get-the-real-path-with-action-open-document-tree-intent-lollipop-api-21

## 預覽圖片

![alt text](https://lh3.googleusercontent.com/x_Uuy7w6xzhbgfyfOA5KRm5rd3WIGd_diHqO_8DJ8MCQEwUYLEt30jhEMtI-lSLiww=h900-rw)
![alt text](https://lh3.googleusercontent.com/jWUHMsILZ_xcPLVfHVFTe5AHZ6yLRN-KuZDqbRRkCLX7hf48Z4ALC4BeELfThjwWJS86=h900-rw)
![alt text](https://lh3.googleusercontent.com/khwQ3p7kF0B06xmMJ8Z7VgBYz8yJE8_nVt-l57lNcn_UhvN_pyoz1dPq39zUT6ujEko=h900-rw)

