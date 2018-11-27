using UnityEngine;
using System.Collections;
using HedgehogTeam.EasyTouch;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategoryAttribute("EasyTouch Setting 2 fingers gesture")]
	[Tooltip("Set the 2 fingers selectable method.")]
	public class EasyTouchSet2FingersPickMethod : FsmStateAction {

		public EasyTouch.TwoFingerPickMethod method;
		
		public override void OnEnter(){
			EasyTouch.SetTwoFingerPickMethod( method );
			Finish();
		}
	}
}
