using UnityEngine;
using System.Collections;
using HedgehogTeam.EasyTouch;

namespace HutongGames.PlayMaker.Actions{

	[ActionCategory("EasyTouch")]
	[Tooltip("Catch action on gesture drag.")]
	public class EasyTouchQuickDrag : EasyTouchQuickFSM {

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

					// Drag start
					if (currentGesture.type == EasyTouch.EvtType.On_DragStart && fingerIndex ==-1 && CheckActivateDrag(currentGesture)){
						fingerIndex = currentGesture.fingerIndex;
					}

					// Start
					if (currentGesture.type == EasyTouch.EvtType.On_DragStart && actionTriggering == ActionTriggering.Start){

						if (fingerIndex == currentGesture.fingerIndex || (allowMultiTouch && CheckActivateDrag(currentGesture))){
							DoAction(currentGesture);
						}
					}

					// In progress
					if (currentGesture.type == EasyTouch.EvtType.On_Drag && actionTriggering == ActionTriggering.InProgress){
						if (currentGesture.fingerIndex == fingerIndex || allowMultiTouch){
							DoAction(currentGesture);
						}
					}

					// End
					if (currentGesture.type == EasyTouch.EvtType.On_DragEnd && actionTriggering == ActionTriggering.End){
						if (currentGesture.fingerIndex == fingerIndex || allowMultiTouch){
							DoAction(currentGesture);
						}
					}

				}
				else{
					// Start
					if (currentGesture.type == EasyTouch.EvtType.On_DragStart2Fingers && actionTriggering == ActionTriggering.Start){
						if (CheckActivateDrag(currentGesture)){
							DoAction(currentGesture);
						}
					}

					// In Progress
					if (currentGesture.type == EasyTouch.EvtType.On_Drag2Fingers && actionTriggering == ActionTriggering.InProgress){
						if (CheckActivateDrag(currentGesture)){
							DoAction(currentGesture);
						}
					}

					// End
					if (currentGesture.type == EasyTouch.EvtType.On_DragEnd2Fingers && actionTriggering == ActionTriggering.End){
						if (CheckActivateDrag(currentGesture)){
							DoAction(currentGesture);
						}
					}
				}

			}

		}
		#endregion

		protected override void DoAction(Gesture gesture){
			
			Fsm.Event( sendEvent);
			Finish();
		}

		bool CheckActivateDrag(Gesture gesture){
			if ( realType == GameObjectType.UI){
				if (gesture.isOverGui ){
					if ((gesture.pickedUIElement == Owner.gameObject || gesture.pickedUIElement.transform.IsChildOf( Owner.transform))){
						return true;
					}
				}
			}
			else{
				if ((!enablePickOverUI && gesture.pickedUIElement == null) || enablePickOverUI){
					if (gesture.pickedObject == Owner){
						return true;
					}
				}
			}
			return false;
		}
	}
}
