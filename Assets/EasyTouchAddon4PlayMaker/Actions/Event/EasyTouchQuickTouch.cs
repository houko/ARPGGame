using UnityEngine;
using System.Collections;
using HedgehogTeam.EasyTouch;

namespace HutongGames.PlayMaker.Actions{

	[ActionCategory("EasyTouch")]
	[Tooltip("Catch action on gesture touch.")]
	public class EasyTouchQuickTouch : EasyTouchQuickFSM {

		public enum ActionTriggering {Start,Down,Up};

		[Tooltip("The trigger stage.")]
		public ActionTriggering actionTriggering;

		public FsmEvent sendEventNotOverMe;

		private EasyTouchObjectProxy proxy;
		
		public override void OnEnter(){
			proxy = Owner.GetComponent<EasyTouchObjectProxy>();
		}

		#region PlayMaker Callback
		public override void OnUpdate (){

			if (proxy.currentGesture != null){

				currentGesture = proxy.currentGesture;

				if (!twoFingerGesture){

					// GetIndex at touch start
					if (currentGesture.type == EasyTouch.EvtType.On_TouchStart && fingerIndex == -1 && IsOverMe(currentGesture) ){
						fingerIndex = currentGesture.fingerIndex;
					}

					// start
					if (currentGesture.type == EasyTouch.EvtType.On_TouchStart && actionTriggering == ActionTriggering.Start){
						if (currentGesture.fingerIndex == fingerIndex || allowMultiTouch){
							DoAction( currentGesture);
						}
					}

					// Down
					if (currentGesture.type == EasyTouch.EvtType.On_TouchDown  && actionTriggering == ActionTriggering.Down){
						if (currentGesture.fingerIndex == fingerIndex || allowMultiTouch){

							DoAction( currentGesture);
						}
					}

					// Up
					if (currentGesture.type == EasyTouch.EvtType.On_TouchUp){
						if ( actionTriggering == ActionTriggering.Up){
							if (currentGesture.fingerIndex == fingerIndex || allowMultiTouch){
								if (IsOverMe( currentGesture)){
									if (currentGesture.fingerIndex == fingerIndex) fingerIndex =-1;
									Fsm.Event( sendEvent);
									Finish();
								}
								else{
									if (currentGesture.fingerIndex == fingerIndex) fingerIndex =-1;
									Fsm.Event( sendEventNotOverMe);
									Finish();
								}
								//DoAction( currentGesture);

							}
						}
						if (currentGesture.fingerIndex == fingerIndex) fingerIndex =-1;
					}
				}
				else{
					if (currentGesture.type == EasyTouch.EvtType.On_TouchStart2Fingers && actionTriggering == ActionTriggering.Start){
						DoAction( currentGesture);
					}
					
					if (currentGesture.type == EasyTouch.EvtType.On_TouchDown2Fingers && actionTriggering == ActionTriggering.Down){
						DoAction( currentGesture);
					}
					
					if (currentGesture.type == EasyTouch.EvtType.On_TouchUp2Fingers && actionTriggering == ActionTriggering.Up){
						DoAction( currentGesture);
					}
				}
			}
		}
		#endregion
	}
}
