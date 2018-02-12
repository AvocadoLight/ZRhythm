using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BurningxEmpires.ZRhythm.Editor{

	using editor = GameMapEditorManager;

	[ExecuteInEditMode]
	public class CommandWheelModuleManager : BlockModuleManager {

		public static CommandWheelModuleManager getInstance{private set;get;}

		public float offset = 0;

		public float arm = 45;

		public int fontSize = 50;

		public int number {get{return callBoxes.Count;}}

		public Transform center;

		public GameObject background;

		public List<BlockElement_CallBox> callBoxes = new List<BlockElement_CallBox>();

		public bool apply = false;

		void OnEnable () {
			getInstance = this;	
		}

		void Start () {
			UIEventListener.Get(background).onClick += onBackgroundClick;
		}
		
		public void SetEnable () {
			background.SetActive(true);
		}

		public void SetDisable () {
			background.SetActive(false);
		}

		void onBackgroundClick (GameObject go) {
			background.SetActive(false);
		}

		#if UNITY_EDITOR

		void OnValidate () {
			for(int i = 0 ; i < callBoxes.Count;i++){
				if(callBoxes[i] == null)
					continue;
				callBoxes[i].index = i;
			}
		}

		#endif

	}
}