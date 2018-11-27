
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using HutongGames.PlayMaker;
#if UNITY_EDITOR
using System.Linq;
using UnityEditor;
#endif
using HedgehogTeam.EasyTouch;

#if UNITY_EDITOR
[ExecuteInEditMode]
public class EasyTouchSceneProxy : MonoBehaviour {

	public static Gesture currentGesture;
	public bool isAutoProxyObject = true;

	private bool register = false;

	void OnEnable(){

	}

	void Update(){

		#if UNITY_EDITOR
			if (!EditorApplication.isPlaying){
				InitGlobalFSMEvent();
				if (!register){
					SceneView.onSceneGUIDelegate += SceneGUI;
					register = true;
				}
			}
			else{
				SceneView.onSceneGUIDelegate -= SceneGUI;
			}
		#endif
			
	}

	void OnDisable(){
		Unsubscribe();
	}

	void OnDestroy(){
		Unsubscribe();
	}
		
	void SceneGUI(SceneView sceneView){

		if (!Application.isPlaying && isAutoProxyObject){
			UpdateFsms();
		}

	}
	
	void Unsubscribe(){
		SceneView.onSceneGUIDelegate -= SceneGUI;
	}

void InitGlobalFSMEvent(){
	
	if ( !EditorApplication.isPlaying){
		foreach( var value in Enum.GetValues( typeof(EasyTouch.EvtType))){
			CreateGlobalEvent( "EASYTOUCH / SCENE / " + value.ToString().ToUpper());
			CreateGlobalEvent( "EASYTOUCH / OWNER / " + value.ToString().ToUpper());
		}
	}
}

void CreateGlobalEvent(string globalEventName){

	if (!FsmEvent.IsEventGlobal(globalEventName)){

		FsmEvent _event = new FsmEvent(globalEventName);
		_event.IsGlobal = true;
		FsmEvent.AddFsmEvent(_event);
	}

}

	public void UpdateFsms(bool force=false){

		// Find FMS
		PlayMakerFSM[] fsms =  (PlayMakerFSM[])GameObject.FindObjectsOfType<PlayMakerFSM>();
		foreach( PlayMakerFSM fsm in fsms){

			if (fsm.gameObject.GetComponent<EasyTouchObjectProxy>()==null){

				// Find event 
				foreach( FsmEvent evt in fsm.FsmEvents){
					// Test event name
					foreach( var value in Enum.GetValues( typeof(EasyTouch.EvtType))){
						if ( "EASYTOUCH / SCENE / " + value.ToString().ToUpper() == evt.Name.ToString().ToUpper() || 
						    "EASYTOUCH / OWNER / " + value.ToString().ToUpper() == evt.Name.ToString().ToUpper() ){
							
							// Create EasyTouch Object proxy
							if (fsm.GetComponent<EasyTouchObjectProxy>()==null){
								fsm.gameObject.AddComponent<EasyTouchObjectProxy>();
							}
						}
					}
				}
				
				// Find action
				foreach( FsmState state in fsm.FsmStates){
					if (force) state.LoadActions();
					if (state.ActionsLoaded){
						foreach( FsmStateAction action in state.Actions){
							if (action.GetType().ToString().IndexOf("EasyTouch")>-1){
								// Create EasyTouch Object proxy
								if (fsm.GetComponent<EasyTouchObjectProxy>()==null){
									fsm.gameObject.AddComponent<EasyTouchObjectProxy>();
								}
							}
						}
					}
				}

			}
		}
	}
}
#endif