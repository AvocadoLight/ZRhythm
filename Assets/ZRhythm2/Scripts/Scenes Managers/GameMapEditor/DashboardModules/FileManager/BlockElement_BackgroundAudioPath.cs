using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System.IO;
using System;

namespace BurningxEmpires.ZRhythm.Editor{

	using editor = GameMapEditorManager;

	public class BlockElement_BackgroundAudioPath : BlockElement_PathSelector {

		// Use this for initialization
		void Start () {
			
		}
		
		// Update is called once per frame
		void Update () {
			path.text = editor.getTrackMap.header.AudioFileFullName;
		}

		public override void onClick ()	{
			if(editor.audioPlayer.isPlaying)
				return;

			if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer)
				this.windowsPlatformLoader ();
			else if (Application.platform == RuntimePlatform.Android)
				this.androidPlatformLoader ();
			else
				editor.debugLog.LogWarning(ExceptionList.platformNotFound.Value);
		}

		void androidPlatformLoader () {
			ZRAndroidFilePicker.PickAudio ((filepath) => {
				//ZRFilePicker.Creat((filepath)=>{
				if (!string.IsNullOrEmpty (filepath)) {
					StartCoroutine (this.LoadAudioCilp (filepath));
				} else {
					editor.debugLog.LogWarning(ExceptionList.openFileError.Value);
				}
			});
		}

		void windowsPlatformLoader () {
			string path = ConfigUtility.persistentDataPath + "/" + editor.getTrackMap.header.Title + TrackMap.extension;

			OpenFileName ofn = new OpenFileName ();  
			ofn.structSize = Marshal.SizeOf (ofn);  
			ofn.filter = "All Files\0*.*\0\0";  
			//ofn.filter = "MP3 audio (*.mp3) | *.mp3";
			ofn.file = new string (new char[256]);  
			ofn.maxFile = ofn.file.Length;  
			ofn.fileTitle = new string (new char[64]);  
			ofn.maxFileTitle = ofn.fileTitle.Length;  
			ofn.initialDir = ConfigUtility.persistentDataPath;
			ofn.title = "請選擇一個Mp3或Wav檔案";  
			ofn.defExt = "Wav";
			//ofn.flags = 0x00080000 | 0x00001000 | 0x00000800 | 0x00000200 | 0x00000008;//OFN_EXPLORER|OFN_FILEMUSTEXIST|OFN_PATHMUSTEXIST| OFN_ALLOWMULTISELECT|OFN_NOCHANGEDIR  
			ofn.flags = 0x00080000 | 0x00001000 | 0x00000800 | 0x00000008;//OFN_EXPLORER|OFN_FILEMUSTEXIST|OFN_PATHMUSTEXIST| OFN_NOCHANGEDIR  
			if (DllOpebFile.GetOpenFileName (ofn)) { 
				if (!string.IsNullOrEmpty (ofn.file)) {
					StartCoroutine (this.LoadAudioCilp (ofn.file));
				} else {
					editor.debugLog.LogWarning(ExceptionList.openFileError.Value);
				}
			} 
		}

		public IEnumerator LoadAudioCilp (string filepath)
		{
			var popup = PopupLoading.Creat ();
			yield return new WaitForEndOfFrame ();
			WWW www = new WWW (ConfigUtility.fileLoadPath(filepath));
			yield return www;
			Destroy (popup);
			yield return new WaitForEndOfFrame ();

			try {
				if (Path.GetExtension (filepath).ToLower () == ".mp3" && Application.platform != RuntimePlatform.Android) {
					var clip = NAudioPlayer.FromMp3Data (www.bytes);
					clip.name = Path.GetFileNameWithoutExtension (filepath);
					editor.audioPlayer.SetClip(clip);
				} else if (Path.GetExtension (filepath).ToLower () == ".wav" ||
					(Path.GetExtension (filepath).ToLower () == ".mp3" && Application.platform == RuntimePlatform.Android)) {
					var clip = www.audioClip;
					clip.name = Path.GetFileNameWithoutExtension (filepath);
					editor.audioPlayer.SetClip(clip);
				} else {
					editor.debugLog.LogWarning(ExceptionList.openFileError.Value);
					yield break;
				}

			} catch (Exception e) {
				editor.debugLog.LogWarning(ExceptionList.openFileError.Value);
				yield break;
			}
			editor.getTrackMap.header.AudioFileFullName = filepath;
			editor.debugLog.Log(MessageList.openFileSuccess.Value);
		}
	}
}