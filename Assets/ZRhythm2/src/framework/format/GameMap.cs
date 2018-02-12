//----------------------------------------------
//            ZRhythm: Z-Rhythm Framework kit
// Copyright © 2015-2017 BurningxEmpires 火雨連城
//----------------------------------------------

using UnityEngine;
using System.Collections;
using System;
using System.IO;
using Ionic.Zip;

namespace BurningxEmpires.ZRhythm{

	public class GameMap {
		
		public const string extension = ".gmp";

		/// <summary>
		/// The file path.
		/// 這個檔案的完整路徑
		/// </summary>
		public string filePath;

		/// <summary>
		/// Gets the name of the file.
		/// 這個檔案包含副檔名的檔案名稱
		/// </summary>
		/// <value>The name of the file.</value>
		public string fileName{
			get{
				return Path.GetFileName(filePath);
			}
		}

		public TrackMap trackMap;

		/// <summary>
		/// The name of the track map file.
		/// track map 的檔案實體名稱,
		/// (在GameMap壓縮檔案之中的名稱)
		/// </summary>
		public string trackMapFileName;

		/// <summary>
		/// Gets the name of the audio file.
		/// return filename.extension
		/// </summary>
		/// <value>The name of the audio file.</value>
		public string audioFileName{
			get{
				return trackMap.header.getAudioFileName;
			}
		}

		/// <summary>
		/// Gets the name of the background file.
		/// return filename.extension
		/// </summary>
		/// <value>The name of the background file.</value>
		public string backgroundFileName{
			get{
				return trackMap.header.getBackgroundFileName;
			}
		}

		/// <summary>
		/// The has track map file.
		/// 壓縮檔中是否有歌譜檔案
		/// </summary>
		public bool hasTrackMapFile{
			get{
				return (trackMap != null) && (!string.IsNullOrEmpty(trackMapFileName));
			}
		}

		internal bool _hasAudioFile = false;
		/// <summary>
		/// The has audio file.
		/// 壓縮檔中是否有音檔
		/// 及音檔和header裡面的紀錄是否一致
		/// </summary>
		public bool hasAudioFile {
			get{
				return _hasAudioFile;
			}
		}

		internal bool _hasBackgroundFile = false;
		/// <summary>
		/// The has background file.
		/// 壓縮檔中是否有背景圖片
		/// 及圖片和header的紀錄是否一致
		/// </summary>
		public bool hasBackgroundFile{
			get{
				return _hasBackgroundFile;
			}
		}

		/// <summary>
		/// The is valid.
		/// 是否為一個有效的Track Map File壓縮檔 
		/// 判斷依據為trackmap是否可用和audio音檔是否存在
		/// </summary>
		public bool isValid{
			get{
				return hasTrackMapFile && hasAudioFile;
			}
		}

		public AudioClip audioClip;
		public Texture2D backgroundImage;

		public bool isDecompress{
			get{
				return audioClip != null;
			}
		}

		internal GameMap(){
			
		}

		//T ODO:如最下方註釋
//		internal GameMap(string path){
//			filePath = path;
//			string audioName = null;
//			string backgroundName = null;
//			using (ZipFile zip = ZipFile.Read (path)) {  
//				zip.AlternateEncodingUsage = ZipOption.Always;
//				zip.AlternateEncoding = System.Text.Encoding.UTF8;
//				foreach (ZipEntry file in zip) {
//					string extension = Path.GetExtension (file.FileName).ToLower ();
//					if(string.Compare(extension,TrackMap.extension) == 0){
//						using(var memoryStream = new MemoryStream()){
//							file.Extract(memoryStream);
//							memoryStream.Seek(0,SeekOrigin.Begin);
//							using(var stream = new StreamReader(memoryStream)){
//								string jsonData = stream.ReadToEnd();									
//								trackMap = TrackMap.FromJson(jsonData);
//								trackMapFileName = file.FileName;
//							}
//						}
//					}else if(ConfigUtility.isSupportedAudioType(extension)){
//						audioName = file.FileName;
//					}else if(ConfigUtility.isSupportedTextureType(extension)){
//						backgroundName = file.FileName;
//					}
//				}
//			}
//
//			if(!hasTrackMapFile){				
//				return;
//			}
//
//			if(string.Compare (
//				trackMap.header.getAudioFileName
//				, audioName) == 0){
//				_hasAudioFile = true;
//			}
//
//			if(string.Compare (
//				trackMap.header.getBackgroundFileName
//				, backgroundName) == 0){
//				_hasBackgroundFile = true;
//			}
//
//		}

		//TODO:完成這個格式並將需要做成Utility的部分從src中分開
		//TrackMapFile和GameMap之間的轉換務必考慮邏輯層和UI層之間的關係 (UI和實作分開)
		//謹慎選擇把解壓縮的部分放到這裡面來或是分開處理
		//因為會關係到Game Map Loader的部分
		//載入Game Map仍然是希望能夠選擇記憶體讀取(較慢)或是解壓縮後讀取(較快)
		//所以把解壓縮的部分和直接讀取的部分兩者合併到同一個流程中處理是一個比較理想的情況

	}

}