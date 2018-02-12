//----------------------------------------------
//            ZRhythm: Z-Rhythm Framework kit
// Copyright © 2015-2017 BurningxEmpires 火雨連城
//----------------------------------------------

using System;
using UnityEngine;
using System.Collections;

namespace BurningxEmpires.ZRhythm{

	/// <summary>
	/// Note Type.
	/// 音符的類型
	/// </summary>

	[Serializable]
	public enum NoteType{
		/// <summary>
		/// The null.
		/// 一個空的音符
		/// </summary>
		Null = 1<<0,
		/// <summary>
		/// The tap.
		/// 點擊
		/// </summary>
		Tap = 1<<1,
		/// <summary>
		/// The hold.
		/// 長按
		/// </summary>
		Hold = 1<<2,
		/// <summary>
		/// The swipe.
		/// 快速滑動
		/// </summary>
		Swipe = 1<<3
	}

	public static class NoteTypeUtility{

		/// <summary>
		/// Tos the name.
		/// 得到音符的名字
		/// </summary>
		/// <returns>The name.</returns>
		/// <param name="notetype">Notetype.</param>

		public static string ToName(this NoteType notetype){

			string name = string.Empty;

			switch (notetype){
			case NoteType.Tap:
				name = "點擊";
				break;
			case NoteType.Hold:
				name = "長按";
				break;
			case NoteType.Swipe:
				name = "快速滑動";
				break;
			default:
			case NoteType.Null:
				name = "空";
				break;
			}

			return name;
		}
	}

	[Serializable]
	public class NotePrefabsField{
		public GameObject NoteTapPrefab;
		public GameObject NoteHoldPrefab;
		public GameObject NoteSwipePrefab;
	}

}