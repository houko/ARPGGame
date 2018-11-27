using UnityEngine;
using System.Collections;
using HedgehogTeam.EasyTouch;

namespace HutongGames.PlayMaker.Actions{

	[ActionCategory("EasyTouch")]
	[Tooltip("Catch action on gesture long tap.")]
	public class EasyTouchQuickLongTap : EasyTouchQuickFSM {

		public enum ActionTriggering {Start,InProgress,End};

		[Tooltip("The trigger stage.")]
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
					
					if (currentGesture.type == EasyTouch.EvtType.On_TouchStart && fingerIndex == -1){
						fingerIndex = currentGesture.fingerIndex;
					}
					
					if (currentGesture.type == EasyTouch.EvtType.On_LongTapStart && actionTriggering == ActionTriggering.Start){
						if (currentGesture.fingerIndex == fingerIndex || allowMultiTouch){
							DoAction( currentGesture);
						}
					}
					
					if (currentGesture.type == EasyTouch.EvtType.On_LongTap  && actionTriggering == ActionTriggering.InProgress){
						if (currentGesture.fingerIndex == fingerIndex || allowMultiTouch){
							DoAction( currentGesture);
						}
					}
					
					if (currentGesture.type == EasyTouch.EvtType.On_LongTapEnd && actionTriggering == ActionTriggering.End){
						if (currentGesture.fingerIndex == fingerIndex || allowMultiTouch){
							DoAction( currentGesture);
							fingerIndex =-1;
						}
					}
				}
				else{
					if (currentGesture.type == EasyTouch.EvtType.On_LongTapStart2Fingers && actionTriggering == ActionTriggering.Start){
						DoAction( currentGesture);
					}
					
					if (currentGesture.type == EasyTouch.EvtType.On_LongTap2Fingers && actionTriggering == ActionTriggering.InProgress){
						DoAction( currentGesture);
					}
					
					if (currentGesture.type == EasyTouch.EvtType.On_LongTapEnd2Fingers && actionTriggering == ActionTriggering.End){
						DoAction( currentGesture);
					}
				}

			}
		}
		#endregion

	}
}
