using UnityEngine;
using System.Collections;
using HedgehogTeam.EasyTouch;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategoryAttribute("EasyTouch Setting General gesture")]
	[Tooltip("Set the gesture priority.")]
	public class EasyTouchSetGesturePriority : FsmStateAction {

		public EasyTouch.GesturePriority priority;

		public override void OnEnter(){
			EasyTouch.SetGesturePriority( priority );
			Finish();
		}
	}
}
