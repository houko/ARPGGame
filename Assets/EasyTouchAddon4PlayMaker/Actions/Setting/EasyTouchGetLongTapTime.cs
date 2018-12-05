using UnityEngine;
using System.Collections;
using HedgehogTeam.EasyTouch;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategoryAttribute("EasyTouch Setting General gesture")]
	[Tooltip("Return true if 2D collider detection is enabled.")]
	public class EasyTouchGetLongTapTime : FsmStateAction {

		[UIHint(UIHint.Variable)]
		public FsmFloat time;
		
		
		public override void Reset(){
			time = null;
		}
		
		public override void OnEnter(){
			
			time.Value = EasyTouch.GetlongTapTime();
			Finish();
		}
	}
}