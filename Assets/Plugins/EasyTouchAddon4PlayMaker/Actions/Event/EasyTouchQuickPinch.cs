using UnityEngine;
using System.Collections;
using HedgehogTeam.EasyTouch;

namespace HutongGames.PlayMaker.Actions{

	[ActionCategory("EasyTouch")]
	[Tooltip("Catch action on gesture pinch.")]
	public class EasyTouchQuickPinch : EasyTouchQuickFSM {

		public enum ActionTriggering {Pinch,PinchIn,PinchOut};

		[Tooltip("Gesture must over owner")]
		public bool onOwner;

		[Tooltip("The trigger stage.")]
		public ActionTriggering actionTriggering;

		private EasyTouchObjectProxy proxy;

		public override void OnEnter(){
			proxy = Owner.GetComponent<EasyTouchObjectProxy>();
		}

		public override void OnUpdate (){

			if (proxy.currentGesture != null){
				currentGesture = proxy.currentGesture;
				// Pinch
				if (currentGesture.type == EasyTouch.EvtType.On_PinchIn && actionTriggering == ActionTriggering.PinchIn){
					DoAction( currentGesture);
				}
				else if (currentGesture.type == EasyTouch.EvtType.On_PinchOut && actionTriggering == ActionTriggering.PinchOut){

					DoAction( currentGesture);
				}
				else if (currentGesture.type == EasyTouch.EvtType.On_Pinch && actionTriggering == ActionTriggering.Pinch){
					DoAction( currentGesture);
				}
			}
		}

		protected override void DoAction (Gesture gesture){
		
			if (onOwner){
				base.DoAction (gesture);
			}
			else{
				if ( realType == GameObjectType.UI){
					if (gesture.isOverGui ){
						Fsm.Event( sendEvent);
						Finish();
					}
				}
				else if ((!enablePickOverUI && gesture.pickedUIElement == null) || enablePickOverUI){
					Fsm.Event( sendEvent);
					Finish();
				}
			}
		}
	}
}
