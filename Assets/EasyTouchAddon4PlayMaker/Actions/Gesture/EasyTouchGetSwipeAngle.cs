using UnityEngine;
using System.Collections;
using HedgehogTeam.EasyTouch;

namespace HutongGames.PlayMaker.Actions{
	
	[ActionCategory("EasyTouch Gesture Info")]
	[Tooltip("Get swipe angle from lastest gesture sent.")]
	public class EasyTouchGetSwipeAngle : FsmStateAction {


		[UIHint(UIHint.Variable)]
		public FsmFloat swipeAngle;

		private EasyTouchObjectProxy proxy;

		public override void Reset(){
			swipeAngle = null;
		}
		
		public override void OnEnter(){
			
			proxy = Owner.GetComponent<EasyTouchObjectProxy>();
			
			if (proxy){
				swipeAngle.Value = proxy.currentGesture.GetSwipeOrDragAngle();
			}
			else{
				Debug.LogError("EasyTouchObjectProxy component is missing", Owner.gameObject);
			}
			
			Finish();
			
		}
	}

}