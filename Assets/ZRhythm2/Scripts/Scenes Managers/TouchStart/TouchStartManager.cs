using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using  BurningxEmpires.ZRhythm.Tools;
using System.Windows.Forms;

namespace BurningxEmpires.ZRhythm{
	
	public class TouchStartManager : MonoBehaviour {

		public static TouchStartManager getInstance{
			private set;
			get;
		}

		#if UNITY_EDITOR

		public UnityEditor.SceneAsset scene_GameMapEditor;

		public UnityEditor.SceneAsset scene_GameMapSelector;

		#endif

		public string scene_GameMapEditor_name;
		public string scene_GameMapSelector_name;

		// Use this for initialization
		void Awake () {
			getInstance = this;
		}

		public void openPickFolder (){
			if(UnityEngine.Application.platform == RuntimePlatform.Android){
				//要求API level 21
				AndroidFilePicker.PickFolder(callback);
			}else{
				var openFolder = 
					OpenFolder.Open(
						"請選擇一個資料夾"
					);
				var result = openFolder.ShowDialog();
				if( result == DialogResult.OK )
				{
					var folderName = openFolder.SelectedPath;
					print(folderName);
				}
			}
		}

		public void openPickFile (){
			if(UnityEngine.Application.platform == RuntimePlatform.Android){
				AndroidFilePicker.PickFile(callback);
			}else{
				var openFile = 
					OpenFile.Open(
						"請選擇一個資料夾",
						ConfigUtility.persistentDataPath,
						"All Files\0*.*\0\0",
						""
					);
				if(DllOpebFile.GetOpenFileName(openFile)){
					if(!string.IsNullOrEmpty(openFile.file)){
						print(openFile.file);
					}
				}
			}
		}

		public void openPickAudio (){
			AndroidFilePicker.PickAudio(callback);
		}

		public void openPickImage (){
			AndroidFilePicker.PickImage(callback);
		}

		public void callback (string result) {
			AndroidTool.MakeToast("路徑:" + result);
		}

		public void Goto_GameMapEditor () {
			LoadScene (scene_GameMapEditor_name);
		}

		public void Goto_GameMapSelector () {
			LoadScene (scene_GameMapSelector_name);
		}

		public void LoadScene (string sceneName) {
			SceneManager.LoadScene(sceneName);
		}

		#if UNITY_EDITOR

		void OnValidate () {
			if(scene_GameMapEditor != null){
				scene_GameMapEditor_name = scene_GameMapEditor.name;
			}
			if(scene_GameMapSelector != null){
				scene_GameMapSelector_name = scene_GameMapSelector.name;
			}
		}

		#endif
	}

}