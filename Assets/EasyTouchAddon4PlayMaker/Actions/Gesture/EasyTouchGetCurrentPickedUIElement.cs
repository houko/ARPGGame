using UnityEngine;
using System.Collections;
using HedgehogTeam.EasyTouch;

namespace HutongGames.PlayMaker.Actions{
	
	[ActionCategory("EasyTouch Gesture Info")]
	[Tooltip("Get current UI Element from position of lastest gesture sent.")]
	public class EasyTouchGetCurrentPickedUIElement : FsmStateAction {

		public bool isTwoFinger;
		
		[UIHint(UIHint.Variable)]
		public FsmGameObject currentUIElement;
		
		private EasyTouchObjectProxy proxy;

		public override void Reset(){
			proxy = null;
			currentUIElement = null;
		}

		public override void Awake (){
			
			base.Awake ();
			EasyTouch.instance.enableUIMode = true;
			EasyTouch.SetUICompatibily( false);
		}

		public override void OnEnter(){
			
			proxy = Owner.GetComponent<EasyTouchObjectProxy>();
			
			if (proxy){
				currentUIElement.Value = proxy.currentGesture.GetCurrentFirstPickedUIElement(isTwoFinger);
			}
			else{
				Debug.LogError("EasyTouchObjectProxy component is missing", Owner.gameObject);
			}
			
			Finish();
		}
	}
}
