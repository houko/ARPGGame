using UnityEngine;
using System.Collections;
using HedgehogTeam.EasyTouch;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategoryAttribute("EasyTouch Setting 2 fingers gesture")]
	[Tooltip("Enables or disables twist gesture.")]
	public class EasyTouchSetEnableTwist : FsmStateAction {

			public FsmBool enable;
			
			public override void Reset(){
				enable = null;
			}
			
			public override void OnEnter(){
				
				EasyTouch.SetEnableTwist( enable.Value);
				Finish();
			}
	}
}