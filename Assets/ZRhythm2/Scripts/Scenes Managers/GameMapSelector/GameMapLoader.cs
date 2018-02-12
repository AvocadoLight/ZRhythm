using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace BurningxEmpires.ZRhythm.Selector{
	
	using selector = GameMapSelectorManager;

	public class GameMapLoader{
		//Full Name Path 
		public List<string> GameMapTempFolderPaths = new List<string>();
		public List<GameMapTempFolder> GameMapTempFolders = new List<GameMapTempFolder>();

		public static GameMapLoader LoadGameMaps () {
			GameMapLoader loader = new GameMapLoader();
			loader.LoadTemps();
			loader.LoadFiles();
			return loader;
		}

		/// <summary>
		/// Loads the temps.
		/// 載入暫時檔案資料
		/// </summary>
		private void LoadTemps(){
			//Debug.Log("暫存路徑" + ConfigUtility.temporaryPath);
			DirectoryInfo directory = new DirectoryInfo(ConfigUtility.temporaryPath);
			DirectoryInfo[] directoryInfos = directory.GetDirectories();
			//Debug.Log("數量:" + directoryInfos.Length);
			foreach (var directoryInfo in directoryInfos) {
				//Debug.Log("載入暫存" + directoryInfo.Name);
				if(GameMapTempFolder.isTempFolder(directoryInfo.FullName)){
					string fullPath = directoryInfo.FullName;
					GameMapTempFolderPaths.Add(fullPath);
					GameMapTempFolders.Add(new GameMapTempFolder(fullPath));
				}
			}
		}

		/// <summary>
		/// Loads the files.
		/// 載入歌譜
		/// 並生成暫時檔案資料
		/// </summary>
		private void LoadFiles(){
			//Debug.Log("檔案路徑" + ConfigUtility.persistentDataPath);
			DirectoryInfo directory = new DirectoryInfo(ConfigUtility.persistentDataPath);
			FileInfo[] fileInfos = directory.GetFiles("*"+GameMap.extension);
			//Debug.Log("數量:" + fileInfos.Length);
			foreach (var fileInfo in fileInfos) {
				Debug.Log(fileInfo.Name);
				if(!GameMapTempFolder.hasTempFolder(fileInfo.FullName)){	
					//Debug.Log("生成暫存" + fileInfo.Name);
					//GameMap trackMapFile = new GameMap(fileInfo.FullName);
					GameMap trackMapFile = GameMapUtility.GenerateGameMap(fileInfo.FullName);
					GameMapTempFolder temp = new GameMapTempFolder(trackMapFile);
					temp.GenerateFolder(trackMapFile);
					GameMapTempFolderPaths.Add(temp.folderPath);
					GameMapTempFolders.Add(temp);
				}
			}
		}
	}
}