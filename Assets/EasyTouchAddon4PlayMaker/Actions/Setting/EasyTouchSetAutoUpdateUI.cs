using UnityEngine;
using System.Collections;
using HedgehogTeam.EasyTouch;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategoryAttribute("EasyTouch Setting UGUI compatibility")]
	[Tooltip("Enables or disables auto-update picked UI element.")]
	public class EasyTouchSetAutoUpdateUI : FsmStateAction {

		public FsmBool enable;
		
		public override void Reset(){
			enable = null;
		}
		
		public override void OnEnter(){
			EasyTouch.SetAutoUpdateUI( enable.Value);
			Finish();
		}
	}
}