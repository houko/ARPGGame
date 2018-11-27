using UnityEngine;
using System.Collections;
using HedgehogTeam.EasyTouch;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategoryAttribute("EasyTouch Setting 2 fingers gesture")]
	[Tooltip("Enables or disables pinch gesture.")]
	public class EasyTouchSetEnablePinch : FsmStateAction {

		public FsmBool enable;
		
		public override void Reset(){
			enable = null;
		}
		
		public override void OnEnter(){
			
			EasyTouch.SetEnablePinch( enable.Value);
			Finish();
		}

	}
}
