using UnityEngine;
using System.Collections;
using HedgehogTeam.EasyTouch;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategoryAttribute("EasyTouch Setting Automatic Selection")]
	[Tooltip("Return true if auto-selection is enabled.")]
	public class EasyTouchGetEnableAutoSelect : FsmStateAction {

		[UIHint(UIHint.Variable)]
		public FsmBool enabled;
		
		
		public override void Reset(){
			enabled = null;
		}
		
		public override void OnEnter(){
			
			enabled.Value = EasyTouch.GetEnableAutoSelect();
			Finish();
		}
	}
}

