using UnityEngine;
using System.Collections;
using HedgehogTeam.EasyTouch;

namespace HutongGames.PlayMaker.Actions{
	
	[ActionCategory("EasyTouch Setting")]
	[Tooltip("Return true is EasyTouch is enabled.")]
	public class EasyTouchGetEnabled : FsmStateAction {

		[UIHint(UIHint.Variable)]
		public FsmBool enabled;


		public override void Reset(){
			enabled = null;
		}

		public override void OnEnter(){

			enabled.Value = EasyTouch.GetEnabled();
			Finish();
		}

	}

}
