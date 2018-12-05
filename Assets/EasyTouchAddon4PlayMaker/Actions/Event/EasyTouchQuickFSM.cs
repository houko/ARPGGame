using UnityEngine;
using System.Collections;
using HedgehogTeam.EasyTouch;

namespace HutongGames.PlayMaker.Actions{

	public abstract class EasyTouchQuickFSM : FsmStateAction {

		protected enum GameObjectType { Obj_3D,Obj_2D,UI};
		protected GameObjectType realType;
		protected int fingerIndex =-1;

		protected bool enablePickOverUI = false;
		protected Gesture currentGesture;

		[Tooltip("Only two fingers gesture.")]
		public bool twoFingerGesture;
		
		[Tooltip("Allow multi-touch on owner.")]
		public bool allowMultiTouch;
		
		[Tooltip("UI Element don't block the selection.")]
		public bool allowOverUIElement;

		public FsmEvent sendEvent;

		public override void Awake(){

			EasyTouch.SetEnableAutoSelect( true);
			
			realType = GameObjectType.Obj_3D;
			
			if (Owner.GetComponent<Collider>()){
				realType = GameObjectType.Obj_3D;
			}
			else if (Owner.GetComponent<Collider2D>()){
				realType = GameObjectType.Obj_2D;
			}
			else if (Owner.GetComponent<CanvasRenderer>()){
				realType = GameObjectType.UI;
			}

			switch (realType){
				
			case GameObjectType.Obj_3D:
				LayerMask mask = EasyTouch.Get3DPickableLayer();
				mask = mask | 1<<Owner.layer;
				EasyTouch.Set3DPickableLayer( mask);
				break;
				//2D
			case GameObjectType.Obj_2D:
				EasyTouch.SetEnable2DCollider( true);
				mask = EasyTouch.Get2DPickableLayer();
				mask = mask | 1<<Owner.layer;
				EasyTouch.Set2DPickableLayer( mask);
				break;
				// UI
			case GameObjectType.UI:
				EasyTouch.instance.enableUIMode = true;
				EasyTouch.SetUICompatibily( false);
				break;
			}
			
			if (enablePickOverUI){
				EasyTouch.instance.enableUIMode = true;
				EasyTouch.SetUICompatibily( false);
			}


		}
	
		protected virtual void DoAction(Gesture gesture){

			if (IsOverMe( gesture)){
				Fsm.Event( sendEvent);
				Finish();
			}
			/*
			if ( realType == GameObjectType.UI){
				if (gesture.isOverGui ){
					if ((gesture.pickedUIElement == Owner.gameObject || gesture.pickedUIElement.transform.IsChildOf( Owner.transform))){
						Fsm.Event( sendEvent);
						Finish();
					}
				}
			}
			else{
				if ((!enablePickOverUI && gesture.pickedUIElement == null) || enablePickOverUI){
					
					if (EasyTouch.GetGameObjectAt( gesture.position,twoFingerGesture) == Owner){
						Fsm.Event( sendEvent);
						Finish();
					}
				}
			}*/
			
		}

		protected bool IsOverMe(Gesture gesture){
			bool returnValue = false;

			if ( realType == GameObjectType.UI){
				if (gesture.isOverGui ){
					if ((gesture.pickedUIElement == Owner.gameObject || gesture.pickedUIElement.transform.IsChildOf( Owner.transform))){
						returnValue = true;
					}
				}
			}
			else{
				if ((!enablePickOverUI && gesture.pickedUIElement == null) || enablePickOverUI){
					
					if (EasyTouch.GetGameObjectAt( gesture.position,twoFingerGesture) == Owner){
						returnValue = true;
					}
				}
			}

			return returnValue;
		}
	} 
}
