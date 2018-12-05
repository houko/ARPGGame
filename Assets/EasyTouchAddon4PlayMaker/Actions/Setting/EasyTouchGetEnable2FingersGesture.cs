using UnityEngine;
using System.Collections;
using HedgehogTeam.EasyTouch;

namespace HutongGames.PlayMaker.Actions{
	
	[ActionCategory("EasyTouch Setting 2 fingers gesture")]
	[Tooltip("Return true if 2 fingers gesture is enabled.")]
	public class EasyTouchGetEnable2FingersGesture : FsmStateAction {

		[UIHint(UIHint.Variable)]
		public FsmBool enable;
		
		
		public override void Reset(){
			enable = null;
		}
		
		public override void OnEnter(){
			
			enable.Value = EasyTouch.GetEnable2FingersGesture();
			Finish();
		}
	}
}