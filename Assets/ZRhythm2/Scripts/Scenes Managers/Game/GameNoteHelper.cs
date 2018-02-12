using UnityEngine;
using System.Collections;

namespace BurningxEmpires.ZRhythm.Game{
	
	public class GameNoteHelper : MonoBehaviour {

		public GameManager manager{get{return GameManager.getInstance;}}

		private Note_Base m_note;

		public Note_Base note{
			get{
				if(m_note == null)
					m_note = GetComponentInChildren<Note_Base> ();
				return m_note;
			}
		}

		private Note m_cachedNote = null;

		public Note cachedNote{
			get{				
				if(m_cachedNote == null)
					m_cachedNote = manager.currentTrackMap.GetNote(note.noteId);
				return m_cachedNote;
			}
		}

		public float time{
			get{
				return cachedNote.position * manager.currentTrackMap.header.SecondPer32Note;
			}
		}

		private Transform m_Transform;

		public Transform getTransform {
			get{
				if(m_Transform == null)
					m_Transform = transform;
				return m_Transform;
			}
		}

		private UIWidget m_widget;

		public UIWidget getWidget{
			get{
				if(m_widget==null)
					m_widget = GetComponentInChildren<UIWidget>();
				return m_widget;
			}
		}

		public float page = -1;

		protected float timeProgress{
			get{
				return manager.audioPlayer.getProgress.totalSeconds;
			}
		}

		public void Awake() {
			m_cachedNote = null;
		}

		public void SetAlpha (float alpha) {
			getWidget.alpha = alpha;
		}

		public void SetDepth (int depth) {
			getWidget.depth = depth;
		}

		public virtual void onUpdate () {			

			note.update(timeProgress,manager.currentTrackMap.header.SecondPer32Note);

			if(timeProgress >= time + manager.config.range_Bad){						
				onMiss ();
			}

		}

		public virtual void onExcellent () {
			manager.scorer.AddExcellent(1);
			manager.effect.NoteExcellent(getTransform.localPosition);
			Destroy(this.gameObject);
			onDestroy ();
		}

		public virtual void onGood () {
			manager.scorer.AddGood(1);
			manager.effect.NoteGood(getTransform.localPosition);
			Destroy(this.gameObject);
			onDestroy ();
		}

		public virtual void onBad () {
			manager.scorer.AddBad(1);
			manager.effect.NoteBad(getTransform.localPosition);
			Destroy(this.gameObject);
			onDestroy ();
		}

		public virtual void onMiss () {			
			manager.scorer.AddMiss(1);
			manager.effect.NoteMiss(getTransform.localPosition);
			Destroy(this.gameObject);
			onDestroy ();
		}

		public virtual void onDestroy () {
			manager.helpers.Remove(this);
			manager.effect.NoteFade(getTransform.localPosition);
		}

	}

}