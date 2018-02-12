using UnityEngine;
using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Security.Cryptography;
using Ionic.Zip;
using LitJson;

namespace BurningxEmpires.ZRhythm{
	[Serializable]
	public class GameMapTempFolder {

		public ChunkHeader chunkHeader;

		public string folderPath;

		public bool isGenerated{
			get{
				return false;
			}
		}

		//creat by exsits folder
		public GameMapTempFolder (string folderpath) {
			folderPath = folderpath;
			DirectoryInfo directory = new DirectoryInfo(folderPath);
			FileInfo[] files = directory.GetFiles("*"+ChunkHeader.extension,SearchOption.TopDirectoryOnly);				
			if(files.Length > 0){
				// chunkheader file
				FileInfo file = files[0]; 
				if(!ChunkHeader.isChunkHeader(file.FullName,out chunkHeader)){
					Debug.Log("ChunkHeader is not exsits : " + file.FullName);
				}
			}
		}


		//creat by track_map_file
		public GameMapTempFolder (GameMap gameMap) {
			chunkHeader = new ChunkHeader(gameMap);
		}

		public void GenerateFolder (GameMap gameMap) {

			string filepath = gameMap.filePath;

			string filename = Path.GetFileNameWithoutExtension(filepath);

			filename = getMD5Hash(filename);

			string tempfilepath = 
				Path.Combine(ConfigUtility.temporaryPath,filename);

			if(Directory.Exists(tempfilepath)){
				Debug.Log("already has temp folder : " + tempfilepath);
				return;
			}

			ZipUtil.Unzip(filepath,tempfilepath);

			chunkHeader.GenerateFile(tempfilepath);

			DirectoryInfo tempDirectory = new DirectoryInfo(tempfilepath);

			folderPath = tempfilepath;

			FileInfo[] files = tempDirectory.GetFiles();

			Debug.Log(JsonUtility.ToJson(chunkHeader));

			foreach(var file in files){
				if(string.Equals(file.Name,gameMap.trackMapFileName)){
					File.Move(file.FullName,Path.Combine(tempfilepath,chunkHeader.trackMapFileCode));
				}else if(string.Equals(file.Name,gameMap.audioFileName)){
					File.Move(file.FullName,Path.Combine(tempfilepath,chunkHeader.audioFileCode));
				}else if(string.Equals(file.Name,gameMap.backgroundFileName)){	
					//Debug.Log("backgroundFileCode:" + chunkHeader.backgroundFileCode);	
					File.Move(file.FullName,Path.Combine(tempfilepath,chunkHeader.backgroundFileCode));
				}
			}	

		}

		public static bool isTempFolder(string folderpath){
			ChunkHeader chunkheader = null;
			return isTempFolder(folderpath,out chunkheader);
		}

		/// <summary>
		/// Is the temp folder.
		/// 路徑是否為解壓縮暫存資料夾,
		/// 針對chunk header(*.ini)和以下的
		/// audio file, background file, trackmap file檔案code name作簡單檢查
		/// </summary>
		/// <returns><c>true</c>, if temp folder was ised, <c>false</c> otherwise.</returns>
		/// <param name="folderpath">track map file暫存路徑</param>
		/// <param name="chunkheader">Chunkheader.</param>
		public static bool isTempFolder(string folderpath,out ChunkHeader chunkheader){
			DirectoryInfo directory = new DirectoryInfo(folderpath);
			FileInfo[] files = directory.GetFiles("*"+ChunkHeader.extension,SearchOption.TopDirectoryOnly);				
			if(files.Length > 0){
				// chunkheader file
				FileInfo file = files[0]; 
				if(ChunkHeader.isChunkHeader(file.FullName,out chunkheader)){
					if(!File.Exists(Path.Combine(folderpath,chunkheader.audioFileCode))){
						return false;
					}else if(!File.Exists(Path.Combine(folderpath,chunkheader.backgroundFileCode))){
						return false;
					}else if(!File.Exists(Path.Combine(folderpath,chunkheader.trackMapFileCode))){
						return false;
					}
					return true;
				}
			}
			chunkheader = null;
			return false;
		}

		/// <summary>
		/// Hases the temp folder.
		/// 是否已有解壓縮暫存資料的生成,
		/// 不檢查內部結構
		/// </summary>
		/// <returns><c>true</c>, if temp folder was hased, <c>false</c> otherwise.</returns>
		/// <param name="filepath">Filepath.</param>
		public static bool hasTempFolder(string filepath){

			string filename = Path.GetFileNameWithoutExtension(filepath);
			filename = getMD5Hash(filename);
			string temppath = Path.Combine(ConfigUtility.temporaryPath,filename);
			//Debug.Log(temppath);
			if(Directory.Exists(temppath))
				return true;

			return false;
		}

		public static string getMD5Hash(string data){
			StringBuilder sb = new StringBuilder();
			using (MD5 md5 = MD5.Create()) {
				byte[] hash = md5.ComputeHash(Encoding.UTF8.GetBytes(data));
				foreach(var h in hash){
					sb.Append(h.ToString("x2"));
				}
			}
			return sb.ToString();
		}

		public static string encode (string data) {
			return Convert.ToBase64String(
				UTF8Encoding.UTF8.GetBytes(data)
			);
		}

		public static string decode (string data) {
			return UTF8Encoding.UTF8.GetString(
				Convert.FromBase64String(data)
			);					
		}

		//google : chunk 檔案格式
		//faculty.stust.edu.tw/~tang/multi-media/midi.ppt
		//MIDI檔案結構. MIDI檔是由Chunk所組成，包含了二種型式的Chunk：Header Chunk及Track Chunk。
		[Serializable]
		public class ChunkHeader{

			public const string extension = ".ini";

			/// <summary>
			/// The name of the file.
			/// Chunk Header檔案名稱,
			/// 由所在資料夾名稱轉成Hash Code當成檔名 (MD5)
			/// </summary>
			public string fileName;

			/// <summary>
			/// The audio file code.
			/// audio file name 轉成的 Hash Code name
			/// </summary>
			public string audioFileCode;

			/// <summary>
			/// The background file code.
			/// background file name 轉成的 Hash Code name
			/// </summary>
			public string backgroundFileCode;

			/// <summary>
			/// The track map file code.
			/// track map file name 轉換成的 Hash Code name
			/// </summary>
			public string trackMapFileCode;

			//public string audioExtension;

			/// <summary>
			/// <para>Initializes a new instance of the <see cref="ZRhythm.Game.Selection.GameMapFolder+ChunkHeader"/> class.</para>
			/// <para>Do Not Use It Creat Instance</para>
			/// </summary>
			public ChunkHeader () {

			}

			/// <summary>
			/// Initializes a new instance of the <see cref="ZRhythm.Game.Selection.GameMapFolder+ChunkHeader"/> struct.
			/// 或者也可使用FromJson生成
			/// </summary>
			/// <param name="trackMapFile">Track map file.</param>
			public ChunkHeader (GameMap trackMapFile) {

				fileName = Path.ChangeExtension(getMD5Hash(trackMapFile.trackMapFileName),extension);
				Debug.Log("ChunkHeader.fileName : "+fileName);
				trackMapFileCode 	= getMD5Hash(trackMapFile.trackMapFileName);
				audioFileCode 		= getMD5Hash(trackMapFile.audioFileName);

				//audioExtension = Path.GetExtension(trackMapFile.audioFileName);

				if(trackMapFile.hasBackgroundFile)
					backgroundFileCode 	= getMD5Hash(trackMapFile.backgroundFileName);
			}

			/// <summary>
			/// Generates the file.
			/// 生成ChunkHeader(md5編碼).ini檔案
			/// </summary>
			/// <param name="path">directory. 檔案生成路徑</param>
			public void GenerateFile(string path){
				path = Path.Combine(path,fileName);
				string chunkheader = this.ToJson();
				chunkheader = encode(chunkheader);
				//Debug.Log("chunkheader ebcoded : " + chunkheader);
				File.WriteAllText(
					path,
					chunkheader
				);
			}

			public string ToJson () {
				string jsonData = JsonUtility.ToJson(this,true);
				//JsonMapper.UnregisterExporters();
				return jsonData;
			}

			public static bool isChunkHeader (string filepath,out ChunkHeader chunkheader) {
				string json = null;
				try{
					json = File.ReadAllText(filepath);
					json = decode(json);
					chunkheader = FromJson(json);
					return true;
				}catch(Exception e){					
					Debug.LogError(e.Message + "\n" + json);
				}
				chunkheader = null;
				return false;
			}

			/// <summary>
			/// Froms the json.
			/// 從json格式的string資料生成chunk header實體,
			/// 記得解碼 (encode)
			/// </summary>
			/// <returns>The json.</returns>
			/// <param name="jsonData">Json data.</param>
			public static ChunkHeader FromJson(string jsonData){
				//JsonMapper.RegisterImporter<double, float>(input => Convert.ToSingle(input));
				ChunkHeader chunkheader = JsonMapper.ToObject<ChunkHeader>(jsonData);
				//JsonMapper.UnregisterExporters();
				return chunkheader;
			}	

		}


	}

}