using UnityEngine;
using UnityEditor;
using System.Collections;
using HedgehogTeam.EasyTouch;

public class EasyTouchPlayMakerMenu{

	[MenuItem ("GameObject/EasyTouch/EasyTouch Scene Proxy (PlayMaker)", false, 100)]
	static void  AddEasyTouchSceneProxy(){
		
		// EasyTouch
		if (GameObject.FindObjectOfType<EasyTouchSceneProxy>()==null){
			new GameObject("EasyTouchSceneProxy", typeof(EasyTouchSceneProxy));
		}

		EasyTouch.instance.GetInstanceID();

		Selection.activeObject = GameObject.FindObjectOfType<EasyTouchSceneProxy>().gameObject;
	}
}
