using UnityEngine;
using System.Collections;
using HedgehogTeam.EasyTouch;

namespace HutongGames.PlayMaker.Actions{
	
	[ActionCategory("EasyTouch Gesture Info")]
	[Tooltip("Get current object from position of lastest gesture sent.")]
	public class EasyTouchGetCurrentPickedObject : FsmStateAction {

		public bool isTwoFinger;

		[UIHint(UIHint.Variable)]
		public FsmGameObject currentObject;

		[UIHint(UIHint.Variable)]
		public FsmString name;

		private EasyTouchObjectProxy proxy;

		public override void Reset(){
			proxy = null;
			currentObject = null;
		}

		public override void OnEnter(){
			
			proxy = Owner.GetComponent<EasyTouchObjectProxy>();
			
			if (proxy){
				currentObject.Value = EasyTouch.GetGameObjectAt(proxy.currentGesture.position, isTwoFinger );
				if (currentObject.Value){
					name = currentObject.Value.name;
				}
			}
			else{
				Debug.LogError("EasyTouchObjectProxy component is missing", Owner.gameObject);
			}

			Finish();
		}
	}
}
