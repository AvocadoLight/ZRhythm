using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BurningxEmpires.ZRhythm.Selector{

	using selector = GameMapSelectorManager;

	public class currentPointer : MonoBehaviour {
		
		public UITexture mask;
		private UITexture arrow;
		public UIButton playgame;

		[Range(100f,1280f)]
		public float range = 200;

		private float maxAlpha = 0.8f;
		private float minAlpha = 0.2f;

		public AnimationCurve curve = new AnimationCurve(new Keyframe(0,0.2f),new Keyframe(0,0.8f));

		GameMapElement currentMapFile;

		// Use this for initialization
		void Start () {
			arrow = playgame.GetComponent<UITexture> ();
			//var pb = playgame.gameObject.AddComponent<playButton>();
			//pb.manager = this;
		}

		// Update is called once per frame
		void Update () {
			var currentActive = selector.getActiveGameMapElement;
			if (currentActive != null) {				
				//this.transform.position = current.transform.position;

				if(transform.parent != currentActive.transform){
					this.transform.SetParent(currentActive.transform);
					currentMapFile = currentActive.GetComponent<GameMapElement>();
				}
				this.transform.localPosition = Vector3.zero;

				mask.alpha = 1 - Mathf.Abs (transform.position.x * (1f/transform.lossyScale.x)) / range;
				arrow.alpha = mask.alpha;
				//mask.alpha = Mathf.Clamp(mask.alpha,minAlpha,maxAlpha);
				mask.alpha = curve.Evaluate(mask.alpha);
			} else {
				mask.alpha = 0;
				arrow.alpha = 0;
			}

		}

		public void onClick () {
			if(selector.getCurrentGameMapElement == selector.getActiveGameMapElement) {
				print("Go to Game Scene");
				SceneManager.LoadScene(selector.getInstance.scene_Game_name);
			}
		}
	}

}