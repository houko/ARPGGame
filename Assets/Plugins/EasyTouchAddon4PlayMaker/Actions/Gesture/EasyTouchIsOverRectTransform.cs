using UnityEngine;
using System.Collections;
using HedgehogTeam.EasyTouch;

namespace HutongGames.PlayMaker.Actions{
	
	[ActionCategory("EasyTouch Gesture Info")]
	[Tooltip("Is current touch from lastest gesture sent, is over a RectTransform.")]
	public class EasyTouchIsOverRectTransform : FsmStateAction {

		public FsmGameObject rectTransformOwner;

		[UIHint(UIHint.Variable)]
		public FsmBool isOverRectTransform;

		public FsmEvent overEvent;
		public FsmEvent notOverEvent;
		
		private EasyTouchObjectProxy proxy;

		public override void Reset(){
			proxy =null;
			overEvent = null;
			notOverEvent = null;
			isOverRectTransform = null;
		}
		
		public override void Awake (){
			
			base.Awake ();
			EasyTouch.instance.enableUIMode = true;
			EasyTouch.SetUICompatibily( false);
		}

		public override void OnEnter(){
			
			proxy = Owner.GetComponent<EasyTouchObjectProxy>();
			
			if (proxy){
				RectTransform recTransform = (rectTransformOwner.Value as GameObject).transform as RectTransform;

				bool tmp = proxy.currentGesture.IsOverRectTransform( recTransform,null);
				isOverRectTransform.Value = tmp;
				if (tmp){
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
