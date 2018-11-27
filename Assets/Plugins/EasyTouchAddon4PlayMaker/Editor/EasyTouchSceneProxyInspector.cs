using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(EasyTouchSceneProxy))]
public class EasyTouchSceneProxyInspector : Editor {

	public override void OnInspectorGUI(){

		EasyTouchSceneProxy t = (EasyTouchSceneProxy)target;

		EditorGUILayout.Space();

		t.isAutoProxyObject = EditorGUILayout.ToggleLeft("Auto create easytouch proxy",t.isAutoProxyObject);

		if (!t.isAutoProxyObject){
			if (GUILayout.Button("Upadte PlayMaker FSM")){
				t.UpdateFsms( true);
			}
		}

		if (GUI.changed){
			EditorUtility.SetDirty(t);
		}
	}
}
