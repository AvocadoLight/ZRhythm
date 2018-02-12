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
			
	public static class GameMapUtility {

//		public static GameMap GenerateEmptyGameMap(){			
//			return new GameMap ();
//		}

		public static GameMap GenerateGameMap(string gameMapPath){
			GameMap gameMap = new GameMap();
			gameMap.filePath = gameMapPath;

			string audioName = string.Empty;
			string backgroundName = string.Empty;

			using (ZipFile zip = ZipFile.Read (gameMapPath)) {  
				zip.AlternateEncodingUsage = ZipOption.Always;
				zip.AlternateEncoding = System.Text.Encoding.UTF8;
				foreach (ZipEntry file in zip) {
					string extension = Path.GetExtension (file.FileName).ToLower ();
					if(string.Compare(extension,TrackMap.extension) == 0){
						using(var memoryStream = new MemoryStream()){
							file.Extract(memoryStream);
							memoryStream.Seek(0,SeekOrigin.Begin);
							using(var stream = new StreamReader(memoryStream)){
								string jsonData = stream.ReadToEnd();									
								gameMap.trackMap = TrackMap.FromJson(jsonData);
								gameMap.trackMapFileName = file.FileName;
							}
						}
					}else if(ConfigUtility.isSupportedAudioType(extension)){
						audioName = file.FileName;
					}else if(ConfigUtility.isSupportedTextureType(extension)){
						backgroundName = file.FileName;
					}
				}
			}

			if(!gameMap.hasTrackMapFile){
				return gameMap;
			}

			if(string.Compare (
				gameMap.audioFileName
				, audioName) == 0){
				gameMap._hasAudioFile = true;
			}

			if(string.Compare (
				gameMap.backgroundFileName
				, backgroundName) == 0){
				gameMap._hasBackgroundFile = true;
			}

			return gameMap;
		}

	}

}