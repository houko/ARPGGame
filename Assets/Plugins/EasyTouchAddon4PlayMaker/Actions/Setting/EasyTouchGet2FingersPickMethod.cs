using UnityEngine;
using System.Collections;
using HedgehogTeam.EasyTouch;

namespace HutongGames.PlayMaker.Actions{
	
	[ActionCategory("EasyTouch Setting 2 fingers gesture")]
	[Tooltip("Return the 2 fingers selectable method.")]
	public class EasyTouchGet2FingersPickMethod : FsmStateAction {

		[UIHint(UIHint.Variable)]
		[ObjectType(typeof(EasyTouch.TwoFingerPickMethod))]
		public FsmEnum method;
		
		public override void Reset(){
			method = null;
		}
		
		public override void OnEnter(){
			method.Value = EasyTouch.GetTwoFingerPickMethod( );
			Finish();
		}

	}
}
