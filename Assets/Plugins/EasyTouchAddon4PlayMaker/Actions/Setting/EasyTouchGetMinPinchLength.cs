using UnityEngine;
using System.Collections;
using HedgehogTeam.EasyTouch;

namespace HutongGames.PlayMaker.Actions{
	
	[ActionCategory("EasyTouch Setting 2 fingers gesture")]
	[Tooltip("Return the min pinch detection lenght.")]
	public class EasyTouchGetMinPinchLength : FsmStateAction {

		[UIHint(UIHint.Variable)]
		public FsmFloat length;
		
		
		public override void Reset(){
			length = null;
		}
		
		public override void OnEnter(){
			
			length.Value = EasyTouch.GetMinPinchLength();
			Finish();
		}
	}
}
