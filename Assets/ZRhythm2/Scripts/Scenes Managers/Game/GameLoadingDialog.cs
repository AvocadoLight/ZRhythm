using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BurningxEmpires.ZRhythm.Game{
	public class GameLoadingDialog : MonoBehaviour {

		public GameManager manager{
			get{
				return GameManager.getInstance;	
			}
		}

		public static GameLoadingDialog getInstance{get;private set;}

		public UILabel text_Loading;

		public UITexture image_Loading;

		private int loadingCount = 5;

		private float cd = 0.2f;

		private float timer = 0f;

		void Awake () {
			getInstance = this;
		}

		void Start () {
			//Close();
		}

		void Update () {
			if(timer >= cd){
				timer = 0;
				loadingCount++;
				if(loadingCount > 5){
					loadingCount = 0;
				}
				text_Loading.text = "Loading";
				for(int i = 0 ; i < loadingCount; i++){
					text_Loading.text = string.Concat(text_Loading.text,".");
				}
			}
			timer += Time.deltaTime;
		}

		public void ReportProgress (float value) {
			image_Loading.fillAmount = value;
		}


		public void Close (){
			gameObject.SetActive(false);
		}

		public void Open (){
			gameObject.SetActive(true);
		}

	}
}