using UnityEngine;
using System.Collections;
using HedgehogTeam.EasyTouch;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategoryAttribute("EasyTouch Setting UGUI compatibility")]
	[Tooltip("Enables or disables UGUI detection.")]
	public class EasyTouchSetEnableUIDetection : FsmStateAction {

		public FsmBool enable;
		
		public override void Reset(){
			enable = null;
		}
		
		public override void OnEnter(){
			EasyTouch.SetEnableUIDetection( enable.Value);
			Finish();
		}

	}
}
