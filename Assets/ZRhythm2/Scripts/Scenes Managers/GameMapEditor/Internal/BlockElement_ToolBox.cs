using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BurningxEmpires.ZRhythm.Editor{
	
	public class BlockElement_ToolBox : BlockElement {

		public ToolBoxModuleManager toolBoxManager{
			get{
				return ToolBoxModuleManager.getInstance;
			}
		}

		private UIPanel _panel;

		protected UIPanel m_Panel{
			get{
				if(_panel == null)
					_panel = GetComponent<UIPanel> ();
				return _panel;
			}
		}

		private BoxCollider _box;

		private BoxCollider m_BoxCollider{
			get{
				if(_box == null)
					_box = GetComponent<BoxCollider> ();
				return _box;
			}
		}

		protected UIRoot root{get{return m_Panel.root;}}

		public string defaultContent = "BlockElement_ToolBox";

		public string multiple_language_key;

		public UILabel banner;

		public UITexture led;

		private Vector2 led_border;//left,right

		private float originWidth = 100;

		public float minHeigth = 40;

		public float originHeight = 100;

		public float scale{get{return ConfigUtility.toolBoxSize;}set{ConfigUtility.toolBoxSize = value;}}

		void Awake () {
			
		}

		public virtual void Start () {
			MultipleLanguageManager.getInstance.Value(multiple_language_key,"CHN",defaultContent);
			//OnLanguageChange () , or fire LanguageUtility once in start
			ToolBoxModuleManager.Add(this);
			led_border = new Vector2(led.leftAnchor.absolute,led.rightAnchor.absolute);
			originWidth = m_Panel.baseClipRegion.z;
			CloseDialog();
		}

		public virtual void Update () {
			//name = led.leftAnchor.relative.ToString();

			var half_screen = (float)root.manualWidth / 2f;

			var abs_pos = Mathf.Abs(cachedTransform.localPosition.x);

			var border_distance = half_screen - abs_pos;

			var near_side = abs_pos - (m_Panel.width/2f);

			var m_border = half_screen - near_side;

			if( m_border < toolBoxManager.border*1.5f){
				//small
				onSmall();

				//if(side < toolBoxManager.border){
				if( m_border < toolBoxManager.border){
					cachedTransform.localPosition = new Vector3(
						(half_screen + m_Panel.width/2f - toolBoxManager.border) * (cachedTransform.localPosition.x > 0 ? 1 : -1),
						cachedTransform.localPosition.y,
						cachedTransform.localPosition.z
					);
				}

			} else {
				//regular
				onRegular();
			}
			m_BoxCollider.size = new Vector3(m_Panel.baseClipRegion.z,m_Panel.baseClipRegion.w);
			m_BoxCollider.center = new Vector3(m_Panel.baseClipRegion.x,m_Panel.baseClipRegion.y);
		}

		void OnLanguageChange () {			
			//var content = LanguageUtility.GetContent(multiple_language_key);
			//更新多語言介面
			//banner.text = content;
			//input.defaultText = content;
		}

		public virtual void onSmall () {
			m_Panel.baseClipRegion = new Vector4 (
				m_Panel.baseClipRegion.x,
				m_Panel.baseClipRegion.y,
				m_Panel.baseClipRegion.z,
				minHeigth
			);

			//right and left
			if(cachedTransform.localPosition.x > 0){
				led.leftAnchor.relative = 0;
				led.leftAnchor.absolute = (int)led_border.x;
				led.rightAnchor.relative = 0;
				led.rightAnchor.absolute = (int)led_border.y;
				banner.pivot = UIWidget.Pivot.Left;
				banner.cachedTransform.localPosition = new Vector3(
					led.width/2,banner.cachedTransform.localPosition.y);
			}else{
				led.rightAnchor.relative = 1;
				led.rightAnchor.absolute = (int)-led_border.x;
				led.leftAnchor.relative = 1;
				led.leftAnchor.absolute = (int)-led_border.y;
				banner.pivot = UIWidget.Pivot.Right;
				banner.cachedTransform.localPosition = new Vector3(
					-led.width/2,banner.cachedTransform.localPosition.y);
			}
		}

		public virtual void onRegular () {
			m_Panel.baseClipRegion = new Vector4 (
				m_Panel.baseClipRegion.x,
				m_Panel.baseClipRegion.y,
				originWidth * scale,
				originHeight * scale
			);
		}

		public virtual void SetDepth (int depth) {			
			m_Panel.depth = depth;
		}

		public virtual void SetSortingOrder (int sortingOrder) {			
			m_Panel.sortingOrder = sortingOrder;
		}

		public virtual void OnPress (bool ispress) {
			if(ispress){
				ToolBoxModuleManager.BringToFront(this);
				ToolBoxModuleManager.startDrag(this);
			}else{
				ToolBoxModuleManager.endDrag(this);
			}
		}

		public void OpenDialog (){
			cachedTransform.localPosition = Vector3.zero;
			gameObject.SetActive(true);
		}

		public void CloseDialog (){
			gameObject.SetActive(false);
		}

		#if UNITY_EDITOR

		protected void OnValidate (){
			if(!Application.isPlaying){				
				banner.text = defaultContent;
				originHeight = m_Panel.height;
				m_BoxCollider.size = new Vector3(m_Panel.baseClipRegion.z,m_Panel.baseClipRegion.w);
				m_BoxCollider.center = new Vector3(m_Panel.baseClipRegion.x,m_Panel.baseClipRegion.y);
			}
		}

		#endif

	}

}