using UnityEngine;
using System.Collections;
using HedgehogTeam.EasyTouch;

namespace HutongGames.PlayMaker.Actions{
	
	[ActionCategory("EasyTouch Gesture Info")]
	[Tooltip("Get normalized position relative to screen size, from lastest gesture sent.")]
	public class EasyTouchNormalizedPosition : FsmStateAction {

		[UIHint(UIHint.Variable)]
		public FsmVector3 normalizedPosition;

		private EasyTouchObjectProxy proxy;
		
		public override void Reset(){
			normalizedPosition = null;
		}
		
		public override void OnEnter(){
			
			proxy = Owner.GetComponent<EasyTouchObjectProxy>();
			
			if (proxy){
				normalizedPosition.Value = proxy.currentGesture.NormalizedPosition();
			}
			else{
				Debug.LogError("EasyTouchObjectProxy component is missing", Owner.gameObject);
			}
			
			Finish();
			
		}
	}
}