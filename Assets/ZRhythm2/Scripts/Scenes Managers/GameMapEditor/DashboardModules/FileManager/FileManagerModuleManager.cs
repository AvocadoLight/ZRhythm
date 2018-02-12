using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BurningxEmpires.ZRhythm.Editor{

	using editor = GameMapEditorManager;
	public class FileManagerModuleManager : MonoBehaviour {

		public static FileManagerModuleManager getInstance{private set;get;}

		public BlockElement_BackgroundImagePath imagePath;

		public BlockElement_BackgroundAudioPath audioPath;

		public BlockElement_TrackMapPath trackMapPath;

		public BlockElement_ExportDialog exportDialog;

		public string cacheFilePath;

		void Awake () {
			getInstance = this;
		}

	}
}