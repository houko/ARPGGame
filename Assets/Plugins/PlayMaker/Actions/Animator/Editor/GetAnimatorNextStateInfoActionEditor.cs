// (c) Copyright HutongGames, LLC 2010-2016. All rights reserved.

using UnityEngine;
using UnityEditor;

using HutongGames.PlayMaker.Actions;
using HutongGames.PlayMakerEditor;

[CustomActionEditor(typeof(GetAnimatorNextStateInfo))]
public class GetAnimatorNextStateInfoActionEditor : OnAnimatorUpdateActionEditorBase
{

	public override bool OnGUI()
	{
		EditField("gameObject");
		EditField("layerIndex");
		EditField("name");
		EditField("nameHash");
		
		#if UNITY_5
		EditField("fullPathHash");
		EditField("shortPathHash");
		#endif
		
		
		EditField("tagHash");
		EditField("isStateLooping");
		EditField("length");
		EditField("normalizedTime");
		EditField("loopCount");
		EditField("currentLoopProgress");

		bool changed = EditEveryFrameField();
		
		return GUI.changed || changed;
	}

}
