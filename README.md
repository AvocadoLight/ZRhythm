# ZRhythm

�o�O�@�ӧK�O�}�������ֹC��
�A�i�H�b�o�̤U���åB�b�A������̭�����

https://play.google.com/store/apps/details?id=com.BurningxEmpires.ZRhythm2&hl=zh_HK

![alt text](https://lh3.googleusercontent.com/Nr1mn3itf1Ah5m_Sav4wlyqeV7O8PFzuq1vgjrSSdhk0bxOWN8Lv2kuOWfeCp3pHsjw=h900-rw)

�̭��]�t�F�b�������Ϥ��άO���T�ɮסB�٦��s����Ƨ����\��A�A�i�H�b�A���M�׸̭��L�v�ϥγo�ǥ\��

```
AndroidFilePicker.PickFolder(callback);

AndroidFilePicker.PickFile(callback);

AndroidFilePicker.PickAudio(callback);

AndroidFilePicker.PickImage(callback);
```
�̭��٥]�t�F�I�s�w�����x�@�Ǳ`������ͥ\��:

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
�޳N����:

https://stackoverflow.com/questions/33295300/how-to-get-absolute-path-in-android-for-file
http://givemepass.blogspot.tw/2016/09/imagepickercreatechooser.html
https://stackoverflow.com/questions/29713587/howto-get-the-real-path-with-action-open-document-tree-intent-lollipop-api-21
