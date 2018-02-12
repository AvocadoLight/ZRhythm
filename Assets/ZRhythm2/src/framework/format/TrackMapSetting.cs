//----------------------------------------------
//            ZRhythm: Z-Rhythm Framework kit
// Copyright © 2015-2017 BurningxEmpires 火雨連城
//----------------------------------------------

using System;
using UnityEngine;
using System.Collections;

namespace BurningxEmpires.ZRhythm{

	[Serializable]
	public class TrackMapSetting{
		/// <summary>
		/// Beat Bar 的起始位置(-1~1, 0為正中間)
		/// </summary>
		public float PageStart = 0;
		/// <summary>
		/// 遊戲視窗的大小倍率(0.5x~1.5x)
		/// </summary>
		public float PageSize_x = 1;
		public float PageSize_y = 1;
		/// <summary>
		/// 音符單位距離的倍率(0.5x~5x)
		/// </summary>
		public float PageScale = 1;
		/// <summary>
		/// Gets the defluat.
		/// </summary>
		/// <value>The defluat.</value>
		public static TrackMapSetting Defluat{
			get{
				return new TrackMapSetting{
					PageStart = 0f,
					PageSize_x = 1f,
					PageSize_y = 1f,
					PageScale = 1f,
				};
			}
		}
	}

}