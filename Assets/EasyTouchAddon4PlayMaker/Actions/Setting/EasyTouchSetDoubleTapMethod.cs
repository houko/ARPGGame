using UnityEngine;
using System.Collections;
using HedgehogTeam.EasyTouch;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategoryAttribute("EasyTouch Setting General gesture")]
	[Tooltip("Set the double tap method.")]
	public class EasyTouchSetDoubleTapMethod : FsmStateAction {

		public EasyTouch.DoubleTapDetection method;
		
		public override void OnEnter(){
			EasyTouch.SetDoubleTapMethod( method );
			Finish();
		}
	}
}
