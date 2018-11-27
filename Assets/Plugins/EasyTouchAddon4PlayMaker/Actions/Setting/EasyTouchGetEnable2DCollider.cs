using UnityEngine;
using System.Collections;
using HedgehogTeam.EasyTouch;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategoryAttribute("EasyTouch Setting Automatic Selection")]
	[Tooltip("Return true if 2D collider detection is enabled.")]
	public class EasyTouchGetEnable2DCollider : FsmStateAction {

		[UIHint(UIHint.Variable)]
		public FsmBool enabled;
		
		
		public override void Reset(){
			enabled = null;
		}
		
		public override void OnEnter(){
			
			enabled.Value = EasyTouch.GetEnable2DCollider();
			Finish();
		}
	}
}
