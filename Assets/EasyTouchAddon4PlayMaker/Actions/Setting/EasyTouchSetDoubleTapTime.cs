using UnityEngine;
using System.Collections;
using HedgehogTeam.EasyTouch;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategoryAttribute("EasyTouch Setting General gesture")]
	[Tooltip("Set the double tap time.")]
	public class EasyTouchSetDoubleTapTime : FsmStateAction {

		public FsmFloat time;
		
		public override void Reset(){
			time = null;
		}
		
		public override void OnEnter(){
			EasyTouch.SetDoubleTapTime( time.Value);
			Finish();
		}
	}
}
