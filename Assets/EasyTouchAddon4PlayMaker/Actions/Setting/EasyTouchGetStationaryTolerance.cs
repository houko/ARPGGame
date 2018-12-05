using UnityEngine;
using System.Collections;
using HedgehogTeam.EasyTouch;

namespace HutongGames.PlayMaker.Actions{
	
	[ActionCategory("EasyTouch Gesture Info")]
	[Tooltip("Get the stationary tolerance.")]
	public class EasyTouchGetStationaryTolerance : FsmStateAction {

		[UIHint(UIHint.Variable)]
		public FsmFloat tolerance;
		
		
		public override void Reset(){
			tolerance = null;
		}
		
		public override void OnEnter(){
			
			tolerance.Value = EasyTouch.GetStationaryTolerance();
			Finish();
		}
	}
}
