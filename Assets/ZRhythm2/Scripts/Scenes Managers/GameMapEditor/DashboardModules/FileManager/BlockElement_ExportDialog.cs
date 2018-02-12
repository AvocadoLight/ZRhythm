using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System.IO;
using System;
using BurningxEmpires.ZRhythm.Tools;

namespace BurningxEmpires.ZRhythm.Editor{

	using editor = GameMapEditorManager;

	public class BlockElement_ExportDialog : BlockElement_ToolBox {

		public FileManagerModuleManager manager{get{return FileManagerModuleManager.getInstance;}}

		public override void onRegular () {
			m_Panel.baseClipRegion = new Vector4 (
				m_Panel.baseClipRegion.x,
				m_Panel.baseClipRegion.y,
				m_Panel.baseClipRegion.z,
				originHeight 
			);
		}

		//TODO:更新Log列表, MassageList , ExceptionList
		public void onExport () {
			SaveFile(false,"_export_save_");

			string RctFilePath = manager.cacheFilePath;
			string BackgroundFilePath 	= editor.getTrackMap.header.BackgroundFileFullName;
			string AudioFilePath 		= editor.getTrackMap.header.AudioFileFullName;

			if(!editor.audioPlayer.hasAudioClip){
				editor.debugLog.LogWarning("找不到音訊檔案 , 無法輸出");
				return;
			}else if(string.IsNullOrEmpty(editor.getTrackMap.header.Title)){
				editor.debugLog.LogWarning("沒有輸入檔案標題 , 無法輸出");
				return;
			}else if(string.IsNullOrEmpty(editor.getTrackMap.header.Artist)){
				editor.debugLog.LogWarning("沒有輸入創作者名稱 , 無法輸出");
				return;
			}else if(editor.getTrackMap.Notes.Count<=0){
				editor.debugLog.LogWarning("找不到任何編輯的音符 , 無法輸出");
				return;
			}else if(string.IsNullOrEmpty( AudioFilePath) || !File.Exists(AudioFilePath)){
				editor.debugLog.LogWarning("音訊檔路徑遺失 : " + AudioFilePath + ",無法輸出");
				return;
			}

			List<string> outputFiles = new List<string>();

			if(string.IsNullOrEmpty(RctFilePath))this.onSaveFile();

			outputFiles.Add(RctFilePath);
			outputFiles.Add(AudioFilePath);

			if(!string.IsNullOrEmpty( BackgroundFilePath) ){
				if( !File.Exists(BackgroundFilePath)){
					editor.debugLog.LogWarning("背景圖檔路徑遺失 : " + BackgroundFilePath);					
				}else{
					outputFiles.Add(BackgroundFilePath);
				}
			}

			string outputPath = 
				ConfigUtility.persistentDataPath + "/" +
				editor.getTrackMap.header.Title + "(" +
				editor.getTrackMap.header.Artist + ")";
			
			if(File.Exists(Path.ChangeExtension(outputPath,GameMap.extension))){
				outputPath +="_" + System.DateTime.Now.ToString("hh-mm-ss-yyyy-M-d");
			}
			outputPath=Path.ChangeExtension(outputPath,GameMap.extension);

			try{

				ZipUtil.Zip(outputPath,outputFiles.ToArray());
			}catch (Exception e){
				editor.debugLog.LogWarning("檔案:"+outputPath + ",輸出失敗"+ "\n" +e.ToString());
				return;
			}
			editor.debugLog.Log("檔案:"+outputPath + ",輸出成功");
			#if UNITY_STANDALONE_WIN || UNITY_EDITOR
			if (File.Exists (outputPath))
				System.Diagnostics.Process.Start("explorer.exe", "/select," +outputPath.Replace("/","\\"));
			#else
			if (File.Exists (outputPath))
				AndroidTool.MakeToast("輸出檔案成功 " + outputPath);
			#endif
		}

		public void onNewFile () {
			editor.getTrackMap = new TrackMap ();
			editor.gameScreen.setBackgroundImage(null);
			editor.audioPlayer.SetClip(null);
			editor.debugLog.Log ("打開了一個空白檔案");
		}

		public void onSaveFile () {
			SaveFile (true,string.Empty);
		}

		public void onSaveAsFile () {
			editor.debugLog.Log("此功能尚未開放");
			AndroidTool.MakeToast("此功能尚未開放");
		}

		void SaveFile (bool showSaveTemp,string extension)
		{

			string path = ConfigUtility.persistentDataPath + "/" + editor.getTrackMap.header.Title+extension;
			editor.debugLog.Log("執行動作:在以下路徑儲存檔案:");
			editor.debugLog.Log(path);
			if(File.Exists(path + TrackMap.extension)){
				path += "_" + System.DateTime.Now.ToString("hh-mm-ss-yyyy-M-d");
			}
			path += TrackMap.extension;
			string json = editor.getTrackMap.ToJson ();
			File.WriteAllText(path,json);
			//editor.debugLog.Log (json);
			manager.cacheFilePath = path;
			#if UNITY_STANDALONE_WIN || UNITY_EDITOR
			if (File.Exists (path) && showSaveTemp)
				System.Diagnostics.Process.Start("explorer.exe", "/select," +path.Replace("/","\\"));
			#else
			//editor.debugLog.Log("此功能尚未在手機平台開放");
			if (File.Exists (path) && showSaveTemp)
				AndroidTool.MakeToast("儲存檔案成功 " + path);
			#endif

		}

	}
}