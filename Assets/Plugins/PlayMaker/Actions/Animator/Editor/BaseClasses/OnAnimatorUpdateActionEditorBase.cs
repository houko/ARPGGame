// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

using HutongGames.PlayMaker.Actions;
using HutongGames.PlayMakerEditor;
using UnityEditor;
using UnityEngine;


public class OnAnimatorUpdateActionEditorBase : CustomActionEditor {
	
	public override bool OnGUI()
	{
		return false;
	}
	
	public bool EditEveryFrameField()
	{
		FsmStateActionAnimatorBase _target = (FsmStateActionAnimatorBase)target;
		
		if (_target.everyFrame) 
		{
			GUILayout.BeginHorizontal();
			EditorGUILayout.PrefixLabel("Every Frame");
			_target.everyFrame = GUILayout.Toggle(_target.everyFrame,"");
			_target.everyFrameOption = (FsmStateActionAnimatorBase.AnimatorFrameUpdateSelector)EditorGUILayout.EnumPopup(_target.everyFrameOption);
			GUILayout.EndHorizontal();
			
		}else{
			EditField("everyFrame");
		}
		
		return GUI.changed;
	}
	
}
