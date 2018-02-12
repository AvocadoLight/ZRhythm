using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BurningxEmpires.ZRhythm.Editor{
	public class BlockElement_Button : BlockElement {

		public string defaultContent = "BlockElement_Button";

		public string multiple_language_key;

		public UILabel banner;

		public UIButton button;

		void Start () {			
			EventDelegate.Add( button.onClick , onClick );
			//OnLanguageChange () , or fire LanguageUtility once in start
		}

		public void SetActive (bool value) {
			gameObject.SetActive(value);
		}

		public void SetEnable (bool value) {
			button.isEnabled = value;
		}

		public virtual void onClick () {

		}

		void OnLanguageChange () {
			//var content = LanguageUtility.GetContent(multiple_language_key);
			//更新多語言介面
			//banner.text = content;
			//input.defaultText = content;
		}

		#if UNITY_EDITOR

		void OnValidate (){
			if(!Application.isPlaying){				
				banner.text = defaultContent;
			}
		}

		#endif

	}
}