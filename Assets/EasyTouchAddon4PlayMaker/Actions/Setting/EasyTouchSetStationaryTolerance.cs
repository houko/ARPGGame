using UnityEngine;
using System.Collections;
using HedgehogTeam.EasyTouch;

namespace HutongGames.PlayMaker.Actions
{
	[ActionCategoryAttribute("EasyTouch Setting General gesture")]
	[Tooltip("Set the stationary tolerance.")]
	public class EasyTouchSetStationaryTolerance : FsmStateAction {

		public FsmFloat tolerance;
		
		public override void Reset(){
			tolerance = null;
		}
		
		public override void OnEnter(){
			EasyTouch.SetStationaryTolerance( tolerance.Value);
			Finish();
		}
	}
}
