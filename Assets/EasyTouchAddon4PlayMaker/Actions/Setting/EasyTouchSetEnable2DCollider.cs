using UnityEngine;
using System.Collections;
using HedgehogTeam.EasyTouch;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategoryAttribute("EasyTouch Setting Automatic Selection")]
	[Tooltip("Enables or disables 2D collider detection.")]
	public class EasyTouchSetEnable2DCollider : FsmStateAction {

		public FsmBool enable;
		
		public override void Reset(){
			enable = null;
		}
		
		public override void OnEnter(){
			EasyTouch.SetEnable2DCollider( enable.Value);
			Finish();
		}
	}
}