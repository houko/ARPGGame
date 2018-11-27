using UnityEngine;
using System.Collections;
using HedgehogTeam.EasyTouch;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategoryAttribute("EasyTouch Setting Automatic Selection")]
	[Tooltip("Enables or disable auto-update picked object.")]
	public class EasyTouchSetAutoUpdatePickedObject : FsmStateAction {

		public FsmBool enable;
		
		public override void Reset(){
			enable = null;
		}
		
		public override void OnEnter(){
			EasyTouch.SetAutoUpdatePickedObject( enable.Value);
			Finish();
		}
	}
}