using UnityEngine;
using System.Collections;
using HedgehogTeam.EasyTouch;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategoryAttribute("EasyTouch Setting 2 fingers gesture")]
	[Tooltip("Enables or disables 2 fingers gestures.")]
	public class EasyTouchSetEnable2FingerGesture : FsmStateAction {

		public FsmBool enable;
		
		public override void Reset(){
			enable = null;
		}
		
		public override void OnEnter(){
			EasyTouch.SetEnable2FingersGesture( enable.Value);
			Finish();
		}
	}
}
