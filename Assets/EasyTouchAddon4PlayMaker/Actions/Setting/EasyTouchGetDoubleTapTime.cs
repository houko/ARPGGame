using UnityEngine;
using System.Collections;
using HedgehogTeam.EasyTouch;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategoryAttribute("EasyTouch Setting General gesture")]
	[Tooltip("Get the double tap time.")]
	public class EasyTouchGetDoubleTapTime : FsmStateAction {

		[UIHint(UIHint.Variable)]
		public FsmFloat time;
		
		
		public override void Reset(){
			time = null;
		}
		
		public override void OnEnter(){
			
			time.Value = EasyTouch.GetDoubleTapTime();
			Finish();
		}
	}
}
