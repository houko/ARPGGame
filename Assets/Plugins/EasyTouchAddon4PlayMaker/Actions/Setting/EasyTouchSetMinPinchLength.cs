using UnityEngine;
using System.Collections;
using HedgehogTeam.EasyTouch;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategoryAttribute("EasyTouch Setting 2 fingers gesture")]
	[Tooltip("Set the min pinch detection length.")]
	public class EasyTouchSetMinPinchLength : FsmStateAction {

		public FsmFloat length;
		
		public override void Reset(){
			length = null;
		}
		
		public override void OnEnter(){
			EasyTouch.SetMinPinchLength( length.Value);
			Finish();
		}
	}
}
