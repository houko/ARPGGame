using UnityEngine;
using System.Collections;
using HedgehogTeam.EasyTouch;

namespace HutongGames.PlayMaker.Actions{
	
	[ActionCategory("EasyTouch Gesture Info")]
	[Tooltip("Get world position from position of lastest gesture sent.")]
	public class EasyTouchGetTouchToWorldPoint : FsmStateAction {

		public enum DepthType {Value,GameObjectReference, Position};

		public DepthType depthType;
		public FsmFloat z;
		public FsmVector3 position;
		public FsmGameObject gameObjectReference;

		[UIHint(UIHint.Variable)]
		public FsmVector3 worldPosition;

		private EasyTouchObjectProxy proxy;

		public override void Reset(){
			proxy = null;
			gameObjectReference = null;
			z = null;
		}

		public override void OnEnter(){
			
			proxy = Owner.GetComponent<EasyTouchObjectProxy>();
			
			if (proxy){

				switch( depthType){
				case DepthType.Value:
					worldPosition.Value = proxy.currentGesture.GetTouchToWorldPoint( z.Value);
					break;
				case DepthType.GameObjectReference:
					worldPosition.Value = proxy.currentGesture.GetTouchToWorldPoint( gameObjectReference.Value.transform.position);
					break;
				case DepthType.Position:
					worldPosition.Value = proxy.currentGesture.GetTouchToWorldPoint( position.Value);
					break;
				}
			}
			else{
				Debug.LogError("EasyTouchObjectProxy component is missing", Owner.gameObject);
			}
			
			Finish();
		}

	}
}