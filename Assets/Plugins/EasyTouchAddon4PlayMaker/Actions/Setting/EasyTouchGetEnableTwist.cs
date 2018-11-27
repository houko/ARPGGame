using UnityEngine;
using System.Collections;
using HedgehogTeam.EasyTouch;

namespace HutongGames.PlayMaker.Actions{
	
	[ActionCategory("EasyTouch Setting 2 fingers gesture")]
	[Tooltip("Return true if twist gesture is enabled")]
	public class EasyTouchGetEnableTwist : FsmStateAction {

		[UIHint(UIHint.Variable)]
		public FsmBool enabled;
		
		
		public override void Reset(){
			enabled = null;
		}
		
		public override void OnEnter(){
			
			enabled.Value = EasyTouch.GetEnableTwist();
			Finish();
		}
	}
}
