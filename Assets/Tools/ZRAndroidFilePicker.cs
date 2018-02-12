using UnityEngine;
using System.Collections;
using System;

public class ZRAndroidFilePicker : MonoBehaviour {

	private static ZRAndroidFilePicker m_Instance;

	public static ZRAndroidFilePicker getInstance{
		get{
			if(m_Instance == null){
				m_Instance = (new GameObject(typeof(ZRAndroidFilePicker).ToString())).AddComponent<ZRAndroidFilePicker>();
			}
			return m_Instance;
		}
	}

	const string PACKAGE_GALLERY = "com.burningxempires.tool.Gallery";


	private Action<string> callback;

	public static void PickFile(Action<string> onSucceeded,string mimeType = "*/*"){
		getInstance.callback = onSucceeded;
		using (AndroidJavaClass Gallery  = new AndroidJavaClass (PACKAGE_GALLERY)) {
			Gallery.CallStatic("openGallery",mimeType,getInstance.name,"OnFileChooserSucceeded");
		}
	}

	/*public static void PickRctFile(){

	}*/

	public static void PickAudio(Action<string> onSucceeded){
		getInstance.callback = onSucceeded;
		using (AndroidJavaClass Gallery  = new AndroidJavaClass (PACKAGE_GALLERY)) {
			Gallery.CallStatic("openAudioGallery",getInstance.name,"OnFileChooserSucceeded");
		}
	}

	public static void PickImage(Action<string> onSucceeded){
		getInstance.callback = onSucceeded;
		using (AndroidJavaClass Gallery  = new AndroidJavaClass (PACKAGE_GALLERY)) {
			Gallery.CallStatic("openImageGallery",getInstance.name,"OnFileChooserSucceeded");
		}
	}

	private void OnFileChooserSucceeded(string result){
		if(callback != null){
			callback(result);
			callback = null;
		}
	}


}
