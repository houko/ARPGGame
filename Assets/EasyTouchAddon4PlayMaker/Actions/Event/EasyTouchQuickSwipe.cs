using UnityEngine;
using System.Collections;
using HedgehogTeam.EasyTouch;

namespace HutongGames.PlayMaker.Actions{
	
	[ActionCategory("EasyTouch")]
	[Tooltip("Catch action on gesture swipe.")]
	public class EasyTouchQuickSwipe : EasyTouchQuickFSM {

		public enum ActionTriggering {Start,InProgress,End};

		public enum SwipeDirection {Vertical, Horizontal, DiagonalRight,DiagonalLeft,Up,UpRight, Right,DownRight,Down,DownLeft, Left,UpLeft,All};

		[Tooltip("The trigger stage.")]
		public ActionTriggering actionTriggering;

		[Tooltip("Swipe direction.")]
		public SwipeDirection swipeDirection;

		private EasyTouchObjectProxy proxy;
		
		public override void OnEnter(){
			proxy = Owner.GetComponent<EasyTouchObjectProxy>();
		}

		#region PlayMaker Callback
		public override void OnUpdate (){

			if (proxy.currentGesture != null){
				
				currentGesture = proxy.currentGesture;
			
				if (!twoFingerGesture){
					//bool activ = false;

					// Touch Start
					if (currentGesture.type == EasyTouch.EvtType.On_SwipeStart && fingerIndex == -1){
						fingerIndex = currentGesture.fingerIndex;
					}

					//  Start
					if (currentGesture.type == EasyTouch.EvtType.On_SwipeStart && actionTriggering == ActionTriggering.Start){
						if (currentGesture.fingerIndex == fingerIndex || allowMultiTouch){
							CheckDirection(currentGesture);
						}
					}

					// In progress
					if (currentGesture.type == EasyTouch.EvtType.On_Swipe && actionTriggering == ActionTriggering.InProgress){
						if (currentGesture.fingerIndex == fingerIndex || allowMultiTouch){
							CheckDirection(currentGesture);
						}
					}

					// End
					if (currentGesture.type == EasyTouch.EvtType.On_SwipeEnd && actionTriggering == ActionTriggering.End){
						if (currentGesture.fingerIndex == fingerIndex || allowMultiTouch){
							CheckDirection(currentGesture);
						}

						if (currentGesture.fingerIndex == fingerIndex){
							fingerIndex =-1;
						}
					}

				}
				else{

					if (currentGesture.type == EasyTouch.EvtType.On_SwipeStart2Fingers && actionTriggering == ActionTriggering.Start ){
						CheckDirection(currentGesture);
					}

					if (currentGesture.type == EasyTouch.EvtType.On_Swipe2Fingers && actionTriggering == ActionTriggering.InProgress ){
						CheckDirection(currentGesture);
					}
					
					if (currentGesture.type == EasyTouch.EvtType.On_SwipeEnd2Fingers && actionTriggering == ActionTriggering.End ){
						CheckDirection(currentGesture);
					}
				}

			}
		}
		#endregion

		void CheckDirection(Gesture gesture){
			switch (swipeDirection){
			case SwipeDirection.All:
				DoAction(gesture);
				break;
			case SwipeDirection.Horizontal:
				if (gesture.swipe == EasyTouch.SwipeDirection.Left || gesture.swipe == EasyTouch.SwipeDirection.Right){
					DoAction(gesture);
				}
				break;
			case SwipeDirection.Vertical:
				if (gesture.swipe == EasyTouch.SwipeDirection.Up || gesture.swipe == EasyTouch.SwipeDirection.Down){
					DoAction(gesture);
				}
				break;
			case SwipeDirection.DiagonalLeft:
				if (gesture.swipe == EasyTouch.SwipeDirection.UpLeft || gesture.swipe == EasyTouch.SwipeDirection.DownRight){
					DoAction(gesture);
				}
				break;
			case SwipeDirection.DiagonalRight:
				if (gesture.swipe == EasyTouch.SwipeDirection.UpRight || gesture.swipe == EasyTouch.SwipeDirection.DownLeft){
					DoAction(gesture);
				}
				break;
			case SwipeDirection.Left:
				if (gesture.swipe == EasyTouch.SwipeDirection.Left){
					DoAction(gesture);
				}
				break;
			case SwipeDirection.Right:
				if (gesture.swipe == EasyTouch.SwipeDirection.Right){
					DoAction(gesture);
				}
				break;
			case SwipeDirection.DownLeft:
				if (gesture.swipe == EasyTouch.SwipeDirection.DownLeft){
					DoAction(gesture);
				}
				break;
			case SwipeDirection.DownRight:
				if (gesture.swipe == EasyTouch.SwipeDirection.DownRight){
					DoAction(gesture);
				}
				break;
			case SwipeDirection.UpLeft:
				if (gesture.swipe == EasyTouch.SwipeDirection.UpLeft){
					DoAction(gesture);
				}
				break;
			case SwipeDirection.UpRight:
				if (gesture.swipe == EasyTouch.SwipeDirection.UpRight){
					DoAction(gesture);
				}
				break;
			case SwipeDirection.Up:
				if (gesture.swipe == EasyTouch.SwipeDirection.Up){
					DoAction(gesture);
				}
				break;
			case SwipeDirection.Down:
				if (gesture.swipe == EasyTouch.SwipeDirection.Down){
					DoAction(gesture);
				}
				break;
			}
		}

		protected override void DoAction (Gesture gesture){
		
			if ( realType == GameObjectType.UI){
				Fsm.Event( sendEvent);
				Finish();
			}
			else{
				if ((!enablePickOverUI && gesture.pickedUIElement == null) || enablePickOverUI){
					Fsm.Event( sendEvent);
					Finish();
				}
			}
		}

	}
}