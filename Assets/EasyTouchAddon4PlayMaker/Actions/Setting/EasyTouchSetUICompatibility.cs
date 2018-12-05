using UnityEngine;
using System.Collections;
using HedgehogTeam.EasyTouch;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategoryAttribute("EasyTouch Setting UGUI compatibility")]
	[Tooltip("Enables or disables Unity UI compatibility.")]
	public class EasyTouchSetUICompatibility : FsmStateAction {

		public FsmBool enable;
		
		public override void Reset(){
			enable = null;
		}
		
		public override void OnEnter(){
			EasyTouch.SetUICompatibily( enable.Value);
			Finish();
		}
	}

}
