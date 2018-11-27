using UnityEngine;
using System.Collections;
using HedgehogTeam.EasyTouch;

namespace HutongGames.PlayMaker.Actions{
	
	[ActionCategory("EasyTouch Gesture Info")]
	[Tooltip("Get the swipe tolerance.")]
	public class EasyTouchGetSwipeTolerance : FsmStateAction {

		[UIHint(UIHint.Variable)]
		public FsmFloat tolerance;
		
		
		public override void Reset(){
			tolerance = null;
		}
		
		public override void OnEnter(){
			
			tolerance.Value = EasyTouch.GetSwipeTolerance();
			Finish();
		}
	}
}
