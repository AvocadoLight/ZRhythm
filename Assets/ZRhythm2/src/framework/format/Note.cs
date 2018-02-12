//----------------------------------------------
//            ZRhythm: Z-Rhythm Framework kit
// Copyright © 2015-2017 BurningxEmpires 火雨連城
//----------------------------------------------

using System;
using UnityEngine;
using System.Collections;

namespace BurningxEmpires.ZRhythm{

	/// <summary>
	/// Note.
	/// 音符
	/// </summary>
	[Serializable]
	public class Note{
		
		/// <summary>
		/// The instance ID.
		/// 音符的唯一索引值
		/// </summary>
		public int InstanceID;

		/// <summary>
		/// The type.
		/// 音符的種類
		/// </summary>
		public NoteType type;

		/// <summary>
		/// <para>X offset. (-1~1)</para>
		/// <para>遊戲畫面上的左右位置</para>
		/// </summary>
		public float Xoffset;

		/// <summary>
		/// <para>grid lenght.</para>
		/// <para>在歌譜上的絕對長度</para>
		/// </summary>
		public int lenght;

		/// <summary>
		/// <para>grid position. </para>
		/// <para>在曲譜上的絕對距離</para>
		/// </summary>
		public int position;

		/// <summary>
		/// 角度
		/// </summary>
		public float angle;

		/// <summary>
		/// <para>Default.</para>
		/// <para>取的預設的音符</para>
		/// <para>預設的音符為空音符</para>
		/// </summary>
		/// <value>The default.</value>
		/// 用Null取代
		/*public readonly static Note Default =
			new Note{
				InstanceID = NoteUtility.GetInstanceID(),
				type = NoteType.Null,
				Xoffset = 0,
				lenght = 1,
				position = -1,
			};*/

		public Note () {
			type = NoteType.Null;
			InstanceID = NoteUtility.GetInstanceID();
		}
	}

	public static class NoteUtility{

		private static int lastInstanceID = -1;

		readonly static DateTime seed = 
			new DateTime(2015,1,1,0,0,0,DateTimeKind.Utc);

		public static int GetInstanceID () {
			
			if(lastInstanceID == -1){
				lastInstanceID = (int)(DateTime.UtcNow - seed).TotalSeconds;
			}else{
				++lastInstanceID;
			}
		
			return lastInstanceID;
		}

		public static Note GenerateNote (NoteType type) {
			return new Note{
				InstanceID = NoteUtility.GetInstanceID(),
				type = type,
				Xoffset = 0,
				lenght = 1,
				position = -1,
			};	
		}

	}

}