using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BurningxEmpires.ZRhythm.Tools{
	[AddComponentMenu("NGUI/Tween/Tween Translate")]
	public class TweenTranslate : UITweener {
		public Vector3 from;
		public Vector3 to;

		bool mCached = false;
		Transform mTrans;
		UIRect mRect;

		public Transform cachedTransform { get { if (mTrans == null) mTrans = transform; return mTrans; } }
		Material mMat;
		SpriteRenderer mSr;

		[System.Obsolete("Use 'value' instead")]
		public Vector3 position { get { return this.value; } set { this.value = value; } }

		/// <summary>
		/// Tween's current value.
		/// </summary>
		[HideInInspector]
		public Vector3 value;

		/// <summary>
		/// Tween the value.
		/// </summary>

		protected override void OnUpdate (float factor, bool isFinished) {
			value = Vector3.Lerp(from, to, factor); 
			cachedTransform.Translate(value*Time.deltaTime);
		}

		/// <summary>
		/// Start the tweening operation.
		/// </summary>

		static public TweenTranslate Begin (GameObject go, float duration, Vector3 target)
		{
			TweenTranslate comp = UITweener.Begin<TweenTranslate>(go, duration);
			comp.from = comp.value;
			comp.to = target;

			if (duration <= 0f)
			{
				comp.Sample(1f, true);
				comp.enabled = false;
			}
			return comp;
		}

		public override void SetStartToCurrentValue () { from = value; }
		public override void SetEndToCurrentValue () { to = value; }


	}
}