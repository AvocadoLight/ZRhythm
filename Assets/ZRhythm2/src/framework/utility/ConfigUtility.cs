//----------------------------------------------
//            ZRhythm: Z-Rhythm Framework kit
// Copyright © 2015-2017 BurningxEmpires 火雨連城
//----------------------------------------------

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace BurningxEmpires.ZRhythm{
	public static class ConfigUtility{

		public const int width = 1280;
		public const int height = 720;

		public static Rect screen{
			get{
				return new Rect(0,0,width,height);
			}
		}

		public static readonly Vector2 border = new Vector2(15,15);

		public static readonly float reactionSensitive = 0.2f;

		public static readonly string Mp3Extension = ".mp3";

		public static readonly string WavExtension = ".wav";

		public static readonly string PngExtension = ".png";

		public static readonly string JpgExtension = ".jpg";

		public static readonly string[] supportAudioType = new string[]{Mp3Extension,WavExtension};

		public static readonly string[] supportTextureType = new string[]{PngExtension,JpgExtension};

		public static bool isSupportedAudioType(string extension){
			extension = extension.ToLower();
			foreach(var t in supportAudioType){
				if(string.Compare(extension,t)==0)
					return true;
			}
			return false;
		}

		public static bool isSupportedTextureType(string extension){
			extension = extension.ToLower();
			foreach(var t in supportTextureType){
				if(string.Compare(extension,t)==0)
					return true;
			}
			return false;
		}

		private static float _toolBoxSize = -1;

		public static float toolBoxSize{
			get{			
				if(_toolBoxSize < 0){
					_toolBoxSize = 
						PlayerPrefs.GetFloat(
							"toolBoxSize",
							1
						);
				}
				return _toolBoxSize;
			}set{
				_toolBoxSize = value;
				PlayerPrefs.SetFloat(
					"toolBoxSize",
					_toolBoxSize
				);
			}
		}

		private static string _temporaryPath;

		/// <summary>
		/// Gets or sets the temporary path.
		/// 歌譜解壓縮的佔存目錄
		/// </summary>
		/// <value>The temporary path.</value>
		public static string temporaryPath{
			get{			
				if(string.IsNullOrEmpty(_temporaryPath)){
					_temporaryPath = 
						PlayerPrefs.GetString(
							"temporaryPath",
							Application.temporaryCachePath
						);
				}
				return _temporaryPath;
			}set{
				_temporaryPath = value;
				PlayerPrefs.SetString(
					"temporaryPath",
					_temporaryPath
				);
			}
		}

		private static string _persistentDataPath;

		/// <summary>
		/// Gets or sets the track map files path.
		/// 讀取/儲存歌譜的檔案目錄
		/// </summary>
		/// <value>The track map files path.</value>
		public static string persistentDataPath{
			get{			
				if(string.IsNullOrEmpty(_persistentDataPath)){
					_persistentDataPath = 
						PlayerPrefs.GetString(
							"persistentDataPath",
							Application.persistentDataPath
						);
				}
				return _persistentDataPath;
			}set{
				_persistentDataPath = value;
				PlayerPrefs.SetString(
					"persistentDataPath",
					_persistentDataPath
				);
			}
		}

		public const string file = "file:///";

		public static string fileLoadPath (string path) {
			return string.Format("{0}{1}",file,path);
		}
	}

	public static class MessageList {
		public static readonly KeyValuePair<string,string>  openFileSuccess=
			new KeyValuePair<string,string>("file open success","開啟檔案成功");
	}

	public static class ExceptionList {

		public static readonly KeyValuePair<string,string> error =
			new KeyValuePair<string,string>("error","執行錯誤");
		public static readonly KeyValuePair<string,string>  platformNotFound=
			new KeyValuePair<string,string>("platform not found","無法判斷執行平台");
		public static readonly KeyValuePair<string,string>  openFileError=
			new KeyValuePair<string,string>("file open error","無法開啟檔案");
		public static readonly KeyValuePair<string,string>  shouldBeNumber=
			new KeyValuePair<string,string>("input should be a number","輸入必須為一個數字");
		public static readonly KeyValuePair<string,string>  fileNotFound=
			new KeyValuePair<string,string>("file not found","找不到檔案");

	}

}