using UnityEngine;
using System.Collections;
using System;

namespace BurningxEmpires.ZRhythm.Tools{
	public class AndroidFilePicker : MonoBehaviour {

		private static AndroidFilePicker m_Instance;

		public static AndroidFilePicker getInstance{
			get{
				if(m_Instance == null){
					m_Instance = (new GameObject(typeof(AndroidFilePicker).ToString())).AddComponent<AndroidFilePicker>();
				}
				return m_Instance;
			}
		}

		const string PACKAGE_GALLERY = "com.burningxempires.tool.Gallery";


		private Action<string> callback;

		public static void PickFolder(Action<string> onSucceeded){
			getInstance.callback = onSucceeded;
			using (AndroidJavaClass Gallery  = new AndroidJavaClass (PACKAGE_GALLERY)) {
				Gallery.CallStatic("openFolderGallery",getInstance.name,"OnFileChooserSucceeded");
			}
		}

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
}