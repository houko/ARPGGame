using UnityEngine;
using System.Collections;
using HedgehogTeam.EasyTouch;

namespace HutongGames.PlayMaker.Actions{
	
	[ActionCategory("EasyTouch Gesture Info")]
	[Tooltip("Number of touches.")]
	public class EasyTouchGetTouchCount : FsmStateAction {

		[UIHint(UIHint.Variable)]
		public FsmInt touchCount;

		public FsmBool everyFrame;

		public override void Reset(){
			touchCount = null;
		}

		public override void OnUpdate(){
	
			touchCount.Value = EasyTouch.GetTouchCount();

			if (!everyFrame.Value)
				Finish();
		}
	}
}