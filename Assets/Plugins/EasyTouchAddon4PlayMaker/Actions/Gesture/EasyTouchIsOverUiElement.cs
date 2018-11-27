using UnityEngine;
using System.Collections;
using HedgehogTeam.EasyTouch;

namespace HutongGames.PlayMaker.Actions{
	
	[ActionCategory("EasyTouch Gesture Info")]
	[Tooltip("Is current touch from lastest gesture sent, is over UI Element.")]
	public class EasyTouchIsOverUiElement : FsmStateAction {

		[UIHint(UIHint.Variable)]
		public FsmBool isOverUIElement;

		public FsmEvent overEvent;
		public FsmEvent notOverEvent;

		private EasyTouchObjectProxy proxy;

		public override void Reset(){
			proxy = null;
			isOverUIElement = null;
			overEvent = null;
			notOverEvent = null;
		}

		public override void Awake (){
		
			base.Awake ();
			EasyTouch.instance.enableUIMode = true;
			EasyTouch.SetUICompatibily( false);
		}

		public override void OnEnter(){

			proxy = Owner.GetComponent<EasyTouchObjectProxy>();

			if (proxy){
				isOverUIElement.Value = proxy.currentGesture.IsOverUIElement();
				if (isOverUIElement.Value){
					Fsm.Event( overEvent);
					Finish();
				}
				else{
					Fsm.Event( notOverEvent);
					Finish();
				}
			}
			else{
				Debug.LogError("EasyTouchObjectProxy component is missing", Owner.gameObject);
				Finish();
			}
			
		}
	}
}
