using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BurningxEmpires.ZRhythm.Editor{

	using editor = GameMapEditorManager;

	public class BlockElement_BrushSelector : BlockElement_ToolBox {

		public static BlockElement_BrushSelector getInstance{get; private set;}

		public UIPanel dragPanel;
		public UIScrollView scrollView;

		private UICenterOnChild centerOnChild;

		public GameObject brushElementPrefab;

		public float offset = 230f;

		public List<BrushTypePair> brushes = new List<BrushTypePair>();

		public NoteType currentBrush;

		void Awake () {
			getInstance = this;
		}

		public override void Start () {			

			centerOnChild = scrollView.GetComponent<UICenterOnChild> ();

			var tap = Instantiate(editor.getInstance.prefabs.NoteTapPrefab) as GameObject;
			var hold = Instantiate(editor.getInstance.prefabs.NoteHoldPrefab) as GameObject;
			var swipe = Instantiate(editor.getInstance.prefabs.NoteSwipePrefab) as GameObject;

			AddBrush(tap);
			AddBrush(hold);
			AddBrush(swipe);

			centerOnChild.onCenter += OnBrushCenter;
		
			centerOnChild.Recenter();

			base.Start ();
		}

		void AddBrush (GameObject note) {
			var brush = Instantiate(brushElementPrefab,scrollView.panel.cachedTransform);
			note.transform.SetParent(brush.transform);
			note.transform.position = Vector3.zero;
			note.transform.localScale = Vector3.one / 2f;
			brush.transform.localPosition = Vector3.right * brushes.Count * offset;
			brush.transform.localScale = Vector3.one;
			var boxCollider = note.GetComponent<BoxCollider>();
			if(boxCollider != null)
				Destroy(boxCollider);
			var pair = brush.AddComponent<BrushTypePair>();
			//pair.type = note.GetComponentInChildren<Note_Base>().type;
			brushes.Add(pair);
		}

		void OnBrushCenter (GameObject brush){
			var pair = brush.GetComponent<BrushTypePair> ();
			currentBrush = pair.type;
			if(currentBrush != NoteType.Null && editor.getCurrentNote != null)
				editor.getCurrentNote.type = currentBrush;
			//TODO:用一個虛擬的note來表示筆刷
			//若有選擇的note可執行add和insert
			//沒有選擇的note只能add
		}

		public override void onSmall () {
			base.onSmall ();

			scrollView.enabled = false;

			dragPanel.cachedTransform.localScale = Vector3.one * 0.5f;

		}

		public override void onRegular () {
			base.onRegular ();

			scrollView.enabled = true;

			dragPanel.cachedTransform.localScale = Vector3.one * 1f;
		}

		public override void SetDepth (int depth) {		
			base.SetDepth(depth);
			dragPanel.depth = depth + 1;
		}

		public override void SetSortingOrder (int sortingOrder) {	
			base.SetSortingOrder(sortingOrder);
			dragPanel.sortingOrder = sortingOrder;
		}

		public void onPreviousBrush () {
			int id = centerOnChild.centeredObject.transform.GetSiblingIndex ();
			//Debug.Log("id:" + id);
			if (id-- > 0) {
				centerOnChild.CenterOn (centerOnChild.transform.GetChild (id));
			} else {
				centerOnChild.CenterOn (centerOnChild.transform.GetChild (centerOnChild.transform.childCount - 1));
			}
		}

		public void onNextBrush () {
			int id = centerOnChild.centeredObject.transform.GetSiblingIndex ();
			//Debug.Log("id:" + id);
			if (id++ < centerOnChild.transform.childCount - 1) {
				centerOnChild.CenterOn (centerOnChild.transform.GetChild (id));
			} else {
				centerOnChild.CenterOn (centerOnChild.transform.GetChild (0));
			}
		}

		public class BrushTypePair:MonoBehaviour {

			private Note_Base m_note;

			private Note_Base note{
				get{
					if(m_note == null)
						m_note = GetComponentInChildren<Note_Base>();
					return m_note;
				}
			}

			public NoteType type{
				get{
					return note.type;
				}
			}
		}

	}
}