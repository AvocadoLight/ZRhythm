//----------------------------------------------
//            ZRhythm: Z-Rhythm Framework kit
// Copyright © 2015-2017 BurningxEmpires 火雨連城
//----------------------------------------------

using System;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

namespace BurningxEmpires.ZRhythm{

	/// <summary>
	/// Track Map. 歌譜 (另有用於遊戲中壓縮過的遊戲譜面 .gmp , game map)
	/// 一個用於儲存歌譜資訊的格式
	/// </summary>

	[Serializable]
	public class TrackMap : ICloneable{

		public const string version = "v1.0";

		public const string name_cn = "歌譜";

		public const string extension = ".tm";

		public TrackMapHeader header = TrackMapHeader.Default;

		public TrackMapSetting setting = TrackMapSetting.Defluat;

		public List<Note> Notes = new List<Note>();

		public string ToJson(){			
			
			string jsonData = JsonUtility.ToJson(this,true);

			return jsonData;
		}

		public static TrackMap FromJson(string json){
			JsonMapper.RegisterImporter<double, float>(input => Convert.ToSingle(input));
			TrackMap trackMap = JsonMapper.ToObject<TrackMap>(json);
			JsonMapper.UnregisterExporters();
			return trackMap;
		}

		public object Clone(){
			return this.MemberwiseClone();
		}

		public TrackMap Copy(){ 
			return this.Clone() as TrackMap;
		}

		public void Sort(){
			Notes.Sort((x, y) => {return x.position.CompareTo(y.position);});
		}

		/// <summary>
		/// Gets the size of the game screen.
		/// x width , y height
		/// </summary>
		/// <returns>The game screen size.</returns>
		public Vector2 getScreenSize () {			
			return new Vector2(
				ConfigUtility.width * setting.PageSize_x,
				ConfigUtility.height * setting.PageSize_y);
		}

		public float GetPositionY (float gridPosition) {
			var screenSize = ConfigUtility.screen;
			var trackMap = this;

			screenSize = 
				new Rect (Vector2.zero,getScreenSize());

			float posY = 
				screenSize.height * setting.PageStart / 2f +
				screenSize.height * (gridPosition /32f) *
				setting.PageScale;

			int quotient = (int)(posY / screenSize.height);
			float remainder = posY % screenSize.height;

			if (quotient % 2 == 0) {
				posY = screenSize.height / 2 - remainder;
			} else {
				posY = -screenSize.height / 2 + remainder;
			}

			return posY;
		}

		public float GetPage (float gridPosition) {
			var screenSize = ConfigUtility.screen;
			var trackMap = this;

			screenSize = 
				new Rect (Vector2.zero,getScreenSize());

			float posY = 
				screenSize.height * setting.PageStart / 2f +
				screenSize.height * (gridPosition /32f) *
				setting.PageScale;

			float quotient = posY / screenSize.height;

			return quotient;
		}



		public float GetPositionX (float Xoffset) {			

			return Xoffset * getScreenSize ().x;
		}

		public Note GetNote (int noteId) {
			return Notes.Find((obj)=>{return obj.InstanceID == noteId;});
		}

		public int GetIndex (int noteId) {
			var note = Notes.Find((obj)=>{return obj.InstanceID == noteId;});
			return Notes.IndexOf(note);
		}

		public int GetIndex (Note note) {
			return Notes.IndexOf(note);
		}
	}
}