using UnityEngine;
using System.Collections;
using HedgehogTeam.EasyTouch;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategoryAttribute("EasyTouch Setting UGUI compatibility")]
	[Tooltip("Return true is Unity UI detection is enabled.")]
	public class EasyTouchGetEnableUIDetection : FsmStateAction {

		[UIHint(UIHint.Variable)]
		public FsmBool enabled;

		public override void Reset(){
			enabled = null;
		}
		
		public override void OnEnter(){
			
			enabled.Value = EasyTouch.GetEnableUIDetection();
			Finish();
		}
	}
}
