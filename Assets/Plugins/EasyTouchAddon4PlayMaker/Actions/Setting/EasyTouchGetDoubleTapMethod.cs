using UnityEngine;
using System.Collections;
using HedgehogTeam.EasyTouch;

namespace HutongGames.PlayMaker.Actions{
	
	[ActionCategory("EasyTouch Setting General gesture")]
	[Tooltip("Get double tap method.")]
	public class EasyTouchGetDoubleTapMethod : FsmStateAction {

		[UIHint(UIHint.Variable)]
		[ObjectType(typeof(EasyTouch.DoubleTapDetection))]
		public FsmEnum method;
		
		public override void Reset(){
			method = null;
		}
		
		public override void OnEnter(){
			method.Value = EasyTouch.GetDoubleTapMethod( );
			Finish();
		}
	}

}
