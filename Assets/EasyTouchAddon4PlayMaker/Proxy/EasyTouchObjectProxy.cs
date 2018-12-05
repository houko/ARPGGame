#if true
using UnityEngine;
using System.Collections;
using HutongGames.PlayMaker;
using HedgehogTeam.EasyTouch;

public class EasyTouchObjectProxy : MonoBehaviour {

	public bool allowSceneEvent = false;

	private PlayMakerFSM cachedFsm;

	[HideInInspector]
	public Gesture currentGesture;

	void Awake(){
		cachedFsm = GetComponent<PlayMakerFSM>();
	}

	void Update(){
		
		Gesture current = EasyTouch.current;

        if (current != null)
        {
            // Scene
            currentGesture = current;
            if (current.type != EasyTouch.EvtType.None && allowSceneEvent)
            {
                SendEventToGameObject("EASYTOUCH / SCENE / " + current.type.ToString().ToUpper(), false, null);
            }

            // Owner
            if (current.pickedObject == gameObject || current.pickedUIElement == gameObject)
            {
                SendEventToGameObject("EASYTOUCH / OWNER / " + current.type.ToString().ToUpper(), false, null);
            }
        }

	}
	
	private void SendEventToGameObject(string fsmEvent,bool includeChildren,FsmEventData eventData){

	
		if (eventData!=null){
			HutongGames.PlayMaker.Fsm.EventData = eventData;
		}
		
		if (cachedFsm == null){
			
			return;
		}


		FsmEventTarget _eventTarget = new FsmEventTarget();
		_eventTarget.excludeSelf = false;
		_eventTarget.sendToChildren = includeChildren;
		
		_eventTarget.target = FsmEventTarget.EventTarget.GameObject; 
		
		FsmOwnerDefault owner = new FsmOwnerDefault();
		owner.OwnerOption = OwnerDefaultOption.SpecifyGameObject;
		owner.GameObject = new FsmGameObject();
		owner.GameObject.Value = this.gameObject;
		
		_eventTarget.gameObject = owner;
		
		cachedFsm.Fsm.Event(_eventTarget,fsmEvent);
		
	}

}
#endif