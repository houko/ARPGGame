using UnityEngine;
using System.Collections;
using HedgehogTeam.EasyTouch;

namespace HutongGames.PlayMaker.Actions{
	
	[ActionCategory("EasyTouch Setting 2 fingers gesture")]
	[Tooltip("Return the min twist detection angle.")]
	public class EasyTouchGetMinTwistAngle : FsmStateAction {

		[UIHint(UIHint.Variable)]
		public FsmFloat angle;
		
		
		public override void Reset(){
			angle = null;
		}
		
		public override void OnEnter(){
			
			angle.Value = EasyTouch.GetMinTwistAngle();
			Finish();
		}

	}
}
