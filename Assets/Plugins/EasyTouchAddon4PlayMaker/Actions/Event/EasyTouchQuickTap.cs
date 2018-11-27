using UnityEngine;
using System.Collections;
using HedgehogTeam.EasyTouch;

namespace HutongGames.PlayMaker.Actions{
	
	[ActionCategory("EasyTouch")]
	[Tooltip("Catch action on gesture tap.")]
	public class EasyTouchQuickTap : EasyTouchQuickFSM {

		public enum ActionTriggering {Simple_Tap,Double_Tap};

		[Tooltip("The tap type.")]
		public ActionTriggering actionTriggering;

		private EasyTouchObjectProxy proxy;
		
		public override void OnEnter(){
			proxy = Owner.GetComponent<EasyTouchObjectProxy>();
		}

		#region PlayMaker Callback
		public override void OnUpdate (){
		
			if (proxy.currentGesture != null){
				
				currentGesture = proxy.currentGesture;

				if (!twoFingerGesture){
					if (currentGesture.type == EasyTouch.EvtType.On_DoubleTap && actionTriggering == ActionTriggering.Double_Tap){
						DoAction(currentGesture);
					}

					if (currentGesture.type == EasyTouch.EvtType.On_SimpleTap && actionTriggering== ActionTriggering.Simple_Tap){
						DoAction(currentGesture);
					}

				}
				else{

					if (currentGesture.type == EasyTouch.EvtType.On_DoubleTap2Fingers && actionTriggering== ActionTriggering.Double_Tap){
						DoAction(currentGesture);
					}

					if (currentGesture.type == EasyTouch.EvtType.On_SimpleTap2Fingers && actionTriggering== ActionTriggering.Simple_Tap){
						DoAction(currentGesture);
					}
				}
			}
		}
		#endregion

	}
}
