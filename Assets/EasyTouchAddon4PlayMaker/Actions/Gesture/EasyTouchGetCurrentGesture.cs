
using UnityEngine;
using System.Collections;
using HedgehogTeam.EasyTouch;

namespace HutongGames.PlayMaker.Actions{

	[ActionCategory("EasyTouch Gesture Info")]
	[Tooltip("Get lastest geture sent.")]
	public class EasyTouchGetCurrentGesture : FsmStateAction {


		[UIHint(UIHint.Variable)]
		public FsmInt fingerIndex;

		[UIHint(UIHint.Variable)]
		public FsmInt touchCount;

		[UIHint(UIHint.Variable)]
		public FsmVector3 startPosition;

		[UIHint(UIHint.Variable)]
		public FsmVector3 position;

		[UIHint(UIHint.Variable)]
		public FsmVector3 deltaPosition;

		[UIHint(UIHint.Variable)]
		public FsmFloat deltaPositionX;

		[UIHint(UIHint.Variable)]
		public FsmFloat deltaPositionY;

		[UIHint(UIHint.Variable)]
		public FsmFloat actionTime;

		[UIHint(UIHint.Variable)]
		public FsmFloat deltaTime;

		[UIHint(UIHint.Variable)]
		public FsmGameObject pickedGameObject;

		[UIHint(UIHint.Variable)]
		public FsmGameObject pickedUIElement;

		[UIHint(UIHint.Variable)]
		[ObjectType(typeof(EasyTouch.SwipeDirection))]
		public FsmEnum swipe;

		[UIHint(UIHint.Variable)]
		public FsmFloat swipeLength;

		[UIHint(UIHint.Variable)]
		public FsmVector3 swipeVector;

		[UIHint(UIHint.Variable)]
		public FsmFloat deltaPinch;

		[UIHint(UIHint.Variable)]
		public FsmFloat twistAngle;

		[UIHint(UIHint.Variable)]
		public FsmBool isOverGUI;

		[UIHint(UIHint.Variable)]
		public FsmFloat twoFingerDistance;


		private EasyTouchObjectProxy proxy;

		public override void Reset(){
			fingerIndex = null;
			startPosition = null;
			position = null;
			deltaPosition = null;
			deltaPositionX = null;
			deltaPositionY = null;
			actionTime = null;
			deltaTime = null;
			pickedGameObject = null;
			pickedUIElement = null;
			touchCount = null;
			swipe = null;
			swipeVector = null;
			deltaPinch = null;
			twistAngle = null;
			twoFingerDistance = null;
			isOverGUI = null;
		}

		public override void OnEnter(){

			proxy = Owner.GetComponent<EasyTouchObjectProxy>();

			if (proxy){
				fingerIndex.Value = proxy.currentGesture.fingerIndex;
				touchCount.Value = proxy.currentGesture.touchCount;
				startPosition.Value = proxy.currentGesture.startPosition;
				position.Value = proxy.currentGesture.position;
				deltaPosition.Value = proxy.currentGesture.deltaPosition;
				deltaPositionX.Value = proxy.currentGesture.deltaPosition.x;
				deltaPositionY.Value = proxy.currentGesture.deltaPosition.y;
				actionTime.Value = proxy.currentGesture.actionTime;
				deltaTime.Value = proxy.currentGesture.deltaTime;
				pickedGameObject.Value = proxy.currentGesture.pickedObject;
				pickedUIElement.Value =  proxy.currentGesture.pickedUIElement;

				swipe.Value = proxy.currentGesture.swipe;
				swipeVector.Value = proxy.currentGesture.swipeVector;
				swipeLength.Value = proxy.currentGesture.swipeLength;

				deltaPinch.Value = proxy.currentGesture.deltaPinch;
				twistAngle.Value = proxy.currentGesture.twistAngle;
				twoFingerDistance.Value = proxy.currentGesture.twoFingerDistance;

				isOverGUI.Value = proxy.currentGesture.isOverGui;

			}
			else{
				Debug.LogError("EasyTouchObjectProxy component is missing", Owner.gameObject);
			}
			
			Finish();

		}
	}
}
