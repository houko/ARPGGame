using UnityEngine;
using System.Collections;
using HedgehogTeam.EasyTouch;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategoryAttribute("EasyTouch Setting UGUI compatibility")]
	[Tooltip("Enables or disables auto-update picked UI element.")]
	public class EasyTouchGetAutoUpdateUI : FsmStateAction {

		[UIHint(UIHint.Variable)]
		public FsmBool enabled;
		
		
		public override void Reset(){
			enabled = null;
		}
		
		public override void OnEnter(){
			
			enabled.Value = EasyTouch.GetAutoUpdateUI();
			Finish();
		}

	}
}
