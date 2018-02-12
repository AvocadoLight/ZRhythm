using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BurningxEmpires.ZRhythm.Editor{

	using editor = GameMapEditorManager;

	public class BlockElement_Recorder : BlockElement {

		private RecorderModuleManager manager{get{return RecorderModuleManager.getInstance;}}

		// Use this for initialization
		void Start () {
			
		}
		
		// Update is called once per frame
		void Update () {

			if(Input.GetMouseButtonDown(0) && editor.audioPlayer.isPlaying){
				var mouse = UICamera.lastEventPosition;
				var point = manager.getPanel.cachedTransform.InverseTransformPoint(manager.getPanel.anchorCamera.ScreenToWorldPoint(mouse));
				var note = editor.trackMapEditor.noteEditor.AddNote();
				note.position = (int)(editor.audioPlayer.getProgress.totalSeconds / editor.getTrackMap.header.SecondPer32Note);
				note.Xoffset = point.x / editor.getTrackMap.getScreenSize().x;
//				print(point);
			}

		}

	}

}