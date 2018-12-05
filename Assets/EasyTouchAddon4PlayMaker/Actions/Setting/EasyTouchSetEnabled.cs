using UnityEngine;
using System.Collections;
using HedgehogTeam.EasyTouch;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategoryAttribute("EasyTouch Setting")]
	[Tooltip("Enables or disables EasyTouch.")]
	public class EasyTouchSetEnabled : FsmStateAction {

		public FsmBool enable;
		
		public override void Reset(){
			enable = null;
		}
		
		public override void OnEnter(){
			
			EasyTouch.SetEnabled( enable.Value);
			Finish();
		}
	}

}
