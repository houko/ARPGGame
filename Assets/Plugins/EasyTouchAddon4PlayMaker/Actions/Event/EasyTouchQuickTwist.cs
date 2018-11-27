using UnityEngine;
using System.Collections;
using HedgehogTeam.EasyTouch;

namespace HutongGames.PlayMaker.Actions{

	[ActionCategory("EasyTouch")]
	[Tooltip("Catch action on gesture twist.")]
	public class EasyTouchQuickTwist : EasyTouchQuickFSM {

		public enum ActionTriggering {Twist, Clockwise, Counterclockwise};
		
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

				if (currentGesture.type == EasyTouch.EvtType.On_Twist && actionTriggering == ActionTriggering.Clockwise && IsRightRotation(currentGesture)){
					DoAction( currentGesture);
				}
				else if (currentGesture.type == EasyTouch.EvtType.On_Twist && actionTriggering == ActionTriggering.Counterclockwise && IsRightRotation(currentGesture)){
					DoAction( currentGesture);
				}
				else if (currentGesture.type == EasyTouch.EvtType.On_Twist && actionTriggering == ActionTriggering.Twist && IsRightRotation(currentGesture)){
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

		bool IsRightRotation(Gesture gesture){
			
			switch (actionTriggering){
			case ActionTriggering.Twist:
				return true;				
			case ActionTriggering.Clockwise:
				if (gesture.twistAngle<0){
					return true;
				}
				break;
			case ActionTriggering.Counterclockwise:
				if (gesture.twistAngle>0){
					return true;
				}
				break;
			}
			
			return false;
		}
	}
}
