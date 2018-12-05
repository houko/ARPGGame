using UnityEngine;
using System.Collections;
using HedgehogTeam.EasyTouch;

namespace HutongGames.PlayMaker.Actions{
	
	[ActionCategory("EasyTouch Setting General gesture")]
	[Tooltip("Return the gesture priority.")]
	public class EasyTouchGetGesturePriority : FsmStateAction {

		[UIHint(UIHint.Variable)]
		[ObjectType(typeof(EasyTouch.GesturePriority))]
		public FsmEnum priority;

		public override void Reset(){
			priority = null;
		}
		
		public override void OnEnter(){
			priority.Value = EasyTouch.GetGesturePriority( );
			Finish();
		}

	}
}