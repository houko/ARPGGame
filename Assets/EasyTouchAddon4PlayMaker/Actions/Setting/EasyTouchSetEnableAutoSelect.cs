using UnityEngine;
using System.Collections;
using HedgehogTeam.EasyTouch;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategoryAttribute("EasyTouch Setting Automatic Selection")]
	[Tooltip("Enables or disables auto selection.")]
	public class EasyTouchSetEnableAutoSelect : FsmStateAction {

		public FsmBool enable;
		
		public override void Reset(){
			enable = null;
		}

		public override void OnEnter(){
			EasyTouch.SetEnableAutoSelect( enable.Value);
			Finish();
		}
	}
}