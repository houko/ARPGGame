using UnityEngine;
using System.Collections;
using HedgehogTeam.EasyTouch;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategoryAttribute("EasyTouch Setting 2 fingers gesture")]
	[Tooltip("Set the min twist angle angle.")]
	public class EasyTouchSetMinTwistAngle : FsmStateAction {

		public FsmFloat angle;
		
		public override void Reset(){
			angle = null;
		}
		
		public override void OnEnter(){
			EasyTouch.SetMinTwistAngle( angle.Value);
			Finish();
		}
	}
}
