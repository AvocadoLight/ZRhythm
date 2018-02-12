using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System.IO;
using System;

namespace BurningxEmpires.ZRhythm.Editor{

	using editor = GameMapEditorManager;

	public class BlockElement_TrackMapPath : BlockElement_PathSelector {

		public FileManagerModuleManager manager{get{return FileManagerModuleManager.getInstance;}}

		// Use this for initialization
		void Start () {
			
		}
		
		// Update is called once per frame
		void Update () {
			path.text = manager.cacheFilePath;
		}

		public override void onClick ()	{
			
			if(editor.audioPlayer.isPlaying)
				return;

			if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer)
				this.windowsPlatformLoader();					
			else if (Application.platform == RuntimePlatform.Android)
				this.androidPlatformLoader ();
			else
				editor.debugLog.LogWarning(ExceptionList.platformNotFound.Value);


		}

		IEnumerator LoadContent (TrackMapHeader header) {

			if(File.Exists(header.BackgroundFileFullName))
				yield return StartCoroutine ( manager.imagePath.LoadImage(header.BackgroundFileFullName));
			else
				editor.debugLog.LogWarning(ExceptionList.fileNotFound.Value + " : " + header.BackgroundFileFullName);
			
			if(File.Exists(header.AudioFileFullName))
				yield return StartCoroutine (manager.audioPath.LoadAudioCilp(header.AudioFileFullName));
			else
				editor.debugLog.LogWarning(ExceptionList.fileNotFound.Value + " : " + header.AudioFileFullName);
			
		}

		void androidPlatformLoader () { 
			ZRAndroidFilePicker.PickFile(
				//ZRFilePicker.Creat ( 
				( filepath) => {
					TrackMap ZRTrackFile;
					if (!string.IsNullOrEmpty (filepath)) {			

						string json = File.ReadAllText(filepath);
						try{								
							ZRTrackFile = TrackMap.FromJson(json);
						}catch(Exception e){
							editor.debugLog.LogWarning(ExceptionList.openFileError.Value);
							return;
						}
						manager.cacheFilePath = filepath;
						editor.getTrackMap = ZRTrackFile;
						//TODO:Load its AudioClip and Background Image
						StartCoroutine(LoadContent(editor.getTrackMap.header));
					} else {
						editor.debugLog.LogWarning(ExceptionList.openFileError.Value);
					}
				});
		}

		void windowsPlatformLoader () { 
			string path = ConfigUtility.persistentDataPath + "/" + editor.getTrackMap.header.Title + TrackMap.extension;

			TrackMap ZRTrackFile;
			OpenFileName ofn = new OpenFileName ();  
			ofn.structSize = Marshal.SizeOf (ofn);  
			ofn.filter = "All Files\0*.*\0\0";  
			//ofn.filter = "MP3 audio (*.mp3) | *.mp3";
			ofn.file = new string (new char[256]);  
			ofn.maxFile = ofn.file.Length;  
			ofn.fileTitle = new string (new char[64]);  
			ofn.maxFileTitle = ofn.fileTitle.Length;  
			ofn.initialDir = ConfigUtility.persistentDataPath;
			ofn.title = "請選擇一個" + TrackMap.extension + "檔案";  
			ofn.defExt = TrackMap.extension.ToUpper();
			//ofn.flags = 0x00080000 | 0x00001000 | 0x00000800 | 0x00000200 | 0x00000008;//OFN_EXPLORER|OFN_FILEMUSTEXIST|OFN_PATHMUSTEXIST| OFN_ALLOWMULTISELECT|OFN_NOCHANGEDIR  
			ofn.flags = 0x00080000 | 0x00001000 | 0x00000800 | 0x00000008;//OFN_EXPLORER|OFN_FILEMUSTEXIST|OFN_PATHMUSTEXIST| OFN_NOCHANGEDIR  
			if (DllOpebFile.GetOpenFileName (ofn)) { 
				if (!string.IsNullOrEmpty (ofn.file)) {
					string json = File.ReadAllText(ofn.file);
					try{								
						ZRTrackFile = TrackMap.FromJson(json);
					}catch(Exception e){
						editor.debugLog.LogWarning(ExceptionList.openFileError.Value);
						return;
					}
					manager.cacheFilePath = ofn.file;
					editor.getTrackMap = ZRTrackFile;
					//TODO:Load its AudioClip and Background Image
					StartCoroutine(LoadContent(editor.getTrackMap.header));
				} else {
					editor.debugLog.LogWarning(ExceptionList.openFileError.Value);
				}
			} 
		}





	}
}