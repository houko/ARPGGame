using UnityEngine;
using System.Collections;
using HedgehogTeam.EasyTouch;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategoryAttribute("EasyTouch Setting General gesture")]
	[Tooltip("Set the long tap time.")]
	public class EasyTouchSetLongTapTime : FsmStateAction {

		public FsmFloat time;
		
		public override void Reset(){
			time = null;
		}
		
		public override void OnEnter(){
			EasyTouch.SetLongTapTime( time.Value);
			Finish();
		}
	}
}
