using UnityEngine;
using System.Collections;
using HedgehogTeam.EasyTouch;


namespace HutongGames.PlayMaker.Actions
{
	[ActionCategoryAttribute("EasyTouch Setting UGUI compatibility")]
	[Tooltip("Return true is Unity UI compatibility is enabled.")]
	public class EasyTouchGetUICompatibility : FsmStateAction {

		[UIHint(UIHint.Variable)]
		public FsmBool enabled;
		
		
		public override void Reset(){
			enabled = null;
		}
		
		public override void OnEnter(){
			
			enabled.Value = EasyTouch.GetUIComptability();
			Finish();
		}
	}

}
