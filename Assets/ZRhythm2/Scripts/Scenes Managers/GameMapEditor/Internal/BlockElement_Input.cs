using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BurningxEmpires.ZRhythm.Editor{

	public class BlockElement_Input : BlockElement {

		public string defaultContent = "BlockElement_Input";

		public string multiple_language_key;

		public UILabel banner;

		public UIInput input;

		void Start () {			
			EventDelegate.Add( input.onChange , onContentChange );
			//OnLanguageChange () , or fire LanguageUtility once in start
		}

		public virtual void onContentChange () {
			
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
				Color inactiveColor = input.label.color;
				banner.text = defaultContent;
				input.label.text = defaultContent;
				//input.defaultText = defaultContent;
				input.label.color = inactiveColor;
			}
		}

		#endif
	}

}