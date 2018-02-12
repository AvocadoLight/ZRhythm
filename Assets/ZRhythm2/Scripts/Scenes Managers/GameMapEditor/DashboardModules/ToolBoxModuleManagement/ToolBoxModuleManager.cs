using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BurningxEmpires.ZRhythm.Editor{

	[ExecuteInEditMode]
	public class ToolBoxModuleManager : BlockModuleManager {

		public static ToolBoxModuleManager getInstance{private set;get;}

		public int startDepth = 1;

		public int sortingOrder = 2;

		public int step = 2;

		public float border = 120;

		public List<BlockElement_ToolBox> toolBoxes = new List<BlockElement_ToolBox>();

		private BlockElement_ToolBox m_activeToolBox;

		public BlockElement_ToolBox getActiveToolBox{
			get{
				return m_activeToolBox;
			}
		}

		private int rec_startDepth = 1;

		private int rec_sortingOrder = 2;

		private int rec_step = 2;

		private int rec_toolBoxesCount = 0;

		void Awake () {
			getInstance = this;
		}

		void OnEnable () {
			getInstance = this;
		}

		void Start () {
			Apply () ;
		}
		
		// Update is called once per frame
		void Update () {
			if(isDirty()){
				Apply ();
			}
		}

		public bool isDirty () {
			return (sortingOrder != rec_sortingOrder) ||
				(step != rec_step) || 
				(toolBoxes.Count != rec_toolBoxesCount) ||
				(startDepth != rec_startDepth);
		}

		void Apply () {
			var boxes = toolBoxes;

			for (int i = 0 ; i < boxes.Count ; i++) {
				boxes[i].SetSortingOrder(sortingOrder);
				boxes[i].SetDepth (startDepth + i * step);
			}

			rec_sortingOrder = sortingOrder;
			rec_step = step;
			rec_toolBoxesCount = boxes.Count;
			rec_startDepth = startDepth;
		}

		public static void Add (BlockElement_ToolBox toolBox) {
			if(!getInstance.toolBoxes.Contains(toolBox))
				getInstance.toolBoxes.Add(toolBox);
		}

		public static void BringToFront (BlockElement_ToolBox toolBox) {

			var boxes = getInstance.toolBoxes;

//			var index = boxes.IndexOf(toolBox);
//
//			for (int i = index ; i > 1 ; i--) {
//				if(i-1 >=0 )
//					boxes[i] = boxes[i - 1];
//			}
//
//			boxes[0] = toolBox;

			boxes.Remove(toolBox);
			boxes.Add(toolBox);
			Sort();

		}

		public static void startDrag (BlockElement_ToolBox toolBox) {		

			getInstance.m_activeToolBox = toolBox;

			toolBox.SetSortingOrder(getInstance.sortingOrder + 1);
		}

		public static void endDrag (BlockElement_ToolBox toolBox) {

			getInstance.m_activeToolBox = null;

			Sort ();

		}

		public static void Sort() {

			var boxes = getInstance.toolBoxes;

			for (int i = 0 ; i < boxes.Count ; i ++) {
				boxes[i].SetSortingOrder(getInstance.sortingOrder);
				boxes[i].SetDepth (getInstance.startDepth + i * getInstance.step);
			}

		}

	}

}